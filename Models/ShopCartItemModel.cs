using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Shope_ASP.NET.Models
{
    public class ShopCartItemModel
    {

        [Key]
        public int IdCart { get; set; }
        public string IdCartSesion { get; set; }

        public int IdProduct { get; set; }
        public virtual ShopProduct ShopProduct { get; set; }

        public double Quantity { get; set; }
        public DateTime CreationDate { get; set; }



    }
}
