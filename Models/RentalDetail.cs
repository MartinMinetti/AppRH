using System.ComponentModel.DataAnnotations;

namespace AppRH.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }


        public int RentalID { get; set; }
        public virtual Rental? Rental { get; set; }


        public int HouseID { get; set; }
        public virtual House? House { get; set; }


        [Display(Name = "Nombre de la Casa")]
        public string? HouseName { get; set; }
    }
}