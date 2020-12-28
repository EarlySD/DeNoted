using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections.ObjectModel;

namespace DeNoted
{
    public partial class NoteEditor : ContentPage, INotifyPropertyChanged
    {

        private Note currentNote;
        public ObservableCollection<Note> list, tempList;
        // public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public NoteEditor()
        {
            InitializeComponent();
            //var highlighter = new HighlightMarker()

        }


        public NoteEditor(ObservableCollection<Note> list)
        {
            InitializeComponent();
            currentNote = null;
            this.list = list;

        }

        public NoteEditor(ObservableCollection<Note> list, Note note)
        {
            InitializeComponent();
            currentNote = note;

            Editor editor = (Editor)FindByName("editor");
            editor.Text = note.text;
            this.list = list;

           // var highlighter = new HighlightMarker(currentNote.text, "test");


        }

        private void addNote()
        {
            if (string.IsNullOrWhiteSpace(editor.Text))
            {
                if (currentNote != null)
                {
                    list.Remove(list.FirstOrDefault(t => t.text.Equals(currentNote.text)));
                }
            }
            else
            {
                //if editing a new note
                if (currentNote == null)
                {
                    string newTitle;
                    using (var reader = new StringReader(editor.Text))
                    {
                        newTitle = reader.ReadLine();
                    }
                    list.Add(new Note(newTitle, editor.Text));
                }
                else
                {   //if note was edited
                    if (!editor.Text.Equals(currentNote.text))
                    {
                        Note tempNote = new Note();
                        list.Remove(list.FirstOrDefault(t => t.title.Equals(currentNote.title)));

                        tempNote.text = editor.Text;
                        tempNote.LastEdited = DateTime.Now;
                        using (var reader = new StringReader(editor.Text))
                        {
                            tempNote.title = reader.ReadLine();
                        }
                        list.Add(tempNote);
                    }
                }
            }

            // list.OrderByDescending(x => x.LastEdited);
            list = new ObservableCollection<Note>(list.OrderBy(x => x.LastEdited).Reverse().ToList());

            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

            File.WriteAllText(file, JsonConvert.SerializeObject(list));
            NotifyPropertyChanged("TempList");
        }

        private async void Rhyme_Clicked(object sender, EventArgs e)
        {
            if (currentNote != null)
            {
                addNote();
                var diag = new HighlightView(currentNote.text);
                Navigation.PushModalAsync(diag);
            }
        }


        private async void Back_Clicked(object sender, EventArgs e)
        {
            //if note is empty we delete.
            addNote();
          
            await Navigation.PopModalAsync();
        }



        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
