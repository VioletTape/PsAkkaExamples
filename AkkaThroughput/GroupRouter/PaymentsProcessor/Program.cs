using System;
using System.Diagnostics;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using PaymentsProcessor.Actors;
using PaymentsProcessor.ExternalSystems;
using PaymentsProcessor.Messages;

namespace PaymentsProcessor
{
    class Program
    {
        private static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            CreateActorSystem();

            IActorRef jobCoordinator= ActorSystem.ActorOf<JobCoordinatorActor>("JobCoordinator");

            ActorSystem.ActorOf(ActorSystem.DI().Props<PaymentWorkerActor>(), "PaymentWorker1");
            ActorSystem.ActorOf(ActorSystem.DI().Props<PaymentWorkerActor>(), "PaymentWorker2");
            ActorSystem.ActorOf(ActorSystem.DI().Props<PaymentWorkerActor>(), "PaymentWorker3");

            var jobTime = Stopwatch.StartNew();

            jobCoordinator.Tell(new ProcessFileMessage("file1.csv"));

            ActorSystem.AwaitTermination();

            jobTime.Stop();

            Console.WriteLine("Job complete in {0}ms ", jobTime.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static void CreateActorSystem()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DemoPaymentGateway>().As<IPaymentGateway>();
            builder.RegisterType<PaymentWorkerActor>();
            var container = builder.Build();

            ActorSystem = ActorSystem.Create("PaymentProcessing");

            IDependencyResolver resolver = new AutoFacDependencyResolver(container, ActorSystem);
        }
    }
}
