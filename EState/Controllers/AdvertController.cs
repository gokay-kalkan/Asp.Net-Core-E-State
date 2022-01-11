using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    public class AdvertController : Controller
    {
        AdvertManager am = new AdvertManager(new EfAdvertDal());
        ImageManager im = new ImageManager(new EfImageDal());
        CityManager cm = new CityManager(new EfCityDal());
        SituationManager sm = new SituationManager(new EfSituationDal());
        DataContext c = new DataContext();

        [Route("Advert/Details/{id?}/{AdvertTitle}")]
        public IActionResult Details(int id)
        {
            var detail = am.GetById(id);
            var imglist = im.GetList(x=>x.Status==true);

            ViewBag.imglist = imglist;
            var advert = am.GetList(x => x.Status == true);
            ViewBag.advert = advert;
            var imageadvert = c.Images.Where(x => x.AdvertId == id).ToList();
            ViewBag.imageadvert = imageadvert;

            var advertcomment = c.Comments.Where(x => x.AdvertId == id).ToList();
            ViewBag.comment = advertcomment;
            var sayi = c.Comments.Where(x => x.AdvertId == id).ToList().Count();
            ViewBag.sayi = sayi;
            var ortalama = c.Comments.Where(x => x.AdvertId == id).ToList().Select(x => x.RatingAdvert).DefaultIfEmpty(0).Average();
           
            ViewBag.Ortalama = Math.Round(ortalama);
          
            return View(detail);
        }
        [Route("Advert/Details/{id?}/{AdvertTitle}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Comment data)
        {

                data.CommentDate = DateTime.Now;
                data.CommentStatus = true;
                c.Comments.Add(data);
                c.SaveChanges();
          
            return RedirectToAction("Details");
 
        }

        public IActionResult AdvertRent()
        {
            DropDown();
            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            var imagelist = im.ImageListGet(x => x.Status == true);
            ViewBag.imagelist = imagelist;
            var rent = am.AdvertRent();
            return View(rent);
        }

        public IActionResult AdvertSale()
        {
            DropDown();
            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            var imagelist = im.ImageListGet(x => x.Status == true);
            ViewBag.imagelist = imagelist;
            var sale = am.AdvertSale();
            return View(sale);
        }
        public List<Situation> SituationGet()
        {
            List<Situation> situations = sm.GetList(x => x.Status == true);
            return situations;
        }
        public List<City> CityGet()
        {
            List<City> cities = cm.GetList(x => x.Status == true);
            return cities;
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
