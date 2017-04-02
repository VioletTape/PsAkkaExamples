using System;
using Akka.Actor;
using Akka.Event;

namespace AkkaDIs.Actors {
    public class PlaybackActor : ReceiveActor {
        private readonly ILoggingAdapter logger = Context.GetLogger();

        public PlaybackActor() {
            Context.ActorOf<UserCoordinatorActor>("userCoordinator");
            Context.ActorOf<PlaybackStatisitcsActor>("playbackStatistics");
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