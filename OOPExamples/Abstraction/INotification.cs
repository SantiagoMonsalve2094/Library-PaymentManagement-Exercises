using System;
using System.Collections.Generic;
using System.Text;

namespace OOPExamples.Abstraction
{
    public interface INotification
    {
        void Send(string message);
    }

    public class EmailNotification : INotification
    {
        public void Send(string message) =>
            Console.WriteLine($"[EMAIL] {message}");
    }

    public class SmsNotification : INotification
    {
        public void Send(string message) =>
            Console.WriteLine($"[SMS] {message}");
    }

    // The service doesn't know or care how the message is sent
    public class AlertService
    {
        private readonly INotification _channel;
        public AlertService(INotification channel) => _channel = channel;
        public void AlertUser(string msg) => _channel.Send(msg);
    }
}
