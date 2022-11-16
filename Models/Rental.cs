using System.ComponentModel.DataAnnotations;

namespace AppRH.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }


        [Display(Name = "Fecha de Alquiler")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }


        [Display(Name = "Cliente")]
        public int CustomerID { get; set; }
        public virtual Customer? Customer { get; set; }


        public virtual ICollection<RentalDetail>? RentalDetails { get; set; }
    }

}
