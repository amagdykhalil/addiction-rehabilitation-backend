using Microsoft.AspNetCore.Identity;

namespace ARC.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Name_ar { get; set; }
    }
}
