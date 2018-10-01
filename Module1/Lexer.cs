using System;
using System.Collections.Generic;


namespace CompLess1
{
	public class LexerException : System.Exception
	{
		public LexerException(string msg) : base(msg)
		{
		}

	}

	public class Lexer
	{

		protected int position;
		protected char currentCh;
		protected System.IO.StringReader inputReader;
		protected string inputString;

		public Lexer(string input)
		{
			inputReader = new System.IO.StringReader(input);
			inputString = input;
		}

		public void Error()
		{
			System.Text.StringBuilder o = new System.Text.StringBuilder();
			// TODO: print ^ shifted to character #<position>
			o.AppendFormat("^{0}  ", position);
			o.AppendFormat("Error in symbol: {0}", currentCh);
			throw new LexerException(o.ToString());
		}

		protected void NextCh()
		{
			this.currentCh = (char)this.inputReader.Read();
			this.position += 1;
		}

		public virtual void Parse()
		{

		}
	}

	public class IntLexer : Lexer
	{

		protected System.Text.StringBuilder intString;

		public IntLexer(string input) : base(input)
		{
			intString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			string countString = "";
			NextCh();
			if (currentCh == '+' || currentCh == '-')
			{
				if (currentCh == '-')
					countString += '-';
				NextCh();
			}

			if (char.IsDigit(currentCh))
			{
				countString += currentCh;
				NextCh();
			}
			else
			{
				Error();
			}

			while (char.IsDigit(currentCh))
			{
				countString += currentCh;
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}

			int count = Int32.Parse(countString);

			System.Console.WriteLine("Interger are recognized " + count);

		}
	}

	public class IDLexer : Lexer
	{

		protected System.Text.StringBuilder idString;

		public IDLexer(string input) : base(input)
		{
			idString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			NextCh();
			if (char.IsLetter(currentCh))
			{
				NextCh();
			}
			else
				Error();


			while (char.IsLetter(currentCh) || char.IsDigit(currentCh))
			{
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}

			System.Console.WriteLine("ID are recognized");

		}
	}

	public class IntLexerNotZero : Lexer
	{

		protected System.Text.StringBuilder intString;

		public IntLexerNotZero(string input) : base(input)
		{
			intString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			NextCh();
			if (currentCh == '+' || currentCh == '-')
			{
				NextCh();
			}

			if (char.IsDigit(currentCh) && currentCh != '0')
			{
				NextCh();
			}
			else
			{
				Error();
			}

			while (char.IsDigit(currentCh))
			{
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}


			System.Console.WriteLine("Interger With Sign Not Zero are recognized ");

		}
	}

	public class StartLetterLexer : Lexer
	{

		protected System.Text.StringBuilder idString;

		public StartLetterLexer(string input) : base(input)
		{
			idString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			NextCh();
			if (char.IsLetter(currentCh))
			{
				NextCh();
			}
			else
				Error();

			int state = 1;

			while (true)
			{
				if (state == 1)
				{
					if (!char.IsDigit(currentCh))
						break;
					state = 2;
				}
				else
				{
					if (state == 2)
					{
						if (!char.IsLetter(currentCh))
							break;
						state = 1;
					}
				}
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}

			System.Console.WriteLine("Letters and Digits are recognized");

		}
	}

	public class ListLetterLexer : Lexer
	{

		protected System.Text.StringBuilder idString;

		public ListLetterLexer(string input) : base(input)
		{
			idString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			List<char> list = new List<char>() { };
			NextCh();
			if (char.IsLetter(currentCh))
			{
				list.Add(currentCh);
				NextCh();
			}
			else
				Error();

			int state = 1;

			while (true)
			{
				if (state == 1)
				{
					if (!(currentCh == ',' || currentCh == ';'))
						break;
					state = 2;			
				}
				else if (state == 2)
				{
					if (!char.IsLetter(currentCh))
						break;
                    list.Add(currentCh);
                    state = 1;
				}		
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}

			System.Console.WriteLine("List of Letters are recognized:");
			foreach (char c in list)
			{		
				System.Console.Write(c + " ");
			}
			System.Console.Write("\r\n");
		}
	}

	//extra 1
	public class ListDigitLexer : Lexer
	{

		protected System.Text.StringBuilder idString;

		public ListDigitLexer(string input) : base(input)
		{
			idString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			List<char> list = new List<char>() { };
			NextCh();
			if (char.IsDigit(currentCh))
			{
				list.Add(currentCh);
				NextCh();
			}
			else
				Error();

			int state = 1;

			while (true)
			{
				if (state == 1)
				{
					if (currentCh == ' ')
						state = 2;
					else
						break;
				}
				else
				{
					if (state == 2)
					{
						if (currentCh == ' ')
							state = 2;
						else
							state = 3;
					}
					if (state == 3)
					{
						if (char.IsDigit(currentCh))
						{
							list.Add(currentCh);
							state = 1;
						}
						else
							break;
					}
				}	
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}

			System.Console.WriteLine("List of Digitss are recognized:");
			foreach (char c in list)
			{
				System.Console.Write(c + " ");
			}
			System.Console.Write("\r\n");
		}
	}

	//extra 2
	public class DoubleGroupLexer : Lexer
	{

		protected System.Text.StringBuilder idString;

