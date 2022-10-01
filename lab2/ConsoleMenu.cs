using lab2.Menu.MenuCommand;
using lab2.PRNG;

namespace lab2
{
	public class ConsoleMenu
	{
		private int keyPos = 0;
		private int maxKeyPos = 2;
		private bool isFirstInput = true;

		private static MenuStorage _menu = new ProgrammCommand();
		private static List<string> _commands = _menu.GetMenus();

		private Generator generator = new Generator();

		private int FindKeyPos(ConsoleKeyInfo key)
		{
			if (key.Key == ConsoleKey.DownArrow)
			{
				if (keyPos < _commands.Count - 1)
				{
					keyPos++;
				}
				return 0;
			}
			if (key.Key == ConsoleKey.UpArrow)
			{
				if (keyPos > 0)
				{
					keyPos--;
				}
				return 0;
			}
			if (key.Key == ConsoleKey.Enter)
			{
				return 1;
			}
			return -1;
		}

		private void DrawMenu()
		{
			var menu = "--------------------------------------\n";
			for (int pos = 0; pos < _menu.GetMenus().Count; pos++)
			{
				string buffStr = "  ";
				if (pos == keyPos)
				{
					buffStr = "->";
				}
				buffStr += _commands[pos].ToString();
				menu += buffStr + "\n";
			}
			//Console.Clear();
			Console.WriteLine(menu);
		}

		private void SwitchingMenu(int pos)
		{
			keyPos = 0;
			var isSwitchMenu = false;
			if (!isSwitchMenu && _menu.GetType() == typeof(ProgrammCommand))
			{
				if(pos == 0)
				{
					_menu = new PRNGCommand();
				}
				if (pos == 1)
				{
					_menu = new FileCommand();
				}
				isSwitchMenu = true;
			}
			if (!isSwitchMenu && _menu.GetType() == typeof(PRNGCommand))
			{
				_menu = new ProgrammCommand();
				isSwitchMenu = true;
				isFirstInput = true;
			}
			if (!isSwitchMenu && _menu.GetType() == typeof(FileCommand))
			{
				_menu = new ProgrammCommand();
				isSwitchMenu = true;
				isFirstInput = true;
			}
			_commands = _menu.GetMenus();
			maxKeyPos = _menu.GetMenus().Count();
		}


		private void ApplicationCommand()
		{
			if (_menu.GetType() == typeof(ProgrammCommand))
			{
				ApplicationProgrammCommand();
			}
			if (_menu.GetType() == typeof(PRNGCommand))
			{
				ApplicationPRNGCommand();
			}
			if (_menu.GetType() == typeof(FileCommand))
			{
				ApplicationFileCommand();
			}
		}

		private void ApplicationProgrammCommand()
		{
			switch (keyPos)
			{
				case 0:
				case 1:
					SwitchingMenu(keyPos);
					break;
				case 2:
					System.Environment.Exit(0);
					break;
			}
		}

		private void ApplicationPRNGCommand()
		{
			if (isFirstInput)
			{
				isFirstInput = false;
				return;
			}

			switch (keyPos)
			{
				case 0:
					generator.GenerateParameters();
					var tuple = generator.GetParameters();
					Console.WriteLine($"a = {tuple.a} | b = {tuple.b} | c = {tuple.c}");
					break;
				case 1:
					Console.WriteLine(generator.Next());
					break;
				case 2:
					Console.WriteLine("Введите количество чисел в последовательности: ");
					var count = Convert.ToInt32(Console.ReadLine());
					BuildHistogramm(count);
					break;
				case 3:
					generator.WriteGeneratorParameters();
					break;
				case 4:
					SwitchingMenu(keyPos);
					break;
			}
		}

		private void BuildHistogramm(int countNumber)
        {
			var lNumber = new List<long>();
            for (int i = 0; i < countNumber; i++)
            {
				lNumber.Add(generator.Next());
            }

			string str = String.Join("\n", lNumber);
			File.WriteAllText(StaticData.PathHistogrammData, str);

			var histogramm = new Histogramm.Histogramm();
			histogramm.InitializeHistogram(lNumber);
			File.WriteAllText(StaticData.PathHistogrammCapture, histogramm.BuildHistorgramm());
        }

		private void CodeText(string inputFile, string outputFile, bool readKey)
		{
			if(File.ReadAllText(StaticData.PathKeyFale, System.Text.Encoding.UTF8) == null || File.ReadAllText(StaticData.PathKeyFale, System.Text.Encoding.UTF8).Length == 0)
            {
				generator.GenerateParameters();
            }

            if (readKey)
            {
				var lParams = File.ReadAllText(StaticData.PathKeyFale, System.Text.Encoding.UTF8)
								  .Split("\n")
								  .Select(x => Convert.ToInt64(x)).ToList<long>();
                generator.SetParameters(lParams[0], lParams[1], lParams[2]);
            }

			var str = File.ReadAllText(inputFile, System.Text.Encoding.UTF8);
			var codeStr = "";
			foreach (var symbol in str)
			{
				var num = generator.Next() % 65536;
				codeStr += Convert.ToChar(num ^ Convert.ToInt64(symbol));
			}

			File.WriteAllText(outputFile, codeStr, System.Text.Encoding.UTF8);
		}

		private void ApplicationFileCommand()
		{
			if (isFirstInput)
			{
				isFirstInput = false;
				return;
			}   

			switch (keyPos)
			{
				case 0:
					CodeText(StaticData.PathInputText, StaticData.PathEncodeText, false);
                    Console.WriteLine("Преобразование окончено");
					break;
				case 1:
					CodeText(StaticData.PathEncodeText, StaticData.PathDecodeText, true);
					Console.WriteLine("Преобразование окончено");
					break;
				case 2:
					SwitchingMenu(5);
					break;
			}   
		}

		public void CallMenu()
		{
			DrawMenu();
			while (true)
			{
				switch (FindKeyPos(Console.ReadKey(true)))
				{
					case 0:
						DrawMenu();
						break;
					case 1:
						ApplicationCommand();
						DrawMenu();
						break;
					case -1:
						break;
				}
				
			}
			
		}
	}
}
