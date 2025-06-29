using Application.Abstractions.Messaging;

namespace ARC.Application.Features.Patients.Queries.ExistsById
{
    public record ExistsByIdQuery : IQuery<bool>
    {
        public int Id { get; init; }
    
    }
} 