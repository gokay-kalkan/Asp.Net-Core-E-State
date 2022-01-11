using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CityValidation:AbstractValidator<City>
    {
        public CityValidation()
        {
            RuleFor(x => x.CityName).NotEmpty().WithMessage("Şehir Alanı Boş Geçilemez");
            RuleFor(x => x.CityName).MinimumLength(4).WithMessage("En az 4 karakter girişi yapın");
            RuleFor(x => x.CityName).MaximumLength(20).WithMessage("Maximum 20 Karakter olmalıdır");
        }
    }
}
