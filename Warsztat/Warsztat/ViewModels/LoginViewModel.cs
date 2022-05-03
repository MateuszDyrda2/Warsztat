using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warsztat.Commands;

namespace Warsztat.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        private ICommand _loginCommand;

        private string sth;

        private string usernameText { get; set; }

        public LoginViewModel()
        {
            //sth = "COS";
            LoginCommand = new RelayCommand(LogIn);
        }

        private void LogIn(object obj)
        {
            //usernameText = new RelayCommand();
            if(usernameText == null)
            {
                MessageBox.Show("The username field is empty!");
            }
            else
            MessageBox.Show("Logged in!");
        }

        public ICommand LoginCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropetryChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
