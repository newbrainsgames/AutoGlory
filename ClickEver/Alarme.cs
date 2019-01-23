using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace AutoGlory
{
    static class Alarme
    {
        static SoundPlayer music = new SoundPlayer(Properties.Resources.MorningFlower);
        static HotKey StopMusic = new HotKey(ModifierKeys.Control, ModifierKeys.Alt, Keys.K, true);

        public static void Start()
        {
            PlayMusic();
        }

        private static void ExecuteIn(int milliseconds, Action action)
        {
            System.Timers.Timer runonce = new System.Timers.Timer(720000);
            runonce.Elapsed += (s, e) => { action(); };
            runonce.AutoReset = false;
            runonce.Start();
        }

        private static void PlayMusic()
        {
            ExecuteIn(720000, () =>
            {
                music.PlayLooping();
            });
        }

        public static void KeyPressed(object sender, KeyPressedEventArgs e)
        {
            music.Stop();
            DialogResult result = MessageBox.Show("Resetar timer?", "Concluído", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Start();
            }
        }
    }
}
