using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Bai06
{
    public partial class UserInfoForm : Form
    {
        private string authToken;
        public UserInfoForm(string token)
        {
            InitializeComponent();
            this.authToken = token; 
        }
        public UserInfoForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                MessageBox.Show("Chưa có Token! Vui lòng đăng nhập trước.");
                return;
            }
            string url = "https://nt106.uitiot.vn/api/v1/user/me";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        JObject userData = JObject.Parse(responseString);
                        txtUsername.Text = userData["username"]?.ToString();
                        txtEmail.Text = userData["email"]?.ToString();
                        txtFirstName.Text = userData["first_name"]?.ToString() ?? "";
                        txtLastName.Text = userData["last_name"]?.ToString() ?? "";
                    }
                    else
                    {
                        MessageBox.Show("Lỗi lấy thông tin: " + responseString);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
    }
}
