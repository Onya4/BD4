using System;
using System.Collections.Generic;
using System.Data;
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
using BD4.masterDataSetTableAdapters;

namespace BD4
{

    public partial class Page1 : Page
    {
        groopTableAdapter groop = new groopTableAdapter();
        public Page1()
        {
            InitializeComponent();
            VolGrid.ItemsSource = groop.GetData();
        }
        private void VoleybalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VolGrid.SelectedItem != null)
            {
                var play = VolGrid.SelectedItem as DataRowView;
                num.Text = (string)play.Row[1];
                years.Text = Convert.ToString((int)play.Row[2]);
                local.Text = (string)play.Row[3];
            }
        }

        private void setgroop_Click(object sender, RoutedEventArgs e)
        {
            groop.InsertQuery(num.Text, Convert.ToInt32(years.Text), local.Text);
            VolGrid.ItemsSource = groop.GetData();

            (Application.Current.MainWindow as MainWindow).combogroop.ItemsSource = groop.GetData();
        }

        private void delgroop_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(VolGrid.SelectedItem as DataRowView).Row[0];
            groop.DeleteQuery(id);
            VolGrid.ItemsSource = groop.GetData();
        }

        private void apgroop_Click(object sender, RoutedEventArgs e)
        {
            if (VolGrid.SelectedItem != null)
            {
                var play = VolGrid.SelectedItem as DataRowView;
                groop.UpdateQuery(num.Text, Convert.ToInt32(years.Text), local.Text, (int)play.Row[0]);
                VolGrid.ItemsSource = groop.GetData();
            }
        }
    }
}
