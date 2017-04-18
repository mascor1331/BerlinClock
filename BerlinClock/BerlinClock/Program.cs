/*
  @author Mahesh Hariharasubramanian
  Email: mahesh.h@rutgers.edu
 
  This is a program which takes time input from the user and displays the output according to the Berlin Clock representaion
  
  For reference to Berlin Clock, please refer the following link:
  http://www.3quarks.com/en/BerlinClock/
 */
 
using System;

namespace BerlinClock
{
	//class for each indicator(lamp) which has a color as attribute
	class Lamp<String>
	{
		private string colour;
		//color of the Lamp

		public Lamp ()
		{
			this.colour = default(string);
		}

		public Lamp (string colour)
		{
			this.colour = colour;
		}

		public override string ToString ()
		{
			return string.Format ("{0}", colour);
		}

		public string Colour {
			get {
				return this.colour;
			}
			set {
				colour = value;
			}
		}
	}

	//class for each series of lamps which has a array of Lamps of varying sizes as attribute
	class LampSeries<String>
	{
		private Lamp<String>[] lampRow;
		//array of Lamps

		//constructor with number of lamps as parameter
		public LampSeries (int numberOfLamps, string offColor)
		{
			LampRow = new Lamp<String>[numberOfLamps];
			for (int i = 0; i < numberOfLamps; i++) {
				LampRow [i] = new Lamp<String> (offColor);
			}
		}

		public override string ToString ()
		{
			foreach (Lamp<String> c in lampRow) {
				Console.Write (c);
			}
			return string.Format ("");
		}

		public Lamp<String>[] LampRow {
			get {
				return this.lampRow;
			}
			set {
				lampRow = value;
			}
		}
	}
	//class for the BerlinClock which has 5 rows each of which is a LampSeries of varying sizes
	class BerlinClock<String>
	{
		private LampSeries<String>[] rows;
		//Rows of Berlin Clock
		private string offColor = "O";
		//Default color to indicate off lamp
		private string secondsIndicator = "Y";
		//Default color to indicate secons lamp of Row 1
		private string hoursIndicator = "R";
		//Default color to indicate hours lamp of Row 2 an Row 3
		private string minutesIndicator = "Y";
		//Default color to indicate minutes lamp of Row 4 an Row 5
		private string quarterIndicator = "R";
		//Default color to indicate quarter hours lamp of Row 4

		public LampSeries<String>[] Rows {
			get {
				return this.rows;
			}
			set {
				rows = value;
			}
		}

		public override string ToString ()
		{
			for (int i = 0; i < Rows.Length; i++) {
				Console.Write (Rows [i] + " ");
			}
			return null;
		}

		public string OffColor {
			get {
				return this.offColor;
			}
			set {
				offColor = value;
			}
		}

		public string SecondsIndicator {
			get {
				return this.secondsIndicator;
			}
			set {
				secondsIndicator = value;
			}
		}

		public string HoursIndicator {
			get {
				return this.hoursIndicator;
			}
			set {
				hoursIndicator = value;
			}
		}

		public string MinutesIndicator {
			get {
				return this.minutesIndicator;
			}
			set {
				minutesIndicator = value;
			}
		}

		public string QuarterIndicator {
			get {
				return this.quarterIndicator;
			}
			set {
				quarterIndicator = value;
			}
		}


		public BerlinClock (string offColor)
		{
			rows = new LampSeries<String>[5];
			for (int i = 0; i < 5; i++) {
				if (i == 0) // Row 1 --> Seconds indicator
					rows [i] = new LampSeries<String> (1, offColor); //1 Lamp
				else if (i == 3) // Row 4 --> Minutes X 5 indicator
					rows [i] = new LampSeries<String> (11, offColor); // 11 Lamps 
				else // Row 2,3,5 --> Hours X 5, Hours X 1 and Minutes X 1 indicator, 
					rows [i] = new LampSeries<String> (4, offColor); //4 Lampss
			}
		}

		public void buildClock (TimeReader t)
		{
			if (t.Seconds % 2 == 0) { // Row 1 --> Seconds indicator has to glow on even seconds
				this.Rows [0].LampRow [0].Colour = this.SecondsIndicator;
			}
			for (int i = 0; i < t.Hours / 5; i++) { // Row 2 --> Hours X 5 indicator
				this.Rows [1].LampRow [i].Colour = this.HoursIndicator;
			}
			for (int i = 0; i < t.Hours % 5; i++) { // Row 3 --> Hours X 1  indicator
				this.Rows [2].LampRow [i].Colour = this.HoursIndicator;
			}
			for (int i = 0; i < t.Minutes / 5; i++) { // Row 4 --> Minutes X 5 indicator
				this.Rows [3].LampRow [i].Colour = this.MinutesIndicator;
				if (((i + 1) * 5) % 15 == 0)
					this.Rows [3].LampRow [i].Colour = this.QuarterIndicator;
			}
			for (int i = 0; i < t.Minutes % 5; i++) { // Row 5 --> Minutes X 1 indicator
				this.Rows [4].LampRow [i].Colour = this.MinutesIndicator;
			}
		}
	}

