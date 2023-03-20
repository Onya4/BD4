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

    public partial class MainWindow : Window
    {
        playersTableAdapter players = new playersTableAdapter();
        groopTableAdapter groop = new groopTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
            //вызываем таблицу players
            VoleybalGrid.ItemsSource = players.GetData();
            //считываем данные таблицы groop
            combogroop.ItemsSource = groop.GetData();
            //показываем данные какого столбца надо отображать в ComboBox
            combogroop.DisplayMemberPath = "number groop";
            //показываем какие данные, соответствующие данным пролшой строки, надо добавить в БД
            combogroop.SelectedValuePath = "groop id";
        }
        private void Players_Click(object sender, RoutedEventArgs e)
        {

        }
        private void two_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void groopV_Click(object sender, RoutedEventArgs e)
        {
            //вызываем вторую страницу
            two.Content = new Page1();
        }

        private void setplay_Click(object sender, RoutedEventArgs e)
        {
            //добавляем в БД новые данные
            players.InsertQuery(family.Text, name.Text, Convert.ToInt32(combogroop.SelectedValue));
            //обновляем БД
            VoleybalGrid.ItemsSource = players.GetData();
        }

        private void combogroop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            //сохраняем в переменную id айдишник строки, выбранный пользователем
            int id = (int)(VoleybalGrid.SelectedItem as DataRowView).Row[0];
            //удаляем соответствующую строку
            players.DeleteQuery(id);
            //обновляем БД
            VoleybalGrid.ItemsSource = players.GetData();
        }

        private void apdate_Click(object sender, RoutedEventArgs e)
        {
            if (VoleybalGrid.SelectedItem != null)
            {
                var play = VoleybalGrid.SelectedItem as DataRowView;
                //загружаем данные в нашу базу данных
                players.UpdateQuery(family.Text, name.Text, (int)combogroop.SelectedValue, (int)play.Row[0]);
                //обновляем базу данных
                VoleybalGrid.ItemsSource = players.GetData();
            }
        }

        private void VoleybalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверка на номер выбранной строки
            if (VoleybalGrid.SelectedItem != null)
            {
                //загружаем данные строки в переменные
                var play = VoleybalGrid.SelectedItem as DataRowView;
                //выгружаем в текстовые поля соответствующие данные
                family.Text = (string)play.Row[1];
                name.Text = (string)play.Row[2];
                combogroop.SelectedValue = (int)play.Row[3];
            }
        }
    }
}
