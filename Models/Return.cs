using System.ComponentModel.DataAnnotations;

namespace AppRH.Models
{
    public class Return
    {
        [Key]
        public int ReturnID { get; set; }


        [Display(Name = "Fecha de Devolución")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }


        [Display(Name = "Cliente")]
        public int CustomerID { get; set; }


        [Display(Name = "Nombre Cliente")]
        public string? CustomerName { get; set; }


        [Display(Name = "Apellido Cliente")]
        public string? CustomerSurname { get; set; }
        public virtual Customer? Customer { get; set; }


        [Display(Name = "House")]
        public int HouseID { get; set; }


        [Display(Name = "Nombre de la Casa")]
        public string? HouseName { get; set; }
        public virtual House? House { get; set; }
        
    }

}
