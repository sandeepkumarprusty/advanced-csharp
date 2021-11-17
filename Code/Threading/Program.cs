//using System;
//using System.Threading;

//namespace Threading
//{

//    public class DBWriter                                                  //- V
//    {
//        public void Insert()
//        {
//            //// Synchronization of threads by using Monitor.Enter and Monitor.Exit
//            Monitor.Enter(this);
//            for (int i = 0; i < 5; i++)
//            {
//                Console.WriteLine($"Inserting Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//                System.Threading.Thread.Sleep(1000);
//            }
//            Monitor.Exit(this);
//        }

//        public void Update()
//        {
//            Monitor.Enter(this);
//            for (int i = 0; i < 5; i++)
//            {
//                Console.WriteLine($"Updating Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//                System.Threading.Thread.Sleep(1000);
//            }
//            Monitor.Exit(this);

//        }
//        public void Delete()
//        {
//            Monitor.Enter(this);
//            for (int i = 0; i < 5; i++)
//            {
//                Console.WriteLine($"Deleting Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//                System.Threading.Thread.Sleep(1000);
//            }
//            Monitor.Exit(this);

//        }

//    }
//    class Program
//    {
//        static void Main_old(string[] args)
//        {
//            Console.WriteLine($"Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");               //- I
//            //// To create a new thread, use System.Threading.Thread
//            //// To refer to the function to be executed by the thread, two inbuilt delegates -> ThreadStart and ParameterizedThreadStart
//            System.Threading.ThreadStart _sendSmsFunRef = new System.Threading.ThreadStart(Program.SendSms);                    //- II
//            System.Threading.Thread _smsThread = new System.Threading.Thread(_sendSmsFunRef);                                   //- II

//            _smsThread.Start();                                                                                                 //- II
//            //SendSms();                                                                                                          //- I
//            System.Threading.ThreadStart _sendEmailFunRef = new System.Threading.ThreadStart(Program.SendEmail);                //- II
//            System.Threading.Thread _emailThread = new System.Threading.Thread(_sendEmailFunRef);                               //- II
//            //// Background threads run only until the last foreground thread returns, after that they are aborted
//            //// By default, System.Threading.Thread is foreground
//            //_emailThread.IsBackground = true;                                                                                   //- III
//            //_emailThread.Start();                                                                                               //- II

//            //// ThreadPool threads are background threads
//            //// WaitCallback delegate used to pass wrapper function of the method to be queued
//            //// wrapper needs to be created because signature of WaitCallback delegate -> void WaitCallback( Object arg);
//            System.Threading.WaitCallback _emailMethodRef = new System.Threading.WaitCallback(SendEmailWrapper);                    //- IV
//            System.Threading.ThreadPool.QueueUserWorkItem(_emailMethodRef);                                                         //- IV
//            //SendEmail();                                                                                                          //- I
//            Console.WriteLine($"End Of Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");            //- I

//        }

//        static void Main()                                                          //- V
//        {
//            Console.WriteLine($"Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//            DBWriter _dbWriterRef = new DBWriter();
//            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Insert)).Start();
//            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Update)).Start();
//            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Delete)).Start();
//            Console.WriteLine($"End Of Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//        }
//        static void SendSms()
//        {

//            for (int i = 0; i < 5; i++)
//            {
//                Console.WriteLine($"Sending Sms...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//                System.Threading.Thread.Sleep(1000);
//            }
//        }
//        static void SendEmailWrapper(object arg)
//        {
//            SendEmail();
//        }
//        static void SendEmail()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.WriteLine($"Sending Email...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
//                System.Threading.Thread.Sleep(1000);
//            }
//        }


//    }
//}