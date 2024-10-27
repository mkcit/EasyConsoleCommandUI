

namespace CLConsoleCommandUI
{

	/// <summary>
	/// Represents a command in the menu shown on the console.
	/// </summary>
	public class Command
	{
		
		private string text;
		internal int Length { get; set; }
		internal string Text
		{
			get { return text; }
			set
			{
				Length = value.Length;
				text = value;
			}
		}

		/// <summary>
		/// The color of a command.
		/// </summary>
		public ConsoleColor ForegroundColor { get; set; }

		/// <summary>
		/// The background color of a command when it should be activated.
		/// </summary>
		public ConsoleColor HightlightingColor { get; set; }

		/// <summary>
		/// The background color style over a command, it should cover full row or just the command length.
		/// </summary>
		public SelectionStyle SStyle { get; set; }

		/// <summary>
		/// A command will be shown in Left, Center or Right location of console screen.
		/// </summary>
		public TextAlignment TAlignment { get; set; }

		/// <summary>
		/// A command which be activated, it will be hightlighting.
		/// </summary>
		public bool Activated { get; set; } = false;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text">Represent the text of command</param>
		/// <param name="foregroundColor">Represent the font color of text</param>
		/// <param name="hightlightingColor">Represent the background color of activated command</param>
		/// <param name="selectionStyle"></param>
		/// <param name="textAlignment">Represent the location of a command, Left, Right or Center.</param>
		public Command(string text,
			ConsoleColor foregroundColor = ConsoleColor.White,
			ConsoleColor hightlightingColor = ConsoleColor.Green,
			SelectionStyle selectionStyle = SelectionStyle.FULL,
			TextAlignment textAlignment = TextAlignment.LEFT)
		{
			Text = this.text = text;
			ForegroundColor = foregroundColor;
			HightlightingColor = hightlightingColor;
			TAlignment = textAlignment;
			SStyle = selectionStyle;
			Length = text.Length;

		}

		/// <summary>
		/// Converts the current Command instance to its equivalent string.
		/// </summary>
		/// <returns>The text representation of the current instance.</returns>
		public override string ToString()
		{
			return Text;
		}

		public enum SelectionStyle
		{
			DEFAULT,
			FULL
		}

		public enum TextAlignment
		{
			LEFT,
			RIGHT,
			CENTER
		}
	}
}
