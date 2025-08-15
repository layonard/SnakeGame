using SnakeGame.Core.Abstractions;
using SnakeGame.Core.Models;
using SnakeGame.Core.Models.Foods;

namespace SnakeGame;

public class ConsoleRenderer : IRenderer
{
    private const char FIELD = ' ';
    private const char FOOD = '*';
    private const char WALL = '█';
    private const char HEAD = '@';
    private const char BODY = 'o';
    private const int MARGIN = 2;

    private readonly BoardSettings _boardSettings;
    private readonly int _canvasWidth;
    private readonly int _canvasHeight;
    private readonly Position _endCursorPosition;

    public ConsoleRenderer(BoardSettings boardSettings)
    {
        _boardSettings = boardSettings;
        _canvasWidth = _boardSettings.Width + MARGIN;
        _canvasHeight = _boardSettings.Height + MARGIN;
        _endCursorPosition = new Position(0, _canvasHeight - 1);
    }

    public void Initialize()
    {
        Console.SetWindowSize(_canvasWidth, _canvasHeight);
        Console.SetBufferSize(_canvasWidth, _canvasHeight);
        Console.CursorVisible = false;
        Console.Clear();
    }

    public void DrawBorder(BoardSettings boardSettings)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(new string(WALL, boardSettings.Width));
        Console.SetCursorPosition(0, boardSettings.Height - 1);
        Console.Write(new string(WALL, boardSettings.Width));
        for (int i = 1; i < boardSettings.Height; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(WALL);
            Console.SetCursorPosition(boardSettings.Width - 1, i);
            Console.Write(WALL);
        }
    }

    public void DrawFood(Food food)
    {
        SetCursorPosition(food.Position);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(FOOD);
        Console.ResetColor();
    }

    public void DrawSnake(IReadOnlyList<Position> snakeBody)
    {
        for (int i = 0; i < snakeBody.Count; i++)
        {
            Position bodyPart = snakeBody[i];
            SetCursorPosition(bodyPart);
            if (i < snakeBody.Count - 1)
                Console.Write(BODY);
            else
                Console.Write(HEAD);
        }

        SetCursorPosition(_endCursorPosition);
    }

    public void ClearFood(Food food)
    {
        SetCursorPosition(food.Position);
        Console.Write(FIELD);
    }

    public void ClearSnake(IReadOnlyList<Position> snakeBody)
    {
        foreach (var bodyPart in snakeBody)
        {
            SetCursorPosition(bodyPart);
            Console.Write(FIELD);
        }
    }

    public void DisplayGameOver()
    {
        SetCursorPosition(_endCursorPosition);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("GAME OVER!");
        Console.ResetColor();
    }

    public void DisplayScore(int score)
    {
        Position scorePosition = new(0, _endCursorPosition.Y - 1);
        SetCursorPosition(scorePosition);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"SCORE: {score,-3}");
        Console.ResetColor();
    }

    public void Clear()
    {
        Console.Clear();
    }

    private void SetCursorPosition(Position position)
    {
        Console.SetCursorPosition(position.X, position.Y);
    }
}
