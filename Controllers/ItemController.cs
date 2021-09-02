using System;
using System.Linq;
using System.Collections.Generic;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Extensions;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _repository;

        public ItemController(IItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetItems()
        {
            var items = _repository.GetItems();
            if (items is null)
            {
                return NotFound();
            }

            var itemDtOs = items.Select(item => item.AsDto());
            return Ok(itemDtOs);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }
        
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.Now,
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            _repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new {id=item.Id}, item.AsDto());
        }

        [HttpPut("{id:guid}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            var updateItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            
            _repository.UpdateItem(updateItem);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }
            
            _repository.DeleteItem(id);

            return NoContent();
        }
    }
}