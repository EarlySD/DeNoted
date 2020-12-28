using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace DeNoted
{
    public class NoteController : ContentPage
    {
        string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");
        public ObservableCollection<Note> NotesList { get; set; } = new ObservableCollection<Note>();

        //public event PropertyChangedEventHandler PropertyChanged;
        private Note selectedNote;

        public NoteController()
        {
            selectedNote = new Note();

            if (File.Exists(file))
            {
                string listOfNotes = File.ReadAllText(file);

               List<Note> Temp = (List<Note>)JsonConvert.DeserializeObject<IEnumerable<Note>>(listOfNotes);
                Temp.ForEach(NotesList.Add);
                NotesList = new ObservableCollection<Note>(NotesList.OrderBy(x => x.LastEdited).Reverse().ToList());
            }
        }

        public Note SelectedNote
        {
            get
            {
                return selectedNote;
            }

            set
            {
                selectedNote = value;
            }
        }
 
    }
}
