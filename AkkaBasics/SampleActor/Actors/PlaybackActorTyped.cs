using System;
using Akka.Actor;
using SampleActor.Messages;

namespace SampleActor.Actors {
    public class PlaybackActorTyped : ReceiveActor{
        public PlaybackActorTyped() {
            Receive<PlayMovieMessage>(m => Handle(m));
        }

        private void Handle(PlayMovieMessage message) {
            Console.WriteLine(message);
        }

        protected override void PreStart() {
            Console.WriteLine(">>> PreStart hook");
        }

        protected override void PostStop() {
            Console.WriteLine(">>> PostStop hook");
        }

        protected override void PreRestart(Exception reason, object message) {
            Console.WriteLine(">>> PreRestart hook");
            Console.WriteLine(">>> " + reason.Message);


            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason) {
            Console.WriteLine(">>> PostRestart hook");
            Console.WriteLine(">>> " + reason.Message);

            base.PostRestart(reason);
        }
    }
}