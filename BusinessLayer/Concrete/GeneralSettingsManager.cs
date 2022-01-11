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
    public class GeneralSettingsManager : GeneralSettingsService
    {
        IGeneralSettingsDal _generalSettingsDal;
        public GeneralSettingsManager(IGeneralSettingsDal generalSettingsDal)
        {
            _generalSettingsDal = generalSettingsDal;
        }

        public void Add(GeneralSettings p)
        {
            _generalSettingsDal.Add(p);
        }

        public void Delete(GeneralSettings p)
        {
            _generalSettingsDal.Delete(p);
        }

        public GeneralSettings GetById(int id)
        {
           return  _generalSettingsDal.GetById(id);
        }

        public List<GeneralSettings> GetList(Expression<Func<GeneralSettings, bool>> filter)
        {
            return _generalSettingsDal.List(filter);
        }

        public List<GeneralSettings> List()
        {
            return _generalSettingsDal.List();
        }

        public void Update(GeneralSettings p)
        {
            _generalSettingsDal.Update(p);
        }
    }
}
