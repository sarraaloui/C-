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
    public partial class espace_agent : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-60R27DS;Initial Catalog=remboursement;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public espace_agent()
        {
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();
        }
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }
        private void espace_agent_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //    bool sortir = true;

            string num = textBox1.Text;

            if (num.Length == 0)
            {
                MessageBox.Show("saisir le numero du bulletin svp !");
            }

            deconnecter();
            cnx.Open();
            SqlCommand cmd1 = new SqlCommand(" select * from bulletin where numero = '" + num + "' ", cnx);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (!reader1.HasRows)
            {
                MessageBox.Show("bulletin inexistant");
                cnx.Close();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Load(reader1);
                dataGridView1.DataSource = dt;
                cnx.Close();
            }








        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            deconnecter();
            cnx.Open();
            if (textBox1.Text == "")
            {
                MessageBox.Show("saisir num bulletin ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SqlCommand cmd1 = new SqlCommand("select numero from bulletin where numero= '" + textBox1.Text + "' ", cnx);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                cnx.Close();
                cnx.Open();
                SqlCommand cmd2 = new SqlCommand($"delete from bulletin where numero=@num", cnx);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@num", int.Parse(textBox1.Text));
                cmd2.ExecuteNonQuery();
                cnx.Close();

                MessageBox.Show("bulletin supprime avec succés ");
                cnx.Close();
            }
            else
            {
                MessageBox.Show(" num bulletin erroné  ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cnx.Close();
            }


        }

        private void saisir_Click(object sender, EventArgs e)
        {
            espace_agent2 ee2 = new espace_agent2();
            this.Hide();
            ee2.Show();



        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }

        private void saisir_rapport_Click(object sender, EventArgs e)
        {
            //bool test = true;
            if (textBox1.Text == "")
            {
                MessageBox.Show("saisir numero vulletin ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (textBox2.Text == "")
            {
                MessageBox.Show("saisir matricule ");
            }
            cnx.Close();
            cnx.Open();
            SqlCommand cmd = new SqlCommand("select *  from bulletin where date_depot ='" + date1.SelectionRange.Start.ToString("yyyy-MM-dd") + "' and numero = '" + Convert.ToInt32(textBox1.Text) + "'   ", cnx);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                double x = Convert.ToDouble(reader["frais_acte"]);

                double y = x * 0.3;
                // il faux recupérer la matricule de l'employée car textbox 1 est num bulletin 
                // &jouter un text box 2 pour la saisie dela matricule  
                cnx.Close();
                cnx.Open();
                SqlCommand cmd1 = new SqlCommand(" select *  from employee where matricule = '" + textBox2.Text+"'", cnx);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                reader1.Read();
                double pla = Convert.ToDouble(reader1["plafont"]);
                cnx.Close();
                cnx.Open();
                if (pla == 0){
                    SqlCommand cmd2 = new SqlCommand(" update bulletin set reponse = 'refusé'   where date_depot ='" + date1.SelectionRange.Start.ToString("yyyy-MM-dd") + "' and numero = '" + textBox1.Text + "'   ", cnx);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                }

                else if (pla - y > 0)
                {
                    SqlCommand cmd2 = new SqlCommand(" update bulletin set reponse = 'accepte'  where date_depot ='" + date1.SelectionRange.Start.ToString("yyyy-MM-dd") + "' and numero = '" + textBox1.Text + "'   ", cnx);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    double p=pla-y;
                    SqlCommand cmd3 = new SqlCommand(" update employee set plafont = '"+p+"'    where matricule = '" + textBox2.Text + "'   ", cnx);
                    cnx.Close();
                    cnx.Open();
                    reader2 = cmd3.ExecuteReader();
                    cnx.Close();
                    cnx.Open();
                    SqlCommand cmd4 = new SqlCommand(" update bulletin set remboursement  = '"+y+"'  where numero = '" + Convert.ToInt32(textBox1.Text) + "'   ", cnx);

                    SqlDataReader reader4 = cmd4.ExecuteReader();

                }
                else if (pla - y < 0){

                    SqlCommand cmd2 = new SqlCommand(" update bulletin set reponse = 'accepte'   where date_depot ='" + date1.SelectionRange.Start.ToString("yyyy-MM-dd") + "' and numero = '" + Convert.ToInt32(textBox1.Text) + "'   ", cnx);
                    SqlCommand cmd3 = new SqlCommand(" update bulletin set remboursement  = '"+pla+"'   where numero= '" + Convert.ToInt32(textBox1.Text) + "'   ", cnx);
                    SqlCommand cmd4 = new SqlCommand(" update employee set plafont = 0    where matricule = '" + textBox2.Text + "'   ", cnx);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    SqlDataReader reader4 = cmd4.ExecuteReader();
                }





            }

            cnx.Close();

            this.Hide();
            rapport r = new rapport(textBox1.Text);
            r.Show();


        }
        //consulter tt les bulletins
        private void button3_Click(object sender, EventArgs e)
        {
           string  num = textBox1.Text;

        
                cnx.Open();
                SqlCommand cmd1 = new SqlCommand("select * from bulletin  ", cnx);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(reader1);
                dataGridView1.DataSource = d;
                cnx.Close();

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = default;
            textBox1.Text = "";
            textBox2.Text = "";


        }
    }
}
    