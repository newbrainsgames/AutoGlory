using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace AutoGlory
{
    public partial class Form1 : Form
    {
        //Musica
        SoundPlayer sound = new SoundPlayer(Properties.Resources.MorningFlower);
        //Instanciar atalhos
        HotKey Start = new HotKey(AutoGlory.ModifierKeys.Control, AutoGlory.ModifierKeys.Alt, Keys.S);
        HotKey Pause = new HotKey(AutoGlory.ModifierKeys.Control, AutoGlory.ModifierKeys.Alt, Keys.P);

        //Início do programa
        public Form1()
        {
            InitializeComponent();
            HotKey.form = this;
        }

        public void KeyPressed()
        {

        }

        /*public void Attack()
        {
            Thread.Sleep(3000);
            Cursor.Position = new Point(444, 233);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(3000);
            Cursor.Position = new Point(1027, 552);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(3000);
            Cursor.Position = new Point(787, 847);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }*/

        private void fightButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Abra o navegador em \"www.marketglory.com\"\nE clique Ctrl+Alt+S para iniciar.", "Aviso", MessageBoxButtons.OK);
        }
    }
}