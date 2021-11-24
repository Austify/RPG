using System;
using AutoMapper;
using RPG.DTOs.CharacterDto;
using RPG.Models;

namespace RPG.DTOs.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}
