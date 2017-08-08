using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class Event
    {
        private string title;
        private string start;
        private string end;
        private string backgroundColor;
        private string borderColor;
        private string ubicacion;
        private string organizador;
        private string organizador_mail;
        private string descripcion;

        public String Title { get { return title; } set { title = value; } }
        public String Start { get { return start; } set { start = value; } }
        public String End { get { return end; } set { end = value; } }
        public String BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } }
        public String BorderColor { get { return borderColor; } set { borderColor = value; } }
        public String Ubicacion { get { return ubicacion; } set { ubicacion = value; } }
        public String Organizador { get { return organizador; } set { organizador = value; } }
        public String Organizador_mail { get { return organizador_mail; } set { organizador_mail = value; } }
        public String Descripcion { get { return borderColor; } set { descripcion = value; } }

        /// <summary>
        /// Instancia un nuevo evento para FullCalendar
        /// </summary>
        /// <param name="title"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="borderColor"></param>
        public Event(string title,string start,string end,string backgroundColor,string borderColor, string organizador, string ubicacion, 
            string descripcion,string organizador_mail)
        {
            this.title = title;
            this.start = start;
            this.end = end;
            this.backgroundColor = backgroundColor;
            this.borderColor = borderColor;
            this.organizador = organizador;
            this.organizador_mail = organizador_mail;
            this.descripcion = descripcion;
            this.ubicacion = ubicacion;
        }
    }
}
