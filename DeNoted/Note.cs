using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace DeNoted
{
    [Serializable]
    public class Note
    {
        public string text { get; set; }
        public DateTime LastEdited { get; set; }
        public string title { get; set; }
       // public FormattedString highlights { get; set; }

        public Note()
        {
            text = " ";
            title = " ";
            LastEdited = DateTime.MinValue;
           // highlights = " ";
        }

        public Note(string title, string text)
        {
            this.title = title;
            this.text = text;
            LastEdited = DateTime.Now;
        }
        public override string ToString()
        {
            return title + " (" + LastEdited.ToString("MM/dd/yyyy h:mm tt") + ") ";
           
        }
    }
}
