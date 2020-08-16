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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatUi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsFitToWidth { set; get; } 

        public MainWindow()
        {
            InitializeComponent();
            IsFitToWidth = false;
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void FitToWidthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFitToWidth)
            {
                SystemCommands.MaximizeWindow(this);
                (this.FitToWidthButton.Content as Image).Source = new BitmapImage(new Uri(@"/ChatUi;component/Assets/SystemButtons/collapse32px.png", UriKind.Relative));
                IsFitToWidth = true;
            }
            else
            {
                SystemCommands.RestoreWindow(this);
                (this.FitToWidthButton.Content as Image).Source = new BitmapImage(new Uri(@"/ChatUi;component/Assets/SystemButtons/fitToWidth_32px.png", UriKind.Relative));
                IsFitToWidth = false;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
