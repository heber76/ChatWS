using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatDesktop
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            //FrmLogin frmLogin = new FrmLogin();
            //frmLogin.Show();
        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {

           
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {

            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
        }

        private void iniciarSessiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Business.Session.oUser = null;
        }
    }
}
