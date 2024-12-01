using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Please Provide Name")]
        [StringLength(256)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please Generate SUK")]
        public string? SKU { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new();
        [Required(ErrorMessage = "Please Provide BasePrice")]
        public int BasePrice { get; set; }
        [Required(ErrorMessage = "Please Provide MRP")]
        public int MRP { get; set; }
        public string Description { get; set; }
        public CurrencyType Currency { get; set; }
        [Required]
        public DateTime ManufacturedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddMonths(5);
    }

    public enum CurrencyType
    {
        USD = 1,
        INR,
        EUR,
        GBP,
        JPY
    }
}
