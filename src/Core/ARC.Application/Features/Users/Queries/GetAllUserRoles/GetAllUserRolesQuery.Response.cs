namespace ARC.Application.Features.Users.Queries.GetAllRoles
{
    public record GetAllUserRolesQueryResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
} 