using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string ImageName { get; set; }
        public bool Status { get; set; }
        public int AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}
