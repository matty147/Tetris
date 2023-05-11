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
		static int movePiece(int blockPosition, int place)
		{
			int result = (blockPosition / (int)Math.Pow(10, place - 1)) % 10; // gets the x position of the block

			return result;
		}
		static void Main(string[] args)
		{
			//int[,] board;
			float blockShoudMove = 5; // This will determin the game speed
			float blockShoudMoveDef = blockShoudMove; 
			Board board = new Board(10, 20);
			int blockPosition = 04; // fisrt number is a x and the second is y 
			int result1;
			int result2;
			result1 = movePiece(blockPosition, 1);
			result2 = movePiece(blockPosition, 2);
			board.Data[result1, result2] = 1;
			board.Print();
			for (; ; )
			{
				if (blockShoudMove < 0)
				{
					Console.Clear();
					result1 = movePiece(blockPosition, 1);
					result2 = movePiece(blockPosition, 2);
					board.Data[result1, result2] = 0;
					blockPosition = blockPosition + 10;
					result1 = movePiece(blockPosition, 1);
					result2 = movePiece(blockPosition, 2);
					board.Data[result1, result2] = 1;
					board.Print();
					blockShoudMove = blockShoudMoveDef;				
					if (blockShoudMoveDef > 5)
					{
						blockShoudMoveDef = blockShoudMoveDef - 0.01f;
					}
				}
				else
				{
					//MyConsole.Color("1", ConsoleColor.Red);
					Thread.Sleep(50);
					blockShoudMove--;
				}
			}
			Console.ReadKey();
		}
	}
}
