using Codeplex.Data;
using System;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace yesno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Answer { get; set; }
        public string Image { get; set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            progressBar1.Value = 0;

            var json = "";

            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows XP)");
                webClient.Encoding = Encoding.UTF8;
                json = webClient.DownloadString("https://yesno.wtf/api");
            }

            var obj = DynamicJson.Parse(json);

            Answer = obj.answer;
            Image = obj.image;

            //非同期的に画像を読み込んで表示する
            pictureBox1.WaitOnLoad = false;
            pictureBox1.LoadAsync(Image);

            //pictureBox1.ImageLocation = obj.image;//同期表示版
        }

        private void PictureBox1_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            if (e.ProgressPercentage == 100)
            {
                MessageBox.Show(Answer.ToUpper());
            }
        }
    }
}
