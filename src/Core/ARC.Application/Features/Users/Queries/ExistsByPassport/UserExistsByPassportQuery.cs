namespace ARC.Application.Features.Users.Queries.ExistsByPassport
{
    public record UserExistsByPassportQuery : IQuery<int?>
    {
        public string PassportNumber { get; set; }
    }
}