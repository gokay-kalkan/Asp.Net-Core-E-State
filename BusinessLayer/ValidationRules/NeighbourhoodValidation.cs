using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class NeighbourhoodValidation:AbstractValidator<Neighbourhood>
    {
        public NeighbourhoodValidation()
        {
            RuleFor(x => x.NeighbourhoodName).NotEmpty().WithMessage("Mahalle Adı Boş Geçilemez");
            RuleFor(x => x.NeighbourhoodName).MinimumLength(3).WithMessage("Mahalle Adı Minumum 3 karakter olmalıdır");
            RuleFor(x => x.NeighbourhoodName).MaximumLength(25).WithMessage("Mahalle Adı En Az 25 karakter uzunluğunda olmalıdır");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("Semt Adı Boş Geçilemez");

        }
    }
}
