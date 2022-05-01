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

namespace Warsztat.View
{
    public partial class Admin : Window
    {
        public int AdminId { get; set; }
        public Service Service { get; set; }
        public Admin()
        {
            InitializeComponent();

            if (Service == null)
                throw new NullReferenceException();


        }
    }
}
