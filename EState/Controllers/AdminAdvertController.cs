using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminAdvertController : Controller
    {
        AdvertManager am = new AdvertManager(new EfAdvertDal());
        ImageManager im = new ImageManager(new EfImageDal());
        CityManager cm = new CityManager(new EfCityDal());
        DistrictManager dm = new DistrictManager(new EfDistrictDal());
        NeighbourhoodManager nm = new NeighbourhoodManager(new EfNeighbourhoodDal());
        SituationManager sm = new SituationManager(new EfSituationDal());
        TypeManager tm = new TypeManager(new EfTypeDal());
        DataContext c = new DataContext();
        IWebHostEnvironment env;

      
        public AdminAdvertController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        public IActionResult Index(string id)

        {
            id = HttpContext.Session.GetString("Id");
            var list = am.GetList(x => x.Status == true && x.UserAdminId==id);
            return View(list);
        }
        public IActionResult AdvertAll(string id)
        {
            id = HttpContext.Session.GetString("Id");

            var list = am.GetList(x => x.Status == true && x.UserAdminId != id);
            return View(list);
        }
        public List<City> CityGet()
        {
            List<City> cities = cm.GetList(x => x.Status == true);
            return cities;
        }
        public IActionResult DistrictGet(int Cityid)
        {
            List<District> districtlist = dm.GetList(x => x.CityId == Cityid);
            ViewBag.district = new SelectList(districtlist, "DistrictId", "DistrictName");
            return PartialView("DistrictPartial");
        }

        public IActionResult NeighbourhoodGet(int Districtid)
        {
            List<Neighbourhood> neighlist = nm.GetList(x => x.DistrictId == Districtid);
            ViewBag.neighlist = new SelectList(neighlist, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
        }
        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }
        public PartialViewResult NeighbourhoodPartial()
        {
            return PartialView();
        }

        public List<Situation> SituationGet()
        {
            List<Situation> situations = sm.GetList(x => x.Status == true);
            return situations;
        }

        public IActionResult Add()
        {
            ViewBag.userid = HttpContext.Session.GetString("Id");
            DropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Advert data)
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                    foreach (var item in data.Image)
                    {

                        var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                        using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                        {


                            item.CopyTo(dosyaAkisi);
                        }

                        data.Images.Add(new Image { ImageName = item.FileName, Status = true });

                    }
                    am.Add(data);
                    TempData["Success"] = "İlan Ekleme İşlemi Başarıyla Gerçekleşti.";
                    return RedirectToAction("Index");
                }
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

        public IActionResult TypeGet(int situationid)
        {
            var typelist = tm.GetList(x => x.SituationId == situationid);
            ViewBag.type = new SelectList(typelist, "TypeId", "TypeName");

            return PartialView("TypePartial");
        }

        public PartialViewResult TypePartial()
        {
            return PartialView();
        }

      
        public IActionResult Update(int id)
        {
            ViewBag.userid = HttpContext.Session.GetString("Id");
            DropDown();
            var update=am.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Advert data)
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
               
                    am.Update(data);
                    TempData["Update"] = "İlan Güncelleme İşlemi Başarıyla Gerçekleşti";
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
            var delete = am.GetById(id);
            am.Delete(delete);
            return RedirectToAction("Index");
        }

        public IActionResult AdvertImages(int id)
        {
            var list = im.GetList(x => x.AdvertId == id && x.Status==true);
            return View(list);
        }

        public IActionResult ImageDelete(int id)
        {
            var delete = im.GetById(id);
            im.Delete(delete);
            return RedirectToAction("Index");
        }
        public IActionResult ImageUpdate(int id)
        {
            var update = im.GetById(id);

            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageUpdate(Image data)
        {
            ImageValidation validationRules = new ImageValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                if (data.File != null)
                {
                    var dosyaYolu = Path.Combine(env.WebRootPath, "img");

                    var tamDosyaAdi = Path.Combine(dosyaYolu, data.File.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {


                        data.File.CopyTo(dosyaAkisi);
                    }

                    im.Update(data);
                    return RedirectToAction("Index");
                }

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
        public IActionResult ImageCreate(int id)
        {
            var image = am.GetById(id);
            return View(image);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult ImageCreate(Advert data)
        {
            
           
                var image = am.GetById(data.AdvertId);
                if (data.Image != null)
                {
                    var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                    foreach (var item in data.Image)
                    {

                        
                        var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                        using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                        {


                            item.CopyTo(dosyaAkisi);
                        }

                        im.Add(new Image { ImageName = item.FileName, Status = true,AdvertId=image.AdvertId });


                    }

                c.SaveChanges();
                    return RedirectToAction("Index");
                }

            
           
            return View();
        }   
        
        public void DropDown()
        {
            ViewBag.citylist = new SelectList(CityGet(), "CityId", "CityName");
            ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");
         

            List<SelectListItem> value1 = (from i in c.Districts.Where(x => x.Status == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.DistrictName,
                                              Value = i.DistrictId.ToString()
                                          }).ToList();
            ViewBag.district = value1;

            List<SelectListItem> value2 = (from i in c.Neighbourhoods.Where(x => x.Status == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.NeighbourhoodName,
                                              Value = i.NeighbourhoodId.ToString()
                                          }).ToList();
            ViewBag.neighbourhood = value2;
            List<SelectListItem> value3 = (from i in c.Types.Where(x => x.Status == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.TypeName,
                                               Value = i.TypeId.ToString()
                                           }).ToList();
            ViewBag.type = value3;

            List<SelectListItem> value4 = (from i in c.Situations.Where(x => x.Status == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.SituationName,
                                               Value = i.SituationId.ToString()
                                           }).ToList();
            ViewBag.situation = value4;
        }

    }
}
