using CLConsoleCommandUI;

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
				new Command("DIV TOW VALUES [/]")
			};

			ConsoleCommandUI consoleCommandUI = 
				new ConsoleCommandUI(commands);

			consoleCommandUI.CommandPressed += ConsoleCommandUI_CommandPressed;

			consoleCommandUI.ShowOnScreen();
		}

		private static void ConsoleCommandUI_CommandPressed(
			ConsoleCommandUI source, ConsoleKey key, int commandIndex)
		{
			
		}
	}
}
