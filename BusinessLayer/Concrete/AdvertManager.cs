using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdvertManager : AdvertService
    {
        IAdvertDal _advertDal;
        public AdvertManager(IAdvertDal advertDal)
        {
            _advertDal = advertDal;
        }
        
        public void Add(Advert p)
        {
            p.AdvertDate = DateTime.Now;
            p.Status = true;
            _advertDal.Add(p);
        }

        public void Delete(Advert p)
        {
            var delete = _advertDal.GetById(p.AdvertId);
            delete.Status = false;
            _advertDal.Update(p);
        }

        public Advert GetById(int id)
        {
            return _advertDal.GetById(id);
        }

        public List<Advert> List()
        {
            return _advertDal.List();
        }

        public List<Advert> GetList(Expression<Func<Advert, bool>> filter)
        {
            return _advertDal.List(filter);
        }

        public void Update(Advert p)
        {
            var update = _advertDal.GetById(p.AdvertId);
            update.Adress = p.Adress;
            update.AdvertTitle = p.AdvertTitle;
            update.Area = p.Area;
            update.BathroomNumbers = p.BathroomNumbers;
            update.Credit = p.Credit;
            update.Description = p.Description;
            update.Floor = p.Floor;
            update.DistrictId = p.DistrictId;
            update.CityId = p.CityId;
            update.NeighbourhoodId = p.NeighbourhoodId;
            update.PhoneNumber = p.PhoneNumber;
            update.AdvertDate = DateTime.Now;
            update.AirConditoner = p.AirConditoner;
            update.Garage = p.Garage;
            update.Garden = p.Garden;
            update.Furniture = p.Furniture;
            update.Fireplace = p.Fireplace;
            update.Pool = p.Pool;
            update.Teras = p.Teras;
          
            _advertDal.Update(update);
        }

        public List<Advert> AdvertSale()
        {
            return _advertDal.AdvertSaleList();
        }

        public List<Advert> AdvertRent()
        {
            return _advertDal.AdvertRentList();
        }
    }
}
