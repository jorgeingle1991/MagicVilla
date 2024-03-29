﻿using MagicVilla_VillaAPI.Models.Villa.Dto;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.VillaNumber.Dto
{
    public class VillaNumberDTO
    {
        [Required]
        public int No { get; set; }

        [Required]
        public int VillaID { get; set; }

        [Required]
        public string SpecialDetails { get; set; }

        public VillaDTO Villa { get; set; }

    }
}
