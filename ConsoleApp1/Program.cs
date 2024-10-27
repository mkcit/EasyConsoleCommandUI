
using System.Diagnostics;

namespace ConsoleApp1
{

	internal class Program
	{
		static List<Student> students = new List<Student>();
		static void Main(string[] args)
		{
			try
			{

				CLConsoleCommandUI.Command[] cmds =
				[
					new("ADD NEW", textAlignment: CLConsoleCommandUI.Command.TextAlignment.CENTER,selectionStyle:CLConsoleCommandUI.Command.SelectionStyle.DEFAULT),
					new("REMOVE A STUDENT", textAlignment: CLConsoleCommandUI.Command.TextAlignment.CENTER),
					new("SEARCH FOR STUDENT", textAlignment: CLConsoleCommandUI.Command.TextAlignment.CENTER),
					new("RETURN TO MAIN", textAlignment: CLConsoleCommandUI.Command.TextAlignment.CENTER)
				];

				CLConsoleCommandUI.ConsoleCommandUI v = new(
					cmds,
					alignment: CLConsoleCommandUI.ConsoleCommandUI.Alignment.CENTER,
					top:5
				);


				v.ButtomHorzintalLineColor = ConsoleColor.Red;
				v.TopHorzintalLineColor = ConsoleColor.Green;
				v.RightVeritcalLineColor = ConsoleColor.Blue;
				v.LeftVeritcalLineColor = ConsoleColor.Yellow;

				v.CommandPressed += V_CommandPressed;

				v.ShowOnScreen();
			}
			catch (Exception ex) { Console.WriteLine(ex.Message); }

			
		}

		private static void V_CommandPressed(
			CLConsoleCommandUI.ConsoleCommandUI source, ConsoleKey key, int commandIndex)
		{
			if (key == ConsoleKey.Enter)
			{
				Console.Clear();
				switch (commandIndex)
				{
					case 0:
						ConsoleKeyInfo k;
						do
						{
							Console.WriteLine("Enter the name?");
							string name = Console.ReadLine();

							Console.WriteLine(name);


							Console.WriteLine("Enter the Identity?");
							string identity = Console.ReadLine();

							Console.WriteLine(identity);


							students.Add(new Student { ID = identity, Name = name });


							Console.WriteLine("Add Again yes/No ?");
							k = Console.ReadKey();
						}
						while (k.KeyChar != 'n' && k.KeyChar != 'N');

						Console.Clear();
						break;
					case 1:
						Console.WriteLine(source.Commands[commandIndex]);
						break;
					case 2:
						Console.WriteLine(source.Commands[commandIndex]);
						break;
					case 3:
						source.End = true;
						Console.WriteLine("Thanks for using My System");
						break;
				}
			}
		}

		private class Student
		{
			public string Name { get; set; }
			public string ID { get; set; }
		}

		private string GetDebuggerDisplay()
		{
			return ToString();
		}
	}
}
