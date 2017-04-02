using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using AkkaDIs.Exceptions;
using AkkaDIs.Messages;
using AkkaDIs.Services;

namespace AkkaDIs.Actors {
    public class MoviePlayCounterActor : ReceiveActor {
        private readonly ILoggingAdapter logger = Context.GetLogger();
        private readonly Dictionary<string, int> counter = new Dictionary<string, int>();


        public MoviePlayCounterActor(IService service) {
            if (service == null) {
                throw new NullReferenceException();
            }
            Receive<IncrementMoviePlayMessage>(m => _(m));
        }

        private void _(IncrementMoviePlayMessage message) {
            if (counter.ContainsKey(message.MovieName))
                counter[message.MovieName] += 1;
            else
                counter.Add(message.MovieName, 1);

            if (message.MovieName == "superman") {
                throw new TerribleMovieException();
            }

            if (counter[message.MovieName] >= 3) {
                throw new CorruptStateException();
            }

            logger.Info("{0} played {1} times", message.MovieName, counter[message.MovieName]);
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
