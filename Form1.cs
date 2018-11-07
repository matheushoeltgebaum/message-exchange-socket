using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TrabalhoRedes
{
    public partial class Form1 : Form
    {
        private static List<Usuario> UsuariosConectados { get; set; } = new List<Usuario>();

        private string Servidor { get { return tbSistema.Text; } }
        private string Usuario { get { return tbUsuario.Text; } } //4936 | 9924
        private string Senha { get { return tbSenha.Text; } } //aswpa | esmtk
        private bool ConectadoTCP { get; set; } = false;
        private bool ConectadoUDP { get; set; } = false;

        private TcpClient tcpClient { get; set; }
        private UdpClient udpClient { get; set; }

        private Timer timerBuscaUsuarios { get; set; }
        private Timer timerBuscaMensagens { get; set; }

        private object LockConexao { get; set; } = new object();

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
                BuscarUsuarios();
                timerBuscaUsuarios = new Timer();
                timerBuscaUsuarios.Interval = 6000;
                timerBuscaUsuarios.Tick += TimerBuscaUsuarios_Tick;

                timerBuscaMensagens = new Timer();
                timerBuscaMensagens.Interval = 1000;
                timerBuscaMensagens.Tick += TimerBuscaMensagens_Tick;

                timerBuscaUsuarios.Start();
                timerBuscaMensagens.Start();

                HabilitarCampos(true);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Erro ao conectar: {exc.Message}\r\n\r\n");
            }
        }

        private void HabilitarCampos(bool conectado)
        {
            btnConectar.Enabled = !conectado;
            tbUsuario.Enabled = !conectado;
            tbSenha.Enabled = !conectado;

            btnMensagem.Enabled = conectado;
            tbMensagens.Enabled = conectado;
            listaUsuarios.Enabled = conectado;
            btnViewAll.Enabled = conectado;
        }

        private void TimerBuscaMensagens_Tick(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() => { BuscarMensagens(); }));
        }

        private void TimerBuscaUsuarios_Tick(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() => { BuscarUsuarios(); }));
        }

        private void BuscarUsuarios()
        {
            try
            {
                lock (LockConexao)
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
            }
            catch
            {
                if (!ConectadoTCP)
                    throw;
            }
        }

        private void CarregarUsuarios(string data)
        {
            var infoUsuarios = data.Split(':');
            listaUsuarios.Items.Clear();
            UsuariosConectados.Clear();

            for (int i = 0; i < infoUsuarios.Length - 1; i = i + 3)
            {
                var idUsuario = infoUsuarios[i];
                var nomeUsuario = infoUsuarios[i + 1];
                var numeroVitorias = infoUsuarios[i + 2];

                if (!idUsuario.Equals(Usuario))
                    UsuariosConectados.Add(new Usuario { Id = idUsuario, Nome = nomeUsuario, NumeroVitorias = numeroVitorias });
            }

            UsuariosConectados.ForEach(usuario => listaUsuarios.Items.Add(usuario));
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            try
            {
                MensagemEnviar enviar = new MensagemEnviar(UsuariosConectados);
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
                MessageBox.Show($"Erro ao enviar mensagem: {exc.Message}\r\n\r\n");
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
                lock (LockConexao)
                {
                    ConectarTCP();
                    var serverStream = tcpClient.GetStream();
                    byte[] outStream = Encoding.Default.GetBytes($"GET MESSAGE {Usuario}:{Senha}");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                    string returnData = Encoding.Default.GetString(inStream);
                    tbMensagens.Text += returnData;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Erro ao buscar mensagens: {exc.Message}\r\n\r\n");
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

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            listaUsuarios.ClearSelected();
        }

        private void listaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaUsuarios.SelectedIndex == -1)
                tbMensagens.Text = "Todos";
            else
                tbMensagens.Text = listaUsuarios.SelectedIndex.ToString();
        }
    }
}
