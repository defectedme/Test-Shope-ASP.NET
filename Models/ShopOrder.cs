using System.ComponentModel.DataAnnotations;

namespace Test_Shope_ASP.NET.Models
{
    public class ShopOrder
    {

        [Key]
        public int IdOrder { get; set; }


        public DateTime? DateOfOrder { get; set; }

        //[Required(ErrorMessage = "Name Required")]
        //[Display(Name = "Name")]
        //[StringLength(160)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Last Name requaiet")]
        //[Display(Name = "Last Name")]
        //[StringLength(160)]
        public string LastName { get; set; }



        //[Required(ErrorMessage = "Email  requaiet")]
        //[Display(Name = "Adres email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
         ErrorMessage = "Email is not correct.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public double Total { get; set; }

        //public List<ShopOrderItem> ShopOrderItems { get; set; } 


        //[Required(ErrorMessage = "Ulica jest wymagana")]
        //[StringLength(70)]
        public string Street { get; set; }

        //[Required(ErrorMessage = "Miasto jest wymagane")]
        //[StringLength(70)]
        public string City { get; set; }

        //[Required(ErrorMessage = "Wojewodztwo jest wymagane")]
        //[StringLength(70)]
        //[Display(Name = "Województwo")]
        //public string Wojewodztwo { get; set; }

        //[Required(ErrorMessage = "Kod Pocztowy jest wymagany")]
        //[Display(Name = "Kod Pocztowy")]
        //[StringLength(10)]
        public string PostCode { get; set; }

        //[Required(ErrorMessage = "Państwo jest wymagane")]
        //[StringLength(70)]
        //[Display(Name = "Państwo")]
        public string Country { get; set; }

        //[Required(ErrorMessage = "Numer telefonu")]
        //[StringLength(24)]
        //[Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<ShopOrderItem>? ShopOrderItem { get; set; }
        //public List<ShopOrderItem> ShopOrderItems { get; set; }



    }
}
