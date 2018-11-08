using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TrabalhoRedes
{
    public partial class Form1 : Form
    {
        private static List<Usuario> UsuariosConectados { get; set; } = new List<Usuario>();
        private static List<Mensagem> Mensagens { get; set; } = new List<Mensagem>();

        private string Servidor { get { return tbSistema.Text; } }
        private string Usuario { get { return tbUsuario.Text; } } //4936 | 9924
        private string Senha { get { return tbSenha.Text; } } //aswpa | esmtk
        private bool ConectadoTCP { get; set; } = false;
        private bool ConectadoUDP { get; set; } = false;

        private TcpClient tcpClient { get; set; }
        private UdpClient udpClient { get; set; }

        private Timer timerBuscaUsuarios { get; set; }
        private Timer timerBuscaMensagens { get; set; }

        private bool IsConectado { get; set; }

        private object LockConexao { get; set; } = new object();

        private Usuario CurrentUser { get; set; }

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
                IsConectado = true;
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
                MessageBox.Show($"Erro ao conectar: {exc.ToString()}\r\n\r\n");
            }
        }

        private void HabilitarCampos(bool conectado)
        {
            btnConectar.Enabled = !conectado;
            tbUsuario.Enabled = !conectado;
            tbSenha.Enabled = !conectado;

            btnMensagem.Enabled = conectado;
            rtbMensagens.Enabled = conectado;
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
                    byte[] outStream = Encoding.UTF8.GetBytes($"GET USERS {Usuario}:{Senha}");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                    string returnData = Encoding.UTF8.GetString(inStream);

                    if (returnData.Contains("Usuário inválido!"))
                        throw new Exception("O usuário ou a senha estão incorretos!");

                    CarregarUsuarios(returnData);
                }
            }
            catch
            {
                if (!IsConectado)
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
                else
                    CurrentUser = new Usuario { Id = idUsuario, Nome = nomeUsuario, NumeroVitorias = numeroVitorias };
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
                    Mensagens.Add(new Mensagem { Remetente = CurrentUser.Id, Destinatario = idDestinatario, Conteudo = enviar.Mensagem, Minha = true });
                    //if (IsConversaAberta(idDestinatario))
                    //    rtbMensagens.Text += $"[Você]: {enviar.Mensagem}" + "\r\n";
                    BuscarMensagens();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Erro ao enviar mensagem: {exc.ToString()}\r\n\r\n");
            }
        }

        private void BuscarMensagens()
        {
            try
            {
                lock (LockConexao)
                {
                    ConectarTCP();
                    var serverStream = tcpClient.GetStream();
                    byte[] outStream = Encoding.UTF8.GetBytes($"GET MESSAGE {Usuario}:{Senha}");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    byte[] inStream = new byte[tcpClient.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, tcpClient.ReceiveBufferSize);
                    string returnData = Encoding.UTF8.GetString(inStream);
                    //Ignorar mensagens que enviei, pois já adiciono quando eu realizo o envio
                    if (!returnData.Contains(":\r\n") || (CurrentUser != null && returnData.Contains($"{CurrentUser.Id}:")))
                        rtbMensagens.Text += ProcessarMensagem(returnData);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Erro ao buscar mensagens: {exc.ToString()}\r\n\r\n");
            }
        }

        private string ProcessarMensagem(string mensagem)
        {
            var infoMensagem =  mensagem.Split(':');
            if (!infoMensagem[0].Equals(CurrentUser?.Id))
            {
                var infoRemetente = UsuariosConectados.FirstOrDefault(user => user.Id.Equals(infoMensagem[0]));
                Mensagens.Add(new Mensagem { Destinatario = Usuario, Remetente = infoRemetente?.Id == null ? "0" : infoRemetente.Id, Conteudo = infoMensagem[1] });
                if (IsConversaAberta(infoMensagem[0]))
                    return $"[{(infoRemetente?.Nome == null ? "Servidor" : infoRemetente.Nome)}]: {infoMensagem[1]}" + "\r\n";

                return string.Empty;
            }
            else
                return $"[Você]: {infoMensagem[1]}" + "\r\n";
        }

        private bool IsConversaAberta(string idUsuario)
        {
            if (listaUsuarios.SelectedIndex == -1 && idUsuario.Equals("0"))
                return true;
            else if (listaUsuarios.SelectedItem != null)
                return ((listaUsuarios.SelectedItem as Usuario).Id.Equals(idUsuario));
            else
                return false;
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
            ShowMessagesFromAll();
        }

        private void listaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(listaUsuarios.SelectedIndex);
            if (listaUsuarios.SelectedIndex == -1)
                ShowMessagesFromAll();
            else
            {
                var idUsuarioChat = (listaUsuarios.SelectedItem as Usuario);
                rtbMensagens.Clear();
                foreach (var mensagem in Mensagens.Where(msg => msg.Destinatario == idUsuarioChat.Id && msg.Remetente == Usuario ||
                                                                msg.Destinatario == Usuario && msg.Remetente == idUsuarioChat.Id))
                {
                    if (mensagem.Remetente.Equals(CurrentUser.Id))
                        rtbMensagens.Text += $"[Você]: {mensagem.Conteudo}" + "\r\n";
                    else
                    {
                        var infoRemetente = UsuariosConectados.FirstOrDefault(user => user.Id.Equals(mensagem.Remetente));
                        rtbMensagens.Text += $"[{(infoRemetente?.Nome == null ? "Servidor" : infoRemetente.Nome)}]: {mensagem.Conteudo}" + "\r\n";
                    }
                }
            }
        }

        private void ShowMessagesFromAll()
        {
            rtbMensagens.Clear();
            foreach (var mensagem in Mensagens.Where(msg => msg.Destinatario == "0" || msg.Remetente == "0"))
            {
                var infoRemetente = UsuariosConectados.FirstOrDefault(user => user.Id.Equals(mensagem.Remetente));
                rtbMensagens.Text += $"[{(infoRemetente?.Nome == null ? (mensagem.Minha ? "Você" : "Servidor") : infoRemetente.Nome)}]: {mensagem.Conteudo}" + "\r\n";
            }
        }

        private void rtbMensagens_TextChanged(object sender, EventArgs e)
        {
            rtbMensagens.SelectionStart = rtbMensagens.Text.Length;
            rtbMensagens.ScrollToCaret();
        }
    }
}
