using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminCityController : Controller
    {
        CityManager cm = new CityManager(new EfCityDal());
        DataContext c = new DataContext();
      
        public IActionResult Index()
        {
            var list = cm.GetList(x=>x.Status==true);
            return View(list);
        }
      
        public IActionResult CityAdd()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CityAdd(City p)
        {
            CityValidation validationRules = new CityValidation();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                cm.Add(p);
                TempData["Success"] = "Şehir Ekleme İşlemi Başarıyla Gerçekleşti.";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                
                }
            }
            return View();
           
        }

        public IActionResult Update(int id)
        {
          var update=  cm.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(City data)
        {
            CityValidation validationRules = new CityValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                cm.Update(data);
                TempData["Update"] = "Şehir Güncelleme İşlemi Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                
                }
            }


            return View();
        }

        public IActionResult Delete(int id)
        {

            var delete = cm.GetById(id);

               cm.Delete(delete);

            return RedirectToAction("Index");
        }
    }
}
