namespace FileServer.Models;

public class LoginModel
{
    public required string UserName {get;set;}
    public required string Password {get;set;}
    public string? ReturnUrl { get; set; } = "/";
}