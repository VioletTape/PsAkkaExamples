﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActorModel.Actors;
using ActorModel.Messages;
using Akka.TestKit.NUnit;
using Akka.TestKit.TestActors;
using FluentAssertions;
using NUnit.Framework;

namespace ActorModel.Tests {
    public class StatisticsActorTests : TestKit {
        [Test]
        public void ShouldHaveInitialPlayCountValue() {
            var actor = new StatisticsActor(null);

            actor.PlayCounts
                 .Should()
                 .BeNull();
        }

        [Test]
        public void ShouldSetInitialPlayCount() {
            // This is DIRECT TEST of actor class
            // There is no actor system and we can't use any features like
            // stashing, sending, recieving messages
            var actor = new StatisticsActor(null);

            var initStats = new Dictionary<string, int> {
                                                            {"Dark Knight", 10}
                                                        };

            actor._(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(initStats)));

            actor.PlayCounts["Dark Knight"]
                 .Should()
                 .Be(10);
        }

        [Test]
        public void ShouldRecieveInitialStatisticMessage() {
            var actor = ActorOf(() => new StatisticsActor(ActorOf(BlackHoleActor.Props)));

            var initStats = new Dictionary<string, int> {
                                                            {"Dark Knight", 10}
                                                        };

            actor.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(initStats)), TestActor);

            // no such property and we have to use ActorOfAsTestActorRef
            //actor.PlayCounts
        }

        [Test]
        public void ShouldRecieveInitialStatisticMessageX() {
            var actor = ActorOfAsTestActorRef(() => new StatisticsActor(ActorOf(BlackHoleActor.Props)));

            var initStats = new Dictionary<string, int> {
                                                            {"Dark Knight", 10}
                                                        };

            actor.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(initStats)));

            actor.UnderlyingActor.PlayCounts["Dark Knight"]
                 .Should()
                 .Be(10);
        }

        [Test]
        public void ShouldUpdatePlayCountStatistics() {
            var actor = ActorOfAsTestActorRef<StatisticsActor>(() => new StatisticsActor(ActorOf(BlackHoleActor.Props)));

            var initStats = new Dictionary<string, int> {
                                                            {"Dark Knight", 10}
                                                        };

            actor.Tell(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(initStats)));

            // act
            actor.Tell("Dark Knight");

            // arrange
            actor.UnderlyingActor.PlayCounts["Dark Knight"]
                 .Should()
                 .Be(11);
        }
    }
}
