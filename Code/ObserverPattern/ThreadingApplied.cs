﻿using System;
using System.Threading;

namespace ObserverPatternThreading
{
    public enum OrderState
    {
        CRETAED, CONFIRMED, CANCELLED, CLOSED
    }

    public class Order
    {
        public event Action<string> OrderStateChanged;//event
        //orderClosedEvent - new
        public event Action<string> OrderClosed;
        string orderId;
        OrderState currentState;
        public Order()
        {
            orderId = Guid.NewGuid().ToString();
            currentState = OrderState.CRETAED;
        }
        public void ChangeState(OrderState newState)
        {
            this.currentState = newState;
            if (currentState != OrderState.CLOSED)
            {
                NotifyAll();
            }
            else
            {
                NotifyClosed();
            }
        }
        void NotifyAll()
        {
            if (OrderStateChanged != null)
            {
                //this.OrderStateChanged.Invoke(this.orderId);//one->Many (Multicast Delegate Instance)
                //this.OrderStateChanged.BeginInvoke(orderId, null, null);
                Delegate[] invocationArray = this.OrderStateChanged.GetInvocationList();
                foreach (Action<string> method in invocationArray)
                {
                    new Thread(new ParameterizedThreadStart((object obj) => { method.Invoke(obj.ToString()); })).Start(this.orderId);
                }
            }
        }

        void NotifyClosed()
        {
            if (OrderClosed != null)
            {
                OrderClosed.Invoke(orderId);
            }
        }

        ////Subscribe,Register
        //public void Add_OrderStateChanged(Action observerAddress)
        //{
        //    this.OrderStateChanged += observerAddress;//System.Delegate.Combine
        //}
        ////UnSubScribe
        //public void Remove_OrderStateChanged(Action observerAddress)
        //{
        //    this.OrderStateChanged -= observerAddress;//System.Delegate.Remove
        //}

    }

    public class AuditSystem
    {
        public void CreateAudit(string eventData)
        {
            Console.WriteLine($"Audit done {eventData}");

        }
    }

    public class EmailNotifificationSystem
    {
        public void SendMail(string evtData) { Console.WriteLine($"Email Sent  {evtData}"); }
    }
    public class SMSNotificationSystem
    {
        public void SendSMS(string evtData)
        {
            Console.WriteLine($"SMS Sent  {evtData}");
        }
    }

    public class WhatsappNotificationSystem
    {
        public void SendWhatsapp(string eventData)
        {
            Console.WriteLine($"Whatsapp sent {eventData}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmailNotifificationSystem _emailSystem = new EmailNotifificationSystem();
            SMSNotificationSystem _smsSystem = new SMSNotificationSystem();
            WhatsappNotificationSystem _whatsappSystem = new WhatsappNotificationSystem();
            AuditSystem _auditSystem = new AuditSystem();

            Action<string> _emailObserver = new Action<string>(_emailSystem.SendMail);

            Action<string> _smsObserver = new Action<string>(_smsSystem.SendSMS);

            Action<string> _whatsappObserver = new Action<string>(_whatsappSystem.SendWhatsapp);

            Action<string> _auditObserver = new Action<string>(_auditSystem.CreateAudit);

            Order _order1 = new Order();
            _order1.OrderStateChanged += _emailObserver;// Add_OrderStateChanged(_emailObserver)
            _order1.OrderStateChanged += _smsObserver;
            _order1.OrderStateChanged += _whatsappObserver;
            _order1.OrderClosed += _auditObserver;

            _order1.ChangeState(OrderState.CONFIRMED);
            System.Threading.Tasks.Task.Delay(1000).Wait();
            _order1.ChangeState(OrderState.CANCELLED);
            System.Threading.Tasks.Task.Delay(3000).Wait();
            _order1.ChangeState(OrderState.CONFIRMED);
            System.Threading.Tasks.Task.Delay(5000).Wait();
            _order1.ChangeState(OrderState.CLOSED);
        }
    }
}
