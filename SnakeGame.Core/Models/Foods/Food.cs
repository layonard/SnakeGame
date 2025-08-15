namespace SnakeGame.Core.Models.Foods;

public record Food
{
    private Position _position;
    public Position Position => _position;

    public Food(Position position)
    {
        _position = position;
    }

    public Food(int x, int y)
    {
        _position = new(x, y);
    }
}
