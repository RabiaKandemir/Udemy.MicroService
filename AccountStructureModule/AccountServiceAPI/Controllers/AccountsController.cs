using AccountService.API.Dtos;
using AccountService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly PlayerRepository _playerRepository;

        public AccountsController(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlayerCreateDto dto)
        {
           var result= await _playerRepository.Create(new Data.Entities.Player
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
            });
            return Created("", result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _playerRepository.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _playerRepository.GetById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(PlayerUpdateDto dto)
        {
            await _playerRepository.Update(new Data.Entities.Player { FirstName=dto.FirstName,LastName=dto.LastName,UserName=dto.UserName,Id=dto.Id});
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
          await _playerRepository.Remove(id);
            return NoContent();
        }
    }
}
