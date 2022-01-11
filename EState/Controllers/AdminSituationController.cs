using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    public class AdminSituationController : Controller
    {
        SituationManager sm = new SituationManager(new EfSituationDal());
        public IActionResult Index()
        {
            var list = sm.GetList(x => x.Status == true);
            return View(list);
        }
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Situation p)
        {
            SituationValidation validationRules = new SituationValidation();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                sm.Add(p);
                TempData["Success"] = "Durum Ekleme İşlemi Başarıyla Gerçekleşti.";
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
            var update = sm.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Situation data)
        {
            SituationValidation validationRules = new SituationValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                sm.Update(data);
                TempData["Update"] = "Durum Güncelleme İşlemi Başarıyla Gerçekleşti";
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

            var delete = sm.GetById(id);

            sm.Delete(delete);

            return RedirectToAction("Index");
        }
    }
}
