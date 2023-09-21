﻿using AutoMapper;
using SprintApp.Core.Dtos;
using SprintApp.Core.Models;

namespace SprintApp.UI.Helpers
{
    public class MapInitializer : Profile
    {
        public MapInitializer()
        {
            CreateMap<ProjectManager, GetProjectManagerDto>().ReverseMap();
        }
    }
}
