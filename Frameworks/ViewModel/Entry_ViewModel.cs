using System.ComponentModel;
using System.Windows;
using Frameworks.Model.Services;
using Frameworks.Model;
using Frameworks.Model.Subject;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Frameworks.View;

namespace Frameworks.ViewModel
{
    public class Entry_ViewModel : INotifyPropertyChanged
    {
        private Entry entryModel = new Entry();
        private Person person = new Person("", "", "", "", "", "");
        public ICommand MyCommand { get; set; }
        public ICommand MyCommand1 { get; set; }

        private ObservableCollection<string> items = new ObservableCollection<string> { "Client", "Courier", "Admin" };
        public ObservableCollection<string> Items { get { return items; } }
        public Visibility FirstElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SecondElementsVisibility { get; set; } = Visibility.Collapsed;
        public ICommand ShowFirstElementsCommand { get; private set; }
        public ICommand ShowSecondElementsCommand { get; private set; }

        public Entry_ViewModel()
        {
            MyCommand = new RelayCommand(ExecuteMyCommand, CanExecuteMyCommand);
            ShowFirstElementsCommand = new RelayCommand(ShowFirstElements);
            ShowSecondElementsCommand = new RelayCommand(ShowSecondElements);
            MyCommand1 = new RelayCommand(ExecuteMyCommand1, CanExecuteMyCommand1);
        }

        private void ShowFirstElements(object parameter)
        {
            FirstElementsVisibility = Visibility.Visible;
            SecondElementsVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(FirstElementsVisibility));
            OnPropertyChanged(nameof(SecondElementsVisibility));
        }
        private void ShowSecondElements(object parameter)
        {
            FirstElementsVisibility = Visibility.Collapsed;
            SecondElementsVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(FirstElementsVisibility));
            OnPropertyChanged(nameof(SecondElementsVisibility));
        }
        
        public string PhoneNumber_E
        {
            get { return entryModel.phone_number; }
            set
            {
                entryModel.phone_number = value;
                OnPropertyChanged(nameof(PhoneNumber_E));
            }
        }
        public string Password_E
        {
            get { return entryModel.password; }
            set
            {
                entryModel.password = value;
                OnPropertyChanged(nameof(Password_E));
            }
        }
        private void ExecuteMyCommand(object parameter)
        {
            IPerson person = new Person_Service();
            if (person.Check_Login_Password(entryModel))
            {
                int id_person = person.Get_Id_Person_By_Phone_Number(entryModel.phone_number);
                if (person.Get_Role_By_Phone_Number(entryModel.phone_number) == "CLIENT")   new Client_Window(id_person).Show();
                else if (person.Get_Role_By_Phone_Number(entryModel.phone_number) == "ADMIN")   new Admin_Window(id_person).Show();
                else if (person.Get_Role_By_Phone_Number(entryModel.phone_number) == "COURIER")     new Courier_Window(id_person).Show();
                if (parameter is Window window) window.Close();
            }
        }
        private bool CanExecuteMyCommand(object parameter)  {   return true;    }
 
        public string FirstName
        {
            get { return person.first_name; }
            set
            {
                person.first_name = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string SecondName
        {
            get { return person.second_name; }
            set
            {
                person.second_name = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }
        public string SurName
        {
            get { return person.sur_name; }
            set
            {
                person.sur_name = value;
                OnPropertyChanged(nameof(SurName));
            }
        }
        public string PhoneNumber
        {
            get { return person.phone_number; }
            set
            {
                person.phone_number = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public string PhotoPath
        {
            get { return person.photo_path; }
            set
            {
                person.photo_path = value;
                OnPropertyChanged(nameof(PhotoPath));
            }
        }
        public string Password
        {
            get { return person.password; }
            set
            {
                person.password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string selectedValue = "";
        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                if (selectedValue != value)
                {
                    selectedValue = value;
                    OnPropertyChanged(nameof(SelectedValue));
                }
            }
        }
        private void ExecuteMyCommand1(object parameter)
        {
            person.role = SelectedValue;
            if (person.first_name != "" && person.second_name != "" && person.sur_name != "" && person.photo_path != "" && person.phone_number != "" && person.role != null && person.password != "" && SelectedValue != "")
            {
                int error = 0;
                if (person.role == "Courier")       error = new Person_Service().Register_Person(new Courier(FirstName, SecondName, SurName, PhotoPath, PhoneNumber, Password));               
                else if (person.role == "Admin")    error = new Person_Service().Register_Person(new Admin(FirstName, SecondName, SurName, PhotoPath, PhoneNumber, Password));                
                else if(person.role == "Client")    error = new Person_Service().Register_Person(new Client(FirstName, SecondName, SurName, PhotoPath, PhoneNumber, Password));                                
                if (error == Constants.ERROR_WRONGFORMAT_PHONENUMBER) 
                {
                    MessageBox.Show("Неправильний формат номеру телефона. Приклад привильного: 0XX-XXX-XXXX");
                }
                else
                {
                    new Login_Registration_Windows().Show();
                    if (parameter is Window window) window.Close();
                }
            }
        }
        private bool CanExecuteMyCommand1(object parameter) {   return true;    }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
