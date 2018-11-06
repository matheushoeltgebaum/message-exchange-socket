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
            tcpClient = new TcpClient();
            udpClient = new UdpClient();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarTCP();

                var serverStream = tcpClient.GetStream();
                byte[] outStream = Encoding.Default.GetBytes($"GET USERS {Usuario}:{Senha}");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                string returnData = Encoding.UTF8.GetString(inStream);

                CarregarUsuarios(returnData);
            }
            catch (Exception exc)
            {
                //rtbLog.AppendText($"Erro ao conectar: {exc.ToString()}\r\n\r\n");
            }
        }

        private void CarregarUsuarios(string data)
        {
            var infoUsuarios = data.Split(':');
            listaUsuarios.Items.Clear();

            for (int i = 0; i < infoUsuarios.Length - 1; i = i + 3)
            {
                var idUsuario = infoUsuarios[i];
                var nomeUsuario = infoUsuarios[i + 1];
                var numeroVitorias = infoUsuarios[i + 2];

                listaUsuarios.Items.Add($"{idUsuario} - {nomeUsuario}");
            }
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            try
            {
                MensagemEnviar enviar = new MensagemEnviar();
                if (enviar.ShowDialog() == DialogResult.OK)
                {
                    string idDestinatario = enviar.Para;
                    if (string.IsNullOrEmpty(enviar.Para))
                        idDestinatario = "0";

                    ConectarUDP();
                    byte[] outStream = Encoding.UTF8.GetBytes($"SEND MESSAGE {Usuario}:{Senha}:{idDestinatario}:{enviar.Mensagem}");
                    int result = udpClient.Send(outStream, outStream.Length);
                    BuscarMensagens();
                }
            }
            catch (Exception exc)
            {
                //rtbLog.AppendText($"Erro ao enviar mensagem: {exc.ToString()}\r\n\r\n");
            }
        }

        private void btnGetMensagens_Click(object sender, EventArgs e)
        {
            BuscarMensagens();
        }
        
        private void BuscarMensagens()
        {
            try
            {
                ConectarTCP();
                var serverStream = tcpClient.GetStream();
                byte[] outStream = Encoding.Default.GetBytes($"GET MESSAGE {Usuario}:{Senha}");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                string returnData = Encoding.Default.GetString(inStream);
                tbMensagens.Text = returnData;
            }
            catch (Exception exc)
            {
                //rtbLog.AppendText($"Erro ao buscar mensagens: {exc.ToString()}\r\n\r\n");
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
