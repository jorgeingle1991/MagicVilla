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
    [ApiVersion("1.0")]
    public class VillaNumberAPIv1Controller : ControllerBase
    {
        private readonly ILogger<VillaNumberAPIv1Controller> _logger;
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        public VillaNumberAPIv1Controller(ILogger<VillaNumberAPIv1Controller> logger, IVillaNumberRepository dbVillaNumber, IVillaRepository dbVilla, IMapper mapper)
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
            return new string[] { "string1", "string2" };
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillasNumber()
        {
            try
            {
                IEnumerable<VillaNumberModel> villaNumberList = await _dbVillaNumber.GetAllAsync(includeProperties: "Villa");
                _logger.LogInformation("Getting all Villas");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetNumberVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Get Villa number Error with Id  " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _dbVillaNumber.GetAsync(u => u.No == id);

                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return NotFound(_response);

                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (await _dbVillaNumber.GetAsync(u => u.No == createDTO.No) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already assigned to one record");
                    return BadRequest(ModelState);
                }

                if (await _dbVilla.GetAsync(u => u.Id == createDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is invalid");
                    return BadRequest(ModelState);
                }


                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                VillaNumberModel villaNumber = _mapper.Map<VillaNumberModel>(createDTO);

                await _dbVillaNumber.CreateAsync(villaNumber);

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNumberVilla", new { id = villaNumber.No }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSucccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteNumberVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVillaNumber.GetAsync(u => u.No == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbVillaNumber.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSucccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpPut("{id:int}", Name = "UpdateNumberVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateNumberVilla(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.No)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already assigned to one record");
                }


                VillaNumberModel model = _mapper.Map<VillaNumberModel>(updateDTO);

                await _dbVillaNumber.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSucccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
