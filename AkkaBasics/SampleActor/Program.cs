using System;
using Akka.Actor;
using SampleActor.Actors;

namespace SampleActor {
    internal class Program {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args) {
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