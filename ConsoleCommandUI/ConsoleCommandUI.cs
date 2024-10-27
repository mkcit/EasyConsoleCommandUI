
namespace CLConsoleCommandUI
{
	/// <summary>
	/// 
	/// </summary>
	public class ConsoleCommandUI
	{
		public delegate void ConsoleCommandUIHandler(ConsoleCommandUI source, ConsoleKey key, int commandIndex);
		
		/// <summary>
		/// 
		/// </summary>
		public event ConsoleCommandUIHandler CommandPressed;
		/****************************************************************************/
		private int left;
		private int top;
		private int padding;
		private readonly int maxCommandLength;
		/****************************************************************************/
		private int Left
		{
			get
			{
				return left;
			}
			set
			{
				if (value < 0 || value > Console.WindowWidth) left = 0;
				else
					left = value;
			}
		}
		private int Top
		{
			get
			{
				return top;
			}
			set
			{
				if (value < 0 || value > Console.WindowHeight)
					top = 0;
				else
					top = value;
			}
		}
		private BorderStatus BStatus { get; }
		private int Padding
		{
			get { return padding; }
			set { if (value <= 0) padding = 1; else padding = value; }
		}
		/****************************************************************************/
		/// <summary>
		/// 
		/// </summary>
		public Command[] Commands { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public char TopLeftCharacter { get; set; } = '╔';
		/// <summary>
		/// 
		/// </summary>
		public char TopRightCharacter { get; set; } = '╗';
		/// <summary>
		/// 
		/// </summary>
		public char ButtomLeftCharacter { get; set; } = '╚';
		/// <summary>
		/// 
		/// </summary>
		public char ButtomRightCharacter { get; set; } = '╝';
		/// <summary>
		/// 
		/// </summary>
		public char TopHorzintalCharacter { get; set; } = '═';
		/// <summary>
		/// 
		/// </summary>
		public char ButtomHorzintalCharacter { get; set; } = '═';
		/// <summary>
		/// 
		/// </summary>
		public char LeftVerticalCharacter { get; set; } = '║';
		/// <summary>
		/// 
		/// </summary>
		public char RightVerticalCharacter { get; set; } = '║';
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor TopLeftCharacterColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor TopRightCharacterColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor ButtomLeftCharacterColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor ButtomRightCharacterColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor TopHorzintalLineColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor ButtomHorzintalLineColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor RightVeritcalLineColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleColor LeftVeritcalLineColor { get; set; } = ConsoleColor.White;
		/// <summary>
		/// 
		/// </summary>
		public bool End { get; set; } = false;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleKey MovingUpKey { get; set; } = ConsoleKey.UpArrow;
		/// <summary>
		/// 
		/// </summary>
		public ConsoleKey MovingDownKey { get; set; } = ConsoleKey.DownArrow;
		/****************************************************************************/
		public enum BorderStatus
		{
			HIDDEN,
			VISIBLE
		}
		/****************************************************************************/
		public enum Alignment
		{
			LEFT,
			CENTER,
			RIGHT,
			CUSTOMIZED
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="commands"></param>
		/// <param name="left" ></param>
		/// <param name="top"></param>
		/// <param name="padding"></param>
		/// <param name="alignment"></param>
		/// <param name="borderStatus"></param>
		/// <exception cref="ArgumentException"></exception>
		public ConsoleCommandUI(Command[]commands, int left = 0, int top = 0, int padding = 1, Alignment alignment = Alignment.CENTER, BorderStatus borderStatus = BorderStatus.VISIBLE)
		{
			Console.CursorVisible = false;

			if (commands is null || commands.Length == 0 || AnyItemIsNull(commands))
				throw new ArgumentException($"{nameof(commands)} must not be null or empty.");

			maxCommandLength = GetMaxCommandLength(commands);


			Padding = padding;
			BStatus = borderStatus;
			Top = top;

			// If is Left
			if (alignment == Alignment.LEFT)
				Left = 0;
			// If is Middle
			else if (alignment == Alignment.CENTER)
				Left = (Console.WindowWidth - maxCommandLength) / 2 - Padding/* * 2*/;
			// If is Right
			else if (alignment == Alignment.RIGHT)
				Left = Console.WindowWidth - (maxCommandLength + Padding * 2);
			else
				Left = left;



			if (commands.Length + Padding * 2/*2*/ + Top > Console.WindowHeight)
				throw new ArgumentException(
					$"Error: based on parameters, {nameof(Padding)}, {nameof(Top)} and Number of {nameof(commands)}, the total value is greater than Window Height");
					

			if (maxCommandLength + Padding * 2 /*2*/ + Left > Console.WindowWidth)
				throw new ArgumentException(
					$"Error: based on parameters, {nameof(Padding)}, {nameof(Left)} and Max Command Length, the total value is greater than Window Width");

			Commands = commands;

			Select(0);
		}
		public void Select(int index)
		{
			DeSelectAllCommands();
			Commands[index].Activated = true;
		}
		/****************************************************************************/
		/// <summary>
		/// 
		/// </summary>
		public void ShowOnScreen()
		{
			FormatScreen();

			int index = 0;
			while (!End)
			{
				Show();
				ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
				if (consoleKeyInfo.Key == MovingDownKey)
				{
					if (++index >= Commands.Length)
						index = Commands.Length - 1;
					Select(index);
				}
				else if (consoleKeyInfo.Key == MovingUpKey)
				{
					if (--index <= 0)
						index = 0;
					Select(index);
				}
				else
				{
					if (CommandPressed != null)
						CommandPressed(this, consoleKeyInfo.Key, GetSelectedCommandIndex());
				}
			}

		}
		/****************************************************************************/
		private void DeSelectAllCommands()
		{
			foreach (var cmd in Commands)
				cmd.Activated = false;
		}
		/****************************************************************************/
		private void Show()
		{
			DrawBorder();
			Console.ResetColor();

			PrintCommands();
			Console.ResetColor();

			Console.WriteLine();
		}
		/****************************************************************************/
		private void DrawBorder()
		{
			if (BStatus == BorderStatus.VISIBLE)
			{
				int y1 = Top;
				int x1 = Left;
				int x2 = Left + maxCommandLength + Padding * 2 - 1;
				int y2 = Top + Commands.Length + Padding * 2 - 1;

				Console.CursorLeft = x1;// left - Border.Padding;
				Console.CursorTop = y1;//top;
				Console.ForegroundColor = TopLeftCharacterColor;
				Console.Write(TopLeftCharacter);

				Console.CursorLeft = x2;
				Console.CursorTop = y1;
				Console.ForegroundColor = TopRightCharacterColor;
				Console.Write(TopRightCharacter);

				Console.CursorLeft = x1;
				Console.CursorTop = y2;
				Console.ForegroundColor = ButtomLeftCharacterColor;
				Console.Write(ButtomLeftCharacter);

				Console.CursorLeft = x2;
				Console.CursorTop = y2;
				Console.ForegroundColor = ButtomRightCharacterColor;
				Console.Write(ButtomRightCharacter);


				for (int i = x1 + 1; i < x2; i++)
				{
					// Top Horz Line
					Console.CursorLeft = i;
					Console.CursorTop = y1;
					Console.ForegroundColor = TopHorzintalLineColor;
					Console.Write(TopHorzintalCharacter);

					Console.CursorLeft = i;
					Console.CursorTop = y2;
					Console.ForegroundColor = ButtomHorzintalLineColor;
					Console.Write(ButtomHorzintalCharacter);
				}

				for (int i = y1 + 1; i < y2; i++)
				{
					// Left Ver Line

					Console.CursorTop = i;
					Console.CursorLeft = x1;
					Console.ForegroundColor = LeftVeritcalLineColor;
					Console.Write(LeftVerticalCharacter);


					Console.CursorTop = i;
					Console.CursorLeft = x2;
					Console.ForegroundColor = RightVeritcalLineColor;
					Console.Write(RightVerticalCharacter);
				}

			}
		}
		/****************************************************************************/
		private void PrintCommands()
		{
			//ConsoleColor bgColor = Console.BackgroundColor;
			int counter = 0;
			foreach (var command in Commands)
			{
				if (command.TAlignment == Command.TextAlignment.LEFT)
				{
					if (command.SStyle == Command.SelectionStyle.FULL)
						command.Text = command.Text.PadRight(maxCommandLength, ' ');

					Console.CursorLeft = Left + Padding;
				}
				else if (command.TAlignment == Command.TextAlignment.CENTER)
				{
					if (command.SStyle == Command.SelectionStyle.FULL)
					{
						command.Text = command.Text.
							PadLeft((maxCommandLength - command.Length) / 2 + command.Length, ' ');
						command.Text = command.Text.
							PadRight(maxCommandLength, ' ');
					}

					Console.CursorLeft = (maxCommandLength + padding * 2 - command.Length) / 2 + Left;
				}
				else
				{

					if (command.SStyle == Command.SelectionStyle.FULL)
						command.Text = command.Text.PadLeft(maxCommandLength, ' ');

					Console.CursorLeft = Left + maxCommandLength + padding - command.Length;


				}

				if (command.Activated)
				{
					Console.ForegroundColor = command.ForegroundColor;
					Console.BackgroundColor = command.HightlightingColor;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.BackgroundColor = ConsoleColor.Black;
				}


				Console.CursorTop = Top + counter + Padding;
				Console.Write(command);
				counter++;
			}
		}
		/****************************************************************************/
		private static void FormatScreen()
		{
			Console.Clear();
			Console.CursorLeft = 0;
			Console.CursorTop = 0;
			Console.ResetColor();
		}
		/****************************************************************************/
		private int GetSelectedCommandIndex()
		{
			for (int i = 0; i < Commands.Length; ++i)
			{
				if (Commands[i].Activated) return i;
			}

			return -1;
		}
		/****************************************************************************/
		private int GetMaxCommandLength(Command[] commands)
		{
			//if(commands is null) throw new ArgumentNullException(nameof(commands));

			int maxLength = int.MinValue;
			foreach (var command in commands)
			{
				if (maxLength < command.Length)
					maxLength = command.Length;
			}

			return maxLength;
		}
		/****************************************************************************/
		private bool AnyItemIsNull(Command[] commands)
		{
			foreach (Command command in commands)
			{
				if (command is null) return true;
			}
			return false;
		}
	}
}
