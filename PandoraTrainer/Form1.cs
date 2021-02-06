using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Memory;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DiscordRPC;
using DiscordRPC.Logging;

namespace PandoraTrainer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form1 MainForm = new Form1();

        public DiscordRpcClient client;
        bool initialized = false;

        // New memory instance
        public Mem Memory = new Mem();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Loop forever
            while (true)
            {
                // Getting GT's PID
                int PID = Memory.GetProcIdFromName("Growtopia");
                
                bool isAttached = false;
                
                if (PID > 0)
                {
                    isAttached = Memory.OpenProcess(PID);
                }
                // Is Growtopia attached?
                if (isAttached == true)
                {
                    MainForm.label14.Text = "Attached: Yes";
                } else {
                    MainForm.label14.Text = "Attached: No";
                }
                // Lower CPU usage :D
                Thread.Sleep(25);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // are things done in the background?
            if (!backgroundWorker1.IsBusy)
            {
                // if not, do it
                backgroundWorker1.RunWorkerAsync();
            }

            MainForm.panel1.Show();
            MainForm.panel2.Hide();
            MainForm.panel3.Hide();
            MainForm.panel4.Hide();
            MainForm.panel5.Hide();
            MainForm.panel6.Hide();

            Process[] GTProc = Process.GetProcessesByName("Growtopia");
            if (GTProc.Length == 0)
                MainForm.label1.Text = "Growtopia: Running";
            else
                MainForm.label1.Text = "Growtopia: Not Running";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm.panel1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm.panel1.Hide();
            MainForm.panel2.Show();
            MainForm.panel3.Hide();
            MainForm.panel4.Hide();
            MainForm.panel5.Hide();
            MainForm.panel6.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainForm.panel1.Hide();
            MainForm.panel2.Hide();
            MainForm.panel3.Show();
            MainForm.panel4.Hide();
            MainForm.panel5.Hide();
            MainForm.panel6.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        { }          [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);
        private void GrowtopiaTitle()
        {
            Process p = Process.Start("Growtopia.exe");
            Thread.Sleep(100);
            SetWindowText(p.MainWindowHandle, MainForm.textBox7.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainForm.panel1.Hide();
            MainForm.panel2.Hide();
            MainForm.panel3.Hide();
            MainForm.panel4.Hide();
            MainForm.panel5.Hide();
            MainForm.panel6.Show();
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30.Checked == true)
                MainForm.Opacity = 0.7;
            else
                MainForm.Opacity = 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            initialized = true;
            client = new DiscordRpcClient("806904820765950023");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();


            client.SetPresence(new DiscordRPC.RichPresence()
            {
                Details = $"Premium Growtopia Trainer",
                State = $"Cheating & Scamming",
                Timestamps = Timestamps.Now,
                Assets = new Assets()
                {
                    LargeImageKey = $"main",
                    LargeImageText = "Pandora",
                    SmallImageKey = $"main",
                    SmallImageText = "1.1.2 Beta"
                }
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm.panel1.Hide();
            MainForm.panel2.Hide();
            MainForm.panel3.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            client.Dispose();
        }
    }
}   
    
