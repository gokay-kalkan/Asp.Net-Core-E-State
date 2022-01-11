using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Abstract
{
    public interface IImageDal:IRepository<Image>
    {
        public List<Image> ImageList(Expression<Func<Image,bool>>filter);
    }
}
