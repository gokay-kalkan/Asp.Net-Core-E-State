using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class TypeValidation : AbstractValidator<EntityLayer.Entities.Type>
    {
        public TypeValidation()
        {
            RuleFor(x => x.TypeName).NotEmpty().WithMessage("Tip Adı Boş Geçilemez");
            RuleFor(x => x.TypeName).MinimumLength(2).WithMessage("Minumum 2 Karakter Girişi Yapınız");
            RuleFor(x => x.TypeName).MaximumLength(15).WithMessage("Maximum 15 Karakter Girişi Yapınız");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("Durum Adı Boş Geçilemez");
        }
    }
}
