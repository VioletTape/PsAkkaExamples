using System;
using Akka.Actor;
using SampleActor.Messages;

namespace SampleActor.Actors {
    public class UserActor : ReceiveActor {
        private string currentlyWatching;

        public UserActor() {
            Receive<PlayMovieMessage>(m => _(m));
            Receive<StopMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            if (currentlyWatching != null) {
                Console.WriteLine("!!! user already watching movie");
            }
            else {
                currentlyWatching = message.Name;
                Console.WriteLine("Starting movie " + currentlyWatching);

            }
        }

        private void _(StopMovieMessage message) {
            if (currentlyWatching == null) {
                Console.WriteLine("!!! Nothing to stop");
            }
            else {
                Console.WriteLine("Stopping movie " + currentlyWatching);
                currentlyWatching = null;
                
            }
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