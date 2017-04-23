using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using ActorModel.Actors;
using ActorModel.Messages;
using Akka.Actor;
using Akka.TestKit.NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace ActorModel.Tests {
    public class IntegrationTests : TestKit {
        [Test]
        public void ShouldCountPlays() {
            var statActor = ActorOfAsTestActorRef<StatisticsActor>();

            var initStats = new Dictionary<string, int> {
                                                            {"Dark Knight", 10}
                                                        };

            statActor.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(initStats)));

            // do not use ActorOf for sequential testing. It runs actor on different thread 
            // and results are unrelyable 
            // var user = ActorOf(Props.Create(() => new UserActor(statActor)));

            var user = ActorOfAsTestActorRef<UserActor>(Props.Create(() => new UserActor(statActor)));
            user.Tell(new PlayMovieMessage("Dark Knight"));

            statActor.UnderlyingActor.PlayCounts["Dark Knight"]
                     .Should()
                     .Be(11);
        }
    }
}
