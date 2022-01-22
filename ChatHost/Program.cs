using System;
using System.ServiceModel;
using Chat;

namespace ChatHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал!");
                _ = Console.ReadLine();
            }
        }
    }
}
