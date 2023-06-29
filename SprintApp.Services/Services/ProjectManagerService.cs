using Microsoft.EntityFrameworkCore;
using SprintApp.Core.Dtos;
using SprintApp.Core.Helper;
using SprintApp.Core.IRepositories;
using SprintApp.Core.IServices;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace SprintApp.Services.Services
{
    public class ProjectManagerService : IProjectManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _contex;

        public ProjectManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Public Methods


        /// <summary>
        /// Method to register new user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<string> RegisterManager(RegistrationDto obj)
        {
            if (obj == null)
            {
                return ConstantMessage.Unsuccessful;
            }
            EncriptPassword(obj.Password, out byte[] passwordHash, out byte[] passwordSalt);
            List<ProjectManager> item = _contex.ProjectManagers.ToList();
            var myList = new List<string>();
            foreach (var manager in item) 
            {
                myList.Add(manager.ManagerId);
            }
            var user = new ProjectManager
            {
                EmailId = obj.EmailId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                ManagerId = await GenerateCode("admin", myList, 3),
                VerificationToken = await GenerateToken(),
                LoginAtempt = 3
            };
            await _unitOfWork.projectManagerRepo.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return ConstantMessage.RegistrationSuccess;
        }
        /// <summary>
        /// Method to log user in
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Login (LoginDto dto)
        {
            if (dto == null)
            {
                return ConstantMessage.Unsuccessful;
            }
            var user = await _contex.ProjectManagers.FirstOrDefaultAsync(x=> x.EmailId == dto.EmailId);
            if (user == null)
            {
                return ConstantMessage.InvalidUser;
            }
            var verify = VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt);
            if (!verify)
            {
                user.LoginAtempt--;
                if (user.LoginAtempt <= 0)
                {
                    user.LogoutTime = DateTime.UtcNow.AddMinutes(30);
                }
            }
            return ConstantMessage.RegistrationSuccess;
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Method to encript password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void EncriptPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Method to generate User IDs
        /// </summary>
        /// <param name="name"></param>
        /// <param name="_service"></param>
        /// <param name="charLength"></param>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private async Task<string> GenerateCode(string name, List<string> _service, int charLength, int firstIndex = 2, int secondIndex = 3, int startIndex = 4)
        {

            if (charLength > 2)
            {
                startIndex = startIndex + (charLength - 2);
            }
            string myString = _service[_service.Count - 1];
            int num = int.Parse(myString.Substring(startIndex, 4));
            char firstChar = myString[firstIndex + (charLength - 2)];
            char secondChar = myString[secondIndex + (charLength - 2)];
            char thirdChar = myString[myString.Length - 1];
            if (num + 1 > 9999)
            {
                secondChar = (char)(secondChar + 1);
                num = 0;
                if (secondChar > 'Z')
                {
                    firstChar = (char)(firstChar + 1);
                    secondChar = (char)(secondChar - 26);

                    if (firstChar > 'Z')
                    {
                        firstChar = (char)(firstChar - 26);
                        thirdChar = (char)(thirdChar + 1);
                    }
                }
            }
            return (name.Substring(0, charLength).ToUpper() + firstChar + secondChar + (num + 1).ToString("D4") + thirdChar);
        }

        /// <summary>
        /// Method to generate randome token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenerateToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
       

        /// <summary>
        /// Method to verify password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        #endregion Private Methods
    }
}
