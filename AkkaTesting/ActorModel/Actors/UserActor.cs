
using System.Runtime.InteropServices;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Actors {
    public class UserActor : ReceiveActor {
        public string CurrentlyPlaying { get; private set; }

        public UserActor() {
            Receive<PlayMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            CurrentlyPlaying = message.MovieTitle;
        }
    }
}