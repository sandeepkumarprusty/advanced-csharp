using System;
using System.Threading;

namespace ConsoleApp1
{

    public class DBWriter
    {
        object _syncObjForWriters = new object();
        object _syncObjForLog = new object();
        //public void Insert(DBWriter this){}
        public void Insert()
        {
            Console.WriteLine("Insert Started...");
            Monitor.Enter(_syncObjForWriters);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Inserting Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
            Monitor.Exit(_syncObjForWriters);
            Console.WriteLine("Insert End...");
        }

        public void Update()
        {
            //public void Update(DBWriter this){}
            Console.WriteLine("Update Started...");
            Monitor.Enter(_syncObjForWriters);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Updating Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
            Monitor.Exit(_syncObjForWriters);
            Console.WriteLine("Update End...");

        }
        //public void Delete(DBWriter this){}
        public void Delete()
        {
            Console.WriteLine("Delete Started...");
            Monitor.Enter(_syncObjForWriters);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Deleting Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
            Monitor.Exit(_syncObjForWriters);
            Console.WriteLine("Delete End...");
        }

        public void Log()
        {
            Monitor.Enter(_syncObjForLog);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Logging  Operation...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
            Monitor.Exit(_syncObjForLog);
        }
    }

    class Program
    {
        static void Main_old(string[] args)
        {
            Console.WriteLine($"Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            System.Threading.ThreadStart _sendSmsFunRef = new System.Threading.ThreadStart(Program.SendSms);
            System.Threading.Thread _smsThread = new System.Threading.Thread(_sendSmsFunRef);

            _smsThread.Start();
            //SendSms();
            //System.Threading.ThreadStart _sendEmailFunRef = new System.Threading.ThreadStart(Program.SendEmail);
            //System.Threading.Thread _emailThread = new System.Threading.Thread(_sendEmailFunRef);
            //_emailThread.IsBackground = true;
            //_emailThread.Start();
            System.Threading.WaitCallback _emailMethodRef = new System.Threading.WaitCallback(SendEmailWrapper);
            System.Threading.ThreadPool.QueueUserWorkItem(_emailMethodRef);
            //SendEmail();
            Console.WriteLine($"End Of Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");

        }

        static void Main()
        {
            DBWriter _dbWriterRef = new DBWriter();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Insert)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Update)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Delete)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Log)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Log)).Start();
        }
        static void SendSms()
        {

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Sending Sms...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }
        static void SendEmailWrapper(object arg)
        {
            SendEmail();
        }
        static void SendEmail()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"Sending Email...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }


    }
}