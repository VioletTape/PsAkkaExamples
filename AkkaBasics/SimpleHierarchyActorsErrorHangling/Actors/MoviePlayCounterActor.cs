using System;
using System.Collections.Generic;
using Akka.Actor;
using NLog;
using SimpleHierarchyActors.Messages;

namespace SimpleHierarchyErrorHangling.Actors {
    public class MoviePlayCounterActor : ReceiveActor {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, int> counter = new Dictionary<string, int>();


        public MoviePlayCounterActor() {
            Receive<IncrementMoviePlayMessage>(m => _(m));
        }

        private void _(IncrementMoviePlayMessage message) {
            if (counter.ContainsKey(message.MovieName)) {
                counter[message.MovieName] += 1;
            }
            else {
                counter.Add(message.MovieName, 1);
            }
            logger.Info("{0} played {1} times", message.MovieName, counter[message.MovieName]);
        }

        protected override void PreStart()
        {
            logger.Trace("PreStart hook");
        }

        protected override void PostStop()
        {
            logger.Trace("PostStop hook");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            logger.Trace("PreRestart hook");
            logger.Trace("PreRestart reason: " + reason.Message);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            logger.Trace("PostRestart hook");
            logger.Trace("PostRestart reason: " + reason.Message);

            base.PostRestart(reason);
        }
    }
}