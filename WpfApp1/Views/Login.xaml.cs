using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using log4net;
using WpfApp1.Utils;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Login()
        {
            InitializeComponent();
//            Database.CheckLogin("", "");
        }

        public void LoginButton(object sender, RoutedEventArgs e)
        {
            if (!Database.CheckLogin(Username.Text, Password.Password))
            {
                MessageBox.Show("Incorrect Username/Password");
                return;
            }
//            if (Username.Text != "home" || Password.Password != "1234")
//            {
//                MessageBox.Show("Incorrect Username/Password");
//            }
            var window = Application.Current.MainWindow as MainWindow;
            //window.ContentTemplate = MainWindow
//            MainWindow.SwitchView = 1;
            window.TabView.Visibility = Visibility.Visible;
            Visibility = Visibility.Hidden;
            Log.Info("Logged in");
            
            Trace.WriteLine("text");
        }
    }
}
