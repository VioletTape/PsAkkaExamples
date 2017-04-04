using ActorModel.Actors;
using ActorModel.Messages;
using Akka.TestKit.NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace ActorModel.Tests {
    
    public class UserActorTests : TestKit{
        [Test]
        public void ShouldHaveInitialState() {
            var actor = ActorOfAsTestActorRef<UserActor>();

            actor.UnderlyingActor.CurrentlyPlaying
                 .Should()
                 .BeNullOrEmpty();
        }

        [Test]
        public void ShouldUpdateCurrentlyPlayingState() {
            var actor = ActorOfAsTestActorRef<UserActor>();

            actor.Tell(new PlayMovieMessage("Batman"));

            actor.UnderlyingActor.CurrentlyPlaying
                 .Should()
                 .Be("Batman");
        }
    }
}
