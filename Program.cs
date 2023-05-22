using System;

class Program
{
	static void Main()
	{
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
		Console.WriteLine($"Введённый вами гамильтониан: {a} sigma_{sigma_1} + {b} sigma{sigma_2} + {c} sigma{sigma_3}");

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
			string sigma_13 = Calculation.Factors(sigma_1, sigma_3) + "*Sigma_" + Calculation.PauliMatrices(sigma_1, sigma_3);
			string sigma_12 = Calculation.Factors(sigma_1, sigma_2) + "*Sigma_" + Calculation.PauliMatrices(sigma_1, sigma_2);
			string sigma_23 = Calculation.Factors(sigma_2, sigma_3) + "*Sigma_" + Calculation.PauliMatrices(sigma_2, sigma_3);

			return $"sigma_AC = {sigma_13}, sigma_AB = {sigma_12}, sigma_BC = {sigma_23}";
		}
	}
}
