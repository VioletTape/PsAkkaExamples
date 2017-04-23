using System.Collections.Generic;
using ActorModel.Actors;
using ActorModel.Messages;
using Akka.Actor;
using Akka.TestKit.NUnit;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ActorModel.Tests.MockingNonActorDep {
    [TestFixture]
    public class DatabaseActorTest : TestKit {

        /// <summary>
        /// Simplest way of mocking. Pay attention on ActorOf method
        /// </summary>
        [Test]
        public void TraditionalMocking() {
            // arrange
            var stats = new Dictionary<string, int> {
                                            {"Batman", 100}
                                            , {"Batman Returns", 200}
                                        };

            var mock = new Mock<IDatabaseGateway>();
            mock.Setup(_ => _.GetStredStatistics())
                .Returns(stats);

            var actorRef = ActorOf(Props.Create<DatabaseActor>(mock.Object));

            // act
            actorRef.Tell(new GetInititalStatisticsMessage(), TestActor);

            // assert
            var recieved = ExpectMsg<InitialStatisticsMesage>();

            recieved.PlayCounts["Batman"].Should().Be(100);
        }

    }
}
    