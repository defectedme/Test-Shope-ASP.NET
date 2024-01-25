using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Shope_ASP.NET.Models
{
    public class ShopOrderItem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int IdOrderItem { get; set; }

        public double Quantity { get; set; }
        public double Price { get; set; }

        public float Total { get; set; }


        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ShopProduct ShopProduct { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]


        //public virtual ShopOrder ShopOrder { get; set; }

        public List<ShopOrder> ShoppingCartItems { get; set; }




    }

}

