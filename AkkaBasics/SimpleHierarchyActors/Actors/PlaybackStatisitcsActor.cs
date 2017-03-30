﻿using System;
using Akka.Actor;
using NLog;

namespace SimpleHierarchyActors.Actors {
    public class PlaybackStatisitcsActor : ReceiveActor {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();


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