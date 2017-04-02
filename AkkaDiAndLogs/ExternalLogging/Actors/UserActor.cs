using System;
using Akka.Actor;
using NLog;
using NLogExplicitUsage.Messages;

namespace NLogExplicitUsage.Actors {
    public class UserActor : ReceiveActor {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private string currentlyWatching;

        public UserActor() {
            Receive<PlayMovieMessage>(m => _(m));
            Receive<StopMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            

            if (currentlyWatching != null) {
                logger.Warn("User {0} already watching movie", message.UserId);
            }
            else {
                currentlyWatching = message.MovieName;
                logger.Info("Starting movie " + currentlyWatching);
            }
        }

        private void _(StopMovieMessage message) {
            if (currentlyWatching == null) {
                logger.Warn("Nothing to stop");
            }
            else {
                logger.Info("Stopping movie " + currentlyWatching);
                currentlyWatching = null;
            }
        }


        protected override void PreStart() {
            logger.Trace("PreStart hook");
        }

        protected override void PostStop() {
            logger.Trace("PostStop hook");
        }

        protected override void PreRestart(Exception reason, object message) {
            logger.Trace("PreRestart hook");
            logger.Trace("PreRestart reason: " + reason.Message);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason) {
            logger.Trace("PostRestart hook");
            logger.Trace("PostRestart reason: " + reason.Message);

            base.PostRestart(reason);
        }
    }
}