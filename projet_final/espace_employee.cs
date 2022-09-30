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
    public partial class espace_employee : Form
    {

        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-60R27DS;Initial Catalog=remboursement;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public espace_employee()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }

        private void espace_employee_Load(object sender, EventArgs e)
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
            string date = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            string matricule = textBox1.Text;
            bool sortie = true;
            while (sortie) {
            if (date==default)
            {
                    label1.Show();
                    label1.Text = "saisir la date de depot *";
                    sortie = false;
                }
             if (matricule.Length == 0)
            {
                label2.Show();
                label2.Text="saisir votre matricule*";
                    sortie = false;

                }
              

                    deconnecter();
                    cnx.Open();
                    SqlCommand cmd1 = new SqlCommand("select matricule from employee where matricule='" + matricule + "'", cnx);
                    SqlDataReader reader1 = cmd1.ExecuteReader();

                    if (!reader1.HasRows)
                    {
                        MessageBox.Show("matricule erroné"); 
                        sortie = false;
                        cnx.Close();
                      }
                else { 
                    deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select matricule from bulletin where matricule= '"+matricule+"' and date_depot= '"+date+"'", cnx);
            reader = cmd.ExecuteReader();
                string rep="";
            if (!reader.HasRows)
             /* {
                     deconnecter();
                    cnx.Open();
                    //affiche le resultat
                    SqlCommand cmd3 = new SqlCommand("select reponse from bulletin where matricule= '"+matricule+ "' and date_depot= '" + date + "' ; ", cnx);
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    if (!reader3.Read())
                 {
                       if(reader3.GetInt32(0)==1)
                        {
                            rep = "acceptee";
                        }
                        else
                        {
                            rep = "refuse";

                        }
                        cnx.Close(); }
                    }
                    else*/
                    {
                        MessageBox.Show(" **date introuvable** ");
                        sortie = false;
                    }
                    else { 
               
         deconnecter();
                    cnx.Open();
                    SqlCommand cmd2 = new SqlCommand("select matricule ,acte_effectue,reponse,frais_acte ,date_depot from bulletin where matricule='"+matricule+"' and date_depot= '"+date+"' ;",cnx);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows) {
                    DataTable dt = new DataTable();
                    dt.Load(reader2);
                    dt.Columns.Add ( rep)   ;                  
                    dataGridView1.DataSource = dt;
                        sortie = false;
                    }
    cnx.Close();}

                  
               
                }
            
 }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 c = new Form1();
            this.Hide();
            c.Show();
        }
    }
    }

