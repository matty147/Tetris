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
			this.Data = new int[width,height];
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
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			//int[,] board;
			float blockShoudMove = 5; // This will determin the game speed
			float blockShoudMoveDef = blockShoudMove; 
			Board board = new Board(10, 20);
			int blockX = 4;
			int blockY = 0;
			board.Data[blockX, blockY] = 1;
			board.Data[4,15] = 0;
			board.Print();
			int moveXAxis = 0;
			Console.TreatControlCAsInput = true;
			ConsoleKeyInfo cki;
			MyConsole.Color("Type S to start" , ConsoleColor.Green);
			cki = Console.ReadKey();
			int keyEnterd = 1;
			for (; ; )
			{
				if (keyEnterd == 0)
				{
					cki = Console.ReadKey();
					keyEnterd = 1;
					if (cki.ToString() == "A") //this dosen't work :/
					{
						Console.WriteLine("Here");
						moveXAxis = -1;
					}else if (cki.ToString() == "D")
					{
						moveXAxis = 1;
					}
				}
				Console.WriteLine(cki.Key.ToString());
				if (blockShoudMove < 0)
				{
					Console.Clear();
					if (blockY <= 18 && board.Data[blockX, blockY + 1] == 0)
					{
						board.Data[blockX, blockY] = 0;
						blockX = blockX + moveXAxis;
						Console.WriteLine(blockX);
						Console.WriteLine(moveXAxis);
						blockY++;
						board.Data[blockX, blockY] = 1;
					}
					else
					{
						blockX = 4;
						blockY = 0;
						board.Data[blockX, blockY] = 1;
					}
					board.Print();
					moveXAxis = 0;
					blockShoudMove = blockShoudMoveDef;
					if (blockShoudMoveDef > 5) // makes the game faster
					{
						blockShoudMoveDef = blockShoudMoveDef - 0.01f;
					}
					keyEnterd = 0;
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
