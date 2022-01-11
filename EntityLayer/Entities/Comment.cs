using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
   public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Name { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
        public int RatingAdvert { get; set; }
        public bool CommentStatus { get; set; }
        public int AdvertId { get; set; }
        public virtual Advert Advert { get; set; } 
    
    }
}
