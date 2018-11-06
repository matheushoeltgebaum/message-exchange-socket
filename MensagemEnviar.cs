using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoRedes
{
    public partial class MensagemEnviar : Form
    {
        public string Para { get { return tbPara.Text; } }
        public string Mensagem { get { return tbMensagem.Text; } }

        public MensagemEnviar()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
