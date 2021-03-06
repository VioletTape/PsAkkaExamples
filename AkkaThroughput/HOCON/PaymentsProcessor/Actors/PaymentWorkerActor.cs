using System;
using Akka.Actor;
using PaymentsProcessor.ExternalSystems;
using PaymentsProcessor.Messages;

namespace PaymentsProcessor.Actors
{
    internal class PaymentWorkerActor : ReceiveActor
    {
        private readonly IPaymentGateway _paymentGateway;    

        public PaymentWorkerActor(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;

            Receive<SendPaymentMessage>(message => SendPayment(message));
        }

        private void SendPayment(SendPaymentMessage message)
        {
            Console.WriteLine("Sending payment for {0} {1}", message.FirstName, message.LastName);
            
            _paymentGateway.Pay(message.AccountNumber, message.Amount);

            Sender.Tell(new PaymentSentMessage(message.AccountNumber));
        }
    }
}