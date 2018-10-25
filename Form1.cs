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
        private bool ConectadoTCP { get; set; } = false;
        private bool ConectadoUDP { get; set; } = false;

        private TcpClient tcpClient { get; set; }

        private UdpClient udpClient { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rtbLog.AppendText("Iniciou o programa...\r\n");
            tcpClient = new TcpClient();
            udpClient = new UdpClient();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarTCP();

                rtbLog.AppendText("Conectando...\r\n");
                var serverStream = tcpClient.GetStream();
                byte[] outStream = Encoding.Default.GetBytes($"GET USERS {Usuario}:{Senha}");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                string returnData = Encoding.Default.GetString(inStream);
                rtbLog.AppendText(returnData + "\r\n\r\n");
            }
            catch (Exception exc)
            {
                rtbLog.AppendText($"Erro ao conectar: {exc.ToString()}\r\n\r\n");
            }
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarUDP();
                rtbLog.AppendText("Enviando mensagem...\r\n");
                byte[] outStream = Encoding.UTF8.GetBytes($"SEND MESSAGE {Usuario}:{Senha}:0:Hello World!");
                int result = udpClient.Send(outStream, outStream.Length);
            }
            catch (Exception exc)
            {
                rtbLog.AppendText($"Erro ao enviar mensagem: {exc.ToString()}\r\n\r\n");
            }
        }

        private void btnGetMensagens_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarTCP();
                rtbLog.AppendText("Buscando mensagens...\r\n");
                var serverStream = tcpClient.GetStream();
                byte[] outStream = Encoding.Default.GetBytes($"GET MESSAGE {Usuario}:{Senha}");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                string returnData = Encoding.Default.GetString(inStream);
                rtbLog.AppendText(returnData + "\r\n\r\n");
            }
            catch (Exception exc)
            {
                rtbLog.AppendText($"Erro ao buscar mensagens: {exc.ToString()}\r\n\r\n");
            }
        }

        private void ConectarTCP()
        {
            if (!ConectadoTCP)
            {
                tcpClient.Connect(Servidor, 1012);
                ConectadoTCP = true;
            }
        }

        private void FecharTCP()
        {
            if (ConectadoTCP)
            {
                tcpClient.Close();
                ConectadoTCP = false;
            }
        }

        private void ConectarUDP()
        {
            if (!ConectadoUDP)
            {
                udpClient.Connect(Servidor, 1011);
                ConectadoUDP = true;
            }
        }

        private void FecharUDP()
        {
            if (ConectadoUDP)
            {
                tcpClient.Close();
                ConectadoUDP = false;
            }
        }
    }
}
