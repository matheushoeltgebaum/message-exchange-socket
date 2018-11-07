namespace TrabalhoRedes
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbSistema = new System.Windows.Forms.Label();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.lbSenha = new System.Windows.Forms.Label();
            this.tbSistema = new System.Windows.Forms.TextBox();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.tbSenha = new System.Windows.Forms.TextBox();
            this.lbListaUsuarios = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnMensagem = new System.Windows.Forms.Button();
            this.btnGetMensagens = new System.Windows.Forms.Button();
            this.listaUsuarios = new System.Windows.Forms.ListBox();
            this.tbMensagens = new System.Windows.Forms.TextBox();
            this.lbMensagens = new System.Windows.Forms.Label();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSistema
            // 
            this.lbSistema.AutoSize = true;
            this.lbSistema.Location = new System.Drawing.Point(12, 15);
            this.lbSistema.Name = "lbSistema";
            this.lbSistema.Size = new System.Drawing.Size(47, 13);
            this.lbSistema.TabIndex = 0;
            this.lbSistema.Text = "Sistema:";
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Location = new System.Drawing.Point(12, 41);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(46, 13);
            this.lbUsuario.TabIndex = 1;
            this.lbUsuario.Text = "Usuário:";
            // 
            // lbSenha
            // 
            this.lbSenha.AutoSize = true;
            this.lbSenha.Location = new System.Drawing.Point(12, 67);
            this.lbSenha.Name = "lbSenha";
            this.lbSenha.Size = new System.Drawing.Size(41, 13);
            this.lbSenha.TabIndex = 2;
            this.lbSenha.Text = "Senha:";
            // 
            // tbSistema
            // 
            this.tbSistema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSistema.Location = new System.Drawing.Point(65, 12);
            this.tbSistema.Name = "tbSistema";
            this.tbSistema.Size = new System.Drawing.Size(829, 20);
            this.tbSistema.TabIndex = 3;
            this.tbSistema.Text = "larc.inf.furb.br";
            // 
            // tbUsuario
            // 
            this.tbUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUsuario.Location = new System.Drawing.Point(65, 38);
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(829, 20);
            this.tbUsuario.TabIndex = 4;
            this.tbUsuario.Text = "4936";
            // 
            // tbSenha
            // 
            this.tbSenha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSenha.Location = new System.Drawing.Point(65, 64);
            this.tbSenha.Name = "tbSenha";
            this.tbSenha.Size = new System.Drawing.Size(829, 20);
            this.tbSenha.TabIndex = 5;
            this.tbSenha.Text = "aswpa";
            this.tbSenha.UseSystemPasswordChar = true;
            // 
            // lbListaUsuarios
            // 
            this.lbListaUsuarios.AutoSize = true;
            this.lbListaUsuarios.Location = new System.Drawing.Point(12, 100);
            this.lbListaUsuarios.Name = "lbListaUsuarios";
            this.lbListaUsuarios.Size = new System.Drawing.Size(82, 13);
            this.lbListaUsuarios.TabIndex = 6;
            this.lbListaUsuarios.Text = "Usuários ativos:";
            // 
            // btnConectar
            // 
            this.btnConectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConectar.Location = new System.Drawing.Point(819, 90);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 8;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnMensagem
            // 
            this.btnMensagem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMensagem.Location = new System.Drawing.Point(685, 90);
            this.btnMensagem.Name = "btnMensagem";
            this.btnMensagem.Size = new System.Drawing.Size(75, 23);
            this.btnMensagem.TabIndex = 9;
            this.btnMensagem.Text = "Enviar msg";
            this.btnMensagem.UseVisualStyleBackColor = true;
            this.btnMensagem.Click += new System.EventHandler(this.btnMensagem_Click);
            // 
            // btnGetMensagens
            // 
            this.btnGetMensagens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetMensagens.Location = new System.Drawing.Point(604, 90);
            this.btnGetMensagens.Name = "btnGetMensagens";
            this.btnGetMensagens.Size = new System.Drawing.Size(75, 23);
            this.btnGetMensagens.TabIndex = 10;
            this.btnGetMensagens.Text = "Busca msg";
            this.btnGetMensagens.UseVisualStyleBackColor = true;
            this.btnGetMensagens.Click += new System.EventHandler(this.btnGetMensagens_Click);
            // 
            // listaUsuarios
            // 
            this.listaUsuarios.FormattingEnabled = true;
            this.listaUsuarios.Location = new System.Drawing.Point(15, 116);
            this.listaUsuarios.Name = "listaUsuarios";
            this.listaUsuarios.Size = new System.Drawing.Size(227, 316);
            this.listaUsuarios.TabIndex = 11;
            this.listaUsuarios.SelectedIndexChanged += new System.EventHandler(this.listaUsuarios_SelectedIndexChanged);
            // 
            // tbMensagens
            // 
            this.tbMensagens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMensagens.Location = new System.Drawing.Point(267, 116);
            this.tbMensagens.Multiline = true;
            this.tbMensagens.Name = "tbMensagens";
            this.tbMensagens.Size = new System.Drawing.Size(627, 342);
            this.tbMensagens.TabIndex = 12;
            // 
            // lbMensagens
            // 
            this.lbMensagens.AutoSize = true;
            this.lbMensagens.Location = new System.Drawing.Point(264, 100);
            this.lbMensagens.Name = "lbMensagens";
            this.lbMensagens.Size = new System.Drawing.Size(65, 13);
            this.lbMensagens.TabIndex = 13;
            this.lbMensagens.Text = "Mensagens:";
            // 
            // btnViewAll
            // 
            this.btnViewAll.Location = new System.Drawing.Point(15, 435);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(95, 23);
            this.btnViewAll.TabIndex = 14;
            this.btnViewAll.Text = "Visualizar todos";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 470);
            this.Controls.Add(this.btnViewAll);
            this.Controls.Add(this.lbMensagens);
            this.Controls.Add(this.tbMensagens);
            this.Controls.Add(this.listaUsuarios);
            this.Controls.Add(this.btnGetMensagens);
            this.Controls.Add(this.btnMensagem);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.lbListaUsuarios);
            this.Controls.Add(this.tbSenha);
            this.Controls.Add(this.tbUsuario);
            this.Controls.Add(this.tbSistema);
            this.Controls.Add(this.lbSenha);
            this.Controls.Add(this.lbUsuario);
            this.Controls.Add(this.lbSistema);
            this.Name = "Form1";
            this.Text = "Programa de testes";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSistema;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Label lbSenha;
        private System.Windows.Forms.TextBox tbSistema;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.TextBox tbSenha;
        private System.Windows.Forms.Label lbListaUsuarios;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnMensagem;
        private System.Windows.Forms.Button btnGetMensagens;
        private System.Windows.Forms.ListBox listaUsuarios;
        private System.Windows.Forms.TextBox tbMensagens;
        private System.Windows.Forms.Label lbMensagens;
        private System.Windows.Forms.Button btnViewAll;
    }
}

