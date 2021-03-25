using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Interface.CreateWindow();
            Interface.Scenes("s_menu");
            Interface.resload();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < Interface.res.Length; i++)
            {
                Interface.res[i] = null;
                if (i < Interface.sound.Length)
                    Interface.sound[i] = null;
            }
        }
    }
}

