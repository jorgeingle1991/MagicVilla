using MagicVilla_WEB.Models.Villa.Dto;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WEB.Models.VillaNumber.Dto
{
    public class VillaNumberDTO
    {
        [Required]
        public int No { get; set; }

        [Required]
        public int VillaID { get; set; }

        public VillaDTO Villa { get; set; }

        [Required]
        public string SpecialDetails { get; set; }
    }
}
