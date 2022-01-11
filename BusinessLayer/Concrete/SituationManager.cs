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
    public class SituationManager : SituationService
    {
        ISituationDal _situationDal;
        public SituationManager(ISituationDal situationDal)
        {
            _situationDal = situationDal;
        }
        public void Add(Situation p)
        {
            p.Status = true;
            _situationDal.Add(p);
        }

        public void Delete(Situation p)
        {
            var delete = _situationDal.GetById(p.SituationId);
            delete.Status = false;
            _situationDal.Update(delete);
        }

        public Situation GetById(int id)
        {
            return _situationDal.GetById(id);
        }

        public List<Situation> List()
        {
            return _situationDal.List();
        }

        public List<Situation> GetList(Expression<Func<Situation, bool>> filter)
        {
            return _situationDal.List(filter);
        }

        public void Update(Situation p)
        {
            var update = _situationDal.GetById(p.SituationId);
            update.SituationName = p.SituationName;
           
            _situationDal.Update(update);
        }
    }
}
