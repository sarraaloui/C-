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
    public partial class Form1 : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-60R27DS;Initial Catalog=remboursement;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
            this.WindowState =FormWindowState.Maximized;
        }
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        static public Boolean identiques(string x, string y)
        {
            Boolean ok = true;
            if (x.Length != y.Length)
            {
                ok = false;
            }
            else
            {
                int i = 0;
                while (i < x.Length && ok)
                {
                    if (x[i] != y[i])
                    {
                        ok = false;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return ok;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           string user= textBox1.Text;
            string pass = textBox2.Text;
            bool sortir = true;bool inexistant = false;
            if (user.Length == 0)
            {
                MessageBox.Show("saisir votre id ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (pass.Length==0)
            {
                MessageBox.Show("saisir votre mot de passe", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            while (sortir)
            {
                deconnecter();
                cnx.Open();
                //TEST table EMPLOYEE
                cmd = new SqlCommand("select matricule,cin from employee where matricule= '"+user +"'  ", cnx);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (identiques(reader.GetString(1), pass) == false)
                    {
                        MessageBox.Show("votre mot de passe est incorrecte ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sortir = false;
                        cnx.Close();
                    }
                }
                    
                        deconnecter();
                        cnx.Open();
                        SqlCommand cmd1 = new SqlCommand("select matricule,cin from employee where cin= '" + pass + "'  ", cnx);
                       SqlDataReader reader1 = cmd1.ExecuteReader();
                        if (reader1.Read())
                        {
                            if (identiques(reader1.GetString(0), user) == false)
                            {
                                MessageBox.Show("votre identifiant est incorrecte ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sortir = false;
                                cnx.Close();
                            }
                         }

                deconnecter();
                cnx.Open();
                SqlCommand cmd2 = new SqlCommand("select matricule,cin from employee where cin= '" + pass + "' and matricule= '"+user+"' ", cnx);
                SqlDataReader reader2 = cmd1.ExecuteReader();
                if (reader2.Read())
                {
                    if (identiques(reader2.GetString(0), user) && identiques(reader2.GetString(1), pass))
                    {
                        espace_employee ee = new espace_employee();
                        this.Hide();
                        ee.Show();
                        sortir = false;
                        cnx.Close();
                        inexistant = true;
                    }
                }

                //TEST table admin

                deconnecter();
                cnx.Open();
               
               SqlCommand cmd3 = new SqlCommand("select identifiant,password from admin where identifiant= '" + user + "'  ", cnx);
                SqlDataReader reader3 = cmd3.ExecuteReader();

                if (reader3.Read())
                {
                    if (identiques(reader3.GetString(1), pass) == false)
                    {
                        MessageBox.Show("votre mot de passe admin est incorrecte  ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sortir = false;
                        cnx.Close();
                    }
                }

                deconnecter();
                cnx.Open();
                SqlCommand cmd4 = new SqlCommand("select identifiant,password from admin where password= '" + pass + "'  ", cnx);
                SqlDataReader reader4 = cmd4.ExecuteReader();
                if (reader4.Read())
                {
                    if (identiques(reader4.GetString(0), user) == false)
                    {
                        MessageBox.Show("votre identifiant admin est incorrecte ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sortir = false;
                        cnx.Close();
                    }
                }

                deconnecter();
                cnx.Open();
                SqlCommand cmd5 = new SqlCommand("select identifiant,password from admin where password= '" + pass + "' and identifiant= '" + user + "' ", cnx);
                SqlDataReader reader5 = cmd5.ExecuteReader();
                if (reader5.Read())
                {
                    if (identiques(reader5.GetString(0), user) && identiques(reader5.GetString(1), pass))
                    {
                        espace_admin ee1 = new espace_admin();
                        this.Hide();
                        ee1.Show();
                        sortir = false;
                        inexistant = true;
                        cnx.Close();
                    }
                }
                //TEST table agent

                deconnecter();
                cnx.Open();

                SqlCommand cmd7= new SqlCommand("select identifiant,password from agent where identifiant= '" + user + "'  ", cnx);
                SqlDataReader reader7 = cmd7.ExecuteReader();

                if (reader7.Read())
                {
                    if (identiques(reader7.GetString(1), pass) == false)
                    {
                        MessageBox.Show("votre mot de passe agent est incorrecte  ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sortir = false;
                        cnx.Close();
                    }
                }

                deconnecter();
                cnx.Open();
                SqlCommand cmd8 = new SqlCommand("select identifiant,password from agent where password= '" + pass + "'  ", cnx);
                SqlDataReader reader8 = cmd8.ExecuteReader();
                if (reader8.Read())
                {
                    if (identiques(reader8.GetString(0), user) == false)
                    {
                        MessageBox.Show("votre identifiant agent est incorrecte ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sortir = false;
                        cnx.Close();
                    }
                }

                deconnecter();
                cnx.Open();
                SqlCommand cmd9 = new SqlCommand("select identifiant,password from agent where password= '" + pass + "' and identifiant= '" + user + "' ", cnx);
                SqlDataReader reader9 = cmd9.ExecuteReader();
                if (reader9.Read())
                {
                    if (identiques(reader9.GetString(0), user) && identiques(reader9.GetString(1), pass))
                    {
                        espace_agent ee2 = new espace_agent();
                        this.Hide();
                        ee2.Show();
                        inexistant = true;
                        sortir = false;
                        cnx.Close();
                    }
                }

                if (inexistant == false)
                {
                    MessageBox.Show("login et mdp inexistant");
                    sortir = false;
                }
            }













            }



        }
    }

