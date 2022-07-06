using hw03_solid;
using hw03_solid.factories;

Console.WriteLine("Welcome to The Worldwide Best Casino!");

IGameStrategy guessNumberGame = new GuessNumberGameFactory().CreateStrategy();
guessNumberGame.Play(Console.In, Console.Out);