
using System.Runtime.InteropServices;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Actors {
    public class UserActor : ReceiveActor {
        private readonly IActorRef stat;
        public string CurrentlyPlaying { get; private set; }

        public UserActor(IActorRef stat) {
            this.stat = stat;
            Receive<PlayMovieMessage>(m => _(m));
        }

        /// <summary>
        /// Or you can use BlackHoleActor
        /// </summary>
        public UserActor()
        {
            Receive<PlayMovieMessage>(m => _(m));
        }

        private void _(PlayMovieMessage message) {
            CurrentlyPlaying = message.MovieTitle;
            Sender.Tell(new NowPlayingMessage(CurrentlyPlaying));
            stat?.Tell(message.MovieTitle);
        }
    }
}