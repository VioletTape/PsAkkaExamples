using System;
using Akka.Actor;
using Akka.DI.StructureMap;
using AkkaDIs.Actors;
using AkkaDIs.Messages;
using AkkaDIs.Services;
using StructureMap;

namespace AkkaDIs {
    internal class Program {
        private static void Main(string[] args) {
            var container = new Container();
            container.Configure(_ =>
                                    _.For<IService>().Use<Service>()
                                    );

            var movieStore = ActorSystem.Create("MovieStore");

            new StructureMapDependencyResolver(container, movieStore);

            movieStore.ActorOf<PlaybackActor>("playback");

            Console.WriteLine("Available commands:\n\tplay -<userId> -<movie name>\n\tstop -<userId>\n\texit");

            do {
                var command = Console.ReadLine();
                var myArgs = command.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries);

                if (command.StartsWith("play")) {
                    movieStore.ActorSelection("/user/playback/userCoordinator")
                              .Tell(new PlayMovieMessage(int.Parse(myArgs[1]), myArgs[2]));
                    movieStore.ActorSelection("/user/playback/playbackStatistics/playsCounter")
                              .Tell(new IncrementMoviePlayMessage(myArgs[2]));
                }
                if (command.StartsWith("stop"))
                    movieStore.ActorSelection("/user/playback/userCoordinator")
                              .Tell(new StopMovieMessage(int.Parse(myArgs[1])));

                if (command.StartsWith("exit")) {
                    var task = movieStore.Terminate();
                    Console.WriteLine("Actor system terminating...");
                    task.Wait();
                    Environment.Exit(1);
                }
            } while (true);
        }
    }
}
