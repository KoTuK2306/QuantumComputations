using System;

class Program
{
	static void Main()
	{
		Console.WriteLine("Введите индексы множителей операторов Паули:");
		string K = Console.ReadLine()!;
		string L = Console.ReadLine()!;
		string M = "", w = "";
		Calculation.PauliMatrices(K, L, out M);
		Calculation.Factors(K, L, out w);
		Console.WriteLine("Sigma_" + K + "*Sigma_" + L + "=" + w + "*Sigma_" + M);
	}
	class Calculation
	{
		public static void PauliMatrices(string K, string L, out string M)
		{
			char[] K1 = K.ToCharArray();
			char[] L1 = L.ToCharArray();
			M = "";
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
			}
			else Console.WriteLine("Ошибка размерности кубитов двух операторов");


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
		public static void Factors(string K, string L, out string w)
		{
			char[] K1 = K.ToCharArray();
			char[] L1 = L.ToCharArray();
			int a, b, skl, p = 0, m = 0;
			w = "";
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

		}
	}
}
