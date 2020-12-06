using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private void DoAnimation()
        {
            var backgroundCopy = this.Background.Clone();
            this.Background = null;
            foreach(var item in this.BaseGrid.Children)
            {
                if (item is Control control)
                    control.Visibility = Visibility.Collapsed;
                if (item is Panel panel && panel.Name != "SystemButtonsStackPanel")
                    panel.Visibility = Visibility.Collapsed;
            }
            Animation1();
            foreach (var item in this.BaseGrid.Children)
            {
                if (item is Panel panel && panel.Name != "SystemButtonsStackPanel")
                    panel.Visibility = Visibility.Visible;
            }
            this.Background = backgroundCopy;
        }

        private void Animation1()
        {
            Polygon polygon1 = new Polygon();
            polygon1.Points = new PointCollection() { new Point(0, 0), new Point(Width * 0.893d, 0), new Point(0, Height * 0.3125d) };
            polygon1.Fill = new SolidColorBrush(Color.FromRgb(222, 67, 73));

            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(5)
            };

            Polygon polygon2 = new Polygon();
            polygon2.Points = new PointCollection() { new Point(0, Height * 0.3125d - 1), new Point(Width * 0.893d, 0), new Point(0, Height * 0.6875d) };
            polygon2.Fill = new SolidColorBrush(Color.FromRgb(237, 72, 78));

            DoubleAnimation doubleAnimation2 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(4),
                RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(1))
            };

            Polygon polygon3 = new Polygon();
            polygon3.Points = new PointCollection() { new Point(0, Height * 0.6875d - 1), new Point(Width * 0.822d, 0), new Point(0, Height) };
            polygon3.Fill = new SolidColorBrush(Color.FromRgb(189, 151, 166));

            DoubleAnimation doubleAnimation3 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(3),
                RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(1))
            };

            Polygon polygon4 = new Polygon();
            polygon4.Points = new PointCollection() { new Point(0, Height), new Point(Width * 0.915d, 0 - Height * 0.125d), new Point(Width * 0.158d, Height) };
            polygon4.Fill = new SolidColorBrush(Color.FromRgb(202, 166, 178));

            DoubleAnimation doubleAnimation4 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(2),
                RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(1))
            };

            Polygon polygon5 = new Polygon();
            polygon5.Points = new PointCollection() { new Point(Width * 0.156d, Height), new Point(Width * 0.942d, 0 - Height * 0.1875d), new Point(Width * 0.314d, Height) };
            polygon5.Fill = new SolidColorBrush(Color.FromRgb(211, 191, 203));

            DoubleAnimation doubleAnimation5 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(1),
                RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(1))
            };

            Polygon polygon6 = new Polygon();
            polygon6.Points = new PointCollection() { new Point(Width * 0.313d, Height), new Point(Width, 0 - Height * 0.3125d), new Point(Width * 0.471d, Height) };
            polygon6.Fill = new SolidColorBrush(Color.FromRgb(229, 213, 223));

            DoubleAnimation doubleAnimation6 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(1))
            };

            this.BaseGrid.Children.Add(polygon6);
            this.BaseGrid.Children.Add(polygon5);
            this.BaseGrid.Children.Add(polygon4);
            this.BaseGrid.Children.Add(polygon3);
            this.BaseGrid.Children.Add(polygon2);
            this.BaseGrid.Children.Add(polygon1);

            polygon6.BeginAnimation(Polygon.OpacityProperty, doubleAnimation6);
            polygon5.BeginAnimation(Polygon.OpacityProperty, doubleAnimation5);
            polygon4.BeginAnimation(Polygon.OpacityProperty, doubleAnimation4);
            polygon3.BeginAnimation(Polygon.OpacityProperty, doubleAnimation3);
            polygon2.BeginAnimation(Polygon.OpacityProperty, doubleAnimation2);
            polygon1.BeginAnimation(Polygon.OpacityProperty, doubleAnimation);

            Animation1_Completed();
        }

        private void Animation2()
        {
            Polygon polygon1 = new Polygon();
            polygon1.Points = new PointCollection() { new Point(0, 0), new Point(Width * 0.893d, 0), new Point(0, Height * 0.3125d) };
            polygon1.Fill = new SolidColorBrush(Color.FromRgb(222, 67, 73));
            polygon1.Opacity = 0;

            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
            };

            Polygon polygon2 = new Polygon();
            polygon2.Points = new PointCollection() { new Point(0, Height * 0.3125d - 1), new Point(Width * 0.893d, 0), new Point(0, Height * 0.6875d) };
            polygon2.Fill = new SolidColorBrush(Color.FromRgb(237, 72, 78));
            polygon2.Opacity = 0;

            DoubleAnimation doubleAnimation2 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(1)
            };

            Polygon polygon3 = new Polygon();
            polygon3.Points = new PointCollection() { new Point(0, Height * 0.6875d - 1), new Point(Width * 0.822d, 0), new Point(0, Height) };
            polygon3.Fill = new SolidColorBrush(Color.FromRgb(189, 151, 166));
            polygon3.Opacity = 0;

            DoubleAnimation doubleAnimation3 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(2)
            };

            Polygon polygon4 = new Polygon();
            polygon4.Points = new PointCollection() { new Point(0, Height), new Point(Width * 0.915d, 0 - Height * 0.125d), new Point(Width * 0.158d, Height) };
            polygon4.Fill = new SolidColorBrush(Color.FromRgb(202, 166, 178));
            polygon4.Opacity = 0;

            DoubleAnimation doubleAnimation4 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(3),
            };

            Polygon polygon5 = new Polygon();
            polygon5.Points = new PointCollection() { new Point(Width * 0.156d, Height), new Point(Width * 0.942d, 0 - Height * 0.1875d), new Point(Width * 0.314d, Height) };
            polygon5.Fill = new SolidColorBrush(Color.FromRgb(211, 191, 203));
            polygon5.Opacity = 0;

            DoubleAnimation doubleAnimation5 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(4)
            };

            Polygon polygon6 = new Polygon();
            polygon6.Points = new PointCollection() { new Point(Width * 0.313d, Height), new Point(Width, 0 - Height * 0.3125d), new Point(Width * 0.471d, Height) };
            polygon6.Fill = new SolidColorBrush(Color.FromRgb(229, 213, 223));
            polygon6.Opacity = 0;

            DoubleAnimation doubleAnimation6 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                BeginTime = TimeSpan.FromSeconds(5)
            };

            this.BaseGrid.Children.Add(polygon6);
            this.BaseGrid.Children.Add(polygon5);
            this.BaseGrid.Children.Add(polygon4);
            this.BaseGrid.Children.Add(polygon3);
            this.BaseGrid.Children.Add(polygon2);
            this.BaseGrid.Children.Add(polygon1);

            polygon1.BeginAnimation(Polygon.OpacityProperty, doubleAnimation);
            polygon2.BeginAnimation(Polygon.OpacityProperty, doubleAnimation2);
            polygon3.BeginAnimation(Polygon.OpacityProperty, doubleAnimation3);
            polygon4.BeginAnimation(Polygon.OpacityProperty, doubleAnimation4);
            polygon5.BeginAnimation(Polygon.OpacityProperty, doubleAnimation5);
            polygon6.BeginAnimation(Polygon.OpacityProperty, doubleAnimation6);

            Animation2_Completed();
        }

        private async void Animation2_Completed()
        {
            await Task.Delay(6000);
            //Animation1();
        }

        private async void Animation1_Completed()
        {
            await Task.Delay(6000);
            Animation2();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        public void Mouse_Click(object sender, RoutedEventArgs e)
        {
            Friend.Visibility = Visibility.Collapsed;
            Settings.Visibility = Visibility.Collapsed;
            Chat.Visibility = Visibility.Visible;
            Chat.StackPanelSearch.Visibility = Visibility.Visible;
            Chat.ChatList.Visibility = Visibility.Collapsed;
            Chat.Notification.Visibility = Visibility.Collapsed;
            Chat.MyProfile.Visibility = Visibility.Visible;
        }

        public void Menu_ListBoxSelectedIndex(object sender, EventArgs e)
        {
            switch (Menu.SelectedIndex)
            {
                case 0:
                    Friend.Visibility = Visibility.Collapsed;
                    Settings.Visibility = Visibility.Collapsed;
                    Chat.Visibility = Visibility.Visible;
                    Chat.StackPanelSearch.Visibility = Visibility.Visible;
                    Chat.ChatList.Visibility = Visibility.Visible;
                    Chat.Notification.Visibility = Visibility.Collapsed;
                    Chat.MyProfile.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    Friend.Visibility = Visibility.Visible;
                    Settings.Visibility = Visibility.Collapsed;
                    Chat.Visibility = Visibility.Collapsed;
                    Chat.StackPanelSearch.Visibility = Visibility.Collapsed;
                    Chat.ChatList.Visibility = Visibility.Collapsed;
                    Chat.Notification.Visibility = Visibility.Collapsed;
                    Chat.MyProfile.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Friend.Visibility = Visibility.Collapsed;
                    Settings.Visibility = Visibility.Collapsed;
                    Chat.Visibility = Visibility.Visible;
                    Chat.StackPanelSearch.Visibility = Visibility.Collapsed;
                    Chat.ChatList.Visibility = Visibility.Collapsed;
                    Chat.Notification.Visibility = Visibility.Visible;
                    Chat.MyProfile.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    Friend.Visibility = Visibility.Collapsed;
                    Settings.Visibility = Visibility.Visible;
                    Chat.Visibility = Visibility.Collapsed;
                    Chat.StackPanelSearch.Visibility = Visibility.Collapsed;
                    Chat.ChatList.Visibility = Visibility.Collapsed;
                    Chat.Notification.Visibility = Visibility.Collapsed;
                    Chat.MyProfile.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void RoundProfileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Friend.Visibility = Visibility.Collapsed;
            Settings.Visibility = Visibility.Collapsed;
            Chat.Visibility = Visibility.Visible;
            Chat.StackPanelSearch.Visibility = Visibility.Visible;
            Chat.ChatList.Visibility = Visibility.Collapsed;
            Chat.Notification.Visibility = Visibility.Collapsed;
            Chat.MyProfile.Visibility = Visibility.Visible;
        }
    }
}
