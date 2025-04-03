using Microsoft.AspNetCore.Identity;

namespace DinhVanHoangDuy_2180609183.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
    }
}
