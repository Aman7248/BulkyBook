﻿using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkYBook.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,10000)]
        public int ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Price100 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        [Required]       
        [Display(Name ="Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
