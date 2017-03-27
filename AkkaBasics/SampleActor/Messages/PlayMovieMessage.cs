namespace SampleActor.Messages {
    public class PlayMovieMessage {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public PlayMovieMessage(int id, string name) {
            Id = id;
            Name = name;
        }

        public override string ToString() {
            return Id + ": " + Name;
        }
    }

    public class StopMovieMessage {
        
    }
}