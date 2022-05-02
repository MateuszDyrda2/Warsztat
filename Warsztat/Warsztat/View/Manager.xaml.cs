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
using System.Windows.Shapes;
using Warsztat.Services;

namespace Warsztat.View
{
    public partial class Manager : Window
    {
        public Service Service { get; set; }
        private int Page = 1;
        public Manager()
        {
            InitializeComponent();

            if (Service == null)
                throw new NullReferenceException();

            CurrentPage.Text = "Clients";
        }
        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                switch (Page)
                {
                    case 1:
                        Page++;
                        break;
                    case 2:
                        Page++;
                        break;
                    case 3:
                        Page++;
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
    }
}
