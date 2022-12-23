using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ИС_Дозвілля_молоді
{
    public partial class Register_client : Form
    {
        SqlConnection sqlConnection;
        public Register_client()
        {
            InitializeComponent();
        }
        private async void Register_client_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DIMA2003\SQL2;Initial Catalog=Зірочки;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label4.Visible)
                label4.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Users] (Login, Pass, Email)VALUES (@Login, @Pass, @Email)", sqlConnection);
                command.Parameters.AddWithValue("Login", textBox1.Text);
                command.Parameters.AddWithValue("Pass", textBox2.Text);
                command.Parameters.AddWithValue("Email", textBox3.Text);


                await command.ExecuteNonQueryAsync();

                MessageBox.Show("Реєстрація пройшла успішно !", "Дякуємо за реєстрацію");

                this.Hide();
                Forms_clienta Form = new Forms_clienta();
                Form.Show();
                //  this.Close();
            }
            else
            {
                label4.Visible = true;
                label4.Text = "Поля не заповленні";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
       
                if (Regex.IsMatch(textBox3.Text, pattern))
                {
                 errorProvider1.Clear();
                button3.Enabled = true;
                }
                else
                {
                errorProvider1.SetError(this.textBox3, "Невірний формат");
                button3.Enabled = false;
                return;
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
