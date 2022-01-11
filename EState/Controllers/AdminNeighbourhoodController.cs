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
    public class AdminNeighbourhoodController : Controller
    {
        NeighbourhoodManager nh = new NeighbourhoodManager(new EfNeighbourhoodDal());
        DataContext c = new DataContext();

     
        public IActionResult Index()
        {
            var list = nh.GetList(x => x.Status == true);
            return View(list);
        }

        public IActionResult Add()
        {
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Neighbourhood data)
        {
            NeighbourhoodValidation validationRules = new NeighbourhoodValidation();
            ValidationResult result = validationRules.Validate(data);


            if (result.IsValid)
            {
                nh.Add(data);
                TempData["Success"] = "Mahalle Ekleme İşlemi Başarıyla Gerçekleşti.";
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
            var update = nh.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Neighbourhood data)
        {
            NeighbourhoodValidation validationRules = new NeighbourhoodValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                nh.Update(data);
                TempData["Update"] = "Mahalle Güncelleme İşlemi Başarıyla Gerçekleşti";
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

            var delete = nh.GetById(id);

            nh.Delete(delete);

            return RedirectToAction("Index");
        }

        public void DropDown()
        {
            List<SelectListItem> value = (from i in c.Districts.Where(x => x.Status == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.DistrictName,
                                              Value = i.DistrictId.ToString()
                                          }).ToList();
            ViewBag.district = value;
        }
    }
}
