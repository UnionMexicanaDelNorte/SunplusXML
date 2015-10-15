using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Specialized;
using System.Net;
namespace AdministradorXML
{
    public partial class mandarPush : Form
    {
        private class Item
        {
            public string Name;
            public int Value;
            public string Extra;

            public Item(string name, int value, String extra)
            {
                Name = name; Value = value;
                Extra = extra;
            }
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }
        public mandarPush()
        {
            InitializeComponent();
        }

        private void mandarPush_Load(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

            String queryCuentas = "SELECT ANL_CODE,LOOKUP FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ANL_CODE] WHERE ANL_CAT_ID= '07' AND SUBSTRING( ANL_CODE,1,2) = 'ER' order by ANL_CODE asc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCuentas, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int empiezo1 = 1;
                        while (reader.Read())
                        {
                            String ACNT_CODE = reader.GetString(0);
                            personasCombo.Items.Add(new Item(ACNT_CODE, empiezo1, ACNT_CODE));
                            empiezo1++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            personasCombo.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String mensaje = mensajeText.Text.Trim();
              String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
              Item itm = (Item)personasCombo.SelectedItem;
              String WHO = itm.Extra.ToString();
              String token = "";
              int tipo = -1;
              String queryCuentas = "SELECT token,tipo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_tokens] WHERE WHO= '" + WHO + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCuentas, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tipo = reader.GetInt32(1);
                            token = reader.GetString(0).Trim();
                             
                           

                            if(tipo==1)
                            {//ios
                                  token = token.Replace("<", "");
                                token = token.Replace(">", "");
                                token = token.Replace(" ", "");
                                token = token.Trim();
                                string URL = "http://unionnorte.org/push/push.php";
                                WebClient webClient = new WebClient();

                                NameValueCollection formData = new NameValueCollection();
                                formData["token"] = token;
                                formData["mensaje"] = mensaje;

                                byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
                                string responsefromserver = Encoding.UTF8.GetString(responseBytes);
                                Console.WriteLine(responsefromserver);
                                webClient.Dispose();
                            }
                            else
                            {//android
                                string SERVER_API_KEY = "AIzaSyDhONQRcdlOZVliuyhHxebPBSqWWyycVWE";
                                var SENDER_ID = "680915653090";
                                var value = mensaje;
                                WebRequest tRequest;
                                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                                tRequest.Method = "post";
                                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + token + "";
                                Console.WriteLine(postData);
                                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                                tRequest.ContentLength = byteArray.Length;

                                Stream dataStream = tRequest.GetRequestStream();
                                dataStream.Write(byteArray, 0, byteArray.Length);
                                dataStream.Close();

                                WebResponse tResponse = tRequest.GetResponse();

                                dataStream = tResponse.GetResponseStream();

                                StreamReader tReader = new StreamReader(dataStream);

                                String sResponseFromServer = tReader.ReadToEnd();


                                tReader.Close();
                                dataStream.Close();
                                tResponse.Close();
                                // return sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
