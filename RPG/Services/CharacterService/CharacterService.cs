using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPG.Data;
using RPG.DTOs.CharacterDto;
using RPG.Models;

namespace RPG.Services.CharacterService
{

    public class CharacterService : ICharacterService
    {
        private DataContext _dbContext;
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper,DataContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
      
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character mappedCharacter = _mapper.Map<Character>(newCharacter);

           await _dbContext.Characters.AddAsync(mappedCharacter);

            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = _dbContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            var characters = await _dbContext.Characters.ToListAsync();

            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(_dbContext.Characters.SingleOrDefault(c => c.Id == id)); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdatedCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try {

                Character characterToUpdate = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                _dbContext.Characters.Update(characterToUpdate);

                await _dbContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
   
           }catch(Exception ex)
           {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
           }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character characaterToDelete = await _dbContext.Characters.FirstAsync(c => c.Id == id);

                _dbContext.Characters.Remove(characaterToDelete);

                await _dbContext.SaveChangesAsync();

                serviceResponse.Data = _dbContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
