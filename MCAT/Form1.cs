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
using System.IO;

namespace MCAT
{
    public partial class Form1 : Form
    {
        Regex Regex = new Regex("[^0-9.]");
        HtmlWeb client = new HtmlWeb();
        String ServerVersion;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Check Current Version
            //As regions name states, this area checks minecraft.net version.
            HtmlAgilityPack.HtmlDocument doc = client.Load("https://minecraft.net/download");
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//p//a[@href]");

            ServerVersion = Regex.Replace(Nodes[4].InnerText, String.Empty).Remove(0, 1).TrimEnd('.');
            ToolStripVersion.Text = ServerVersion;
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
           


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // The following below allows the program to "Contruct" the download link for the jar.
            // This may not be fool proof, but if all goes well, I won't have to touch this area for awhile

            WebClient WebC = new WebClient();
            string FileName;

            //Goes towards the 7th entry, which the download for the server.jar is located within.
            HtmlAgilityPack.HtmlDocument doc = client.Load("https://minecraft.net/download");
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//p//a[@href]");

            //Get file name
            FileName = (Nodes[6].InnerText);

            string RemoteDLUri = "https://s3.amazonaws.com/Minecraft.Download/versions/" + ServerVersion + "/" + FileName;
            WebC.DownloadFile(RemoteDLUri, FileName);
        }

    }
}
