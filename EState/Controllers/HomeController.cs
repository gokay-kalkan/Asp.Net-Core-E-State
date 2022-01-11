using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using EState.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EState.Controllers
{
    public class HomeController : Controller
    {
        AdvertManager am = new AdvertManager(new EfAdvertDal());
        ImageManager im = new ImageManager(new EfImageDal());
        CityManager cm = new CityManager(new EfCityDal());
        DistrictManager dm = new DistrictManager(new EfDistrictDal());
        NeighbourhoodManager nm = new NeighbourhoodManager(new EfNeighbourhoodDal());
        SituationManager sm = new SituationManager(new EfSituationDal());
        TypeManager tm = new TypeManager(new EfTypeDal());
        DataContext c = new DataContext();


        public IActionResult Index(int page=1)
        {
            DropDown();
            var list = am.GetList(x => x.Status == true).ToPagedList(page,3);
            var imagelist = im.ImageListGet(x => x.Status == true);
            ViewBag.imagelist = imagelist;
            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            return View(list);
        }
        public PartialViewResult SearchFiltre(string search)
        {
         
            return PartialView();
        }
        public IActionResult Filtre(string search)
        {
            DropDown();
            var imagelist = im.ImageListGet(x => x.Status == true);
            ViewBag.imagelist = imagelist;
            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            var list= am.GetList(x => x.AdvertTitle.Contains(search) || x.Description.Contains(search) || x.Type.TypeName.Contains(search) || x.Neighbourhood.NeighbourhoodName.Contains(search) || x.Neighbourhood.District.DistrictName.Contains(search) || x.Neighbourhood.District.City.CityName.Contains(search));
            return View(list);
        }
        public IActionResult AdvertDetails(int id,string Description)
        {
            var detail = am.GetById(id);
            var imglist = im.GetList(x => x.Status == true );
            
            ViewBag.imglist = imglist;
            return View(detail);
        }
        public PartialViewResult PartialFiltre()
        {
            DropDown();
            return PartialView();
        }
        public IActionResult Filter(int min,int max, int cityid,int typeid,int neighbourhoodid,int districtid,int situtationid)
        {
            DropDown();
            var imagelist = im.ImageListGet(x => x.Status == true);
            ViewBag.imagelist = imagelist;
            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;

            var filter = c.Adverts.Where(x => x.Price >= min && x.Price <= max && x.SituationId == situtationid && x.DistrictId == districtid && x.NeighbourhoodId == neighbourhoodid && x.CityId == cityid && x.TypeId == typeid).ToList();
            return View(filter);
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
        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }

        public IActionResult NeighbourhoodGet(int Districtid)
        {
            List<Neighbourhood> neighlist = nm.GetList(x => x.DistrictId == Districtid);
            ViewBag.neighlist = new SelectList(neighlist, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
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
