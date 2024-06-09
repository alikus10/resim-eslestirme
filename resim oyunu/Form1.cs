using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace resim_oyunu
{
    public partial class Form1 : Form
    {
        Label etkt1 = null;
        Label etkt2 = null;
        Random rnd = new Random();
        List<string> icons = new List<string>()
        {
            "!","!","%","%","b","b","e","e",
            "j","j","l","l","p","p","h","h"
        };

        // 1 dakikalık zamanlayıcı 
        Timer gameTimer = new Timer();
        int timeLeft = 60; // 60 saniye (1 dakika)

        private void Atama()
        {
            foreach (Control etkt in tableLayoutPanel1.Controls)
            {
                Label resetkt = etkt as Label;
                if (resetkt != null)
                {
                    int rs = rnd.Next(icons.Count);
                    resetkt.Text = icons[rs];
                    resetkt.ForeColor = resetkt.BackColor;
                    icons.RemoveAt(rs);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            // Zamanlayıcı ayarları
            gameTimer.Interval = 1000; // 1 saniye
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            Atama();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                this.Text = "Eşleştirme Oyunu        Kalan Süre: " + timeLeft.ToString() + " s.";
            }
            else
            {
                gameTimer.Stop();
                DialogResult result = MessageBox.Show("Süre doldu! Oyunu kaybettiniz. Yeniden başlamak ister misiniz?", "Oyun Bitti", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    Close();
                }
            }
        }

        private void label_click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                return;
            }
            Label labelsec = sender as Label;
            
            if (labelsec != null)
            {
                if (labelsec.ForeColor == Color.Black)
                {
                    return;
                }
                if (etkt1 == null)
                {
                    etkt1 = labelsec;
                    etkt1.ForeColor = Color.Black;
                    return;
                }
                etkt2 = labelsec;
                etkt2.ForeColor = Color.Black;
                w1n();

                if (etkt1.Text == etkt2.Text)
                {
                    etkt1 = null;
                    etkt2 = null;
                    return;
                }
                timer1.Start();
            }
        }

        private void w1n()
        {
            foreach (Control etkt in tableLayoutPanel1.Controls)
            {
                Label resetkt = etkt as Label;
                if (resetkt != null)
                {
                    if (resetkt.ForeColor == resetkt.BackColor)
                    {
                        return;
                    }
                }
            }

            gameTimer.Stop();

            DialogResult result = MessageBox.Show("Tebrikler, Kazandınız! Yeniden oynamak ister misiniz?", "Oyun Bitti", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                ResetGame();
            }
            else
            {
                Close();
            }
        }

        private void ResetGame()
        {
            foreach (Control etkt in tableLayoutPanel1.Controls)
            {
                Label resetkt = etkt as Label;
                if (resetkt != null)
                {
                    resetkt.ForeColor = resetkt.BackColor;
                }
            }
            icons = new List<string>()
            {
                "!","!","%","%","b","b","e","e",
                "j","j","l","l","p","p","h","h"
            };
            Atama();
            timeLeft = 60;
            this.Text = "Eşleştirme Oyunu - Kalan Süre: 60 saniye";
            gameTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            etkt1.ForeColor = etkt1.BackColor;
            etkt2.ForeColor = etkt2.BackColor;
            etkt1 = null;
            etkt2 = null;
        }
    }
}
