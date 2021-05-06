using ChatDesktop.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatDesktop
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void frmAcceso_Load(object sender, EventArgs e)
        {
        

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtUser.Text.Equals("") || txtPass.Text.Equals("") )
            {

                MessageBox.Show("Los dos campos son obligatorios");
                return;
            }

            Reply oReply = new Reply();
            AccessRequest oAR = new AccessRequest();
            oAR.Email = txtUser.Text.Trim();
            oAR.Password = UtilitiesChat.Tools.Encrypt.GetSHA256(txtPass .Text.Trim());



            RequestUtil oRequestUtil = new RequestUtil();

            oReply = oRequestUtil.Execute<AccessRequest>(Constants.Url.ACESS, "post", oAR);
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (oReply.result == 1)
            {

                Business.Session.oUser = js.Deserialize<UtilitiesChat.Models.WS.UserResponse>(js.Serialize(oReply.data));

                this.Close();

                MessageBox.Show("Inicio sesion");

            }
            else
            {

                MessageBox.Show(oReply.message);
            }

        }
    }
}
