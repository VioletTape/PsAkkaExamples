using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Tests.MockingNonActorDep {
    public class MockDatabaseActor : ReceiveActor {
        public MockDatabaseActor() {
            Receive<GetInititalStatisticsMessage>(m => {
                var stats = new Dictionary<string, int> {
                                                            {"Batman", 100}
                                                            , {"Batman Returns", 200}
                                                        };
                Sender.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(stats)));
            });
        }
    }
}
