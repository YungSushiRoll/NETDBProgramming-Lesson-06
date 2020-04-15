using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSystemPt3
{
    public class Ticket
    {
        public int ticketId { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string submitter{ get; set; }
        public string assigned { get; set; }
        public List<string> watchers { get; set; }

        public Ticket()
        {
            watchers = new List<string>();
        }

        public virtual string Output()
        {
            return ticketId + " " + summary + " " + status + " " + priority + " " + submitter + " " + assigned + " " + string.Join("|",watchers);
        }

    }

    public class TicketDebug : Ticket
    {
        public string severity { get; set; }

        public override string Output()
        {
            return $"{base.Output()} Severity: {severity}";
        }
    }

    public class TicketEnhancement : Ticket
    {
        public List<string> softwareNeeded { get; set; }
        public string cost { get; set; }
        public string reason { get; set; }
        public string estimate { get; set; }

        public TicketEnhancement()
        {
            softwareNeeded = new List<string>();
        }

        public override string Output()
        {
            return $"{base.Output()} " + string.Join(" | ",softwareNeeded) + $" Cost: {cost} Reason: {reason} Estimate: {estimate}";
        }
    }

    public class TicketTask : Ticket
    {
        public string projectName { get; set; }
        public string dueDate { get; set; }

        public override string Output()
        {
            return $"{base.Output()} Project Name: {projectName} Due Date: {dueDate}";
        }
    }
}
