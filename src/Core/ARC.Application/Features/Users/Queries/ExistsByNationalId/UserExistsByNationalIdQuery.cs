namespace ARC.Application.Features.Users.Queries.ExistsByNationalId
{
    public record UserExistsByNationalIdQuery : IQuery<int?>
    {
        public string NationalIdNumber { get; set; }
    }
}