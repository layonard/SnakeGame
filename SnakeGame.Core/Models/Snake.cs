namespace SnakeGame.Core.Models;

public class Snake
{
    private readonly List<Position> _body;
    private Direction _currentDirection;
    private bool _shouldGrow = false;

    public IReadOnlyList<Position> Body => _body.AsReadOnly();
    public Position Head => _body[_body.Count - 1];

    public Snake(BoardSettings boardSettings)
    {
        Position centerPosition = new((int)boardSettings.Width / 2, (int)boardSettings.Height / 2);

        Position head = centerPosition;
        _body = new List<Position>()
        {
            new Position(centerPosition.X -3, centerPosition.Y),
            new Position(centerPosition.X -2, centerPosition.Y),
            new Position(centerPosition.X -1, centerPosition.Y),
            head,
        };

        _currentDirection = Direction.Right;
    }

    public void Move()
    {
        Position currentHead = Head;
        int newX = currentHead.X;
        int newY = currentHead.Y;

        switch (_currentDirection)
        {
            case Direction.Up:
                newY--;
                break;
            case Direction.Down:
                newY++;
                break;
            case Direction.Right:
                newX++;
                break;
            case Direction.Left:
                newX--;
                break;
        }

        Position newHeadPosition = new(newX, newY);

        _body.Add(newHeadPosition);

        if (_shouldGrow)
            _shouldGrow = false;
        else
            _body.RemoveAt(0);
    }

    public void ChangeDirection(Direction newDirection)
    {
        switch (newDirection)
        {
            case Direction.Up:
                if (_currentDirection is not Direction.Down)
                    _currentDirection = newDirection;
                break;
            case Direction.Right:
                if (_currentDirection is not Direction.Left)
                    _currentDirection = newDirection;
                break;
            case Direction.Down:
                if (_currentDirection is not Direction.Up)
                    _currentDirection = newDirection;
                break;
            case Direction.Left:
                if (_currentDirection is not Direction.Right)
                    _currentDirection = newDirection;
                break;
        }
    }

    public bool IsBody(Position position)
    {
        return _body.Contains(position);
    }

    public bool IsBodyWithoutHead(Position position)
    {
        for (int i = 0; i < _body.Count - 1; i++)
        {
            if (_body[i] == position)
                return true;
        }
        return false;
    }

    public void Eat()
    {
        _shouldGrow = true;
    }
}
