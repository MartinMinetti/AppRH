using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRH.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        

        [Display(Name = "Nombre del Cliente")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? CustomerName { get; set; }


        [Display(Name = "Apellido del Cliente")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? CustomerSurname { get; set; }


        [Display(Name = "DNI")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? CustomerDNI { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime CustomerBirthDate { get; set; }


        [NotMapped]
        public int CustomerAge
        {
            get
            {
                return DateTime.Now.Year - CustomerBirthDate.Year;
            }
        }
       

       public virtual  ICollection<Rental>? Rentals { get; set; }


       public virtual  ICollection<Return>? Returns { get; set; }
    }
}