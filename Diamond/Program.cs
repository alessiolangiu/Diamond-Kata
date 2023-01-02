using Diamond.Application;

namespace Diamond;

using Diamond = Diamond.Application.Diamond;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No input provided. Please pass a letter as parameter.");
            return;
        }

        // KISS, No need to use Ioc containers, 
        var diamond = new Diamond(new LayersGenerator());
        var result  = diamond.Create(args[0]);
        if (result.IsFailed)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Message);
            }
        }
        else
        {
            foreach (var line in result.Value)
            {
                Console.WriteLine(line);
            }
        }
    }
}