		public DoubleGroupLexer(string input) : base(input)
		{
			idString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			string rec = "";
			NextCh();

			int state = 1;

			while (true)
			{
				if (state == 1)
				{
					if (char.IsLetter(currentCh))
						state = 2;
					else
						break;
				}
				else
				{
					if (state == 2)
					{
						if (char.IsLetter(currentCh))
							state = 3;
						else
						{
							state = 3;
							continue;
						}
					}
					else
					{
						if (state == 3)
						{
							if (char.IsDigit(currentCh))
							{
								state = 4;
							}
							else
								break;
						}
						else
						{
							if (state == 4)
							{
								if (char.IsDigit(currentCh))
								{
									state = 1;
								}
								else
								{
									state = 1;
									continue;
								}
							}
						}
					}
				}
				rec += currentCh;
				NextCh();
			}

			if (currentCh != '\n')
			{
				Error();
			}

			System.Console.WriteLine("Double Group are recognized:");
			System.Console.Write(rec);
			System.Console.Write("\r\n");
		}
	}


	//extra 3
	public class DoubleWithPointLexer : Lexer
	{

		protected System.Text.StringBuilder idString;

		public DoubleWithPointLexer(string input) : base(input)
		{
			idString = new System.Text.StringBuilder();
		}

		public override void Parse()
		{
			NextCh();
			if (char.IsDigit(currentCh))
			{
				NextCh();
			}
			else
				Error();

			int state = 1;

			while (true)
			{
				if (state == 1)
				{
                    if (char.IsDigit(currentCh))
                    {
                        state = 1;
                        NextCh();
                    }
                    else
                    {
                        state = 2;
                        continue;
                    }
				}
				else
				{
					if (state == 2)
					{
                        if (currentCh == '.')
                        {
                            state = 3;
                            NextCh();
                        }
                        else
                            break;
					}
					else
					{
						if (state == 3)
						{
                            if (char.IsDigit(currentCh))
                            {
                                state = 4;
                                NextCh();
                            }
                            else
                                Error();
                        }
                        else if (state == 4)
                        {
                            if (char.IsDigit(currentCh))
                            {
                                state = 4;
                                NextCh();
                            }
                            else
                                break;
                        }
                    }
				}

			}

			if (currentCh != '\n')
			{
				Error();
			}

			System.Console.WriteLine("Double With Point are recognized:");
		}
	}

    //extra4   
    public class StringInApostrophes : Lexer
    {

        protected System.Text.StringBuilder idString;

        public StringInApostrophes(string input) : base(input)
        {
            idString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();
            if (currentCh == (char)39)
            {
                NextCh();
            }
            else
                Error();
            while (true)
            {
                if (currentCh == (char)39)
                    break;
                NextCh();
            }
            NextCh();
            if (currentCh != '\n')
            {
                Error();
            }

            System.Console.WriteLine("String In Apostrophesas are recognized:");
        }
    }

    //extra5
    public class StringInComments : Lexer
    {

        protected System.Text.StringBuilder idString;

        public StringInComments(string input) : base(input)
        {
            idString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();
            if (currentCh == '/')
            {
                NextCh();
            }
            else
                Error();
            if (currentCh == '*')
            {
                NextCh();
            }
            else
                Error();
            while (true)
            {
                if (currentCh == '*')
                {
                    NextCh();
                    if (currentCh == '/')
                        break;
                }
                NextCh();
            }
            NextCh();
            if (currentCh != '\n')
            {
                Error();
            }

            System.Console.WriteLine("String In Comments are recognized:");
        }
    }

    //extra extra
    public class IDLexerMany : Lexer
    {

        protected System.Text.StringBuilder idString;

        public IDLexerMany(string input) : base(input)
        {
            idString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            while (true)
            {
                NextCh();
                if (char.IsLetter(currentCh))
                {
                    NextCh();
                }
                else
                    Error();


                while (char.IsLetter(currentCh) || char.IsDigit(currentCh))
                {
                    NextCh();
                }
                if (currentCh != '.')
                    break;
            }

            if (currentCh != '\n')
            {
                Error();
            }

            System.Console.WriteLine("Many ID are recognized");

        }
    }


    public class Program
	{
		public static void Main()
		{
			string input = "-154216\n";
			Lexer L = new IntLexer(input);
			try
			{
				L.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

			input = "f12Fsdasda125623\n";
			Lexer L2 = new IDLexer(input);
			try
			{
				L2.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

			input = "+4568\n";
			Lexer L3 = new IntLexerNotZero(input);
			try
			{
				L3.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

			input = "a1j6A8\n";
			Lexer L4 = new StartLetterLexer(input);
			try
			{
				L4.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

			input = "a,J;a\n";
			Lexer L5 = new ListLetterLexer(input);
			try
			{
				L5.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

			input = "0  1 2  5 8     9   \n";
			Lexer L6 = new ListDigitLexer(input);
			try
			{
				L6.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

			input = "aa12c23Dd1\n";
			Lexer L7 = new DoubleGroupLexer(input);
			try
			{
				L7.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}


			input = "123.45678\n";
            //input = "123.\n";
            Lexer L8 = new DoubleWithPointLexer(input);
			try
			{
				L8.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}

            input = "'строка'\n";
            Lexer L9 = new StringInApostrophes(input);
            try
            {
                L9.Parse();
            }
            catch (LexerException e)
            {
                System.Console.WriteLine(e.Message);
            }

            input = "/*стр45ока*/\n";
            //input = "/*55*/\n";
            //input = "/**/\n";
            Lexer L10 = new StringInComments(input);
            try
            {
                L10.Parse();
            }
            catch (LexerException e)
            {
                System.Console.WriteLine(e.Message);
            }

            input = "id1.k1l.hjk.d1111\n";
            Lexer L11 = new IDLexerMany(input);
            try
            {
                L11.Parse();
            }
            catch (LexerException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
	}
}
