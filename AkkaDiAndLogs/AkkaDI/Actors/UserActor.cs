using System;
using Akka.Actor;
using Akka.Event;
using AkkaDIs.Messages;

namespace AkkaDIs.Actors {
    public class UserActor : ReceiveActor {
        private readonly ILoggingAdapter logger = Context.GetLogger();
        private string currentlyWatching;

        public UserActor() {
            Receive<PlayMovieMessage>(m => _(m));
            Receive<StopMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            

            if (currentlyWatching != null) {
                logger.Warning("User {0} already watching movie", message.UserId);
            }
            else {
                currentlyWatching = message.MovieName;
                logger.Info("Starting movie " + currentlyWatching);
            }
        }

        private void _(StopMovieMessage message) {
            if (currentlyWatching == null) {
                logger.Warning("Nothing to stop");
            }
            else {
                logger.Info("Stopping movie " + currentlyWatching);
                currentlyWatching = null;
            }
        }


        protected override void PreStart() {
            logger.Debug("PreStart hook");
        }

        protected override void PostStop() {
            logger.Debug("PostStop hook");
        }

        protected override void PreRestart(Exception reason, object message) {
            logger.Error("PreRestart hook");
            logger.Error("PreRestart reason: " + reason.Message);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason) {
            logger.Error("PostRestart hook");
            logger.Error("PostRestart reason: " + reason.Message);

            base.PostRestart(reason);
        }
    }
}