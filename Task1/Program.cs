using System.Text;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int c = 1;
            StringBuilder res = new StringBuilder();
            char prevChar = s[0];

            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == prevChar)
                {
                    c++;
                }
                else
                {
                    if (c > 1)
                    {
                        res.Append(prevChar);
                        res.Append(c);
                    }
                    else
                    {
                        res.Append(prevChar);
                    }
                    prevChar = s[i];
                    c = 1;
                }
            }
            if (c > 1)
            {
                res.Append(prevChar);
                res.Append(c);
            }
            else
            {
                res.Append(prevChar);
            }

            Console.WriteLine("Result: " + res.ToString());
        }
    }
}
