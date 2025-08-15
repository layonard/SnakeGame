using SnakeGame.Core.Models;
using SnakeGame.Core.Models.Foods;

namespace SnakeGame.Core.Abstractions;

public interface IRenderer
{
    void Initialize();
    void DrawBorder(BoardSettings boardSettings);
    void DrawSnake(IReadOnlyList<Position> snakeBody);
    void ClearSnake(IReadOnlyList<Position> snakeBody);
    void DrawFood(Food food);
    void ClearFood(Food food);
    void DisplayScore(int score);
    void DisplayGameOver();
}