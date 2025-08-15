using SnakeGame.Core.Models;

namespace SnakeGame.Core.Abstractions;

public interface IInputHandler
{
    Direction? GetDirectionInput();
}