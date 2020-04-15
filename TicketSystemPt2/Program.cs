using System;
using System.IO;
using NLog;

namespace TicketSystemPt3
{
    class Program
    {
        // create a class level instance of logger (can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string file = "Tickets.csv";
            string efile = "Enhancements.csv";
            string tfile = "Task.csv";
            logger.Info("Program started");

            TicketFile ticketFile = new TicketFile(file);
            EnhancementFile enhancementFile = new EnhancementFile(efile);
            TaskFile taskFile = new TaskFile(tfile);
            string choice;

            do
            {
                Console.WriteLine("1) View Ticket file Summary.");
                Console.WriteLine("2) Create Ticket file.");
                Console.WriteLine("3) Create Ticket Enhancement file.");
                Console.WriteLine("4) Create Ticket Task file.");
                Console.WriteLine("Enter any other key to exit.");

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Debug/Defect Tickets\n" +
                            "_______________________________________________________\n");
                    foreach (Ticket t in ticketFile.Tickets)
                    {
                        Console.WriteLine(t.Output());
                    }
                    Console.WriteLine("_______________________________________________________\n\n");
                    Console.WriteLine("Enhancement Tickets\n" +
                            "_______________________________________________________\n");
                    foreach (Ticket t in enhancementFile.Tickets)
                    {
                        Console.WriteLine(t.Output());
                    }
                    Console.WriteLine("_______________________________________________________\n\n");
                    Console.WriteLine("Task Tickets\n" +
                           "_______________________________________________________\n");
                    foreach (Ticket t in taskFile.Tickets)
                    {
                        Console.WriteLine(t.Output());
                    }
                    Console.WriteLine("_______________________________________________________\n\n");
                }
                else if (choice == "2")
                {
                    Ticket ticket = new Ticket();

                    Console.WriteLine("Enter a new ticket? (Y/N)");
                    string newTicket = Console.ReadLine().ToUpper();
                    if (newTicket != "Y") { break; }

                    GeneralQ(ticket);

                    ticketFile.AddTicket(ticket);
                } 
                else if (choice == "3")
                {
                    TicketEnhancement ticketEnhancement = new TicketEnhancement();

                    Console.WriteLine("Enter a new ticket? (Y/N)");
                    string newEnhancement = Console.ReadLine().ToUpper();
                    if (newEnhancement != "Y") { break; }

                    GeneralQ(ticketEnhancement);

                    string newSoftware;
                    string theSoftwares;
                    double softwareCost = 0;
                    do
                    {
                        Console.WriteLine("Is there needed software? (Y/N)");
                        newSoftware = Console.ReadLine();
                        if (!newSoftware.ToUpper().Equals("Y")) { break; }
                        Console.WriteLine("What software is needed?");
                        theSoftwares = Console.ReadLine();
                        ticketEnhancement.softwareNeeded.Add(theSoftwares);
                        Console.WriteLine("Cost of software: ");
                        ticketEnhancement.cost = Console.ReadLine();
                        softwareCost += double.Parse(ticketEnhancement.cost);
                    } while (newSoftware.ToUpper().Equals("Y"));

                    Console.WriteLine("Reason for enhancement:");
                    ticketEnhancement.reason = Console.ReadLine();

                    ticketEnhancement.estimate = softwareCost.ToString();

                    enhancementFile.AddTicket(ticketEnhancement);

                }
                else if (choice == "4")
                {
                    TicketTask ticketTask = new TicketTask();
                    Console.WriteLine("Enter a new ticket? (Y/N)");
                    string newTask = Console.ReadLine().ToUpper();
                    if (newTask != "Y") { break; }

                    GeneralQ(ticketTask);
                    // add the rest of the task prompts
                    // edit the files for task and enhancement so that they write the other stuff to the file
                    Console.WriteLine("What is the name of this project?"); 
                    ticketTask.projectName = Console.ReadLine();

                    Console.WriteLine("When is this project due?");
                    ticketTask.dueDate = Console.ReadLine();
                    
                    // do media Library
                    taskFile.AddTicket(ticketTask);
                }
            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4");

            logger.Info("Program ended");

        }

        private static void GeneralQ(Ticket ticket)
        {
            Console.WriteLine("Enter Ticket Summary:");
            ticket.summary = Console.ReadLine();

            Console.WriteLine("Enter Ticket Status:");
            ticket.status = Console.ReadLine();

            Console.WriteLine("Enter Ticket Priority:");
            ticket.priority = Console.ReadLine();

            Console.WriteLine("Enter Ticket Submitter:");
            ticket.submitter = Console.ReadLine();

            Console.WriteLine("Ticket Assigned to:");
            ticket.assigned = Console.ReadLine();

            string tixWatcher;
            string theWatchers;
            do
            {
                Console.WriteLine("Is there someone watching this ticket? (Y/N)");
                tixWatcher = Console.ReadLine();
                if (!tixWatcher.ToUpper().Equals("Y")) { break; }
                Console.WriteLine("Who is watching the ticket?");
                theWatchers = Console.ReadLine();
                ticket.watchers.Add(theWatchers);
            } while (tixWatcher.ToUpper().Equals("Y"));
        }
    }
}
