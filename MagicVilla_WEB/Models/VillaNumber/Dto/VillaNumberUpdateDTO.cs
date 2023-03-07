using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WEB.Models.VillaNumber.Dto
{
    public class VillaNumberUpdateDTO
    {

        [Required]
        public int No { get; set; }
        [Required]
        public int VillaID { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
    }
}
