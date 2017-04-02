namespace AkkaSerilogSeq.Messages {
    public class PlayMovieMessage {
        public int UserId { get; }
        public string MovieName { get; }

        public PlayMovieMessage(int userId, string movieName) {
            UserId = userId;
            MovieName = movieName;
        }
    }
}