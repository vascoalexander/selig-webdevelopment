using ToDoApp.Models;

namespace ToDoApp.Repositories;

public class ToDoRepository
{
    private readonly List<ToDoItem> _items;
    private int _nextId = 1;

    public ToDoRepository()
    {
        _items = new List<ToDoItem>()
        {
            new ToDoItem { Id = _nextId++, Title = "Einkaufen gehen", IsDone = false },
            new ToDoItem { Id = _nextId++, Title = "C# lernen", IsDone = true },
            new ToDoItem { Id = _nextId++, Title = "Sport machen", IsDone = false }
        };
    }

    public IEnumerable<ToDoItem> GetAll()
    {
        return _items.OrderBy(i => i.Id);
    }

    public ToDoItem GetById(int id)
    {
        return _items.FirstOrDefault(i => i.Id == id)!;
    }

    public void Add(ToDoItem item)
    {
        item.Id = _nextId++;
        _items.Add(item);
    }

    public void Update(ToDoItem item)
    {
        var existingItem = GetById(item.Id);
        if (existingItem != null)
        {
            existingItem.Title = item.Title;
            existingItem.IsDone = item.IsDone;
        }
    }

    public void Delete(int id)
    {
        var itemToRemove = GetById(id);
        if (itemToRemove != null)
        {
            _items.Remove(itemToRemove);
        }
    }
}