using System.ComponentModel.DataAnnotations;

namespace AppRH.Models
{
    public class ReturnDetail
    {
        [Key]
        public int ReturnDetailID { get; set; }


        public int ReturnID { get; set; }
        public virtual Return? Return { get; set; }


        public int HouseID { get; set; }
        public virtual House? House { get; set; }


        [Display(Name = "Nombre de la Casa")]
        public string? HouseName { get; set; }
    }
}