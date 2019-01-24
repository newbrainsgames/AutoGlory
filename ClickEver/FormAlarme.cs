using System;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace AutoGlory
{
    public partial class FormAlarme : Form
    {
        /// <summary>
        /// VARIAVEIS
        /// </summary>

        //Instancia da classe atual
        static FormAlarme formA;

        //Temporizador
        Timer MyTimer = new Timer();

        //Tempo escolhido pelo usuário em segundos
        int tempoTotal = 0;

        //Contador para encher a barra de progresso
        int counter = 0;

        //Player da musica
        WMPLib.WindowsMediaPlayer songPlayer = new WMPLib.WindowsMediaPlayer();
        string selectedMusic;
        bool estaTocando = false;

        //Tecla de atalho
        HotKey StopMusic = new HotKey(AutoGlory.ModifierKeys.Control, AutoGlory.ModifierKeys.Alt, Keys.K, "Alarme");

        //Boolean que diz se é o primeiro alarme
        static bool First = true;

        //Quantidade de vezes que o alarme vai repetir
        int vezes = 0;

        public string RunningPath { get; private set; }

        /// <summary>
        /// FUNÇÕES
        /// </summary>

        public FormAlarme()
        {
            InitializeComponent();

            //Passa a instancia pra variavel
            formA = this;

            //Seleciona a musica "Morning Flower Remix" por padrão
            songSelect.SelectedIndex = 0;

            //Converte o tempo definido pelo usuário para segundos
            tempoTotal = Convert.ToInt32(TimeSpan.Parse("00:" + maskedTextBox1.Text).TotalSeconds);
        }

        //Inicia o programa
        private void Start()
        {
            //Verifica se excedeu a quantidade de vezes definida pelo usuário
            if (vezes <= numericUpDown1.Value)
            {
                //Cria uma caixa de dialogo perguntando ao usuário se ele deseja Iniciar/Repetir o temporizador
                DialogResult result;
                if (First) { result = MessageBox.Show("Iniciar?", "Alarme", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                else { result = MessageBox.Show("Repetir?", "Alarme", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }

                //Se o usuário aceitar repetir/iniciar o temporizador
                if (result == DialogResult.Yes)
                {
                    //Chama a função PlayMusic - Define que First como falso - Incrementa varíavel vezes
                    PlayMusic();
                    First = false;
                    vezes++;
                }
            }

            //Se tiver excedido o numero de quantidade de vezes para o programa repetir
            else
            {
                //Pergunta ao usuário se deseja fechar o alarme - Se o usuário aceitar, o formulário Alarme é fechado
                DialogResult result = MessageBox.Show("Tarefa concluída!\nDeseja fechar o alarme?", "Tarefa concluída!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result == DialogResult.Yes)
                {
                    vezes = 0;
                    this.Close();
                }
            }
        }

        //Inicio do temporizador
        private void PlayMusic()
        {
            //Coloca a barra de progresso como visível
            progressBar.Visible = true;

            //Configura o Timer para que a cada X segundos, seja invocada a função MyTimer_Tick
            MyTimer.Interval = (tempoTotal*1000) / 20;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        //Quando clicado no botão Iniciar, invoca a função Start
        private void Iniciar_Click(object sender, EventArgs e)
        {
            Start();
        }

        //Quando o usuário usa a tecla de atalho para pausar a musica
        public static void Alarme_Pressed()
        {
            //Função soneca que pausa musica por 3 minuto
            if (formA.estaTocando)
            {
                formA.songPlayer.controls.stop();
            }
            System.Threading.Thread.Sleep(60000*3);
            formA.songPlayer.controls.play();
        }

        //Função que é chamada 20 vezes até dar o tempo escolhido pelo usuário
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            //Verifica se é a vigéssima vez que a função é chamada
            if(counter < 20)
            {
                //Incrementa o contador e Incrementa a barra de progresso
                counter++;
                progressBar.Value++;
            }
            else
            {
                //Interrompe o temporizador - Reproduz a musica - Ativa a boolean "estaTocando"
                MyTimer.Stop();
                songPlayer.URL = selectedMusic;
                songPlayer.controls.play();
                estaTocando = true;
            }
        }

        //Confirma qual a musica escolhida pelo usuário
        private void music_Validated(object sender, EventArgs e)
        {
            switch (songSelect.SelectedIndex)
            {
                case 0:
                    selectedMusic = Directory.GetCurrentDirectory() + "\\Resources\\MorningFlowerRemix.mp3";
                    break;
                case 1:
                    selectedMusic = Directory.GetCurrentDirectory() + "\\Resources\\DeathNoteOpening.mp3";
                    break;
                default:
                    selectedMusic = Directory.GetCurrentDirectory() + "\\Resources\\MorningFlowerRemix.mp3";
                    break;
            }
        }

        //Impede o usuário de digitar um tempo errado - Converte o tempo para segundos
        private void tempoBox_Validated(object sender, EventArgs e)
        {
            //Certifica que o tempo não passe de 59
            string format = maskedTextBox1.Text.Replace(":", "");
            if (Convert.ToInt32(format[0].ToString()) > 5)
            {
                var aStringBuilder = new StringBuilder(maskedTextBox1.Text);
                aStringBuilder.Remove(0, 1);
                aStringBuilder.Insert(0, "5");
                maskedTextBox1.Text = aStringBuilder.ToString();
            }
            if (Convert.ToInt32(format[2].ToString()) > 5)
            {
                var aStringBuilder = new StringBuilder(maskedTextBox1.Text);
                aStringBuilder.Remove(2, 1);
                aStringBuilder.Insert(2, "5");
                maskedTextBox1.Text = aStringBuilder.ToString();
            }
            if (format == "0000")
            {
                maskedTextBox1.Text = "00:01";
            }

            tempoTotal = Convert.ToInt32(TimeSpan.Parse("00:" + maskedTextBox1.Text).TotalSeconds);
        }

        //Reinicia o temporizador
        private void Reiniciar_Click(object sender, EventArgs e)
        {
            songPlayer.controls.stop();
            estaTocando = false;
            counter = 0;
            progressBar.Value = 0;
            Start();
        }

        private void Zerar_Click(object sender, EventArgs e)
        {
            songPlayer.controls.stop();
            estaTocando = false;
            counter = 0;
            vezes = 0;
            progressBar.Value = 0;
            maskedTextBox1.Text = "10:00";
            numericUpDown1.Value = 10;
            songSelect.SelectedIndex = 0;
        }
    }
}
