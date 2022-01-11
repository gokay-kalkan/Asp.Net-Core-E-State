using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfImageDal : GenericRepository<Image>, IImageDal
    {
        DataContext c = new DataContext();
        public List<Image> ImageList(Expression<Func<Image, bool>> filter)
        {
            return c.Images.Where(x => x.Status == true).Take(3).ToList();
        }
    }
}
