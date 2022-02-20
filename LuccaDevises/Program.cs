using System;

namespace LuccaDevises
{

    class Program
    {
        static void Main(string[] args)
        {
            string[] firstArgs = args[0].Split(';');
            Int32 devisesTabLength = Int32.Parse(args[1]);
            string[] devisesTab = new string[devisesTabLength];
            for (int i = 0; i < devisesTabLength; i++)
            {
                devisesTab[i] = args[i + 2];
            }

            Console.WriteLine("f" + firstArgs[0] + "l" + devisesTabLength + "p" + devisesTab[0]);
        }
    }
}
