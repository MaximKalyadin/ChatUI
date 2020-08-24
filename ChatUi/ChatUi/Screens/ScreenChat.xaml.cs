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
using System.Windows.Shapes;

namespace ChatUi.Screens
{
    /// <summary>
    /// Логика взаимодействия для ScreenChat.xaml
    /// </summary>
    public partial class ScreenChat : UserControl
    {
        public ScreenChat()
        {
            InitializeComponent();
        }

        private void ButtonMore_MouseEnter(object sender, MouseEventArgs e)
        {
            this.PopupMore.IsOpen = true;
        }

        private void PopupBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            this.PopupMore.IsOpen = false;
        }
    }
}
