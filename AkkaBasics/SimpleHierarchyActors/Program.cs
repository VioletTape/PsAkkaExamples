using System;
using Akka.Actor;
using NLog;
using NLog.Config;
using SimpleHierarchyActors.Actors;

namespace SimpleHierarchyActors {
    internal class Program {
        private static void Main(string[] args) {
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config", true);

            var asMoviewStore = ActorSystem.Create("MovieStore");

            var playbackActor = asMoviewStore.ActorOf<PlaybackActor>("playback");

            Console.ReadLine();

            asMoviewStore.Terminate();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}