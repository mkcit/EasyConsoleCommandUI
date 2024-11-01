﻿using CLConsoleCommandUI;
using System.ComponentModel;

namespace ConsoleCommandUIApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Command[] commands = {
				new Command("SUM TWO VALUES [+]"),
				new Command("SUB TWO VALUES [-]"),
				new Command("MUL TWO VALUES [×]"),
				new Command("DIV TOW VALUES [/]"),
				new Command("END THE SYSTEM [ESC]")
			};

			ConsoleCommandUI consoleCommandUI =
				new ConsoleCommandUI(commands,
				padding: 3);

			consoleCommandUI.CommandPressed += ConsoleCommandUI_CommandPressed;
			consoleCommandUI.ShowOnScreen();
		}

		delegate string Calculate(double value1, double value2);
		private static void ConsoleCommandUI_CommandPressed(
			ConsoleCommandUI source, ConsoleKey key, int commandIndex)
		{

			if (key == ConsoleKey.Escape || 
				commandIndex == source.Commands.Length - 1)
			{
				source.End = true;
				Console.Clear();
			}
			else if (key == ConsoleKey.Enter)
			{
				switch (commandIndex)
				{
					case 0:
						MathOperation((x, y) => { return $"{x} + {y} = {x + y}"; });
						break;
					case 1:
						MathOperation((x, y) => { return $"{x} - {y} = {x - y}"; });
						break;
					case 2:
						MathOperation((x, y) => { return $"{x} * {y} = {x * y}"; });
						break;
					case 3:
						MathOperation((x, y) => { return $"{x} / {y} = {x / y}"; });
						break;
				}
			}
		}

		private static void MathOperation(Calculate calculate)
		{
			Console.WriteLine();

			Console.Clear();

			Console.Write("Enter the first number: ");
			double value1 = 0;
			bool success = double.TryParse(Console.ReadLine(), out double x1);
			if (success)
				value1 = x1;

			Console.Write("Enter the second number: ");
			double value2 = 0;
			success = double.TryParse(Console.ReadLine(), out double x2);
			if (success)
				value2 = x2;


			Console.WriteLine(calculate(value1, value2));

		}

	}
}
