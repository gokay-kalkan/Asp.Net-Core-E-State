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
    public class Advert
    {
            public Advert()
            {
                Images = new List<Image>();
            }
        
        [Key]
        public int AdvertId { get; set; }
        public string AdvertTitle { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Garage { get; set; }
        public bool Garden { get; set; }
        public bool Fireplace { get; set; }
        public bool Furniture { get; set; }//eşyalı mı
        public bool Pool { get; set; }//havuz
        public bool Teras { get; set; }//teras
        public bool AirConditoner { get; set; }
        public int NumberOfRooms { get; set; } //oda sayısı
        public int BathroomNumbers { get; set; } //banyo sayısı

        public bool Credit { get; set; }

        public int Area { get; set; } //alan
        public DateTime AdvertDate { get; set; }
        public int Floor { get; set; } //kat
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public int CityId { get; set; }
       
        public int DistrictId { get; set; }

        public int NeighbourhoodId { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }
        public int SituationId { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }


        public bool Status { get; set; }
        public string UserAdminId { get; set; }
        public virtual  UserAdmin UserAdmin { get; set; }

        [NotMapped]
        public IEnumerable <IFormFile> Image { get; set; }

        public virtual List<Image> Images { get; set; }
        public virtual List<Comment> Comments { get; set; }


    }
}
