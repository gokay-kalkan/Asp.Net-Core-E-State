using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AdvertValidation:AbstractValidator<Advert>
    {
        public AdvertValidation()
        {
            RuleFor(x => x.Adress).NotEmpty().WithMessage("İlan Adresi Boş Geçilemez");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Alan Boş Geçilemez");
            RuleFor(x => x.BathroomNumbers).NotEmpty().WithMessage("Banyo Sayısı Boş Geçilemez");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("Şehir Alanı Boş Geçilemez");
            RuleFor(x => x.Credit).NotEmpty().WithMessage("Kredi Alanı Boş Geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Geçilemez");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("Semt Alanı Boş Geçilemez");
            RuleFor(x => x.Floor).NotEmpty().WithMessage("Kat Sayısı Boş Geçilemez");
            //RuleFor(x => x.Image).NotEmpty().WithMessage("İlan Resimleri Boş Geçilemez");
            RuleFor(x => x.NumberOfRooms).NotEmpty().WithMessage("Oda Sayısı Boş Geçilemez");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Boş Geçilemez");
            RuleFor(x => x.PhoneNumber).Must(IsPhoneNumber).WithMessage("Telefon Numarası Formatı Yanlış Girilmiştir");
            //RuleFor(x => x.PhoneNumber).ExclusiveBetween(1, 11).WithMessage("Telefon Numarası 10 haneli olmalıdır");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Geçilemez");
            RuleFor(x => x.AdvertTitle).NotEmpty().WithMessage("İlan Başlığı Alanı Boş Geçilemez");

      
           
        }
        private bool IsPhoneNumber(string arg)
        {

            Regex regex = new Regex(@"^[0-9]{10}$");
            return regex.IsMatch(arg);
        }

    }
    
}
