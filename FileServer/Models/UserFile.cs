using Microsoft.AspNetCore.Identity;

namespace FileServer.Models;

public class UserFile
{
    public int Id { get; set; }
    public string OriginalName { get; set; } = null!;
    public string StoredName { get; set; } = null!;
    public long FileSize { get; set; }
    public DateTime? UploadDate { get; set; }

    public string UserId { get; set; }
    public AppUser User { get; set; } = null!;
}