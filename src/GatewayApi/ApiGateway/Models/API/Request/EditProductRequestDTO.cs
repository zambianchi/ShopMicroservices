﻿using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Models.API.Request
{
    public class EditProductRequestDTO
    {
        [Required]
        public long IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
        public long CategoryId { get; set; }
    }
}
