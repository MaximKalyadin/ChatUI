using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ClientToServerApi;
using ClientToServerApi.Models;
using ClientToServerApi.Enums;
using System.Threading.Tasks;

namespace ChatUi
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private readonly ClientServerAPI clientServerAPI_;
        public AuthorizationWindow()
        {
            InitializeComponent();
            clientServerAPI_ = ClientServerAPI.GetInstanse();
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
            var emailTextBox = this.TabControl.Template.FindName("EmailTextBox", this.TabControl) as TextBox;
            var passwordRegistrationBox = this.TabControl.Template.FindName("PasswordRegistrationBox", this.TabControl) as PasswordBox;
            if(!string.IsNullOrEmpty(emailTextBox.Text) && !string.IsNullOrEmpty(passwordRegistrationBox.Password))
            {
                var taskSend = clientServerAPI_.SendAsync(new OperationMessageToServer()
                {
                    Operation = Operations.Registration,
                    Data = emailTextBox.Text + "," + passwordRegistrationBox.Password
                });
                if (taskSend.Result.OperationResult == OperationsResults.UnsuccessfullyRegistration)
                {
                    emailTextBox.Text = string.Empty;
                    var labelHint = ((emailTextBox.Style.Resources["CueBannerBrush"] as VisualBrush)?.Visual as Label);
                    labelHint.Content = taskSend.Result.Info;
                    labelHint.Foreground = new SolidColorBrush(Color.FromRgb(102, 0, 20));
                }
                else if(taskSend.Result.OperationResult == OperationsResults.SuccessfullyRegistration)
                {
                    if(this.TabControl.Template.FindName("ConfirmRegistrationTabItem", this.TabControl) is TabItem tabItem)
                    tabItem.IsSelected = true;
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //код, притянутый за уши

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
