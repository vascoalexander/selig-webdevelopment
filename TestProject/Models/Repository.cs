using Microsoft.AspNetCore.Mvc;

namespace TestProject.Models;

public static class Repository
{
    private static int lastId = 0;

    private static List<Position> positions = new();

    public static IEnumerable<Position> Positions
    {
        get { return positions; }
    }

    public static void AddPosition(Position position)
    {
        positions.Add(position);
        position.ID = lastId++;
    }

    public static void ArtikelLoeschen(int? id)
    {
        var position = Positions.FirstOrDefault(p => p.ID == id);
        if (id != null)
        {
            positions.Remove(position);
        }
    }

    public static void IncreaseArticleCount(int? id)
    {
        var position = Positions.FirstOrDefault(p => p.ID == id);
        {
            if (position.Anzahl < 50)
                position.Anzahl++;
        }
    }
    public static void DecreaseArticleCount(int? id)
    {
        var position = Positions.FirstOrDefault(p => p.ID == id);
        {
            if (position.Anzahl > 0)
                position.Anzahl--;
        }
    }
}