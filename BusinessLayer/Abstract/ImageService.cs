using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ImageService:GenericService<Image>
    {
        public List<Image> ImageListGet(Expression<Func<Image, bool>> filter);
    }
}
