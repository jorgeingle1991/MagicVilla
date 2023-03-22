using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_WEB.Models;
using MagicVilla_WEB.Models.Villa.Dto;
using MagicVilla_WEB.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MagicVilla_WEB.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        [Authorize]
        public async  Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new();
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString("JWTToken"));
            if(response != null && response.IsSucccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
        {
            if(ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if(response != null && response.IsSucccess)
                {
                    TempData["success"] = "Villa created succesfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
                   
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if(response!= null && response.IsSucccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaUpdateDTO>(model));
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSucccess)
                {
                    TempData["success"] = "Villa updated succesfully";
                    return RedirectToAction(nameof(IndexVilla));
                }

            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSucccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO model)
        {


                var response = await _villaService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSucccess)
                {
                    TempData["success"] = "Villa deleted succesfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            TempData["error"] = "Error encountered";
            return View(model);
        }


    }
}
