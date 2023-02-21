using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ejednevnichek__
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICRUD
    {
        public MainWindow()
        {
            InitializeComponent();
            DatePicker.Text = Convert.ToString(DateTime.Today);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void notesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                List<Notes> DataPick = Notes.MyNotesDs.Where(x => x.date1.Date == Convert.ToDateTime(DatePicker.Text)).ToList();
                NameOfNote.Text = DataPick[notesListBox.SelectedIndex].name;
                DescriptionOfNote.Text = DataPick[notesListBox.SelectedIndex].description; // Обновление заметки 
            }
            catch (Exception ex)
            {

            }

        }
        public void Create()
        {
            DateTime CurrentDate = Convert.ToDateTime(DatePicker.Text);
            Notes Note = new Notes(NameOfNote.Text, DescriptionOfNote.Text, CurrentDate); //Запихиваем Конструктор 
            Notes.MyNotesDs.Add(Note);
            GenericMethod.Serialize(Notes.MyNotesDs, "JsonNotes.json");
            Read(); //Для отображения в листбоксе обновляем данные с помощью сереализации 
        }
        public void Delete()
        {
            DateTime CurrentDate = Convert.ToDateTime(DatePicker.Text);

            int changingindex = Notes.MyNotesDs.FindIndex(x => x.name == NameOfNote.Text && x.description == DescriptionOfNote.Text && x.date1 == CurrentDate);
            Notes.MyNotesDs.Remove(Notes.MyNotesDs[changingindex]);
            GenericMethod.Serialize(Notes.MyNotesDs, "JsonNotes.json");
            NameOfNote.Text = "";
            DescriptionOfNote.Text = "";
            Read();
            return;
        }
        public void Read()
        {
            Notes.MyNotesDs = GenericMethod.Deserialize<List<Notes>>("JsonNotes.json");
            List<Notes> DataPick = Notes.MyNotesDs.Where(x => x.date1.Date == Convert.ToDateTime(DatePicker.Text)).ToList(); //Чтобы выводились заметки лишь на выбранную дату 
            notesListBox.ItemsSource = DataPick.Select(x => x.name);
        }
        public void Update()
        {
            List<Notes> DataPick = Notes.MyNotesDs.Where(x => x.date1.Date == Convert.ToDateTime(DatePicker.Text)).ToList();
            DateTime CurrentDate = Convert.ToDateTime(DatePicker.Text);
            string notename = DataPick[notesListBox.SelectedIndex].name;
            string notedescription = DataPick[notesListBox.SelectedIndex].description;
            int changingindex = Notes.MyNotesDs.FindIndex(x => x.name == notename && x.description == notedescription && x.date1 == CurrentDate);
            Notes.MyNotesDs[changingindex].name = NameOfNote.Text;
            Notes.MyNotesDs[changingindex].description = DescriptionOfNote.Text;
            GenericMethod.Serialize(Notes.MyNotesDs, "JsonNotes.json");
            Read();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Read();
        }
    }
}
