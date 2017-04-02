using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using AkkaSerilogSeq.Messages;

namespace AkkaSerilogSeq.Actors {
    public class UserCoordinatorActor : ReceiveActor {
        private readonly ILoggingAdapter logger = Context.GetLogger();

        private readonly Dictionary<int, IActorRef> actorRefs;

        public UserCoordinatorActor() {
            actorRefs = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(m => _(m));
            Receive<StopMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            var actor = CreateIfNotExists(message.UserId);
            actor.Tell(message);
        }

        private void _(StopMovieMessage message) {
            var actor = CreateIfNotExists(message.UserId);
            actor.Tell(message);
        }

        private IActorRef CreateIfNotExists(int userId) {
            if (actorRefs.ContainsKey(userId))
                return actorRefs[userId];

            var actor = Context.ActorOf<UserActor>("user" + userId);
            actorRefs.Add(userId, actor);
            return actor;
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