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
    public partial class FrmMain : Form
    {
        int posYFinal = 10;
        Panel lastPanel = null;


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

            SessionStart();
        }

        private void iniciarSessiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            SessionStart();

        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Business.Session.oUser = null;
            iniciarSessiónToolStripMenuItem.Enabled = true;
            cerrarSesiónToolStripMenuItem.Enabled = false;
            splitContainer1.Enabled = false;
        }

        #region HELPER

        private void GetDataInit()
        {

            List<ListRoomResponse> lst = new List<ListRoomResponse>();

            //oUserSession

            SecurityRequest oSecurityRequest = new SecurityRequest();
            oSecurityRequest.AccessToken = Session.oUser.AccessToken;
            Reply oReply = new Reply();
            RequestUtil oRequestUtil = new RequestUtil();

            oReply = oRequestUtil.Execute<SecurityRequest>(Constants.Url.Rooms, "post", oSecurityRequest);
            JavaScriptSerializer js = new JavaScriptSerializer();
            lst = js.Deserialize<List<ListRoomResponse>>(js.Serialize(oReply.data));

            cboRooms.DataSource = lst;
            cboRooms.DisplayMember = "Name";
            cboRooms.ValueMember = "id";
            cboRooms.Refresh();

            //obtener mensajes del chat
            GetMessages();
            

        }

        private void SessionStart()
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();

            if (Business.Session.oUser != null)
            {

                iniciarSessiónToolStripMenuItem.Enabled = false;
                cerrarSesiónToolStripMenuItem.Enabled = true;
                splitContainer1.Enabled = true;
                GetDataInit();


            }
        }

        private void GetMessages()
        {
            int idRoom = 0;
            //posYFinal = 10;
            panelMessages.Controls.Clear();
            lastPanel = null;
            try
            {
                idRoom = (int)cboRooms.SelectedValue;

            }
            catch (Exception ex)
            {

               // throw;
            }


            if (idRoom >0 )
            {
                List<MessagesResponse> lst = new List<MessagesResponse>();

                MessagesRequest oMessagesRequest = new MessagesRequest();
                oMessagesRequest.AccessToken = Business.Session.oUser.AccessToken;
                oMessagesRequest.idRoom = idRoom;

                Reply oReply = new Reply();

                RequestUtil oRequestUtil = new RequestUtil();

                oReply = oRequestUtil.Execute<SecurityRequest>(Constants.Url.Messages, "post", oMessagesRequest);
                JavaScriptSerializer js = new JavaScriptSerializer();
                lst = js.Deserialize<List<MessagesResponse>>(js.Serialize(oReply.data));
                lst = lst.OrderBy(d => d.DateCreated).ToList();


                foreach (MessagesResponse oMessage in lst)
                {

                    AddMessage(oMessage);

                }


            }


        }

        private void AddMessage(MessagesResponse oMessage)
        {

            Panel oPanel = new Panel();

            oPanel.Width = panelMessages.Width - 30;
            oPanel.Height = 70;
            oPanel.BackColor = Color.White;

            //oPanel.Location = new Point(10,posYFinal);



            if (lastPanel == null)
                oPanel.Location = new Point(10, 10);
            else
                oPanel.Location = new Point(10, lastPanel.Location.Y + lastPanel.Height + 10);

            lastPanel = oPanel;
            //posYFinal += oPanel.Height + 10;
            panelMessages.Controls.Add(oPanel);
            panelMessages.ScrollControlIntoView(oPanel);

            // agregamos hijos al panel

            TextBox txtMessage = new TextBox();
            txtMessage.Text = oMessage.Message;
            txtMessage.Location = new Point(10, 10);
            txtMessage.Width = oPanel.Width - 20;
            txtMessage.ReadOnly = true;
            txtMessage.BorderStyle = BorderStyle.None;

            oPanel.Controls.Add(txtMessage);





        }

        #endregion

     


        private void cboRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            posYFinal = 10;
            panelMessages.Controls.Clear();
            GetMessages();
        }
    }
}
