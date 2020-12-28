using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Windows.Input;

namespace DeNoted
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        int check = 0;
   
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new NoteController();
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            var n = (BindingContext as NoteController).SelectedNote;
            if (n != null)
            {
                var diag = new NoteEditor((BindingContext as NoteController).NotesList, n);
                Navigation.PushModalAsync(diag);
            }
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            var diag = new NoteEditor((BindingContext as NoteController).NotesList);
            Navigation.PushModalAsync(diag);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            (BindingContext as NoteController).NotesList.Clear();
        }


    }
}
