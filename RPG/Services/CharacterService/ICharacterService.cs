using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RPG.DTOs.CharacterDto;
using RPG.Models;

namespace RPG.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();

        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);

        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newharacter);
    }
}
