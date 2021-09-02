﻿using Catalog.DTOs;
using Catalog.Entities;

namespace Catalog.Extensions
{
    public static class Item2Dto
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto()
            {
                Id = item.Id,
                CreatedDate = item.CreatedDate,
                Name = item.Name,
                Price = item.Price
            };
        }
    }
}