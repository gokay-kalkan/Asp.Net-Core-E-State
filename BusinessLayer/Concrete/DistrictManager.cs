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
    public class DistrictManager : DistrictService
    {

        IDistrictDal _districtDal;
        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }
        public void Add(District p)
        {
            p.Status = true;
            _districtDal.Add(p);
        }

        public void Delete(District p)
        {
            var delete = _districtDal.GetById(p.DistrictId);
            delete.Status = false;
            _districtDal.Update(delete);
        }

        public District GetById(int id)
        {
            return _districtDal.GetById(id);
        }

        public List<District> List()
        {
            return _districtDal.List();
        }

        public List<District> GetList(Expression<Func<District, bool>> filter)
        {
            return _districtDal.List(filter);
        }

        public void Update(District p)
        {
           var update= _districtDal.GetById(p.DistrictId);
            update.DistrictName = p.DistrictName;
            update.CityId = p.CityId;
            _districtDal.Update(update);
        }
    }
}
