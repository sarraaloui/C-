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
namespace projet_final
{
    public partial class rapport : Form
    {

        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-60R27DS;Initial Catalog=remboursement;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public rapport(string x)
        {
            InitializeComponent();
            remplissage(x);
            this.WindowState = FormWindowState.Maximized;

        }
        public void remplissage(string x)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("select *  from bulletin where  numero = '" + x + "'    ", cnx);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            double f = Convert.ToDouble(reader["frais_acte"]);
            double r = Convert.ToDouble(reader["remboursement"]);
            string rep = Convert.ToString(reader["reponse"]);
            textBox4.Text = rep;
            string m = Convert.ToString(reader["matricule"]);
            cnx.Close();
            cnx.Open();
            SqlCommand cmd7 = new SqlCommand("select *  from employee where  matricule = '" + m + "'    ", cnx);
            SqlDataReader reader11 = cmd7.ExecuteReader();
            if (reader11.HasRows)
            {
                reader11.Read();
                textBox3.Text = m;
                textBox2.Text = Convert.ToString(reader11["nom"]);
                textBox1.Text = Convert.ToString(reader11["prenom"]);
                if (rep.Equals("accepte"))
                {

                    if (f * 0.3 > r)
                    {
                        string r1 = r.ToString();
                        double calcu = f * 0.3 - r;
                        string cal = calcu.ToString();
                        textBox5.Text = "on a juste accepter " + r1 + "et il vous manque " + cal;
                        cnx.Close();


                    }
                    else if (f * 0.3 == r)
                    {
                        textBox5.Text = "on a accepter votre demande";
                        cnx.Close();
                    }
                }
                else
                {
                    textBox5.Text = "votre demande n'est pas accepter ";
                    cnx.Close();

                }
            }else
            {
                MessageBox.Show("numero bulletin errone");
            }

        }

        private void rapport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            espace_agent e4 = new espace_agent();
            this.Hide();
            e4.Show();

        }
    }
    }
