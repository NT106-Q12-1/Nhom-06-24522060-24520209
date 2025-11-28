using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = textBox1.Text;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "HTML Files|*.html|All Files|*.*";
            saveDialog.FileName = "chau.html";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string savePath = saveDialog.FileName;
                    textBox2.Text = savePath;

                    WebClient myClient = new WebClient();

                    myClient.DownloadFile(url, savePath);
                    MessageBox.Show("Tải thành công!");

                    Stream response = myClient.OpenRead(url);
                    StreamReader reader = new StreamReader(response);
                    richTextBox1.Text = reader.ReadToEnd();
                    response.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
