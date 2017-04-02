using System;
using Akka.Actor;
using Akka.Event;
using AkkaNLog.Exceptions;
using NLog;

namespace AkkaNLog.Actors {
    public class PlaybackStatisitcsActor : ReceiveActor {
        private readonly ILoggingAdapter logger = Context.GetLogger();

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
