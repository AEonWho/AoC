// See https://aka.ms/new-console-template for more information
internal class Game
{
    public int Id { get; }

    public List<(string Color, int Count)> Tokens { get; } = new List<(string, int)>();
    public Game(int id)
    {
        this.Id = id;
    }
}