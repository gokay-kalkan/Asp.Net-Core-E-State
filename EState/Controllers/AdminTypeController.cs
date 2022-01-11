using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EState.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTypeController : Controller
    {
        TypeManager tm = new TypeManager(new EfTypeDal());
        DataContext c = new DataContext();
       
        public IActionResult Index()
        {
            var list = tm.GetList(x => x.Status == true);

            return View(list);
        }

        public IActionResult Add()
        {
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EntityLayer.Entities.Type data)
        {
            TypeValidation validationRules = new TypeValidation();
            ValidationResult result = validationRules.Validate(data);


            if (result.IsValid)
            {
                tm.Add(data);
                TempData["Success"] = "Tip Ekleme İşlemi Başarıyla Gerçekleşti.";
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
            var update = tm.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EntityLayer.Entities.Type data)
        {
            TypeValidation validationRules = new TypeValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                tm.Update(data);
                TempData["Update"] = "Tip Güncelleme İşlemi Başarıyla Gerçekleşti";
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

            var delete = tm.GetById(id);

            tm.Delete(delete);

            return RedirectToAction("Index");
        }

        public void DropDown()
        {
            List<SelectListItem> value = (from i in c.Situations.Where(x => x.Status == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.SituationName,
                                              Value = i.SituationId.ToString()
                                          }).ToList();
            ViewBag.situation = value;
        }
    }
}
