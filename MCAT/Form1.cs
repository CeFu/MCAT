using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace MCAT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string Address = "https://minecraft.net/download";
            WebClient WebC = new WebClient();
            Regex Regex = new Regex("[^0-9.]");
            

            //string Container = WebC.DownloadString(Address);

            //label1.Text = Container;
            HtmlWeb client = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = client.Load("https://minecraft.net/download");
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//p//a[@href]");

            String ServerVersion = Regex.Replace(Nodes[4].InnerText, String.Empty).Remove(0,1).TrimEnd('.');
            ToolStripVersion.Text = ServerVersion;

            MessageBox.Show(ServerVersion);


        }

    }
}
