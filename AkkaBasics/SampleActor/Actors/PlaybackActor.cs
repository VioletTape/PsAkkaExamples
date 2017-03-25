using System;
using Akka.Actor;

namespace SampleActor.Actors {
    public class PlaybackActor : UntypedActor {
        public PlaybackActor() {
            Console.WriteLine("Hello from playback actor!");
        }

        protected override void OnReceive(object message) {
            if (message is string) Console.WriteLine("The movie is " + message);
            else if (message is int) Console.WriteLine("some id is " + message);
            else Unhandled(message);
        }
    }
}