using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Shope_ASP.NET.Models
{
    public class ShopeCategory
    {


        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }

        //public List<ShopProduct> ShopProducts { get; set; }

        public virtual ICollection<ShopProduct>? ShopProducts { get; set; }

    }
}
