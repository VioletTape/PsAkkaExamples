using System;
using System.Configuration;
using Akka.Actor;
using Akka.Configuration;
using SampleActor.Actors;

namespace SampleActor {
    internal class Program {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args) {
            var section = ConfigurationManager.GetSection("akka");
            
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingSystem");
            var ms = MovieStreamingActorSystem;

            Console.WriteLine("Actor system was created");

            var props = Props.Create<PlaybackActor>();
            var actorRef = ms.ActorOf(props, "PlaybackActor");

            Console.ReadLine();

            ms.Terminate();

        }
    }
}