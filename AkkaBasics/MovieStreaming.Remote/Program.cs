using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace MovieStreaming.Remote
{
    class Program
    {
        static void Main(string[] args) {
            var actorSystem = ActorSystem.Create("MovieActorSystem");

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
            actorSystem.Terminate();
        }
    }
}
