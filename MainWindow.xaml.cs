using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Drawing;

namespace AgenteJr3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(()=> ConnServer());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ConnServer()
        {
            while (true)
            {
                TcpListener server = new TcpListener(5000);
                server.Start();

                Console.WriteLine("Servidor iniciado na porta 5000");
                Console.WriteLine("Aguardando conexão do cliente...");

                TcpClient client1 = server.AcceptTcpClient();
                Console.WriteLine("Cliente conectado!");

                NetworkStream stream1 = client1.GetStream();

                byte[] buffer = new byte[1024];
                int bytesReceived = stream1.Read(buffer, 0, buffer.Length);
                string data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                ShowNotification("Aviso", data, "info");

                Console.WriteLine("Mensagem recebida: {0}", data);

                client1.Close();
                server.Stop();
            }
        }

        private void ShowNotification(string title, string message, string type)
        {
            NotifyIcon notifyIcon = new NotifyIcon();

            Icon logo = new Icon(@"caminho do icone");
            notifyIcon.Icon = logo;
            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;

            notifyIcon.ShowBalloonTip(10000);
        }
    }
}
