using System;

namespace XCollision
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CollisionTester game = new CollisionTester())
            {
                game.Run();
            }
        }
    }
#endif
}

