using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Shope_ASP.NET.Models
{
    public class ShopProduct
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Color is required")]

        public string Color { get; set; }
        [Required(ErrorMessage = "Price is required")]

        public double Price { get; set; }

        public string? UrlPicture { get; set; }

        public int Category_Id { get; set; }

        [ForeignKey(nameof(Category_Id))]

        public ShopeCategory? ShopeCategory { get; set; }




    }
}
