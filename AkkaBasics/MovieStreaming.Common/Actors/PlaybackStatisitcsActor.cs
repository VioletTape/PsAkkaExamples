using System;
using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using NLog;

namespace MovieStreaming.Common.Actors {
    public class PlaybackStatisitcsActor : ReceiveActor {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PlaybackStatisitcsActor() {
            Context.ActorOf<MoviePlayCounterActor>("playsCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy() {
            return new OneForOneStrategy(e => {
                                             if (e is TerribleMovieException) {
                                                 // resume actor to process next message 
                                                 return Directive.Resume;
                                             }

                                             if (e is CorruptStateException)
                                                 return Directive.Restart;

                                             return Directive.Resume;
                                         });
        }

        protected override void PreStart() {
            logger.Trace("PreStart hook");
        }

        protected override void PostStop() {
            logger.Trace("PostStop hook");
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
