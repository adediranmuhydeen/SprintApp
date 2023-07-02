using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SprintApp.Core.Dtos;
using SprintApp.Core.Helper;
using SprintApp.Core.IRepositories;
using SprintApp.Core.IServices;
using SprintApp.Core.Models;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace SprintApp.Services.Services
{
    public class ProjectManagerService : IProjectManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

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
            var chekuser = await _unitOfWork.projectManagerRepo.GetAsync(x => x.EmailId == obj.EmailId);
            if (chekuser != null)
            {
                return ConstantMessage.Unsuccessful;
            }
            EncriptPassword(obj.Password, out byte[] passwordHash, out byte[] passwordSalt);
            List<ProjectManager> item = await _unitOfWork.projectManagerRepo.GetAllAsync();
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
                VarificationTokenExpires = DateTime.UtcNow.AddMinutes(30),
                LoginAtempt = 3
            };
            await _unitOfWork.projectManagerRepo.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return ConstantMessage.RegistrationSuccess;
        }

        public async Task<string> VerifyUser(VerificationDto dto)
        {
            var user = await _unitOfWork.projectManagerRepo.GetAsync(x=>x.EmailId == dto.EmailId);
            if (user == null)
            {                
                return ConstantMessage.InvalidUser;
            }
            if(dto.VerificationToken != user.VerificationToken)
            {
                user.LoginAtempt--;
                user.VerificationToken = await GenerateToken();
                user.VarificationTokenExpires = DateTime.UtcNow.AddMinutes(30);
                _unitOfWork.projectManagerRepo.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return ConstantMessage.InvalidToken;
            }
            if(user.VarificationTokenExpires < DateTime.UtcNow)
            {
                user.LoginAtempt--;
                if (user.LoginAtempt <= 0)
                {
                    user.LogoutTime = DateTime.Now.AddMinutes(2);
                    user.LoginAtempt = 3;
                    user.VerificationToken = await GenerateToken();
                    user.VarificationTokenExpires = DateTime.UtcNow.AddMinutes(30);
                    _unitOfWork.projectManagerRepo.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    return ConstantMessage.LockedUser;
                }
                user.VerificationToken = await GenerateToken();
                user.VarificationTokenExpires = DateTime.UtcNow.AddMinutes(30);
                _unitOfWork.projectManagerRepo.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return ConstantMessage.TokenExpired;
            }

            user.LoginAtempt=3;
            user.VerifiedAt = DateTime.UtcNow;
            user.VerificationToken = await GenerateToken();
            user.VarificationTokenExpires = DateTime.UtcNow.AddMinutes(30);
            _unitOfWork.projectManagerRepo.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return ConstantMessage.CompleteRequest;
        }


        public async Task<ProjectManager> GetProjectManagerAsync(string Email)
        {
            return await _unitOfWork.projectManagerRepo.GetAsync(x=> x.EmailId == Email);
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
            var user = await _unitOfWork.projectManagerRepo.GetAsync(x=> x.EmailId == dto.EmailId);
            if (user == null)
            {
                return ConstantMessage.InvalidUser;
            }
            if (!VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                
                if (user.LoginAtempt <= 0)
                {
                    user.LogoutTime = DateTime.UtcNow.AddMinutes(2);
                    user.LoginAtempt = 3;
                    _unitOfWork.projectManagerRepo.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    return ConstantMessage.LockedUser;
                }
                user.LoginAtempt--;
                 _unitOfWork.projectManagerRepo.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return ConstantMessage.InvalidUser;
            }
            if (user.VerifiedAt == null)
            {
                //SendEmail(user.VerificationToken, user.EmailId);
                user.LoginAtempt--;
                if (user.LoginAtempt <= 0)
                {
                    user.LogoutTime = DateTime.Now.AddMinutes(2);
                    user.LoginAtempt = 3;
                    _unitOfWork.projectManagerRepo.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    return ConstantMessage.LockedUser;
                }
                return ConstantMessage.TokenExpired;
            }
            if(user.LogoutTime > DateTime.UtcNow && user.LogoutTime != null)
            {
                return ConstantMessage.LockedUser;
            }
            user.LoginAtempt = 3;
            user.LogoutTime = null;
            user.VerifiedAt = null;
            _unitOfWork.projectManagerRepo.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return ConstantMessage.CompleteRequest;
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

        public void SendEmail(string verificationToken, string emailAddress)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("paolo69@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "User verification";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"Your verification token is " +
                $"<h1><b>{verificationToken}<b></h1>, \nkindly copy and paste it in the appropriate box"
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("paolo69@ethereal.email", "3ruk9dJAtmCfj8YAyp");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        #endregion Private Methods
    }
}
