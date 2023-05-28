using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
	static string sigma = "\u03C3";
	static void Main()
	{
		Console.OutputEncoding = Encoding.UTF8;
		Console.WriteLine("Введите коэффициент первого слагаемого гамильтониана");
		string a = Console.ReadLine()!;
		Console.WriteLine("Введите индексы первого оператора Паули");
		string sigma_1 = Console.ReadLine()!;
		Console.WriteLine("Введите коэффициент второго слагаемого гамильтониана");
		string b = Console.ReadLine()!;
		Console.WriteLine("Введите индексы второго оператора Паули");
		string sigma_2 = Console.ReadLine()!;
		Console.WriteLine("Введите коэффициент третьего слагаемого гамильтониана");
		string c = Console.ReadLine()!;
		Console.WriteLine("Введите индексы третьего оператора Паули");
		string sigma_3 = Console.ReadLine()!;
		Console.WriteLine($"Введённый вами гамильтониан: {a}*{sigma}_{sigma_1} + {b}*{sigma}{sigma_2} + {c}*{sigma}{sigma_3}");

		Console.WriteLine(Hamiltonian.countingOperators(sigma_1, sigma_2, sigma_3));
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
		public static string countingOperators(string sigma_1, string sigma_2, string sigma_3)
		{
			string sigma_13 = Calculation.Factors(sigma_1, sigma_3) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_1, sigma_3);
			string sigma_12 = Calculation.Factors(sigma_1, sigma_2) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_1, sigma_2);
			string sigma_23 = Calculation.Factors(sigma_2, sigma_3) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_2, sigma_3);
			string sigma_12_indexes = sigma_12.Split("_")[sigma_12.Split("_").Length - 1];
			string sigma_123_draft = Calculation.Factors(sigma_12_indexes, sigma_3) + $"*{sigma}_" + Calculation.PauliMatrices(sigma_12_indexes, sigma_3);
			string sigma_123;

			Console.WriteLine($"sigma_12 = {sigma_12}");
			Console.WriteLine($"sigma_123_draft = {sigma_123_draft}");

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
			return $"{sigma}_AC = {sigma_13}, {sigma}_AB = {sigma_12}, {sigma}_BC = {sigma_23}, {sigma}_ABC = {sigma_123}";
		}
	}
}
