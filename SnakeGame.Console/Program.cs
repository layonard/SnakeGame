using SnakeGame;
using SnakeGame.Core;
using SnakeGame.Core.Abstractions;
using SnakeGame.Core.Models;

BoardSettings gameBoard = new(40, 20);
IRenderer renderer = new ConsoleRenderer(gameBoard);
IInputHandler inputHandler = new InputHandler();

Game game = new(gameBoard, renderer, inputHandler);
game.Run();