using ARC.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ARC.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}