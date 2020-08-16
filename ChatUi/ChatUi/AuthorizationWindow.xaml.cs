using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatUi
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passBox = sender as PasswordBox;
            if (passBox != null && !string.IsNullOrEmpty(passBox.Password))
                passBox.Background = null;
            else if (passBox != null)
                passBox.Background = (Brush)passBox.Style.Resources["CueBannerBrush"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void RegistrationNextButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.TabControl.Template.FindName("ConfirmRegistrationTabItem", this.TabControl) is TabItem tabItem)
                tabItem.IsSelected = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //код, притянутый за уши
            var tabControl = sender as TabControl;
            if(tabControl != null)
            {
                var tabItem = e.AddedItems[0] as TabItem;
                var doubleAnimationStackPanel = new DoubleAnimation
                {
                    From = 0,
                    To = 300,
                    Duration = TimeSpan.FromSeconds(0.5),
                    AccelerationRatio = 1,
                    FillBehavior = FillBehavior.HoldEnd
                };
                (tabItem.Content as StackPanel).BeginAnimation(StackPanel.WidthProperty, doubleAnimationStackPanel);
            }
        }
    }
}
