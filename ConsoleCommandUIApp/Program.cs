using CLConsoleCommandUI;
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

		private static void ConsoleCommandUI_CommandPressed(
			ConsoleCommandUI source, ConsoleKey key, int commandIndex)
		{
			if (key == ConsoleKey.Escape || commandIndex == source.Commands.Length - 1)
			{
				source.End = true;
			}
			else if (key == ConsoleKey.Enter)
			{
				switch (commandIndex)
				{
					case 0:
						AddOpr();
						break;
					case 1:
						SubOpr();
						break;
					case 2:
						MulOpr();
						break;
					case 3:
						DivOpr();
						break;
				}
			}
		}

		private static void DivOpr()
		{
			throw new NotImplementedException();
		}

		private static void MulOpr()
		{
			throw new NotImplementedException();
		}

		private static void SubOpr()
		{
			throw new NotImplementedException();
		}

		private static void AddOpr()
		{
			throw new NotImplementedException();
		}
	}
}
