using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zad6i7
{
    public class Class1
    {

        public static async Task<int> FactorialDigitSum(int n)
        {
            return await Task.Run(() =>
            {
                int fact = 1;
                while (n != 1)
                {
                    fact *= n;
                    n -= 1;
                }

                char[] digits = fact.ToString().ToArray();

                return digits.Sum(d => int.Parse(d.ToString()));
            });

        }

        private static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }

        // Ignore this part.
        static void Main(string[] args)
        {
            // Main method is the only method that 
            // can’t be marked with async.
            // What we are doing here is just a way for us to simulate
            // async-friendly environment you usually have with
            // other .NET application types (like web apps, win apps etc.)
            // Ignore main method, you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in the call hierarchy. 
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
    }
}
