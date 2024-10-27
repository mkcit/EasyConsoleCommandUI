namespace ConsoleCommandUIApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			CLConsoleCommandUI.Command[] commands = {
				new CLConsoleCommandUI.Command("SUM TWO VALUES [+]"),
				new CLConsoleCommandUI.Command("SUB TWO VALUES [-]"),
				new CLConsoleCommandUI.Command("MUL TWO VALUES [×]"),
				new CLConsoleCommandUI.Command("DIV TOW VALUES [/]")
			};


		}
	}
}
