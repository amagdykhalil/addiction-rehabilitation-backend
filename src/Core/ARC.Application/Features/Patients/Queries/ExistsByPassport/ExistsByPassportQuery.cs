using Application.Abstractions.Messaging;

namespace ARC.Application.Features.Patients.Queries.ExistsByPassport
{
    public record ExistsByPassportQuery : IQuery<int?>
    {
        public string PassportNumber { get; init; }
        
    }
} 