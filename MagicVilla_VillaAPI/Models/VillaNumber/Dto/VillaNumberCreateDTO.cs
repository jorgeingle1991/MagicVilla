﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.VillaNumber.Dto
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int No { get; set; }

        [Required]
        public int VillaID { get; set; }

        [Required]
        public string SpecialDetails { get; set; }
    }
}
