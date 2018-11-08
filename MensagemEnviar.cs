using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TrabalhoRedes
{
    public partial class MensagemEnviar : Form
    {
        private List<Usuario> Usuarios { get; set; }
        public string Para
        {
            get
            {
                if (cbPara.SelectedItem != null && cbPara.SelectedItem is Usuario)
                    return (cbPara.SelectedItem as Usuario).Id;

                return "0";
            }
        }
        public string Mensagem { get { return tbMensagem.Text; } }

        public MensagemEnviar(List<Usuario> usuarios)
        {
            InitializeComponent();
            Usuarios = usuarios;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void MensagemEnviar_Load(object sender, EventArgs e)
        {
            cbPara.Items.AddRange(Usuarios.ToArray());
            cbPara.Items.Add(new Usuario { Id = "0", Nome = "TODOS", NumeroVitorias = "0" });

            cbPara.SelectedIndex = cbPara.Items.Count - 1;
        }
    }
}
