using System;
using Akka.Actor;
using SampleActor.Messages;

namespace SampleActor.Actors {
    public class PlaybackActor : UntypedActor {
        public PlaybackActor() {
            Console.WriteLine("Hello from playback actor!");
        }

        protected override void OnReceive(object message) {
            // dealing with POCO
            var playMovieMessage = message as PlayMovieMessage;
            if (playMovieMessage != null) {
                Console.WriteLine(playMovieMessage);
            }
            else {
                Unhandled(message);
            }
            return;

            // dealing with simple types    
            if (message is string) Console.WriteLine("The movie is " + message);
            else if (message is int) Console.WriteLine("some id is " + message);
            else Unhandled(message);
        }
    }
}