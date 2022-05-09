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
    public delegate void DataTransfer(List<string> data);
    public partial class MyPopup : Window
    {
        private List<TextBlock>? textBlocks;
        public List<TextBlock> TextBlocks
        {
            set
            {
                textBlocks = value;
                for (int i = 0; i < textBlocks.Count; i++)
                {
                    Grid.SetColumn(textBlocks[i], 0);
                    Grid.SetRow(textBlocks[i], i);
                    grid.Children.Add(textBlocks[i]);
                }
            }
        }
        private List<TextBox>? textBoxes;
        public List<TextBox> TextBoxes
        {
            set
            {
                textBoxes = value;
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    Grid.SetColumn(textBoxes[i], 1);
                    Grid.SetRow(textBoxes[i], i);
                    grid.Children.Add(textBoxes[i]);
                }
            }
        }
        private List<ComboBox>? comboBoxes;
        public List<ComboBox> ComboBoxes
        {
            set
            {
                comboBoxes = value;
                for (int i = 0; i < comboBoxes.Count; i++)
                {
                    Grid.SetColumn(comboBoxes[i], 1);
                    Grid.SetRow(comboBoxes[i], i + textBoxes!.Count);
                    grid.Children.Add(comboBoxes[i]);
                }
            }
        }
        private DataTransfer? dt;
        public DataTransfer Dt
        {
            set => dt = value;
        }
        public MyPopup()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           List<string> data = new();

           foreach (TextBox textBox in textBoxes!)
           {
                data.Add(textBox.Text);
           }

            foreach (ComboBox comboBox in comboBoxes!)
            {
                if (comboBox.SelectedItem is Service.Personel worker)
                    data.Add(worker.Id.ToString());
                else if (comboBox.SelectedItem is string role)
                    data.Add(role);
                else
                    data.Add(string.Empty);
            }

            dt!.Invoke(data);

            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class MyPopupBuilder
    {
        private List<TextBlock> textBlocks = new();
        private List<TextBox> textBoxes = new();
        private List<ComboBox> comboBoxes = new();
        private DataTransfer? dt;

        public MyPopupBuilder TextBox(string value)
        {
            TextBlock textBlock = new();
            textBlock.Name = $"{value.Replace(" ", "")}TextBlock";
            textBlock.Text = $"{value}";
            textBlocks.Add(textBlock);

            TextBox textBox = new();
            textBox.Name = $"{value.Replace(" ", "")}";
            textBoxes.Add(textBox);

            return this;
        }
        public MyPopupBuilder ComboBox(List<Service.Personel> workers)
        {
            TextBlock textBlock = new();
            textBlock.Name = $"WorkerTextBlock";
            textBlock.Text = $"Worker";
            textBlocks.Add(textBlock);

            ComboBox comboBox = new();
            foreach (Service.Personel worker in workers)
                comboBox.Items.Add(worker);
            comboBoxes.Add(comboBox);

            return this;
        }
        public MyPopupBuilder ComboBox(List<string> roles)
        {
            TextBlock textBlock = new();
            textBlock.Name = $"RoleTextBlock";
            textBlock.Text = $"Role";
            textBlocks.Add(textBlock);

            ComboBox comboBox = new();
            foreach (string role in roles)
                comboBox.Items.Add(role);
            comboBox.SelectedIndex = 0;
            comboBoxes.Add(comboBox);

            return this;
        }
        public MyPopupBuilder DataTransfer(DataTransfer dt)
        {
            this.dt = dt;
            return this;
        }

        public MyPopup Build()
        {
            MyPopup popup = new()
            {
                TextBlocks = textBlocks,
                TextBoxes = textBoxes,
                ComboBoxes = comboBoxes,
                Dt = dt!
            };
            return popup;
        }
    }
}
