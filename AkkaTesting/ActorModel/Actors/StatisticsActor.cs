using System.Collections.Generic;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Actors {
    public class StatisticsActor : ReceiveActor{
        public Dictionary<string, int> PlayCounts { get; set; }

        public StatisticsActor() {
            Receive<InitialStatisticsMesage>(m => _(m));
            Receive<string>(m => _(m));
        }

        public void _(InitialStatisticsMesage message) {
            PlayCounts = new Dictionary<string, int>(message.PlayCounts);
        }

        public void _(string title) {
            if (PlayCounts.ContainsKey(title)) {
                PlayCounts[title]++;
            }
            else {
                PlayCounts.Add(title, 1);
            }
        }
    }
}
