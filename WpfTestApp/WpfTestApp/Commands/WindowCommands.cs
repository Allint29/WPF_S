using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfTestApp.Commands
{

    public class WindowCommands
    {
        static WindowCommands()
        {
            Exit = new RoutedCommand("Exit", typeof(MainWindow));
        }
        public static RoutedCommand Exit { get; set; }
    }
}
