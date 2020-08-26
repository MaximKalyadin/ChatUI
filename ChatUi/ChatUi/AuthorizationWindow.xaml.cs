using ClientToServerApi;
using ClientToServerApi.Enums;
using ClientToServerApi.Models;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ChatUi
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private readonly ClientServerService clientServerService_;
        public AuthorizationWindow()
        {
            InitializeComponent();
            clientServerService_ = ClientServerService.GetInstanse();
            clientServerService_.AddListener(ListenerType.RegistrationListener, RegistrationListener);
            clientServerService_.AddListener(ListenerType.AuthorizationListener, AuthorizationListener);
        }

        private void RegistrationListener(OperationResultInfo data)
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                if (data.OperationResult == OperationsResults.UnsuccessfullyRegistration)
                {
                    var emailTextBox = this.TabControl.Template.FindName("EmailTextBox", this.TabControl) as TextBox;
                    emailTextBox.Text = string.Empty;
                    var labelHint = ((emailTextBox.Style.Resources["CueBannerBrush"] as VisualBrush)?.Visual as Label);
                    labelHint.Content = data.Info;
                    labelHint.Foreground = new SolidColorBrush(Color.FromRgb(102, 0, 20));
                }
                else if (data.OperationResult == OperationsResults.SuccessfullyRegistration)
                {
                    if (this.TabControl.Template.FindName("ConfirmRegistrationTabItem", this.TabControl) is TabItem tabItem)
                        tabItem.IsSelected = true;
                }
            });
        }

        private void AuthorizationListener(OperationResultInfo data)
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                var loginTextBox = this.TabControl.Template.FindName("LoginTextBox", this.TabControl) as TextBox;
                if (data.OperationResult == OperationsResults.UnsuccessfullyAuthorization)
                {
                    loginTextBox.Text = string.Empty;
                    var labelHint = ((loginTextBox.Style.Resources["CueBannerBrush"] as VisualBrush)?.Visual as Label);
                    labelHint.Content = data.Info;
                    labelHint.Foreground = new SolidColorBrush(Color.FromRgb(102, 0, 20));
                }
                else if (data.OperationResult == OperationsResults.SuccessfullyAuthorization)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
            });
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
            var loginTextBox = this.TabControl.Template.FindName("LoginTextBox", this.TabControl) as TextBox;
            var passwordBox = this.TabControl.Template.FindName("PasswordBox", this.TabControl) as PasswordBox;
            if(string.IsNullOrEmpty(loginTextBox.Text))
            {
                SetNotFillFieldInfo(loginTextBox.Style);
                return;
            }
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                SetNotFillFieldInfo(passwordBox.Style);
                return;
            }
            clientServerService_.SendAsync(new OperationMessageToServer()
            {
                Operation = Operations.Authorization,
                Data = loginTextBox.Text + "," + passwordBox.Password
            });
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ClientServerService.ShutdownReceiving();
            SystemCommands.CloseWindow(this);
        }

        private void RegistrationNextButton_Click(object sender, RoutedEventArgs e)
        {
            var emailTextBox = this.TabControl.Template.FindName("EmailTextBox", this.TabControl) as TextBox;
            var passwordRegistrationBox = this.TabControl.Template.FindName("PasswordRegistrationBox", this.TabControl) as PasswordBox;
            var passwordConfirmRegistrationBox = this.TabControl.Template.FindName("PasswordConfirmRegistrationBox", this.TabControl) as PasswordBox;
            var nameTextBox = this.TabControl.Template.FindName("NameTextBox", this.TabControl) as TextBox;
            var secondNameTextBox = this.TabControl.Template.FindName("SecondNameTextBox", this.TabControl) as TextBox;

            if(string.IsNullOrEmpty(emailTextBox.Text))
            {
                SetNotFillFieldInfo(emailTextBox.Style);
                return;
            }
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                SetNotFillFieldInfo(nameTextBox.Style);
                return;
            }
            if (string.IsNullOrEmpty(secondNameTextBox.Text))
            {
                SetNotFillFieldInfo(secondNameTextBox.Style);
                return;
            }
            if (string.IsNullOrEmpty(passwordRegistrationBox.Password))
            {
                SetNotFillFieldInfo(passwordRegistrationBox.Style);
                return;
            }
            if(!passwordRegistrationBox.Password.Equals(passwordConfirmRegistrationBox.Password))
            {
                var labelHint = ((passwordConfirmRegistrationBox.Style.Resources["CueBannerBrush"] as VisualBrush)?.Visual as Label);
                passwordConfirmRegistrationBox.Password = null;
                labelHint.Content = "Неправильный пароль, попробуйте снова";
                return;
            }
            clientServerService_.SendAsync(new OperationMessageToServer()
            {
                Operation = Operations.Registration,
                Data = emailTextBox.Text + "," + nameTextBox.Text + "," + secondNameTextBox.Text + "," + passwordRegistrationBox.Password
            });
        }

        private void SetNotFillFieldInfo(Style ControlStyle)
        {
            var labelHint = ((ControlStyle.Resources["CueBannerBrush"] as VisualBrush)?.Visual as Label);
            labelHint.Content = "Заполните поле: " + labelHint.Content;
            labelHint.Foreground = new SolidColorBrush(Color.FromRgb(102, 0, 20));
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

        private void ShowRegistrationPasswordButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var passwordRegistrationBox = this.TabControl.Template.FindName("PasswordRegistrationBox", this.TabControl) as PasswordBox;
            var passwordRegistrationTextBox = this.TabControl.Template.FindName("PasswordRegistrationTextBox", this.TabControl) as TextBox;
            passwordRegistrationTextBox.Background = passwordRegistrationBox.Background;
            passwordRegistrationTextBox.Text = passwordRegistrationBox.Password;
            passwordRegistrationBox.Visibility = Visibility.Collapsed;
            passwordRegistrationTextBox.Visibility = Visibility.Visible;
        }

        private void ShowRegistrationPasswordButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var passwordRegistrationBox = this.TabControl.Template.FindName("PasswordRegistrationBox", this.TabControl) as PasswordBox;
            var passwordRegistrationTextBox = this.TabControl.Template.FindName("PasswordRegistrationTextBox", this.TabControl) as TextBox;
            passwordRegistrationBox.Visibility = Visibility.Visible;
            passwordRegistrationTextBox.Visibility = Visibility.Collapsed;
        }

        private void ShowRegistrationConfirmPasswordButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var passwordConfirmRegistrationBox = this.TabControl.Template.FindName("PasswordConfirmRegistrationBox", this.TabControl) as PasswordBox;
            var passwordConfirmRegistrationTextBox = this.TabControl.Template.FindName("PasswordRegistrationConfirmTextBox", this.TabControl) as TextBox;
            passwordConfirmRegistrationTextBox.Background = passwordConfirmRegistrationBox.Background;
            passwordConfirmRegistrationTextBox.Text = passwordConfirmRegistrationBox.Password;
            passwordConfirmRegistrationBox.Visibility = Visibility.Collapsed;
            passwordConfirmRegistrationTextBox.Visibility = Visibility.Visible;
        }

        private void ShowRegistrationConfirmPasswordButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var passwordConfirmRegistrationBox = this.TabControl.Template.FindName("PasswordConfirmRegistrationBox", this.TabControl) as PasswordBox;
            var passwordRegistrationConfirmTextBox = this.TabControl.Template.FindName("PasswordRegistrationConfirmTextBox", this.TabControl) as TextBox;
            passwordConfirmRegistrationBox.Visibility = Visibility.Visible;
            passwordRegistrationConfirmTextBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var passwordBox = this.TabControl.Template.FindName("PasswordBox", this.TabControl) as PasswordBox;
            var passwordTextBox = this.TabControl.Template.FindName("PasswordTextBox", this.TabControl) as TextBox;
            passwordTextBox.Background = passwordBox.Background;
            passwordTextBox.Text = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Visibility = Visibility.Visible;
        }

        private void ShowPasswordButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var passwordBox = this.TabControl.Template.FindName("PasswordBox", this.TabControl) as PasswordBox;
            var passwordTextBox = this.TabControl.Template.FindName("PasswordTextBox", this.TabControl) as TextBox;
            passwordBox.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Collapsed;
        }
    }
}
