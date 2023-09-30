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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.OleDb;

namespace Rehber19
{
    public partial class Form1 : Form
    {

        SqlConnection conn;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable table;


        public Form1()
        {
            conn = new SqlConnection("Data Source=EMIRPC;Initial Catalog=TelefonRehberi;Integrated Security=True");
            InitializeComponent();
        }
        

        void kisigetir()
        {
            
            adapter = new SqlDataAdapter("SELECT * FROM Person", conn);
            table = new DataTable();
            conn.Open();
            adapter.Fill(table);
            dgvPerson.DataSource = table;
            conn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            kisigetir();
        }

     
        private void dgvPerson_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvPerson.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dgvPerson.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = dgvPerson.CurrentRow.Cells[3].Value.ToString();
            txtEMail.Text = dgvPerson.CurrentRow.Cells[4].Value.ToString();
            txtAddress.Text = dgvPerson.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnDell_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE Person Where Id=@id";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("id", dgvPerson.CurrentRow.Cells[0].Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            kisigetir();
        }
        int i = 0;
        private void btnUpload_Click(object sender, EventArgs e)
        {

            string update = ("Update Person Set Name=@name,Surname=@surname,Phone=@phone,EMail=@email,Address=@address Where Id=@id");
            cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@surname", txtSurname.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@email", txtEMail.Text);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("id", dgvPerson.Rows[i].Cells[0].Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            kisigetir();
        }

      

        private void dgvPerson_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            txtName.Text = dgvPerson.Rows[i].Cells[0].Value.ToString();
            txtSurname.Text = dgvPerson.Rows[i].Cells[1].Value.ToString();
            txtPhone.Text = dgvPerson.Rows[i].Cells[2].Value.ToString();
            txtEMail.Text = dgvPerson.Rows[i].Cells[3].Value.ToString();
            txtAddress.Text = dgvPerson.Rows[i].Cells[4].Value.ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            string save = "insert into Person(Name,Surname,Phone,Email,Address) values (@name,@surname,@phone,@email,@address) ";
            cmd = new SqlCommand(save, conn);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@surname", txtSurname.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@email", txtEMail.Text);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            kisigetir();



        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'telefonRehberiDataSet.Person' table. You can move, or remove it, as needed.
            this.personTableAdapter.Fill(this.telefonRehberiDataSet.Person);

        }

        private DataTable GetTable()
        {
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM Person Where Name like '%" + textBox1.Text + "%' ", conn);
            adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet(); 
            adapter.Fill(ds);    
            dgvPerson.DataSource = ds.Tables[0];
            conn.Close();
        }

       
    }
}
