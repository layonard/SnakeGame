using SnakeGame.Core.Abstractions;
using SnakeGame.Core.Models;

namespace SnakeGame;

public class InputHandler : IInputHandler
{
    public Direction? GetDirectionInput()
    {
        Direction? result = null;

        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                case ConsoleKey.I:
                    result = Direction.Up;
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                case ConsoleKey.J:
                    result = Direction.Left;
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.K:
                    result = Direction.Down;
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                case ConsoleKey.L:
                    result = Direction.Right;
                    break;
            }
        }

        return result;
    }
}
