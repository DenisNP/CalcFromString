using System;

namespace Task_5
{
    public class Program
    {
        public static void Main()
        {
            bool mode;

            Console.WriteLine("Ignore redundant parentheses and calulate expression if possible? Type yes to ignore or anything else to show error");
            while (true)
            {
                string input = Console.ReadLine();

                if (!input.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
                {
                    mode = false;
                    break;
                }

                mode = true;
                break;
            }

            while (true)
            {
                Console.WriteLine("Input expression or type \"exit\" to quit");

                try
                {
                    string input = (Console.ReadLine());

                    if (input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                        break;

                    var rc = new Calculator(input, mode);

                    Console.WriteLine($"Result: {rc.Calc()}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
