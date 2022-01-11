using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminDistrictController : Controller
    {
        DistrictManager dm = new DistrictManager(new EfDistrictDal());
        DataContext c= new DataContext();

       
        public IActionResult Index()
        {
            var list = dm.GetList(x => x.Status == true);
            return View(list);
        }
        public IActionResult Add()
        {
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(District data)
        {
            DistrictValidation validationRules = new DistrictValidation();
            ValidationResult result = validationRules.Validate(data);

            
            if (result.IsValid)
            {
                dm.Add(data);
                TempData["Success"] = "Semt Ekleme İşlemi Başarıyla Gerçekleşti.";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            DropDown();
            return View();

        }

        public IActionResult Update(int id)
        {
            DropDown();
            var update = dm.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(District data)
        {
            DistrictValidation validationRules = new DistrictValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                dm.Update(data);
                TempData["Semt"] = "Semt Güncelleme İşlemi Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            DropDown();

            return View();
        }

        public IActionResult Delete(int id)
        {

            var delete = dm.GetById(id);

            dm.Delete(delete);

            return RedirectToAction("Index");
        }

        public void DropDown()
        {
            List<SelectListItem> value = (from i in  c.Cities.Where(x=>x.Status==true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.CityName,
                                              Value = i.CityId.ToString()
                                          }).ToList();
            ViewBag.city = value;
        } 
    }
}
