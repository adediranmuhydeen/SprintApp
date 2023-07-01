﻿using SprintApp.Core.Dtos;

namespace SprintApp.Core.IServices
{
    public interface IProjectManagerService 
    {
        Task<string> RegisterManager(RegistrationDto obj);
        Task<string> Login(LoginDto dto);
        Task<string> VerifyUser(VerificationDto dto);
    }
}
