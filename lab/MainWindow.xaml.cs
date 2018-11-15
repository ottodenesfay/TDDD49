using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
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

namespace lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket connect_s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Socket listen_s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            string ip_connect = IP.Text;
            int port_connect = Convert.ToInt32(Port.Text);
            IPEndPoint connect_ipe = new IPEndPoint(IPAddress.Parse(ip_connect), port_connect);

            try
            {
                connect_s.Connect(connect_ipe);
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show("ArgumentNullException : {0}" + ae.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException : {0}" + se.ToString());
            }
            catch (Exception ue)
            {
                MessageBox.Show("Unexpected exception : {0}" + ue.ToString());
            }
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("This is a test");
            int bytesSent = connect_s.Send(msg);
        }

        private void listenButton_Click(object sender, RoutedEventArgs e)
        {
            string ip_listen = IP.Text;
            int port_listen = Convert.ToInt32(Port.Text);
            IPEndPoint listen_ipe = new IPEndPoint(IPAddress.Parse(ip_listen), port_listen);
            listen_s.Bind(listen_ipe);
            listen_s.Listen(1);
            MessageBox.Show("Listening: IP=" + ip_listen + " Port=" + port_listen);
            while (true)
            {
                byte[] bytes = new byte[1024];
                int bytesRec = listen_s.Receive(bytes);
                MessageBox.Show("Text received: " + System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRec));
            }
        }
    }
}
