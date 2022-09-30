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
    public partial class espace_admin : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-60R27DS;Initial Catalog=remboursement;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        private object axAcroPDF1;

        public espace_admin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }

        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }

        private void espace_admin_Load(object sender, EventArgs e)
        {



        }


        //**AJOUT EMPLOYE**
        private void button1_Click(object sender, EventArgs e)
        {
            string matricule = textBox1.Text;
            string nom = textBox2.Text;
            string prenom = textBox3.Text;
            string cin = textBox4.Text;
            string date = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            string adr = textBox5.Text;
            string tel = textBox6.Text;
            string cnam = textBox7.Text;
            string prenom_c = textBox8.Text;
            string nom_c = textBox9.Text;
            bool exist = true;
            int nb = ((int)numericUpDown1.Value);
            //int nb = int.Parse(textBox1.Text);
            int grade = 0; string etat = " ";
            bool sortir = true;
            while (sortir)
            {
                if (matricule.Length == 0)

                {
                    MessageBox.Show("saisir matricule de l'employe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sortir = false;

                }
                else if (cin.Length == 0)
                {
                    MessageBox.Show("saisir cin de l'employe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sortir = false;

                }
                else if (cnam.Length == 0)
                {
                    MessageBox.Show("saisir le code cnam ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sortir = false;

                }
                else if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false)
                {
                    MessageBox.Show("saisir le grade ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sortir = false;

                }
                else if (radioButton5.Checked == false && radioButton6.Checked == false && radioButton7.Checked == false)
                {
                    MessageBox.Show("saisir l'etat ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sortir = false;

                }
                else if (radioButton5.Checked == true && (nb == default || prenom_c.Length == 0 || nom_c.Length == 0))
                {
                    MessageBox.Show("saisir les champs qui concernent votre etat ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sortir = false;

                }

                if (radioButton1.Checked) { grade = 1; }
                if (radioButton2.Checked) { grade = 2; }
                if (radioButton3.Checked) { grade = 3; }
                if (radioButton4.Checked) { grade = 4; }


                if (radioButton5.Checked) { etat = "marie"; }
                if (radioButton6.Checked) { etat = "celibataire"; }
                if (radioButton7.Checked) { etat = "divorce"; }

                string ch = "already exists";

                deconnecter();
                cnx.Open();
                cmd = new SqlCommand("select matricule from employee where matricule='" + matricule + "'", cnx);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = "";
                    label17.Show();
                    label17.Text = ch;
                    cnx.Close();        
                    sortir = false;exist = false;

                }
                deconnecter();
                cnx.Open();
                SqlCommand cmd1 = new SqlCommand("select cin from employee where cin='" + cin + "'", cnx);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    textBox4.Text = "";
                    label18.Show();
                    label18.Text = ch;
                    sortir = false; exist = false;
                    cnx.Close();

                }
                deconnecter();
                cnx.Open();
                SqlCommand cmd2 = new SqlCommand("select code_cnam from employee where code_cnam='" + cnam + "'", cnx);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    cnam = "";
                    label19.Show();
                    label19.Text = ch;
                    sortir = false;exist = false;
                    cnx.Close();
                }


                float plafond = 0;
                switch (grade)
                {
                    case 1:
                        plafond = 1800;
                        break;
                    case 2:
                        plafond = 1400;
                        break;
                    case 3:
                        plafond = 1000;
                        break;
                    case 4:
                        plafond = 600;
                        break;

                }


                deconnecter();
                cnx.Open();
                SqlCommand cmd4 = new SqlCommand("select * from employee where matricule='" + matricule + "' and cin= '" + cin + "'", cnx);
                SqlDataReader reader4 = cmd4.ExecuteReader();
                DataTable d22 = new DataTable();
                d22.Load(reader4);
                if (d22.Rows.Count > 0)
                {

                    MessageBox.Show("employee already exist");
                    cnx.Close();
                    sortir = false;
                }
                else
                {
                    if (exist == true && sortir==true) {
                    deconnecter();

                    using (SqlCommand cmd3 = new SqlCommand())
                    {

                        cmd3.Connection = cnx;
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = @"insert into employee values(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14)";
                        cmd3.Parameters.AddWithValue("@param1", matricule);
                        cmd3.Parameters.AddWithValue("@param2", nom);
                        cmd3.Parameters.AddWithValue("@param3", prenom);
                        cmd3.Parameters.AddWithValue("@param4", cin);
                        cmd3.Parameters.AddWithValue("@param5", date);
                        cmd3.Parameters.AddWithValue("@param6", adr);
                        cmd3.Parameters.AddWithValue("@param7", grade);
                        cmd3.Parameters.AddWithValue("@param8", tel);
                        cmd3.Parameters.AddWithValue("@param9", cnam);
                        cmd3.Parameters.AddWithValue("@param10", etat);
                        cmd3.Parameters.AddWithValue("@param11", nom_c);
                        cmd3.Parameters.AddWithValue("@param12", prenom_c);
                        cmd3.Parameters.AddWithValue("@param13", nb);
                        cmd3.Parameters.AddWithValue("@param14", plafond);

                        try
                        {
                            cnx.Open();
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("added successfully");
                            sortir = false;
                        }
                        catch (SqlException e1)
                        {
                            MessageBox.Show(e1.Message.ToString(), "erreeeuuur insertion ");
                            sortir = false;
                        }
                    }
                }


 }
            }



        }
        //******Reset button******
        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = default;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            //  string date = monthCalendar1.ToString();
            string date = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            textBox5.Text = "";
            string tel = textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            numericUpDown1.Value = default;
            textBox10.Text = "";
            textBox11.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            label17.Hide();
            label18.Hide();
            label19.Hide();


        }
        //view employeees
        private void button6_Click(object sender, EventArgs e)
        {
            string matricule = textBox1.Text;
            string cin = textBox4.Text;
            deconnecter();
            cnx.Open();
            SqlCommand cmd5 = new SqlCommand("select *  from employee ", cnx);
            SqlDataReader reader5 = cmd5.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader5);
            dataGridView1.DataSource = dt;
            cnx.Close();
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

        //UPDATE employee
        private void button2_Click(object sender, EventArgs e)
        {
            string matricule = textBox1.Text;
            string nom = textBox2.Text;
            string prenom = textBox3.Text;
            string cin = textBox4.Text;
            string date = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            string adr = textBox5.Text;
            string tel = textBox6.Text;
            string cnam = textBox7.Text;
            string prenom_c = textBox8.Text;
            string nom_c = textBox9.Text;

            int nb = ((int)numericUpDown1.Value);

            int grade = 0; string etat = " ";
            bool sortir = true;

            if (matricule.Length == 0)
            {
                MessageBox.Show("saisir matricule de l'employe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cin.Length == 0)
            {

                MessageBox.Show("saisir cin de l'employe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            if (radioButton1.Checked) { grade = 1; }
            if (radioButton2.Checked) { grade = 2; }
            if (radioButton3.Checked) { grade = 3; }
            if (radioButton4.Checked) { grade = 4; }
            string ch1 = "must be filled";
            if (radioButton5.Checked) { etat = "marie"; }
            if (radioButton6.Checked) { etat = "celibataire"; }
            if (radioButton7.Checked) { etat = "divorce"; }


            while (sortir)
            {

                if (matricule.Length == 0)
                {
                    label17.Show();
                    label17.Text = ch1;
                    sortir = false;
                    break;
                }
                if (cin.Length == 0)
                {
                    label18.Show();
                    label18.Text = ch1;
                    sortir = false;
                    break;

                }
                if (cnam.Length == 0)
                {
                    label19.Show();
                    label19.Text = ch1;
                    sortir = false;
                    break;

                }

                //tester 
                deconnecter();
                cnx.Open();
                SqlCommand cmd1 = new SqlCommand("select * from employee where matricule= '" + matricule + "' and cin= '" + cin + "' and code_cnam= '" + cnam + "' ", cnx);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    //Insert les nv champs
                    deconnecter();
                    cnx.Open();
                    SqlCommand cmd6 = new SqlCommand("update employee set  nom ='" + nom + "',prenom ='" + prenom + "', date_naissance ='" + date + "', adresse ='" + adr + "',grade ='" + grade + "',tel = '" + tel + "',etat ='" + etat + "',nom_conjoint ='" + nom_c + "', prenom_conjoint ='" + prenom_c + "',nb_enfants ='" + nb + "' where matricule='" + matricule + "'", cnx);
                    cmd6.ExecuteNonQuery();
                    MessageBox.Show(" successfully  Updated");
                    sortir = false;
                    cnx.Close();
                }
                /*
                if(!reader1.HasRows)
                {
                    MessageBox.Show("   inexistant employee");
                    sortir = false;
                    break;

                    cnx.Close();


                }*/
                // A ne pas changer cin,code cnam de lemployee

                deconnecter();
                cnx.Open();
                SqlCommand cmd2 = new SqlCommand("select matricule,cin,code_cnam from employee where matricule= '" + matricule + "' ", cnx);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    if (identiques(reader2.GetString(1), cin) == false)
                    {
                        MessageBox.Show("vous navez pas le droit de changer votre cin");
                        sortir = false;
                        break;

                    }
                    if (identiques(reader2.GetString(2), cnam) == false)
                    {
                        MessageBox.Show("vous navez pas le droit de changer votre code cnam");
                        sortir = false;
                        break;

                    }
                }
                cnx.Close();

                // A ne pas changer matricule employee
                deconnecter();
                cnx.Open();
                SqlCommand cmd3 = new SqlCommand("select matricule,cin,code_cnam from employee where cin= '" + cin + "' and code_cnam= '" + cnam + "' ", cnx);
                SqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.Read())
                {
                    if (identiques(reader3.GetString(0), matricule) == false)
                    {
                        MessageBox.Show("vous navez pas le droit de changer votre matricule");
                        sortir = false;
                        break;

                    }
                }
                cnx.Close();









            }
            if (sortir == true)
            {
                MessageBox.Show("matricule introuvable");
            }
        }
        //DELETE 
        private void button3_Click(object sender, EventArgs e)
        {
            string matricule = textBox1.Text;
            bool sortir = true;


            while (sortir)
            {

                if (matricule == "")
                {
                    label17.Show();
                    label17.Text = "must be filled";
                    sortir = false;
                }
                deconnecter();
                cnx.Open();
                SqlCommand cmd1 = new SqlCommand("select matricule from employee where matricule='" + matricule + "'", cnx);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    deconnecter();
                    cnx.Open();
                    SqlCommand cmd2 = new SqlCommand("delete from employee where matricule ='" + matricule + "'", cnx);
                    cmd2.ExecuteNonQuery();
                    sortir = false;
                    MessageBox.Show("deleted successfully");
                    cnx.Close();
                }
                else
                {
                    MessageBox.Show(" matricule erroné");
                    sortir = false;

                }

                cnx.Close();





            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        // SEARCH ***************
        private void button5_Click(object sender, EventArgs e)
        {
            deconnecter();
            string matricule = textBox1.Text;
            bool sortie = true;
            while (sortie)
            {
                if (matricule.Length == 0)
                {
                    label17.Show();
                    label17.Text = "must be filled";
                    sortie = false;
                }
                else
                {

                    deconnecter();
                    cnx.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from employee where matricule ='" + matricule + "'", cnx);
                    reader = cmd1.ExecuteReader();
                    DataTable d = new DataTable();
                    d.Load(reader);
                    dataGridView1.DataSource = d;

                    sortie = false;

                    cnx.Close();

                    if (d.Rows.Count < 1)
                    {
                        MessageBox.Show("inexistant employee");
                        sortie = false;
                    }
                }
            }
        }

        //*******VOIR BULLETIN *******
        private void button4_Click(object sender, EventArgs e)
        {
            deconnecter();
            string matricule = textBox1.Text;
            bool sortie = true;
            while (sortie)
            {
                if (matricule.Length == 0)
                {
                    label17.Show();
                    label17.Text = "must be filled";
                    sortie = false;
                }
                else
                {
                    deconnecter();
                    cnx.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from bulletin ", cnx);
                    reader = cmd1.ExecuteReader();
                    DataTable d = new DataTable();
                    d.Load(reader);
                    dataGridView1.DataSource = d;
                    sortie = false;
                    cnx.Close();

                }
            }
        }
        //BULLETIN PER EMPLOYEE
        private void button7_Click(object sender, EventArgs e)
        {
            deconnecter();
            string matricule = textBox1.Text;
            bool sortie = true;
            while (sortie)
            {
                if (matricule.Length == 0)
                {
                    label17.Show();
                    label17.Text = "must be filled";
                    sortie = false;
                }
                else
                {
                    deconnecter();
                    cnx.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from bulletin where matricule ='" + matricule + "'", cnx);
                    reader = cmd1.ExecuteReader();
                    DataTable d = new DataTable();
                    d.Load(reader);
                    dataGridView1.DataSource = d;
                    sortie = false;
                    cnx.Close();

                }
            }
        }

        //fazt l pdf
        private void button8_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document= printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK) { printDocument1.Print(); }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MONTHLY BILL", new Font("arial", 20, FontStyle.Bold), Brushes.Black, new Point(10, 10));
            e.Graphics.DrawString("Office Address", new Font("arial", 10, FontStyle.Bold), Brushes.Black, new Point(10, 150));
        }

        private void button9_Click(object sender, EventArgs e)
        {

            //
            //mois= 0; 
            //annee = 0;
            if(textBox10.Text=="" && textBox11.Text=="")
            {
                MessageBox.Show("saisir lannee ou le mois svp");
            }

            if (textBox10.Text != "") { 
            if (Convert.ToInt32(textBox10.Text) > 2022)
            {
                MessageBox.Show("année invalide");

            }}

            if (textBox11.Text != "") {
            if (Convert.ToInt32(textBox11.Text) > 12)
            {
                MessageBox.Show("mois invalide");
            } }
            if (textBox10.Text !="")
            {
                deconnecter();
                cnx.Open();
                int annee = Convert.ToInt32(textBox10.Text);

                SqlCommand cmd8 = new SqlCommand("select sum(remboursement)  from bulletin where  year(date_depot)= " + annee.ToString() , cnx);
                SqlDataReader r = cmd8.ExecuteReader();
                r.Read();
                label20.Show(); label21.Text=r.GetDouble(0).ToString();
                label22.Show();
                label21.Show();
                label22.Text = "DT";
                label20.Text = "Total remboursement est :";
                cnx.Close();
            }else


            if (textBox11.Text !="")
            {
                deconnecter();
                cnx.Open();
                int mois = Convert.ToInt32(textBox11.Text);
                //  int x = Convert.ToInt32(textBox11.Text);

                SqlCommand cmd4 = new SqlCommand("select sum(frais_acte)   from bulletin where  month(date_depot)= " + mois.ToString(), cnx);
                double x1 = (double)cmd4.ExecuteScalar();
                // MessageBox.Show (x.ToString());
                label20.Show();
                label22.Show();
                label21.Show();
                label21.Text = x1.ToString();
                label22.Text = "DT";
                label20.Text = "Total remboursement est :";
                cnx.Close();
            }


        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form1 c = new Form1();
            this.Hide();
            c.Show();
        }
    }
    }

