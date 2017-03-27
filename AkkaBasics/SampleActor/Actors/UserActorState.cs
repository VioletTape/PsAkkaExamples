using System;
using Akka.Actor;
using SampleActor.Messages;

namespace SampleActor.Actors {
    public class UserActorState : ReceiveActor {
        private string currentlyWatching;

        public UserActorState() {
            Stopped();
        }

        private void Stopped() {
            Receive<PlayMovieMessage>(m => _(m));
            Receive<StopMovieMessage>(m => Console.WriteLine("!!! Nothing to stop"));
        }

        private void Playing() {
            Receive<PlayMovieMessage>(m => Console.WriteLine("!!! user already watching movie"));
            Receive<StopMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            currentlyWatching = message.Name;
            Console.WriteLine("Starting movie " + currentlyWatching);

            Become(Stopped);
        }

        private void _(StopMovieMessage message) {
            Console.WriteLine("Stopping movie " + currentlyWatching);
            currentlyWatching = null;

            Become(Playing);
        }


        protected override void PreStart() {
            Console.WriteLine(">>> UserActor PreStart hook");
        }

        protected override void PostStop() {
            Console.WriteLine(">>> UserActor PostStop hook");
        }

        protected override void PreRestart(Exception reason, object message) {
            Console.WriteLine(">>> UserActor PreRestart hook");
            Console.WriteLine(">>> " + reason.Message);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason) {
            Console.WriteLine(">>> UserActor PostRestart hook");
            Console.WriteLine(">>> " + reason.Message);

            base.PostRestart(reason);
        }
    }
}