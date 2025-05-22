using System.ComponentModel.DataAnnotations;
namespace ToDoApp.Models;

public class ToDoItem
{
    public int Id { get; set; }

    [Display(Name = "Aufgabentitel")]
    public string? Title { get; set; }

    [Display(Name = "Erledigt?")]
    public bool IsDone { get; set; }
}