using System.Text;

class Program
{
	static string sigma = "\u03C3";
	static void Main()
	{
		Console.OutputEncoding = Encoding.UTF8;
		Console.Write("Введите коэффициент первого слагаемого гамильтониана: ");
		string a = Console.ReadLine()!;
		Console.Write("Введите индексы первого оператора Паули: ");
		string sigma_1 = Console.ReadLine()!;
		Console.Write("Введите коэффициент второго слагаемого гамильтониана: ");
		string b = Console.ReadLine()!;
		Console.Write("Введите индексы второго оператора Паули: ");
		string sigma_2 = Console.ReadLine()!;
		Console.Write("Введите коэффициент третьего слагаемого гамильтониана: ");
		string c = Console.ReadLine()!;
		Console.Write("Введите индексы третьего оператора Паули: ");
		string sigma_3 = Console.ReadLine()!;
		Console.WriteLine($"Введённый вами гамильтониан: H = {a}*{sigma}_{sigma_1} + {b}*{sigma}_{sigma_2} + {c}*{sigma}_{sigma_3}");
		string[] sigma_array = Hamiltonian.countingOperators(sigma_1, sigma_2, sigma_3);

		//sigma_12, sigma_23, sigma_123
		Console.WriteLine($"{sigma}_AB = {sigma_array[0]}, {sigma}_BC = {sigma_array[1]}, {sigma}_ABC = {sigma_array[2]}");

		Console.WriteLine($"H^2 = {Hamiltonian.calculateSecondDegreeOfHamiltonian(Convert.ToDouble(a), Convert.ToDouble(b), sigma_array)}");
		Console.WriteLine($"H^3 = {Hamiltonian.calculateThirdDegreeOfHamiltonian(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(c), sigma_1, sigma_2,sigma_array)}");
	}
	class Calculation
	{
		public static string PauliMatrices(string K, string L)
		{
			char[] K1 = K.ToCharArray();
			char[] L1 = L.ToCharArray();
			string M = "";
			int a, b, c;
			if (K1.Length == L1.Length)
			{
				for (int i = 0; i < K1.Length; i++)
				{
					a = K1[i] - '0';
					b = L1[i] - '0';
					c = Calculation.Operations(a, b);
					M = M + Convert.ToString(c);
				}
				return M;
			}
			else
			{
				Console.WriteLine("Ошибка размерности кубитов двух операторов");
				return "";
			};
		}
		public static int Operations(int a, int b)
		{
			int c;
			if (a == b) c = 0;
			else if (a == 0) c = b;
			else if (b == 0) c = a;
			else c = 6 / (a * b);
			return c;
		}
		public static string Factors(string K, string L)
		{
			char[] K1 = K.ToCharArray();
			char[] L1 = L.ToCharArray();
			int a, b, skl, p = 0, m = 0;
			string w = "";
			if (K1.Length == L1.Length)
			{
				for (int i = 0; i < K1.Length; i++)
				{
					a = K1[i] - '0';
					b = L1[i] - '0';
					if (a - b == 1 || a - b == -2) m++;
					if (b - a == 1 || b - a == -2) p++;
					p = p + m;
				}
				if (p % 4 == 0 || p % 4 == 1)
				{
					skl = Convert.ToInt32(Math.Pow(-1, m));
					if (skl == 1) w = "";
					else w = "-1";
				}
				if (p % 4 == 2 || p % 4 == 3)
				{
					skl = Convert.ToInt32(Math.Pow(-1, m + 1));
					if (skl == 1) w = "i";
					else w = "-i";
				}
			}
			return w;
		}
	}
	class Hamiltonian
	{
		public static string[] countingOperators(string sigma_1, string sigma_2, string sigma_3)
		{
			string sigma_12 = Calculation.Factors(sigma_1, sigma_2) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_1, sigma_2);
			string sigma_23 = Calculation.Factors(sigma_2, sigma_3) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_2, sigma_3);
			string sigma_12_indexes = sigma_12.Split("_")[sigma_12.Split("_").Length - 1];
			string sigma_123_draft = Calculation.Factors(sigma_12_indexes, sigma_3) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_12_indexes, sigma_3);
			string sigma_123;

			if (sigma_12.StartsWith("-1"))
			{
				if (sigma_123_draft.StartsWith("-i"))
				{
					sigma_123 = $"i*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else if (sigma_123_draft.StartsWith("i"))
				{
					sigma_123 = $"-i*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else if (sigma_123_draft.StartsWith("-1"))
				{
					sigma_123 = $"{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else
				{
					sigma_123 = $"-1*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
			}
			else if (sigma_12.StartsWith("-i"))
			{
				if (sigma_123_draft.StartsWith("-i"))
				{
					sigma_123 = $"-1*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else if (sigma_123_draft.StartsWith("i"))
				{
					sigma_123 = $"{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else if (sigma_123_draft.StartsWith("-1"))
				{
					sigma_123 = $"i*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else
				{
					sigma_123 = $"-i*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
			}
			else if (sigma_12.StartsWith("i"))
			{
				if (sigma_123_draft.StartsWith("-i"))
				{
					sigma_123 = $"{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else if (sigma_123_draft.StartsWith("i"))
				{
					sigma_123 = $"-1*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else if (sigma_123_draft.StartsWith("-1"))
				{
					sigma_123 = $"-i*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
				else
				{
					sigma_123 = $"i*{sigma}_{sigma_123_draft.Split("_")[sigma_123_draft.Split("_").Length - 1]}";
				}
			}
			else 
			{
				sigma_123 = sigma_123_draft;
			}
			string[] sigma_array = new string[] { sigma_12, sigma_23, sigma_123 };
			return sigma_array;
		}

		public static string calculateSecondDegreeOfHamiltonian(double alpha, double beta, string[] arrayOfSigma)
		{
			return $"{sigma}_000 + {2 * alpha* beta}*{arrayOfSigma[0]}";
		}
		public static string calculateThirdDegreeOfHamiltonian(double alpha, double beta, double gamma, string sigma_1, string sigma_2, string[] arrayOfSigma)
		{
			return $"H + {2 * alpha * Math.Pow(beta, 2)}*{sigma_1}+{2 * Math.Pow(beta, 2) * beta}*{sigma_2} + {2 * alpha * beta * gamma}*{arrayOfSigma[2]}";
		}
	}
}
