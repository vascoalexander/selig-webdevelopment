using Microsoft.AspNetCore.Identity;

namespace FileServer.Models;

public class AppUser : IdentityUser
{
    public ICollection<UserFile> Files { get; set; }
}