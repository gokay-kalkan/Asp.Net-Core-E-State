using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{

    public class EfAdvertDal : GenericRepository<Advert>, IAdvertDal
    {
        DataContext c = new DataContext();
        public List<Advert> AdvertRentList()
        {
            return c.Adverts.Where(x => x.TypeId == 1).ToList();
        }

        public List<Advert> AdvertSaleList()
        {
            return c.Adverts.Where(x => x.TypeId == 2).ToList();
        }
    }
}
