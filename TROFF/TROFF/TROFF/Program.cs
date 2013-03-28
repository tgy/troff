using System;

namespace TROFF
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (App game = new App())
            {
                game.Run();
            }
        }
    }
#endif
}

