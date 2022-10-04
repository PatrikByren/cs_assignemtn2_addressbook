using addressbook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ContactPerson> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<ContactPerson>();
            lvContacts.ItemsSource = contacts;

        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var contact = contacts.FirstOrDefault(x => x.Email == tbEmail.Text);
            if (contact == null)
            {
                contacts.Add(new ContactPerson
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    PhoneNumber = tbPhoneNumber.Text,
                    Email = tbEmail.Text,
                    StreetAddress = tbStreetAddress.Text,
                    PostalCode = tbPostalCode.Text,
                    City = tbCity.Text
                });
            }
            else { MessageBox.Show("Det finns redan en person med denna emailadress!"); }
            ClearContactInfoField();
        }

        private void btnClearInfo_Click(object sender, RoutedEventArgs e)
        {
            ClearContactInfoField();
        }
        private void ClearContactInfoField()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbPhoneNumber.Text = "";
            tbEmail.Text = "";
            tbStreetAddress.Text = "";
            tbPostalCode.Text = "";
            tbCity.Text = "";
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contact = (ContactPerson)button!.DataContext;
            contacts.Remove(contact);


        }

        private void lvContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact = (ContactPerson)lvContacts.SelectedItem; //[0]!;
            tbFirstName.Text = contact.FirstName;
            tbLastName.Text = contact.LastName;
            tbPhoneNumber.Text = contact.PhoneNumber;
            tbEmail.Text = contact.Email;
            tbStreetAddress.Text = contact.StreetAddress;
            tbPostalCode.Text = contact.PostalCode;
            tbCity.Text = contact.City;
        }
    }
}
