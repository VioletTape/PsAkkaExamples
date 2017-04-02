using System;
using Akka.Actor;
using AkkaSerilogSeq.Actors;
using AkkaSerilogSeq.Messages;
using Serilog;

namespace AkkaNLog
{
    internal class Program {
        private static void Main(string[] args) {
            var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger();

            Log.Logger = logger;

            var movieStore = ActorSystem.Create("MovieStore");
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
