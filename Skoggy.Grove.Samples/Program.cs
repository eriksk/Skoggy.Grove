using System;

namespace Skoggy.Grove.Samples
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SampleGame())
                game.Run();
        }
    }
}
