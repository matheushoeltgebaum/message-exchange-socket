using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoRedes
{
    public partial class Form1 : Form
    {
        private string Servidor { get { return tbSistema.Text; } }
        private string Usuario { get { return tbUsuario.Text; } } //4936 | 9924
        private string Senha { get { return tbSenha.Text; } } //aswpa | esmtk

        private TcpClient clientSocket { get; set; }

        private UdpClient udpClient { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rtbLog.AppendText("Iniciou o programa...\r\n");
            clientSocket = new TcpClient();
            udpClient = new UdpClient();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            clientSocket.Connect(Servidor, 1012);

            var serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.Default.GetBytes($"GET USERS {Usuario}:{Senha}");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
            string returnData = Encoding.Default.GetString(inStream);
            rtbLog.AppendText(returnData + "\r\n\r\n");
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            udpClient.Connect(Servidor, 1011);

            byte[] outStream = Encoding.Unicode.GetBytes($"SEND MESSAGE {Usuario}: {Senha}:0:Hello World!");
            udpClient.Send(outStream, outStream.Length);
        }

        private void btnGetMensagens_Click(object sender, EventArgs e)
        {
            clientSocket.Connect(Servidor, 1012);

            var serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.Default.GetBytes($"GET MESSAGE {Usuario}:{Senha}");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);
            string returnData = Encoding.Default.GetString(inStream);
            rtbLog.AppendText(returnData + "\r\n\r\n");
        }
    }
}
