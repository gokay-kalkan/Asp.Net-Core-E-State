using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SituationValidation:AbstractValidator<Situation>
    {
        public SituationValidation()
        {
            RuleFor(x => x.SituationName).NotEmpty().WithMessage("Durum Adı Boş Geçilemez");
            RuleFor(x => x.SituationName).MinimumLength(2).WithMessage("Minumum 2 Karakter Girişi Yapın");
            RuleFor(x => x.SituationName).MaximumLength(10).WithMessage("Maximum 10 Karakter Girişi Yapabilirsiniz");
        }
    }
}
