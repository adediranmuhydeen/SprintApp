using SprintApp.Core.Dtos;
using SprintApp.Core.Helper;
using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace SprintApp.Services.Services
{
    public class ProjectManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Public Methods

        public async Task<string> RegisterManager(RegistrationDto obj)
        {
            var temp = await _unitOfWork.projectManagerRepo.GetAllAsync();
            if (obj == null)
            {
                return ConstantMessage.Unsuccessful;
            }
            EncriptPassword(obj.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new ProjectManager
            {
                EmailId = obj.EmailId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
            };
        }

        #endregion Public Methods

        #region Private Methods

        private void EncriptPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static string GenerateCode(string name, List<string> _service, int charLength, int firstIndex = 2, int secondIndex = 3, int startIndex = 4)
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

        #endregion Private Methods
    }
}
