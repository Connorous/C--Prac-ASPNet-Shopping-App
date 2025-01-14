﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? CategoryName { get; set; }

        public List<Item> Items { get; set; }
    }
}
