using ActorModel.Actors;
using ActorModel.Messages;
using Akka.Actor;
using Akka.TestKit.NUnit;
using Akka.TestKit.TestActors;
using FluentAssertions;
using NUnit.Framework;

namespace ActorModel.Tests {
    public class UserActorTests : TestKit {
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

        [Test]
        public void ShouldPlayMovie() {
            var actor = ActorOf<UserActor>();

            // difference from the course - TestActor should be passed
            actor.Tell(new PlayMovieMessage("Batman"), TestActor);

            // Expect message for 3sec by default
            // can be set explisitly or in config globaly 
            ExpectMsg<NowPlayingMessage>();

            // can't do the next code, because message was already send and consumed
            // var nowPlayingMessage = ExpectMsg<NowPlayingMessage>();
            // nowPlayingMessage.CurrentlyPlaying
            //                  .Should()
            //                  .Be("Batman");
        }

        [Test]
        public void ShouldPlayMovie1() {
            var actor = ActorOf<UserActor>();

            // difference from the course - TestActor should be passed
            actor.Tell(new PlayMovieMessage("Batman"), TestActor);

            // note: explore how to use
            //            ExpectMsgFrom<NowPlayingMessage>(actor, msg => msg.CurrentlyPlaying.Should().Be("Batman"));

            var nowPlayingMessage = ExpectMsg<NowPlayingMessage>();
            nowPlayingMessage.CurrentlyPlaying
                             .Should()
                             .Be("Batman");
        }

        [Test]
        public void ShouldPlayMovie2() {
            var actor = ActorOf<UserActor>();

            // difference from the course - TestActor should be passed
            actor.Tell(new PlayMovieMessage("Batman"), TestActor);

            ExpectMsgFrom<NowPlayingMessage>(actor, msg => msg.CurrentlyPlaying.Should().Be("Batman"));
        }

        /// <summary>
        ///     Testing logging features of akka
        /// </summary>
        [Test]
        public void ShouldLogEvent() {
            var actor = ActorOf(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            EventFilter.Info("Started playing Batman")
                       .And
                       .Info("Replying to sender")
                       .Expect(2, () => actor.Tell(new PlayMovieMessage("Batman")));
        }
    }
}
