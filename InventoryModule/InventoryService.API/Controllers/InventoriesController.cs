using InventoryService.API.Dtos;
using InventoryService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly InventoryRepository _inventoryRepository;

        public InventoriesController(InventoryRepository itemRepository)
        {
            _inventoryRepository = itemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(InventoryCreateDto dto)
        {
            var result = await _inventoryRepository.Create(new Data.Entities.Inventory
            {
               Name = dto.Name,
               PlayerId = dto.PlayerId,

            });
            return Created("", result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _inventoryRepository.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _inventoryRepository.GetById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(InventoryUpdateDto dto)
        {
            await _inventoryRepository.Update(new Data.Entities.Inventory
            {
                PlayerId= dto.PlayerId,
                Name = dto.Name,
                Id = dto.Id
            });
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _inventoryRepository.Remove(id);
            return NoContent();
        }
    }
}
