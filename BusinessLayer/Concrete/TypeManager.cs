using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TypeManager : TypeService
    {
        ITypeDal _typeDal;
        public TypeManager(ITypeDal typeDal)
        {
            _typeDal = typeDal;
        }
        public void Add(EntityLayer.Entities.Type p)
        {
            p.Status = true;
            _typeDal.Add(p);
        }

        public void Delete(EntityLayer.Entities.Type p)
        {
            var delete = _typeDal.GetById(p.TypeId);
            delete.Status = false;
            _typeDal.Update(p);
        }

        public EntityLayer.Entities.Type GetById(int id)
        {
            return _typeDal.GetById(id);
        }

        public List<EntityLayer.Entities.Type> List()
        {
            return _typeDal.List();
        }

        public List<EntityLayer.Entities.Type> GetList(Expression<Func<EntityLayer.Entities.Type, bool>> filter)
        {
            return _typeDal.List(filter);
        }

        public void Update(EntityLayer.Entities.Type p)
        {
            var update = _typeDal.GetById(p.TypeId);
            update.TypeName = p.TypeName;
            update.SituationId = p.SituationId;
            _typeDal.Update(p);
        }
    }
}
