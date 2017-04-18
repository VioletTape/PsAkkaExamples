namespace ActorModel.Messages {
    public class NowPlayingMessage {
        public string CurrentlyPlaying { get; }

        public NowPlayingMessage(string currentlyPlaying) {
            CurrentlyPlaying = currentlyPlaying;
        }
    }
}
