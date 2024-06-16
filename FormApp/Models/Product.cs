using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class Product
    {
        [Display(Name="Urun Id")]
        public int ProductId { get; set; }

        [Display(Name="Urun Adı")]
        [Required(ErrorMessage ="Gerekli Bir Alan")]  //Zorunlu alan
        [StringLength(100)]
        public string Name { get; set; }=null!;

        [Display(Name="Fiyat")]
        [Range(0,10000000)]
        public decimal Price { get; set; }

        [Display(Name="Resim")]
        public string? Image { get; set; }=string.Empty;
        public bool IsActive { get; set; }

        [Display(Name="Category")]
        [Required]
        public int CategoryId { get; set; }

        

       
    }
}