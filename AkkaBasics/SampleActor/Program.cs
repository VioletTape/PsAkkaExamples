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

            UserActorRefactored2(ms);

            Console.ReadLine();

            Console.WriteLine("Actor System terminating...");
            ms.Terminate();
            ms.WhenTerminated.Wait();

            Console.WriteLine("Actor System terminated");
        }


        /// <summary>
        /// Step 1
        /// </summary>
        /// <param name="actorSystem"></param>
        private static void UntypedActorUsage(ActorSystem actorSystem) {
            // set of default properties based on config from config for akka
            // for instance now in app.config serializer changed to Hyperion
            var props = Props.Create<PlaybackActor>();

            // creating of new actor with specific properties
            var playbackActor = actorSystem.ActorOf(props, "PlaybackActor");

            // fire and foreget for complex types
            playbackActor.Tell(new PlayMovieMessage(42, "Batman"));

            // fire and forget way of sending data
            playbackActor.Tell("Batman is the best!");
            playbackActor.Tell(42);
        }

        /// <summary>
        /// Step 2
        /// </summary>
        /// <param name="actorSystem"></param>
        private static void RecievedActorUsage(ActorSystem actorSystem) {
            var actor = actorSystem.ActorOf<PlaybackActorTyped>("PlaybackActorTyped");

            actor.Tell(new PlayMovieMessage(42, "Batman"));
            actor.Tell(new PlayMovieMessage(4, "Superman"));
            actor.Tell(new PlayMovieMessage(6, "Terminator 2"));
            actor.Tell(new PlayMovieMessage(32, "Predator"));

            // Terminate actor in a soft way, let him to process all recieved messages
            actor.Tell(PoisonPill.Instance);
        }


        /// <summary>
        /// Step 3
        /// </summary>
        /// <param name="actorSystem"></param>
        private static void Recieved2ActorUsage(ActorSystem actorSystem)
        {
            var actor = actorSystem.ActorOf<UserActor>("PlaybackActorTyped");

            Console.ReadLine();
            Console.WriteLine("Sending movie");
            actor.Tell(new PlayMovieMessage(42, "Batman"));
            Console.ReadLine();
            Console.WriteLine("Sending another movie");
            actor.Tell(new PlayMovieMessage(6, "Terminator 2"));

            Console.ReadLine();
            Console.WriteLine("Stoping movie");
            actor.Tell(new StopMovieMessage());
            Console.ReadLine();
            Console.WriteLine("Stoping another movie");
            actor.Tell(new StopMovieMessage());

            // Terminate actor in a soft way, let him to process all recieved messages
            actor.Tell(PoisonPill.Instance);
        }

        /// <summary>
        /// Step 4.1 Normal switching
        /// </summary>
        /// <param name="actorSystem"></param>
        private static void UserActorRefactored1(ActorSystem actorSystem)
        {
            var actor = actorSystem.ActorOf<UserActor>("PlaybackActorTyped");

            Console.ReadLine();
            Console.WriteLine("Sending movie");
            actor.Tell(new PlayMovieMessage(42, "Batman"));
            

            Console.ReadLine();
            Console.WriteLine("Stoping movie");
            actor.Tell(new StopMovieMessage());

            Console.ReadLine();
            Console.WriteLine("Sending another movie");
            actor.Tell(new PlayMovieMessage(6, "Terminator 2"));

            Console.ReadLine();
            Console.WriteLine("Stoping another movie");
            actor.Tell(new StopMovieMessage());

            // Terminate actor in a soft way, let him to process all recieved messages
            actor.Tell(PoisonPill.Instance);
        }

        /// <summary>
        /// Step 4.2 Errors in the steps order
        /// </summary>
        /// <param name="actorSystem"></param>
        private static void UserActorRefactored2(ActorSystem actorSystem)
        {
            var actor = actorSystem.ActorOf<UserActor>("PlaybackActorTyped");

            Console.ReadLine();
            Console.WriteLine("Sending movie");
            actor.Tell(new PlayMovieMessage(42, "Batman"));
            Console.ReadLine();
            Console.WriteLine("Sending another movie");
            actor.Tell(new PlayMovieMessage(6, "Terminator 2"));

            Console.ReadLine();
            Console.WriteLine("Stoping movie");
            actor.Tell(new StopMovieMessage());
            Console.ReadLine();
            Console.WriteLine("Stoping another movie");
            actor.Tell(new StopMovieMessage());

            // Terminate actor in a soft way, let him to process all recieved messages
            actor.Tell(PoisonPill.Instance);
        }
    }
}