using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryValidator : AbstractValidator<GetPatientsQuery>
    {
        public GetPatientsQueryValidator(IStringLocalizer<GetPatientsQueryValidator> localizer)
        {
            RuleFor(x => x.SearchQuery)
                .MaximumLength(200)
                .WithMessage(localizer[LocalizationKeys.Validation.BetweenLength, 1, 200])
                .When(x => !string.IsNullOrEmpty(x.SearchQuery));

            RuleFor(x => x.NationalityId)
                .SetValidator(new IdValidator<GetPatientsQuery>(localizer));

            RuleFor(x => x.PageSize)
                .InclusiveBetween((short)1, (short)50)
                .WithMessage(localizer[LocalizationKeys.Validation.BetweenLength, 1, 50]);

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage(localizer[LocalizationKeys.Validation.MustBeGreaterThanOrEquel, 1]);

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage(localizer[LocalizationKeys.Validation.IsInEnum]);

            RuleFor(x => x.SortBy)
                .IsInEnum()
                .WithMessage(localizer[LocalizationKeys.Validation.IsInEnum]);

            RuleFor(x => x.SortDirection)
                .IsInEnum()
                .WithMessage(localizer[LocalizationKeys.Validation.IsInEnum]);
        }
    }
}