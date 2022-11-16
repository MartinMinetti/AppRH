using System.ComponentModel.DataAnnotations;

namespace AppRH.Models
{
    public class House
    {
        [Key]
        public int HouseID { get; set; }


        [Display(Name = "Nombre de la Casa")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? HouseName { get; set; }


        [Display(Name = "Domicilio")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? HouseAddress { get; set; }


        [Display(Name = "Nombre del Dueño")]
        [MaxLength(150, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? OwnerHouse { get; set; }


        [Display(Name = "Foto de la Casa")]
        public byte[]? PhotoHouse { get; set; }

       
        public bool EstaAlquilada { get; set; }

        public bool IsDeleted  { get; set; }



        public virtual  ICollection<RentalDetail>? RentalDetails { get; set; }
        
        public virtual ICollection<ReturnDetail>? ReturnDetails { get; set; }
    }
}