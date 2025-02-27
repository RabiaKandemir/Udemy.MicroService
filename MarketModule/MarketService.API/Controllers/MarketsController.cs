﻿using MarketService.API.Dtos;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.Interfaces;

namespace MarketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        private readonly MarketRepository _marketRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public MarketsController(MarketRepository marketRepository, IPublishEndpoint publishEnpoint)
        {
            _marketRepository = marketRepository;
            _publishEndpoint = publishEnpoint;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _marketRepository.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMarketDto dto)
        {
            var result = _marketRepository.Add(new Data.Entities.Market
            {
                InventoryId = dto.InventoryId,
                ItemId = dto.ItemId,
                PlayerId = dto.PlayerId,
                Price = dto.Price,
            });

            await _publishEndpoint.Publish<MarketCreated>(new { dto.InventoryId, dto.ItemId, Count = 1 });
            return Created("", result);
        }
    }
}
