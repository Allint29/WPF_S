using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace WpfTestApp.Models
{
    public class Phone2 : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty PriceProperty;
        public static readonly DependencyProperty CompanyProperty;

        static Phone2()
        {
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Phone2));
            CompanyProperty = DependencyProperty.Register("Company", typeof(string), typeof(Phone2));
            
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.CoerceValueCallback = new CoerceValueCallback(CorrectValue);
            PriceProperty = DependencyProperty.Register("Price", 
                typeof(int), 
                typeof(Phone2), 
                metadata,
                new ValidateValueCallback(ValidateValue));

        }

        private static object CorrectValue(DependencyObject d, object baseValue)
        {
            int currentValue = (int)baseValue;
            if (currentValue > 1000) return 1000;
            if (currentValue < 0) return 0;
            return currentValue; // иначе возвращаем текущее значение
        }

        private static bool ValidateValue(object value)
        {
            int currentValue = (int)value;
            if (currentValue >= 0) // если текущее значение от нуля и выше
                return true;
            return false;
        }
        
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
                OnPropertyChanged("Title");
            }
        }
        public int Price
        {
            get { return (int)GetValue(PriceProperty); }
            set
            {
                SetValue(PriceProperty, value);
                OnPropertyChanged("Price");
            }
        }

        public string Company
        {
            get { return (string)GetValue(CompanyProperty); }
            set
            {
                SetValue(CompanyProperty, value);
                OnPropertyChanged("Company");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
