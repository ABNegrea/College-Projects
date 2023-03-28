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

namespace Laborator1_SGBD
{
    public partial class Form1 : Form
    {
        SqlConnection cs = new SqlConnection("Data Source=LAPTOP-8UNML2ER\\SQLEXPRESS; Initial Catalog = CollectionDB; Integrated Security = True");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Anime", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox4.Clear();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                textBox1.Text = row.Cells["IdA"].Value.ToString();
                textBox2.Text = row.Cells["IdA"].Value.ToString();
            }
            da.SelectCommand = new SqlCommand("SELECT * FROM AnimeSeason WHERE IdA=" + textBox1.Text, cs);
            RefreshChildForm(1);
        }

        private void RefreshChildForm(int index)
        {
            if (ds.Tables.Count > 1)
                ds.Tables.Remove(ds.Tables[index]);
            ds.Tables.Add();
            da.Fill(ds.Tables[index]);
            dataGridView2.DataSource = ds.Tables[index];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                da.InsertCommand = new SqlCommand("INSERT INTO AnimeSeason (Name, Rating, Episodes, EpCompleted," +
                                                  "Status, IdA) VALUES(@n, @r, @e, @c, @s, @i)", cs);
                da.InsertCommand.Parameters.Add("@n", SqlDbType.VarChar).Value = textBox3.Text;
                da.InsertCommand.Parameters.Add("@r", SqlDbType.TinyInt).Value =  Int32.Parse(textBox7.Text);
                da.InsertCommand.Parameters.Add("@e", SqlDbType.Int).Value = Int32.Parse(textBox5.Text);
                da.InsertCommand.Parameters.Add("@c", SqlDbType.Int).Value = Int32.Parse(textBox6.Text);
                da.InsertCommand.Parameters.Add("@s", SqlDbType.VarChar).Value = comboBox1.Text;
                da.InsertCommand.Parameters.Add("@i", SqlDbType.Int).Value = Int32.Parse(textBox2.Text);
                cs.Open();
                da.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Inserted succesfully to the Database");
                cs.Close();
                // already inserted - apear in the list
                if (ds.Tables.Count > 1)
                    ds.Tables.Remove(ds.Tables[1]);
                ds.Tables.Add();
                da.Fill(ds.Tables[1]);
                dataGridView2.DataSource = ds.Tables[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cs.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                da.UpdateCommand = new SqlCommand("UPDATE AnimeSeason SET Name=@n, Rating=@r, Episodes=@e," +
                                                  "EpCompleted=@c, Status=@s WHERE IdAS = @i", cs);
                da.UpdateCommand.Parameters.Add("@n", SqlDbType.VarChar).Value = textBox8.Text;
                da.UpdateCommand.Parameters.Add("@r", SqlDbType.TinyInt).Value = Int32.Parse(textBox11.Text);
                da.UpdateCommand.Parameters.Add("@e", SqlDbType.Int).Value = Int32.Parse(textBox9.Text);
                da.UpdateCommand.Parameters.Add("@c", SqlDbType.Int).Value = Int32.Parse(textBox10.Text);
                da.UpdateCommand.Parameters.Add("@s", SqlDbType.VarChar).Value = comboBox2.Text;
                da.UpdateCommand.Parameters.Add("@i", SqlDbType.Int).Value = Int32.Parse(textBox4.Text);
                cs.Open();
                int x = da.UpdateCommand.ExecuteNonQuery();
                cs.Close();
                if (x > 0)
                    MessageBox.Show("Object succesfully updated!");
                RefreshChildForm(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cs.Close();
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView2.SelectedRows[0];
                textBox4.Text = row.Cells["IdAS"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Are you sure you want to PERMANENTLY remove this item?",
                                 "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                da.DeleteCommand = new SqlCommand("DELETE FROM AnimeSeason WHERE IdAS=@id", cs);
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView2.SelectedRows[0];
                    da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value = row.Cells["IdAS"].Value;
                }
                cs.Open();
                da.DeleteCommand.ExecuteNonQuery();
                cs.Close();
                RefreshChildForm(1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
/*            da.SelectCommand = new SqlCommand("SELECT * FROM Anime", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];*/
        }
    }
}
