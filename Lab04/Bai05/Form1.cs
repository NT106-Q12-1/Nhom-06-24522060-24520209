using Newtonsoft.Json.Linq; 
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers; 
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bai05
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {

            var url = txtUrl.Text;
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            rtbOutput.Clear();

            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Username và Password");
                return;
            }

            else if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập Username");
                return;
            }

            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Password");
                return;
            }

            

            try
            {
                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent
                    {
                        { new StringContent(username), "username" },
                        { new StringContent(password), "password" }
                    };
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    var responseObject = JObject.Parse(responseString);

                    if (!response.IsSuccessStatusCode)
                    {
                        
                        var detail = responseObject["detail"]?.ToString();
                        rtbOutput.AppendText($"Đăng nhập thất bại.\nDetail: {detail}\n");
                        return; 
                    }

                    var tokenType = responseObject["token_type"]?.ToString();
                    var accessToken = responseObject["access_token"]?.ToString();

                    rtbOutput.AppendText($"Token Type: {tokenType}\n");
                    rtbOutput.AppendText($"Access Token: {accessToken}\n\n");

                    rtbOutput.AppendText("Đăng nhập thành công!\n");


                    string token = responseObject["access_token"].ToString();
                    string pathBai6 = @"D:\HK3\Lap_trinh_mang\Thuchanh\Lab\Nhom-06-24522060-24520209\Lab04\Bai06\bin\Debug\Bai06.exe";

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathBai6;
                    startInfo.Arguments = token;

                    Process.Start(startInfo);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }
            catch (Exception ex)
            {
                rtbOutput.AppendText($"Lỗi ứng dụng: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}