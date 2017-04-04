using System.Collections.ObjectModel;

namespace ActorModel.Messages {
    public class InitialStatisticsMesage {
        public ReadOnlyDictionary<string, int> PlayCounts { get; }

        public InitialStatisticsMesage(ReadOnlyDictionary<string, int> playCounts) {
            PlayCounts = playCounts;
        }
    }
}
