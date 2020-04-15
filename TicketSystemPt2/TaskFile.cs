﻿using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TicketSystemPt3
{
    class TaskFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string filePath { get; set; }

        public List<Ticket> Tickets { get; set; }

        public TaskFile(string path)
        {
            Tickets = new List<Ticket>();
            filePath = path;

            try
            {
                StreamReader sr = new StreamReader(filePath);
                if (sr.EndOfStream)
                {
                    sr.Close();
                    StreamWriter sw = new StreamWriter(filePath);
                    sw.WriteLine("TicketID, Summary, Status, Priority, Submitter, Assigned, Watching");
                    sw.Close();
                }
                else
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        Ticket ticket = new Ticket();
                        string line = sr.ReadLine();
                        string[] ticketStuff = line.Split(',');
                        ticket.ticketId = int.Parse(ticketStuff[0]);
                        ticket.summary = ticketStuff[1];
                        ticket.status = ticketStuff[2];
                        ticket.priority = ticketStuff[3];
                        ticket.submitter = ticketStuff[4];
                        ticket.assigned = ticketStuff[5];
                        ticket.watchers = ticketStuff[6].Split('|').ToList();
                        // ticket.projectName = ticketStuff[7];
                        // ticket.dueDate = ticketStuff[8];
                        Tickets.Add(ticket);
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
            logger.Info("{Count} tickets on file", Tickets.Count);
        }

        public void AddTicket(TicketTask ticket)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, true);

                if (Tickets.Count > 0)
                {
                    ticket.ticketId = Tickets.Max(t => t.ticketId) + 1;
                }
                else
                {
                    ticket.ticketId = 1;
                }
                sw.WriteLine($"{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{string.Join("|", ticket.watchers)},{ticket.projectName},{ticket.dueDate}");
                sw.Close();

                Tickets.Add(ticket);

                logger.Info("Ticket {id} has been added.", ticket.ticketId);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
    }
}
