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
    public class NeighbourhoodManager : NeighbourhoodService
    {
        INeighbourhoodDal _neighbourhoodDal;
        public NeighbourhoodManager(INeighbourhoodDal neighbourhoodDal)
        {
            _neighbourhoodDal = neighbourhoodDal;
        }
        public void Add(Neighbourhood p)
        {
            p.Status = true;
            _neighbourhoodDal.Add(p);
        }

        public void Delete(Neighbourhood p)
        {
            var delete=_neighbourhoodDal.GetById(p.NeighbourhoodId);
            delete.Status = false;
            _neighbourhoodDal.Update(delete);
        }

        public Neighbourhood GetById(int id)
        {
            return _neighbourhoodDal.GetById(id);
        }

        public List<Neighbourhood> List()
        {
            return _neighbourhoodDal.List();
        }

        public List<Neighbourhood> GetList(Expression<Func<Neighbourhood, bool>> filter)
        {
            return _neighbourhoodDal.List(filter);
        }

        public void Update(Neighbourhood p)
        {
            var update = _neighbourhoodDal.GetById(p.NeighbourhoodId);
            update.NeighbourhoodName = p.NeighbourhoodName;
            update.DistrictId = p.DistrictId;
            _neighbourhoodDal.Update(update);
        }
    }
}
