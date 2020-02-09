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
using WpfTestApp.Models;

namespace WpfTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            var listOfPhones = new List<Phone>()
            {
                new Phone {Id = 0, Title="iPhone 6S", Company="Apple", Price=54990 },
                new Phone {Id = 6, Title="Lumia 950", Company="Microsoft", Price=39990 },
                new Phone {Id = 2, Title="Nexus 5X", Company="Google", Price=29990 }
            };

            this.Resources.Add("phones_list", listOfPhones);
            

            InitializeComponent();
            // определение объекта-ресурса
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Green, 1));
            
            // добавление ресурса в словарь ресурсов окна
            this.Resources.Add("buttonGradientBrush", gradientBrush);

            // установка ресурса у кнопки
            button1.Background = (Brush)this.TryFindResource("buttonGradientBrush");
            // или так
            //button1.Background = (Brush)this.Resources["buttonGradientBrush"];

            // создаем привязку команды
            CommandBinding commandBinding = new CommandBinding();
            // устанавливаем команду
            commandBinding.Command = ApplicationCommands.Help;
            // устанавливаем метод, который будет выполняться при вызове команды
            commandBinding.Executed += CommandBinding_Executed;
            // добавляем привязку к коллекции привязок элемента Button
            helpButton.CommandBindings.Add(commandBinding);



            ListBox.ItemsSource = listOfPhones;
            ListBox.DisplayMemberPath = "Title";

            // формируем содержимое вкладки в виде списка
            ListBox notesList = new ListBox();
            notesList.ItemsSource = from l in listOfPhones select l.Title;

            Products2.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Ноутбуки" }, // установка заголовка вкладки
                Content = notesList
                // установка содержимого вкладки
            });
            Products2.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Ноутбуки2" }, // установка заголовка вкладки
                Content = notesList
                // установка содержимого вкладки
            });

            PhonesList.ItemsSource = listOfPhones;
            PhonesList.DisplayMemberPath = "Title";
            PhonesList.IsEditable = true;
            PhonesList.SelectedItem = listOfPhones.FirstOrDefault(p => p.Title == "Lumia 950");


            PhonesList2.ItemsSource = listOfPhones;

            PhonesGrid.ItemsSource = listOfPhones;

            //PhonesGrid.RowBackground.
            txtBox.Background = new LinearGradientBrush(Colors.Red, Colors.Aquamarine, new Point(0.5,1),  new Point(0.5, 0));
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Справка по приложению");
        }

        private void list_Selected(object sender, SelectionChangedEventArgs e)
        {
            Phone p = (Phone)ListBox.SelectedItem;

            MessageBox.Show(ListBox.SelectedIndex.ToString());
        }

        private void PhonesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Phone p = (Phone)PhonesList.SelectedItem;

            MessageBox.Show(p.Id.ToString());
        }

        private void PhonesList2_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Phone p = (Phone)PhonesList2.SelectedItem;

            MessageBox.Show(p.Id.ToString());
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Phone2 phone = (Phone2)this.Resources["iPhone6s"]; // получаем ресурс по ключу
            MessageBox.Show(phone.Price.ToString());
        }

        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBlock1.Text = textBlock1.Text + "sender: " + sender.ToString() + "\n";
            textBlock1.Text = textBlock1.Text + "source: " + e.Source.ToString() + "\n\n";
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadio = (RadioButton)e.Source;
            textBlock1.Text = "Вы выбрали: " + selectedRadio.Content.ToString();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            MessageBox.Show("Координата x=" + p.X.ToString() + " y=" + p.Y.ToString());
        }


        private void textBox2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop(textBox2, textBox2.Text, DragDropEffects.Copy);
        }

        private void button2_Drop(object sender, DragEventArgs e)
        {
            button2.Content = e.Data.GetData(DataFormats.Text);
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("log.txt", true))
            {
                writer.WriteLine("Выход из приложения: " + DateTime.Now.ToShortDateString() + " " +
                                 DateTime.Now.ToLongTimeString());
                writer.Flush();
            }

            this.Close();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            Phone2 phone = (Phone2)this.Resources["iPhone6s"];
            phone.Price = 666; // Меняем с Google на LG
        }

    }
}
