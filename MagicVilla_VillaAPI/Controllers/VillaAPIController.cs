using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.Villa.Dto;
using MagicVilla_VillaAPI.Models.VillaNumber.Dto;
using MagicVilla_VillaAPI.Repository.IRepository.IVilla;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/v{version:apiVersion}/VillaAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersionNeutral]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(ILogger<VillaAPIController> logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _logger = logger;
            _mapper = mapper;
            this._response = new ();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas([FromQuery(Name ="filterOccupancy")]int? occupancy, [FromQuery] string? search, int pageSize=0,int pageNumber = 1)
        {
            try
            {
                IEnumerable<VillaModel> villaList;

                if (occupancy > 0)
                {
                    villaList = await _dbVilla.GetAllAsync(u => u.Occupancy == occupancy,pageSize: pageSize,pageNumber:pageNumber);
                }
                else
                {
                    villaList = await _dbVilla.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    villaList = villaList.Where(u => u.Name.ToLower().Contains(search));
                }

                Pagination pagination =  new() { PageNumber = pageNumber, PageSize = pageSize};

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _logger.LogInformation("Getting all Villas");
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
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

        [HttpGet("{id:int}", Name = "GetVilla")]
        [Authorize(Roles = "admin")]
        [ResponseCache(Duration = 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Get Villa Error with Id  " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _dbVilla.GetAsync(u => u.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaDTO>(villa);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already exists");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                VillaModel villa = _mapper.Map<VillaModel>(createDTO);

                await _dbVilla.CreateAsync(villa);

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSucccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbVilla.RemoveAsync(villa);
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

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                VillaModel model = _mapper.Map<VillaModel>(updateDTO);

                await _dbVilla.UpdateAsync(model);

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

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            try
            {
                        if (patchDTO == null || id == 0)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    };
                    var villa = await _dbVilla.GetAsync(u => u.Id == id,tracked: false);

                    if (villa == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);

                    }

                    VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

                    patchDTO.ApplyTo(villaDTO, ModelState);

                    VillaModel model = _mapper.Map<VillaModel>(villaDTO);

                    await _dbVilla.UpdateAsync(model);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    return NoContent();
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
