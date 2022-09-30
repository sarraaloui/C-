using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlClient;
namespace projet_final
{
    public partial class espace_agent2 : Form
    {

        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-60R27DS;Initial Catalog=remboursement;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public espace_agent2()
        {
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();
        }

        private void espace_agent2_Load(object sender, EventArgs e)
        {

        }
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {



            deconnecter();
            cnx.Open();

            cmd = new SqlCommand("select numero from bulletin where matricule='" + textBox4.Text + "'and date_depot LIKE '" + DateTime.Now.Year + "%'", cnx);

            reader = cmd.ExecuteReader();


            if (!reader.HasRows)
            {
                cnx.Close();

                deconnecter();
                cnx.Open();
              SqlCommand  cmd20 = new SqlCommand("update employee set plafont='" + 1800 + "'  where matricule='" + textBox4.Text + "' and grade='" + 1 + "'", cnx);
                SqlCommand cmd30 = new SqlCommand("update employee set plafont='" + 1400 + "'  where matricule='" + textBox4.Text + "' and grade='" + 2 + "'", cnx);
                SqlCommand cmd4 = new SqlCommand("update employee set plafont='" + 1000 + "'  where matricule='" + textBox4.Text + "' and grade='" + 3 + "'", cnx);
                SqlCommand cmd5 = new SqlCommand("update employee set plafont='" + 600 + "'  where matricule='" + textBox4.Text + "' and grade='" + 4 + "'", cnx);
                cmd20.ExecuteNonQuery();
                cmd30.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                cnx.Close();
            }


            deconnecter();
            cnx.Open(); bool test = true; bool exist = true;

            SqlCommand cmd2 = new SqlCommand($"select * from bulletin where numero=" + int.Parse(textBox1.Text) + "and matricule=" + textBox4.Text, cnx);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            if (reader2.Read())
            {
                MessageBox.Show("Ce bulletin exists");
                test = false;

                cnx.Close();
            }

            deconnecter();
            cnx.Open();
            SqlCommand cmd1 = new SqlCommand("select numero from bulletin  where numero =" + int.Parse(textBox1.Text), cnx);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                MessageBox.Show("numero bulletin deja existant");
                test = false;

                cnx.Close();
            }


            deconnecter();
            cnx.Open();
            SqlCommand cmd3 = new SqlCommand("select matricule from employee  where matricule =" + textBox4.Text, cnx);
            SqlDataReader reader3 = cmd3.ExecuteReader();
            if (!reader3.HasRows)
            {
                MessageBox.Show("matricule employe erroné");
                test = false;
                cnx.Close();
            }

            if (test == true)
            {
                deconnecter();
                cnx.Open();
                SqlCommand cmd = new SqlCommand("insert into  bulletin (numero,date_depot,acte_effectue,frais_acte,matricule)  values (@numero,@dat,@acte,@frais,@matricule)", cnx);
                cmd.Connection = cnx;
                cmd.Parameters.AddWithValue("@numero", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@dat", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@acte", textBox2.Text);
                cmd.Parameters.AddWithValue("@frais", textBox3.Text);
                cmd.Parameters.AddWithValue("@matricule", int.Parse(textBox4.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("bulletin ajouté avec succes");
                cnx.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            espace_agent e3 = new espace_agent();
            this.Hide();
            e3.Show();
        }
    }
    }

