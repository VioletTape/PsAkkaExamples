﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActorModel.Actors;
using ActorModel.Messages;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.NUnit;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ActorModel.Tests.MockingNonActorDep {
    [TestFixture]
    public class DatabaseActorTest : TestKit {
        /// <summary>
        ///     Simplest way of mocking. Pay attention on ActorOf method
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


        /// <summary>
        ///     using handwritten mock actor that works in system
        /// </summary>
        [Test]
        public void MockingActors() {
            // arrange
            var mockDbActor = ActorOfAsTestActorRef<MockDatabaseActor>();

            var actor = ActorOfAsTestActorRef(() => new StatisticsActor(mockDbActor));


            actor.UnderlyingActor.PlayCounts["Batman"].Should().Be(100);
        }

        /// <summary>
        ///     now we are going to use test probe instead of handwritten actor
        /// </summary>
        [Test]
        public void UsingTestProbe() {
            // arrange
            var mockDbActor = CreateTestProbe();
            var autoPilot = new DelegateAutoPilot((sender, message) => {
                                                  var stats = new Dictionary<string, int> {
                                                                                              {"Batman", 100}
                                                                                              , {"Batman Returns", 200}
                                                                                          };
                                                  sender.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(stats)));
                                                  return AutoPilot.KeepRunning;
                                              });
            mockDbActor.SetAutoPilot(autoPilot);

            var actor = ActorOfAsTestActorRef(() => new StatisticsActor(mockDbActor));


            actor.UnderlyingActor.PlayCounts["Batman"].Should().Be(100);
        }

        [Test]
        public void ChekingCorrectMessageByTestProbe() {
            var mockDbActor = CreateTestProbe();

            ActorOf(() => new StatisticsActor(mockDbActor));

            mockDbActor.ExpectMsg<GetInititalStatisticsMessage>();
        }
    }
}
