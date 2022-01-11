using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class DistrictValidation:AbstractValidator<District>
    {
        public DistrictValidation()
        {
            RuleFor(x => x.DistrictName).NotEmpty().WithMessage("Semt Adı Boş Geçilemez");
            RuleFor(x => x.DistrictName).MinimumLength(3).WithMessage("Semt Adı En Az 3 Karakter Olmalıdır");
            RuleFor(x => x.DistrictName).MaximumLength(25).WithMessage("Semt Adı En Fazla 25 Karakter olmalıdır");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("Şehir Adı Boş Geçilemez");

        }
    }
}
