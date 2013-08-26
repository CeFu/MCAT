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

        decimal MinSet, MaxSet;
        bool IsGarbage, IsGui, isArgs;
        string JarName;


        //Brb
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Check Current Version

            try
            {
                //As regions name states, this area checks minecraft.net version.
                HtmlAgilityPack.HtmlDocument doc = client.Load("https://minecraft.net/download");
                HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//p//a[@href]");

                ServerVersion = Regex.Replace(Nodes[4].InnerText, String.Empty).Remove(0, 1).TrimEnd('.');
                ToolStripVersion.Text = ServerVersion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion

            #region Console

            CControl.SendKeyboardCommandsToProcess = !CControl.SendKeyboardCommandsToProcess;

            CControl.StartProcess("cmd", "");


            #endregion
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

        private void cbGarbage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {

        }

        private void MinUpDown_ValueChanged(object sender, EventArgs e)
        {
            MinSet = MinUpDown.Value;
            label1.Text = MinSet.ToString();
        }

        private void MaxUpDown_ValueChanged(object sender, EventArgs e)
        {
            MaxSet = MaxUpDown.Value;
            label2.Text = MaxSet.ToString();

        }

        private void toolStripStart_Click(object sender, EventArgs e)
        {
            if (MinSet >= 128)
            {
                if (MaxSet >= 256)
                {
                    CControl.WriteInput("java -Xms" + MinSet + "M " + "-Xmx" + MaxSet + "M " + "-XX:MaxHeapSize=256m -XX:PermSize=256m -jar " + JarName + ".jar", Color.Transparent, true);
                }
                else
                {
                    MessageBox.Show("Please input Max amount of RAM");
                }
            }
            else
            {
                MessageBox.Show("Please input Min-RAM");
            }
        }

        private void tbJarName_TextChanged(object sender, EventArgs e)
        {
            JarName = tbJarName.Text;
        }
    }
}
