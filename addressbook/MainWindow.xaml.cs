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
        private ObservableCollection<ContactPerson> _contacts;
        private FileService _fileService = new FileService();
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\wpf.Json";
        private bool saveUpdate;


        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileService.Read(_filePath));
                lvContacts.ItemsSource = _contacts;
            }
            catch
            {            
                _contacts = new ObservableCollection<ContactPerson>();
                lvContacts.ItemsSource = _contacts;

            }
        }
        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
   
            if (saveUpdate == false)
            {
                _contacts.Add(new ContactPerson
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    PhoneNumber = tbPhoneNumber.Text,
                    Email = tbEmail.Text,
                    StreetAddress = tbStreetAddress.Text,
                    PostalCode = tbPostalCode.Text,
                    City = tbCity.Text
                });
                _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));
            }
            else
            {
                var contact = (ContactPerson)lvContacts.SelectedItem;
                contact.FirstName = tbFirstName.Text;
                contact.LastName = tbLastName.Text;
                contact.PhoneNumber = tbPhoneNumber.Text;
                contact.Email = tbEmail.Text;
                contact.StreetAddress = tbStreetAddress.Text;
                contact.PostalCode = tbPostalCode.Text;
                contact.City = tbCity.Text;
                _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));
            }
            try
            {
                _contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileService.Read(_filePath));
                lvContacts.ItemsSource = _contacts;
            }
            catch
            { }
            ClearContactInfoField();  
        }
        public void btnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            saveUpdate = false;
            ClearContactInfoField();
        }
        public void ClearContactInfoField()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbPhoneNumber.Text = "";
            tbEmail.Text = "";
            tbStreetAddress.Text = "";
            tbPostalCode.Text = "";
            tbCity.Text = "";
            btnSave.Content = "SAVE CONTACT";
            var contact =(ContactPerson)lvContacts.SelectedItem;
            contact = null!;
        }

        public void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contact = (ContactPerson)button!.DataContext;
            _contacts.Remove(contact);
            _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));
        }

        public void lvContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact = (ContactPerson)lvContacts.SelectedItem;
            tbFirstName.Text = contact.FirstName;
            tbLastName.Text = contact.LastName;
            tbPhoneNumber.Text = contact.PhoneNumber;
            tbEmail.Text = contact.Email;
            tbStreetAddress.Text = contact.StreetAddress;
            tbPostalCode.Text = contact.PostalCode;
            tbCity.Text = contact.City;
            btnSave.Content = "SAVE UPDATE";
            saveUpdate=true;
        }


    }
}
