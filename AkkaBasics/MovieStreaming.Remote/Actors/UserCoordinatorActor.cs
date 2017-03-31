using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Common.Messages;
using NLog;

namespace SimpleHierarchyErrorHangling.Actors {
    public class UserCoordinatorActor : ReceiveActor {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

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