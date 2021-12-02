using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPG.DTOs.CharacterDto;
using RPG.Models;
using RPG.Services.CharacterService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RPG.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : ControllerBase
    {
        // GET: /<controller>/

       // private static Character knight = new Character();

        private  readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(_characterService.GetCharacterById(id)); 
        }

        [HttpPost("addcharacter")]
        public IActionResult AddCharacter(AddCharacterDto newCharacter)
        {
            
            return Ok(_characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatedCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = await _characterService.UpdateCharacter(updateCharacter);

            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
