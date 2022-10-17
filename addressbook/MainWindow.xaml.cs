using addressbook.Models;
using addressbook.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace addressbook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ContactPerson>? _contacts; //Min lista
        private readonly IFileService _fileService = new FileService(); //Instanscerar min FileService
        private readonly string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\wpf.Json"; //Min filväg
        private bool saveUpdate; //En bool som styr om en kontakt ska uppdateras eller sparas som ny
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileService.Read(_filePath));//När programmet startas hämta listan från fil
                lvContacts.ItemsSource = _contacts; //Skapa listan med infon från fil
            }
            catch
            {            
                _contacts = new ObservableCollection<ContactPerson>(); //Finns inte listan på fil skapas det en ny lista
                lvContacts.ItemsSource = _contacts;

            }
        }
        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (saveUpdate == false) //Om false ny kontakt
            {
                try
                {
                    _contacts!.Add(new ContactPerson //Skapa en ny kontakt
                    {
                        FirstName = tbFirstName.Text,
                        LastName = tbLastName.Text,
                        PhoneNumber = tbPhoneNumber.Text,
                        Email = tbEmail.Text,
                        StreetAddress = tbStreetAddress.Text,
                        PostalCode = tbPostalCode.Text,
                        City = tbCity.Text
                    });
                    _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts)); //Spara kontakten till listan
                }
                catch
                {
                    ErrorText(); // Om det inte fungerar meddela det
                }
            }
            else //Om saveUpdate==true uppdatera kontakten
            {
                try
                {
                    var contact = (ContactPerson)lvContacts.SelectedItem; //Uppdatera en kontakt
                    contact.FirstName = tbFirstName.Text;
                    contact.LastName = tbLastName.Text;
                    contact.PhoneNumber = tbPhoneNumber.Text;
                    contact.Email = tbEmail.Text;
                    contact.StreetAddress = tbStreetAddress.Text;
                    contact.PostalCode = tbPostalCode.Text;
                    contact.City = tbCity.Text;
                    _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts)); //Spara den uppdaterade kontakten till listan
                }
                catch { ErrorText(); } // Om det inte fungerar meddela det 
            }

            try
            {
                _contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileService.Read(_filePath)); //Hämta den ny sparade listan
                lvContacts.ItemsSource = _contacts; //Visa upp listan i det grafiska 
            }
            catch { }
            ClearContactInfoField(); // Rensa fälten efter en ny kontakt är sparad eller uppdaterad
        }
        public void btnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            
            ClearContactInfoField(); //Om man använder sig av "Clear info fields" skickas man direkt till denna metod

        }
        public void ClearContactInfoField() //Rensar fälten 
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbPhoneNumber.Text = "";
            tbEmail.Text = "";
            tbStreetAddress.Text = "";
            tbPostalCode.Text = "";
            tbCity.Text = "";
            btnSave.Content = "SAVE CONTACT"; //Ändrar content på texten
            saveUpdate = false; //Uppdatera boolen
        }

        public void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button; //Om jag trycker på Delete knappen
                var contact = (ContactPerson)button!.DataContext; //Ta fram den markerade kontakten
                _contacts!.Remove(contact); //Rensa från listan
                _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));//Spara listan
                ClearContactInfoField();//Rensa fälten
            }
            catch 
            {

            }
        }

        public void lvContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact = (ContactPerson)lvContacts.SelectedItem; //Tar fram kontakter i den grafiska listan
            if (contact != null) //Så länge den inte är null, den kan bli det om man trycker på samma kontakt igen efter rensat fälten, eller vill radera kontakt efter rensat fält
            {
                tbFirstName.Text = contact.FirstName; 
                tbLastName.Text = contact.LastName;
                tbPhoneNumber.Text = contact.PhoneNumber;
                tbEmail.Text = contact.Email;
                tbStreetAddress.Text = contact.StreetAddress;
                tbPostalCode.Text = contact.PostalCode;
                tbCity.Text = contact.City; //Uppdatera kontakten
                btnSave.Content = "SAVE UPDATE"; //Ändra texten på knappen
                saveUpdate = true; //Uppdatera boolen
            }
        }
        public void ErrorText()
        {
            MessageBox.Show("Gratz, you found an Error! Try something else!"); //Om man lyckas hitta en bug meddelas det och metoden nedan körs igen
            ClearContactInfoField();
        }

    }
}
