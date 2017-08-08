using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class Appointment
    {
        private string id;
        private string subject;
        private string body;
        private string members;
        private string organizer;
        private DateTime start;
        private DateTime end;

        public String Id { get { return id; } set { id = value; } }
        public String Subject { get { return subject; } set { subject = value; } }
        public String Body { get { return body; } set { body = value; } }
        public String Members { get { return members; } set { members = value; } }
        public String Organizer { get { return organizer; } set { organizer = value; } }
        public DateTime Start { get { return start; } set { start = value; } }
        public DateTime End { get { return end; } set { end = value; } }

        public Appointment(string id,string subject,DateTime start,DateTime end, string organizer = "", string body = "")
        {
            this.id = id;
            this.subject = subject;
            this.start = start;
            this.end = end;
            this.body = body;
            this.organizer = organizer;
        }
    }
}
