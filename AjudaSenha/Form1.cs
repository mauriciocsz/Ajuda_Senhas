using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AjudaSenha
{
    public partial class Form1 : Form
    {
        public string[] mensagens = loadText();
        public int[] testesCheck = { 0, 0, 0, 0, 0, 0 };
        System.Drawing.Image[] imgs = { AjudaSenha.Properties.Resources._1, AjudaSenha.Properties.Resources._2, AjudaSenha.Properties.Resources._3, AjudaSenha.Properties.Resources._4, AjudaSenha.Properties.Resources._5 };
        public PictureBox[] cores = new PictureBox[5];
        public string RYpath = "";
        public Boolean teste = true;
        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("Para que este programa funcione corretamente, é necessário um arquivo chamado `rockyou.txt`, disponibilizado no gitHub.");
            MessageBox.Show("Abra o arquivo na janela à seguir.");

            openFileDialog1.Title = "Abrir arquivo RockYou";
            openFileDialog1.Filter = "txt files(*.txt)| *.txt";
            openFileDialog1.ShowDialog();
            RYpath = openFileDialog1.FileName;
            teste = checkRY();


            
        }


        public void execTestes(string senha)
        {

            bool[] testes = new bool[5];
            testes[0] = testeUm(senha);
            testes[1] = testeDois(senha);
            testes[2] = testeTres(senha);
            testes[3] = testeQuatro(senha);
            testes[4] = testeCinco(senha);

            cores[0] = pb_1;
            cores[1] = pb_2;
            cores[2] = pb_3;
            cores[3] = pb_4;
            cores[4] = pb_5;

            if (testes[0] == false)
            {
                readMessage(36, 38);
                return;
            }

            updateCheckColors(testes);
            afterTest(testes);

        }

        public Boolean checkRY()
        {
            StreamReader sr = new StreamReader(RYpath);
            string linha = sr.ReadLine();
            if (!linha.Equals("123456"))
            {
                MessageBox.Show("ERRO! Este arquivo não corresponde ao ROCKYOU.txt, baixe o arquivo novamente e tente outra vez!","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private Boolean testeUm(string password)
        {
            string path = RYpath;

            StreamReader sr = new StreamReader(path);
            for (int x = 0; x < 14344391; x++)
            {

                string linha = sr.ReadLine();
                if (linha.Equals(password))
                {
                    return false;
                    break;
                }

            }
            return true;

        } // Lista Rockyou

        private Boolean testeDois(string password)
        {
            bool cond = false;
            for (int x = 0; x < password.Length; x++)
            {
                String letra = password[x].ToString();
                if (Char.IsLetter(password[x]) && letra.Equals(letra.ToUpper()))
                {
                    cond = true;
                    break;

                }

            }

            return cond;
        } // Maíscula

        private Boolean testeTres(string password)
        {
            bool cond = false;
            for (int x = 0; x < password.Length; x++)
            {

                if (char.IsDigit(password[x]))
                {
                    cond = true;
                    break;
                }
            }
            return cond;
        } // Numero

        private Boolean testeQuatro(string password)
        {
            for (int x = 0; x < password.Length; x++)
            {
                if (!Char.IsLetterOrDigit(password[x]))
                {
                    return true;
                }
            }
            return false;
        }// Char especial

        private Boolean testeCinco(string password)
        {
            if (password.Length >= 10)
            {
                return true;
            }
            return false;
        } // 10+ caracteres

        private void updateCheckColors(bool[] testes)
        {
            

            for (int x = 0; x < testes.Length; x++)
            {
                if (testes[x] == true)
                {
                    cores[x].BackColor = Color.Lime;
                    
                }
                else
                {
                    cores[x].BackColor = Color.Red;
                }
            }

            
        }// Update das cores

        private void afterTest(bool[] testes)
        {
            int counter = 0;


            for (int x = 0; x < 5; x++)
            {
                if (testes[x] == true && testesCheck[x] == 0)
                {
                    selectText(x);

                    cores[x].BackgroundImage = imgs[x];
                    testesCheck[x] = 1;
                }
                if (testes[x] == true)
                {
                    counter++;
                }
            }

            if (counter == 5 && testesCheck[5]==0)
            {
                readMessage(29, 35);
                testesCheck[5] = 1;
            }
        }

        private void selectText(int test)
        {
            switch (test + 1)
            {
                case 1:
                    readMessage(9, 11);
                    break;
                case 2:
                    readMessage(16, 18);
                    break;
                case 3:
                    readMessage(19, 21);
                    break;
                case 4:
                    readMessage(22, 24);
                    break;
                case 5:
                    readMessage(25, 28);
                    break;
            }
        }

        private void readMessage(int start, int end)
        {
            for (int x = start; x <= end; x++)
            {
                MessageBox.Show(mensagens[x] + "");
            }
        }

        private static String[] loadText()
        {

            string path = Application.StartupPath + "\\texts.txt";
            StreamReader sr = new StreamReader(path);
            string[] text = new string[39];
            for (int x = 1; x < 39; x++)
            {
                text[x] = sr.ReadLine();
            }

            return text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (teste == false) { this.Close(); }
            readMessage(1, 6);
        }

        private void pb_1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            execTestes(tB_senha.Text);
        }

        private void pb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
