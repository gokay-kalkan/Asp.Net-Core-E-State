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
    public class ImageManager : ImageService
    {
        IImageDal _ımageDal;
        public ImageManager(IImageDal ımageDal)
        {
            _ımageDal = ımageDal;
        }
        public void Add(Image p)
        {
            p.Status = true;
           
            _ımageDal.Add(p);
        }
        
        public void Delete(Image p)
        {
            var delete = _ımageDal.GetById(p.Id);
            
            delete.Status = false;
            _ımageDal.Update(delete);
        }

        public Image GetById(int id)
        {
            return _ımageDal.GetById(id);
        }

        public List<Image> List()
        {
            return _ımageDal.List();
        }

        public List<Image> GetList(Expression<Func<Image, bool>> filter)
        {
            return _ımageDal.List(filter);
        }

        public void Update(Image p)
        {
            var update = _ımageDal.GetById(p.Id);
            update.ImageName = p.File.FileName;

            _ımageDal.Update(update);
        }

        public List<Image> ImageListGet(Expression<Func<Image, bool>> filter)
        {
            return _ımageDal.ImageList(x => x.Status == true);
        }
    }
}
