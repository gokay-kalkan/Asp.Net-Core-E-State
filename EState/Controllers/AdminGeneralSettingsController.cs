using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminGeneralSettingsController : Controller
    {
        GeneralSettingsManager generalSettingsManager = new GeneralSettingsManager(new EfGeneralSettingsDal());

        IWebHostEnvironment env;


        public AdminGeneralSettingsController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        public IActionResult Index()
        {
            var list = generalSettingsManager.List();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GeneralSettings data)
        {
            if (data != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");
               

                    var tamDosyaAdi = Path.Combine(dosyaYolu, data.Image.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {


                        data.Image.CopyTo(dosyaAkisi);
                    }

                generalSettingsManager.Add(data);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int id)
        {
            var update = generalSettingsManager.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(GeneralSettings data)
        {
            if (data != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");


                var tamDosyaAdi = Path.Combine(dosyaYolu, data.Image.FileName);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    data.ImageName = data.Image.FileName;
                    data.Image.CopyTo(dosyaAkisi);
                }

               
            }
            generalSettingsManager.Update(data);
            return RedirectToAction("Index");
        }

        
    }
}
