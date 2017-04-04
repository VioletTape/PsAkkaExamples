namespace ActorModel.Messages {
    public class PlayMovieMessage {
        public string MovieTitle { get; }
        public PlayMovieMessage(string movieTitle) {
            MovieTitle = movieTitle;
        }
    }
}
