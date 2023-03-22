using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.VillaNumber;
using MagicVilla_VillaAPI.Models.VillaNumber.Dto;
using MagicVilla_VillaAPI.Repository.IRepository.IVilla;
using MagicVilla_VillaAPI.Repository.IRepository.IVillaNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.VillaNumber
{
    [Route("api/v{version:apiVersion}/VillaNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberAPIv2Controller : ControllerBase
    {
        private readonly ILogger<VillaNumberAPIv2Controller> _logger;
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        public VillaNumberAPIv2Controller(ILogger<VillaNumberAPIv2Controller> logger, IVillaNumberRepository dbVillaNumber, IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVillaNumber = dbVillaNumber;
            _dbVilla = dbVilla;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet("GetString")]

        public IEnumerable<string> Get()
        {
            return new string[] { "Jorge", "Ingle" };
        }
     

    }
}
