using System;
using System.Collections.Generic;

namespace wpgexamples
{
    class Program
    {

        static void Main(string[] args)
        {
            string xmlUser = args.Length >= 1 ? args[0] : null;
            string xmlPass = args.Length >= 2 ? args[1] : null;
            string merchantCode = args.Length >= 3 ? args[2] : null;

            if (xmlUser == null || xmlPass == null || merchantCode == null)
            {
                Console.WriteLine("Run again with three args: <xmlUser> <xmlPass> <merchantCode>");
            }
            else
            {

                Dictionary<string, DemoApp> apps = new Dictionary<string, DemoApp>();
                apps.Add("card", new Card());
                apps.Add("card-advanced", new CardAdvanced());
                apps.Add("card-tokenisation", new CardTokenisation());
                apps.Add("hpp", new ThreeDs());
                apps.Add("paypal", new PayPal());
                apps.Add("paypal-advanced", new PayPalAdvanced());
                apps.Add("paypal-tokenisation", new PayPalTokenisation());
                apps.Add("threeds", new ThreeDs());
                apps.Add("tokenisation-capture-cvc", new TokenisationCaptureCvc());

                bool running = true;

                do
                {
                    Console.WriteLine("Availalble apps:");
                    foreach (var kv in apps)
                    {
                        Console.WriteLine("- " + kv.Key);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Type 'quit' to exit.");
                    Console.WriteLine("");
                    Console.WriteLine("Which app do you want to run?");

                    string app = Console.ReadLine() ?? "";
                    if (app == "quit")
                    {
                        running = false;
                    }
                    else if (!apps.ContainsKey(app))
                    {
                        Console.WriteLine("Unknown app '" + app + "'");
                    }
                    else
                    {
                        DemoApp obj = apps[app];
                        obj.Run(xmlUser, xmlPass, merchantCode);
                        Console.WriteLine("Finsihed.");
                    }
                }
                while (running);

                Console.WriteLine("Bye!");
            }
        }
    }

}
