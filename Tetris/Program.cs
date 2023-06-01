using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
	public static class MyConsole
	{
		public static void Color(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}

		public static void ColorSameLine(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ResetColor();
		}
	}
	public class Board
	{
		public int Height { get; }

		public int Width { get; }

		public int[,] Data { get; }

		public Board(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			this.Data = new int[width, height];
		}

		public void Print()
		{
			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Width; c++)
				{
					MyConsole.ColorSameLine("|", ConsoleColor.White);
					if (Data[c, r] == 0)
					{
						//MyConsole.ColorSameLine($"{Data[r, c]}", ConsoleColor.Blue);
						MyConsole.ColorSameLine($" ", ConsoleColor.Blue);
					}
					if (Data[c, r] == 1)
					{
						//MyConsole.ColorSameLine($"{Data[r, c]}", ConsoleColor.Blue);
						MyConsole.ColorSameLine($"#", ConsoleColor.Blue);
					}
				}
				Console.WriteLine("|");
			}
		}
		private static void MovePieceDown()
		{

		}
		public void CanClearLine()
		{
			int LineCheck = 0;
			//MyConsole.Color($"W: {Width}", ConsoleColor.Red);
			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Width; c++)
				{
					if (Data[c, r] == 1) LineCheck++;
					if (LineCheck == 10)
					{
						for (int i = 0; i < Width; i++)
						{
							Data[i, r] = 0; // Have to move the line one space lower
							// will call a funcion here will input r as the row and move the lines above it one lower
							//MyConsole.Color($"row = {r}", ConsoleColor.Red);
						}
						break; // Exit the loop after clearing the line
					}
				}
				//MyConsole.Color("G", ConsoleColor.Red);
				LineCheck = 0;
			}
		}
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			//int[,] board;
			float blockShoudMove = 5; // This will determin the game speed
			float blockShoudMoveDef = blockShoudMove;
			int maxBoardX = 10, maxBoardY = 20;
			Board board = new Board(maxBoardX, maxBoardY);
			int blockX = 4;
			int blockY = 0;
			board.Data[blockX, blockY] = 1;
			//board.Data[4,15] = 0;
			int moveXAxis = 0;
			Console.TreatControlCAsInput = true;
			Console.TreatControlCAsInput = true;
			ConsoleKeyInfo cki;
			MyConsole.Color("Please Input the button you would like to use for moving Left", ConsoleColor.Green);
			ConsoleKey Left = Console.ReadKey().Key;
			MyConsole.Color("Please Input the button you would like to use for moving Right", ConsoleColor.Green);
			ConsoleKey Right = Console.ReadKey().Key;
			MyConsole.Color("Type S to start", ConsoleColor.Green);
			cki = Console.ReadKey();
			int keyEntered = 1;
			int forceMoveDown = 5;
			int score = 0;
			Console.Clear();
			board.Print();
			// for testing clear line
			for (int i = 0; i < board.Width - 1; i++)
			{
				board.Data[i, 19] = 1;
			}
			for (int i = 0; i < board.Width - 1; i++)
			{
				board.Data[i, 18] = 1;
			}
			for (; ; )
			{
				if (Console.KeyAvailable)
				{
					cki = Console.ReadKey(true);
					if (cki.Key == Left)
					{
						moveXAxis = -1;
					}
					else if (cki.Key == Right)
					{
						moveXAxis = 1;
					}
				}
				//Console.WriteLine(cki.Key.ToString());

				// Rest of your game logic goes here
				// ...

				// Example: Simulating game loop delay
				//System.Threading.Thread.Sleep(100);
			if (blockShoudMove < 0)
				{
					Console.Clear();
					if (blockY <= maxBoardY - 2 && board.Data[blockX, blockY + 1] == 0)
					{
						if ((blockX + moveXAxis) >= 0 && (blockX + moveXAxis) < maxBoardX)
						{
							board.Data[blockX, blockY] = 0;
							blockX = blockX + moveXAxis;
							if (forceMoveDown == 0 || moveXAxis == 0)
							{
								blockY++;
								score++;
								forceMoveDown = 5;
							}
							else
							{
								forceMoveDown--;
							}
							board.Data[blockX, blockY] = 1;
						}
						else
						{
							board.Data[blockX, blockY] = 0;
							blockY++;
							board.Data[blockX, blockY] = 1;
						}
					}
					else
					{
						blockX = 4;
						blockY = 0;
						board.Data[blockX, blockY] = 1;
						board.CanClearLine();
					}
					board.Print();
					moveXAxis = 0;
					blockShoudMove = blockShoudMoveDef;
					if (blockShoudMoveDef > 5) // makes the game gradualy faster
					{
						blockShoudMoveDef = blockShoudMoveDef - 0.01f;
					}
					//keyEnterd = 0;
				}
				else
				{
					//MyConsole.Color("1", ConsoleColor.Red);
					Thread.Sleep(50);
					blockShoudMove--;
				}
				//Console.ReadKey(true);
			}
			Console.ReadKey();
		}
	}
}
