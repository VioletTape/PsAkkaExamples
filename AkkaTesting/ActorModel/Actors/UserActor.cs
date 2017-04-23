
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using ActorModel.Messages;
using Akka.Actor;
using Akka.Event;

namespace ActorModel.Actors {
    public class UserActor : ReceiveActor {
        private ILoggingAdapter log = Context.GetLogger();

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
            log.Info("Started playing " + message.MovieTitle);
            CurrentlyPlaying = message.MovieTitle;
            log.Info("Replying to sender");

            Sender.Tell(new NowPlayingMessage(CurrentlyPlaying));
            stat?.Tell(message.MovieTitle);
        }
    }
}