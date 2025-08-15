using SnakeGame.Core.Abstractions;
using SnakeGame.Core.Models;
using SnakeGame.Core.Models.Foods;

namespace SnakeGame.Core;

public class Game
{
    private const int MIN_GAME_DELAY_MILLISECONDS = 50;

    private readonly BoardSettings _boardSettings;
    private readonly IRenderer _renderer;
    private readonly IInputHandler _inputHandler;
    private readonly Snake _snake;

    private int _gameDelayMilliseconds = 100;
    private Food _food = new(0, 0);
    private readonly Random _random = new();
    private int _score = 0;
    private bool _isGameOver = false;

    public Game(BoardSettings boardSettings, IRenderer renderer, IInputHandler inputHandler)
    {
        _boardSettings = boardSettings;
        _renderer = renderer;
        _inputHandler = inputHandler;

        _snake = new Snake(_boardSettings);
    }

    public void Run()
    {
        InitializeGame();

        while (!_isGameOver)
        {
            ProcessInput();
            UpdateGameState();
            RenderGameState();
            ControlGameSpeed();
        }
        HandleGameOver();
    }

    private void InitializeGame()
    {
        _renderer.Initialize();
        _renderer.DrawBorder(_boardSettings);
        _renderer.DisplayScore(_score);
        _renderer.DrawSnake(_snake.Body);
        PlaceFood();
    }

    private void ProcessInput()
    {
        Direction? inputDirection = _inputHandler.GetDirectionInput();
        if (inputDirection.HasValue)
            _snake.ChangeDirection(inputDirection.Value);
    }

    private void UpdateGameState()
    {
        IReadOnlyList<Position> oldSnakeBody = _snake.Body.ToList();

        _snake.Move();

        if (FindsFood())
        {
            _snake.Eat();
            _renderer.ClearFood(_food);
            PlaceFood();
            IncreaseSpeed();
            _score++;
        }

        _isGameOver = IsCollission();

        if (_isGameOver)
            return;

        _renderer.ClearSnake(oldSnakeBody);
    }

    private void RenderGameState()
    {
        _renderer.DrawFood(_food);
        _renderer.DrawSnake(_snake.Body);
        _renderer.DisplayScore(_score);
    }

    private void ControlGameSpeed()
    {
        Thread.Sleep(_gameDelayMilliseconds);
    }

    private void HandleGameOver()
    {
        _renderer.DisplayGameOver();
    }

    private void PlaceFood()
    {
        Position foodPosition;
        int foodX = 0;
        int foodY = 0;

        do
        {
            foodX = _random.Next(1, _boardSettings.Width - 1);
            foodY = _random.Next(1, _boardSettings.Height - 1);
            foodPosition = new(foodX, foodY);
        } while (_snake.IsBody(foodPosition));

        _food = new(foodPosition);
    }

    private bool FindsFood()
    {
        return _snake.Head.X == _food.Position.X && _snake.Head.Y == _food.Position.Y;
    }

    private bool IsCollission()
    {
        return CollidesWithBody()
            || CollidesWithWall();
    }

    private bool CollidesWithBody()
    {
        return _snake.IsBodyWithoutHead(_snake.Head);
    }

    private bool CollidesWithWall()
    {
        return _snake.Head.X == 0 || _snake.Head.X >= _boardSettings.Width - 1 ||
               _snake.Head.Y == 0 || _snake.Head.Y >= _boardSettings.Height - 1;
    }

    private void IncreaseSpeed()
    {
        int reduceTime = 10;

        if (_gameDelayMilliseconds > MIN_GAME_DELAY_MILLISECONDS)
        {
            _gameDelayMilliseconds -= reduceTime;
        }
    }
}
