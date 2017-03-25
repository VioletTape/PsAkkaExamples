using System;
using Akka.Actor;
using SampleActor.Actors;
using SampleActor.Messages;

namespace SampleActor {
    internal class Program {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args) {
            // creating local actor system
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingSystem");
            var ms = MovieStreamingActorSystem;

            Console.WriteLine("Actor system was created");

            // set of default properties based on config from config for akka
            // for instance now in app.config serializer changed to Hyperion
            var props = Props.Create<PlaybackActor>();

            // creating of new actor with specific properties
            var playbackActor = ms.ActorOf(props, "PlaybackActor");

            // fire and foreget for complex types
            playbackActor.Tell(new PlayMovieMessage(42, "Batman"));

            // fire and forget way of sending data
            playbackActor.Tell("Batman is the best!");
            playbackActor.Tell(42);

            Console.ReadLine();

            ms.Terminate();
        }
    }
}