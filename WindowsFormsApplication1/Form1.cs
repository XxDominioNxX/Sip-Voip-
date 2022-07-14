using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        String path;
        List<SIP_Request> DATA;
        public Form1()
        {
            InitializeComponent();
            path = "C:\\sign_with_SIP.cap";
            openFileDialog1.FileName = "C:\\sign_with_SIP.cap";
        }



        /// <summary>
        /// Клавиша "Вывести SIP"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Show_all();

        }



        /// <summary>
        /// Вывод информации на экран
        /// </summary>
        public void Show_all()
        {
            richTextBox1.Clear();
            if(Path.GetExtension(openFileDialog1.FileName) == ".cap")
            {
                GetFromCap();
                foreach (var item1 in DATA)
                {
                    for (int i = 0; i < item1.Data.Count; i++)
                    {
                        richTextBox1.AppendText(item1.Data[i]);
                    }
                    richTextBox1.AppendText("\n");
                }
            }
            if (Path.GetExtension(openFileDialog1.FileName) == ".dat")
            {

            }

        }



        /// <summary>
        /// OpenDialog
        /// </summary>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Data files (*.dat)|*.dat|Wireshark files (*.cap)|*.cap";
            openFileDialog1.FileName = "";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
            }
            
        }



        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// Справка
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В разработке","Справка");
        }


        /// <summary>
        /// Получить данные из файла формата .cap и записать
        /// </summary>
        private void GetFromCap()
        {
            DATA = new List<SIP_Request>();
            DATA.Clear();
            String line;
            StreamReader sr = new StreamReader(path);
            


            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("Via: SIP") || 
                    line.Contains("Max-Forwards") || 
                    line.Contains("Session-Expires"))
                {
                    SIP_Request SIP = new SIP_Request();
                    SIP.Data = new List<string>();
                    while (line != "" || !sr.EndOfStream)
                    {
                        SIP.Data.Add(line);
                        line = sr.ReadLine();
                    }
                    DATA.Add(SIP);
                }
            }
            sr.Close();
        }



        private void Search_data(String s, SIP_Request r)
        {

        }
    }
}
