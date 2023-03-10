using AutoMapper;
using MagicVilla_WEB.Models;
using MagicVilla_WEB.Models.Villa.Dto;
using MagicVilla_WEB.Models.VillaNumber.Dto;
using MagicVilla_WEB.Models.VM;
using MagicVilla_WEB.Services;
using MagicVilla_WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

namespace MagicVilla_WEB.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        public VillaNumberController(IVillaService villaService, IVillaNumberService villaNumberService, IMapper mapper)
        {
            _villaService = villaService;
            _villaNumberService = villaNumberService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSucccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSucccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSucccess && response.ErrorMessages.Count == 0 )
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }else
                {
                    if(response.ErrorMessages.Count>0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _villaService.GetAllAsync<APIResponse>();

            if (resp != null && resp.IsSucccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            VillaNumberUpdateVM villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSucccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber =  _mapper.Map<VillaNumberUpdateDTO>(model);
            }

            var resp = await _villaService.GetAllAsync<APIResponse>();

            if (resp != null && resp.IsSucccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSucccess && response.ErrorMessages.Count == 0)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _villaService.GetAllAsync<APIResponse>();

            if (resp != null && resp.IsSucccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            VillaNumberUpdateVM villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSucccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);
            }

            var resp = await _villaService.GetAllAsync<APIResponse>();

            if (resp != null && resp.IsSucccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {


            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.No);
            if (response != null && response.IsSucccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            return View(model);
        }
    }
}