	//helper class for getting the hours, minutes and seconds from the usee entered time
	class TimeReader
	{
		public int Hours {
			get {
				return this.hours;
			}
			set {
				hours = value;
			}
		}

		public int Minutes {
			get {
				return this.minutes;
			}
			set {
				minutes = value;
			}
		}

		public int Seconds {
			get {
				return this.seconds;
			}
			set {
				seconds = value;
			}
		}

		private int hours;
		private int minutes;
		private int seconds;

		private void Initialize (string s1, string s2)
		{
			try {
				this.hours = Convert.ToInt32 (s1);
				this.minutes = Convert.ToInt32 (s2);
				this.seconds = 0;
			} catch (Exception e) {
				Console.WriteLine (e.Message);
				throw new System.ArgumentException ("An error has occured parsing the time. Please check your input");
			}
		}

		private void Initialize (string s1, string s2, string s3)
		{
			try {
				this.hours = Convert.ToInt32 (s1);
				this.minutes = Convert.ToInt32 (s2);
				this.seconds = Convert.ToInt32 (s3);
			} catch (Exception e) {
				Console.WriteLine (e.Message);
				throw new System.ArgumentException ("An error has occured parsing the time. Please check your input");
			}
		}

		public void readTime (String time)
		{
			char[] delim = { ':' };
			String[] timeSplit = time.Split ((char[])delim, StringSplitOptions.RemoveEmptyEntries);
			if (timeSplit.Length == 2)
				Initialize (timeSplit [0], timeSplit [1]);
			if (timeSplit.Length == 3)
				Initialize (timeSplit [0], timeSplit [1], timeSplit [2]);
			else
				throw new System.ArgumentException ("An error has occured parsing the time. Please check your input");
		}
	}

	// Start of Main class
	class MainClass
	{
		public static void Main (string[] args)
		{
			String offIndicator = "O";

//			Customizations start
			Console.WriteLine ("Press Enter to continue with default colors.");
			Console.WriteLine ("Enter the color for off indicator. Default is O");
			string enteredOffInd = Console.ReadLine ();
			if (!(enteredOffInd == null || enteredOffInd == String.Empty))
				offIndicator = enteredOffInd;
			Console.WriteLine ("Enter the color for seconds indicator. Default is Y");
			string enteredSecInd = Console.ReadLine ();
			Console.WriteLine ("Enter the color for hours indicator. Default is R");
			string enteredHrInd = Console.ReadLine ();
			Console.WriteLine ("Enter the color for quarter hour indicator. Default is R");
			string enteredQtrInd = Console.ReadLine ();
			Console.WriteLine ("Enter the color for minutes indicator Default is Y");
			string enteredMinInd = Console.ReadLine ();
//			Customizations end

			BerlinClock<String> bc = new BerlinClock<String> (offIndicator);
			//check whether to apply custom colors or run with default colors
			if (!(enteredSecInd == null || enteredSecInd == String.Empty))
				bc.SecondsIndicator = enteredSecInd;
			if (!(enteredHrInd == null || enteredHrInd == String.Empty))
				bc.HoursIndicator = enteredHrInd;
			if (!(enteredQtrInd == null || enteredQtrInd == String.Empty))
				bc.QuarterIndicator = enteredQtrInd;
			if (!(enteredMinInd == null || enteredMinInd == String.Empty))
				bc.MinutesIndicator = enteredMinInd;

			//instantiate TimeReader to read user's time input
			TimeReader tr = new TimeReader ();
			Boolean isRead = false; //flag variable indicated read status
			while (isRead == false) { // run until user enters time in correct format
				try {
					Console.WriteLine ("Enter time e.g. 04/18/2017 12:00:00 PM, 11 am, 11:45 am, 5 pm, 11:59 PM");
					String enteredTime = Console.ReadLine ();
					DateTime timeValue = Convert.ToDateTime (enteredTime);
					Console.WriteLine ("Interpreted time in HH:mm:ss format is {0}", timeValue.ToString ("HH:mm:ss"));
					String dateTimeString = timeValue.ToString ("HH:mm:ss");
					tr.readTime (dateTimeString); //call to readTime function
					isRead = true; //break out of while loop
				} catch (FormatException e) {
					Console.WriteLine (e.Message);
					Console.WriteLine ("An error has occured parsing the time. Please check your input and try again");
					Console.WriteLine ();
					isRead = false;
				}
			}
			bc.buildClock (tr); //call buildClock function to build the BerlinClock
			Console.Write ("Output: ");
			Console.WriteLine (bc); // Display the output in a single line as required
		}
	}
}
