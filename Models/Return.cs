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
        public virtual Customer? Customer { get; set; }


        public virtual ICollection<ReturnDetail>? ReturnDetails { get; set; }
    }

}
