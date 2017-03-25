using System;
using Akka.Actor;

namespace SampleActor.Actors {
    public class PlaybackActor : UntypedActor {
        public PlaybackActor() {
            Console.WriteLine("Hello from playback actor!");
        }

        protected override void OnReceive(object message) {
            
        }
    }
}