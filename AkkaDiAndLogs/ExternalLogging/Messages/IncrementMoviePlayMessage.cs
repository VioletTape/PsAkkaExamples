namespace NLogExplicitUsage.Messages {
    public class IncrementMoviePlayMessage {
        public string MovieName { get; }
        public IncrementMoviePlayMessage(string movieName) {
            MovieName = movieName;
        }

    }
}