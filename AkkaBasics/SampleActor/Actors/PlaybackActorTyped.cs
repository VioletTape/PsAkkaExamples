﻿using System;
using Akka.Actor;
using SampleActor.Messages;

namespace SampleActor.Actors {
    public class PlaybackActorTyped : ReceiveActor{
        public PlaybackActorTyped() {
            Receive<PlayMovieMessage>(m => Handle(m));
        }

        private void Handle(PlayMovieMessage message) {
            Console.WriteLine(message);
        }

        protected override void PreStart() {
            Console.WriteLine(">>> PreStart hook");
        }

        protected override void PostStop() {
            Console.WriteLine(">>> PostStop hook");

        }
    }
}