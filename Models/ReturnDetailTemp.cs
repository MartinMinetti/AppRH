using System.ComponentModel.DataAnnotations;


namespace AppRH.Models
{
    public class ReturnDetailTemp
    {
        [Key]
        public int ReturnDetailTempID { get; set; }


        public int HouseID { get; set; }


        [Display(Name = "Nombre de la Casa")]
        public string? HouseName { get; set; }
    }
}