using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActorModel.Actors;
using ActorModel.Messages;
using Akka.TestKit.NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace ActorModel.Tests {
    public class StatisticsActorTests : TestKit{
        [Test]
        public void ShouldHaveInitialPlayCountValue() {
            var actor = new StatisticsActor();

            actor.PlayCounts
                 .Should()
                 .BeNull();
        }

        [Test]
        public void ShouldSetInitialPlayCount() {
            // This is DIRECT TEST of actor class
            // There is no actor system and we can't use any features like
            // stashing, sending, recieving messages
            var actor = new StatisticsActor();

            var initStats = new Dictionary<string, int> {
                                            {"Dark Knight", 10}
                                        };

            actor._(new InitialStatisticsMesage(new ReadOnlyDictionary<string, int>(initStats)));

            actor.PlayCounts["Dark Knight"]
                 .Should()
                 .Be(10);
        }
    }
}
