namespace DbToApp2.Models;

public class UploadViewModel
{
    public required string FileName { get; set; }
    public required IFormFile UploadedFile { get; set; }
}