using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19_async
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Fetching...";
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
                    Task<string> t = client.GetStringAsync("http://sharpindepth.com");
                    string text = await t;
                    label1.Text = text;                    
                    MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.GetType().ToString() + " : " + ex.Message);
                }
            }        
        }
    }
}
