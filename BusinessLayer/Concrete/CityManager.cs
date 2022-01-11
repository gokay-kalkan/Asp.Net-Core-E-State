using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CityManager : CityService
    {
        ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public void Add(City p)
        {
            p.Status = true;
            _cityDal.Add(p);
        }
        
        public void Delete(City p)
        {
            var delete = _cityDal.GetById(p.CityId);
            delete.Status = false;
            _cityDal.Update(delete);
        }

        public City GetById(int id)
        {
            return _cityDal.GetById(id);
        }

        public List<City> List()
        {
            return _cityDal.List();
        }

        public List<City> GetList(Expression<Func<City, bool>> filter)
        {
            return _cityDal.List(x=>x.Status==true);
        }

        

        public void Update(City p)
        {
            var update=_cityDal.GetById(p.CityId);
            update.CityName = p.CityName;
            _cityDal.Update(update);
        }
    }
}
