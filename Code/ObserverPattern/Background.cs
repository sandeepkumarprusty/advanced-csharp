//using System;

//namespace ObserverPatternBackground
//{
//    public enum OrderState
//    {
//        CRETAED, CONFIRMED, CANCELLED, CLOSED
//    }

//    public class Order
//    {
//        //List<Action<string>> ObserverList = new List<Action<string>>();                   - I
//        //Action<string> OrderStateChanged;                                                 - II
//        public event Action<string> OrderStateChanged;//event                               - III
//        string orderId;
//        OrderState currentState;
//        public Order()
//        {
//            orderId = Guid.NewGuid().ToString();
//            currentState = OrderState.CRETAED;
//        }
//        public void ChangeState(OrderState newState)
//        {
//            this.currentState = newState;
//            NotifyAll();
//        }
//        void NotifyAll()
//        {
//            if (OrderStateChanged != null)
//            {
//                this.OrderStateChanged.Invoke(this.orderId);//one->Many (Multicast Delegate Instance)
//            }
//            //for (int i = 0; i < ObserverList.Count; i++)                          - I
//            //{
//            //    ObserverList[i].Invoke(orderId);
//            //}
//        }

//        //// We don't need to create the Observer List to subscribe or unsubscribe                                              - II
//        //// Action, Func and CustomDelegates are part of System.MulticastDelegate which has a InvocationList of its own
//        //// We can perform += and -= aka System.Delegate.Combine and System.Delegate.Remove

//        //// If we prefix any delegate with the event keyword,
//        //// it generates two functions of its own - Add_OrderStateChanged and Remove_OrderStateChanged           - III
//        //// So we don't need to write the Add_OrderStateChanged and Remove_OrderStateChanged manually
//        //// We can perform += and -= aka Add and Remove

//        ////Subscribe,Register
//        //public void Add_OrderStateChanged(Action<string> observerAddress)                           //  - II
//        ////public void Add_ObserverList(Action<string> observerAddress)                      - I             
//        //{
//        //    this.OrderStateChanged += observerAddress;//System.Delegate.Combine                  //     - II
//        //    //    ObserverList.Add(observerAddress);                                                - I
//        //}
//        //////UnSubScribe
//        //public void Remove_OrderStateChanged(Action<string> observerAddress)                    //      - II
//        ////public void Remove_ObserverList(Action<string> observerAddress)               
//        //{
//        //    this.OrderStateChanged -= observerAddress;//System.Delegate.Remove                          - II
//        //    //    ObserverList.Remove(observerAddress);                                             - I
//        //}

//    }

//    public class EmailNotifificationSystem
//    {
//        public void SendMail(string evtData) { Console.WriteLine($"Email Sent  {evtData}"); }
//    }
//    public class SMSNotificationSystem
//    {
//        public void SendSMS(string evtData)
//        {
//            Console.WriteLine($"SMS Sent  {evtData}");
//        }
//    }

//    public class WhatsappNotificationSystem
//    {
//        public void SendWhatsapp(string eventData)
//        {
//            Console.WriteLine($"Whatsapp sent {eventData}");
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            EmailNotifificationSystem _emailSystem = new EmailNotifificationSystem();
//            SMSNotificationSystem _smsSystem = new SMSNotificationSystem();
//            WhatsappNotificationSystem _whatsappSystem = new WhatsappNotificationSystem();

//            Action<string> _emailObserver = new Action<string>(_emailSystem.SendMail);              // - III
//            Action<string> _smsObserver = new Action<string>(_smsSystem.SendSMS);
//            Action<string> _whatsappObserver = new Action<string>(_whatsappSystem.SendWhatsapp);

//            Order _order1 = new Order();

//            //_order1.Add_ObserverList(_emailSystem.SendMail);                                      // - I
//            //_order1.Add_ObserverList(_smsSystem.SendSMS);
//            //_order1.Add_ObserverList(_whatsappSystem.SendWhatsapp);
//            //_order1.Add_OrderStateChanged(_emailSystem.SendMail);                                 // - II
//            //_order1.Add_OrderStateChanged(_smsSystem.SendSMS);
//            //_order1.Add_OrderStateChanged(_whatsappSystem.SendWhatsapp);
//            _order1.OrderStateChanged += _emailObserver;// Add_OrderStateChanged(_emailObserver)    // - III
//            _order1.OrderStateChanged += _smsObserver;
//            _order1.OrderStateChanged += _whatsappObserver;

//            _order1.ChangeState(OrderState.CONFIRMED);
//            System.Threading.Tasks.Task.Delay(1000).Wait();
//            _order1.ChangeState(OrderState.CANCELLED);
//            System.Threading.Tasks.Task.Delay(3000).Wait();
//            _order1.ChangeState(OrderState.CONFIRMED);
//            System.Threading.Tasks.Task.Delay(5000).Wait();
//            _order1.ChangeState(OrderState.CLOSED);
//        }
//    }
//}
