using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Actors {
    public class DatabaseActor : ReceiveActor {
        private readonly IDatabaseGateway databaseGateway;

        public DatabaseActor(IDatabaseGateway databaseGateway) {
            this.databaseGateway = databaseGateway;

            Receive<GetInititalStatisticsMessage>(m => _(m));
        }

        private void _(GetInititalStatisticsMessage getInititalStatisticsMessage) {
            var storedStatistics = databaseGateway.GetStredStatistics();
            Sender.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(storedStatistics)));
        }
    }

    public interface IDatabaseGateway {
        IDictionary<string, int> GetStredStatistics();
    }
}
