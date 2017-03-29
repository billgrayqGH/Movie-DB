using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace LabAssign4Quitalig
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection(" Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter ad = new OleDbDataAdapter();
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        void showData()
        {
            try
            {

                dataGridView1.RowTemplate.Height = 180;
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Movie";
                ad.SelectCommand = cmd;
                dt.Clear();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[9].Width = 300;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i] is DataGridViewImageColumn)
                    {
                        ((DataGridViewImageColumn)dataGridView1.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    }
                }

                dataGridView2.DataSource = dt;
                dataGridView2.RowTemplate.Height = 180;
                dataGridView2.Columns[9].Width = 300;
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    if (dataGridView2.Columns[j] is DataGridViewImageColumn)
                    {
                        ((DataGridViewImageColumn)dataGridView2.Columns[j]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    }
                }
                con.Close();
            }
            catch (OleDbException oe)
            {
                MessageBox.Show(oe.Message);
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ".bmp|*.bmp";
            DialogResult r = openFileDialog1.ShowDialog();

            if (r == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                string genre = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
                string rating = this.comboBox1.GetItemText(this.comboBox2.SelectedItem);

                int i = 0;

                try
                {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT into Movie(movieTitle, movieGenre, movieRating, releaseDate, movieDirector, moviePublisher, movieActors, movieDescription, moviePicture) Values ( '" + textBox1.Text + "','" + genre + "','" + rating + "' ,'" + this.dateTimePicker1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "',@moviePicture)";

               

                    MemoryStream stream = new MemoryStream();
                    pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] pic = stream.ToArray();
                    cmd.Parameters.AddWithValue("@moviePicture", pic);

                    i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        MessageBox.Show("Movie Record Added " + i);
                    }
                }
                catch (NullReferenceException x)
                {
                    MessageBox.Show("Please insert an image for the movie");
                }
            }

            else
            {
                MessageBox.Show("Please insert movie title.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "-select one-";
            comboBox2.Text = "-select one-";
            pictureBox1.Image = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ".bmp|*.bmp";
            DialogResult r = openFileDialog1.ShowDialog();

            if (r == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                textBox13.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox5.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePicker2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox12.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox10.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox11.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();



                con.Open();
                cmd.Connection = con;
                cmd.CommandText = " select moviePicture from Movie where movieID = " + dataGridView1.SelectedRows[0].Cells[0].Value;
                
                ad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                byte[] mydata = new byte[0];
                ad.Fill(ds, "Movie");
                DataRow myrow;
                myrow = ds.Tables["Movie"].Rows[0];
                mydata = (byte[])myrow["moviePicture"];
                MemoryStream s = new MemoryStream(mydata);
                pictureBox2.Image = Image.FromStream(s);
                con.Close();
            }
            catch (ArgumentOutOfRangeException m)
            {
                MessageBox.Show("Please Click on the (Silver Bar) Left End of the Row");
            }
          

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            comboBox5.Text = "-select one-";
            comboBox4.Text = "-select one-";
            pictureBox2.Image = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox13.Text != "")
            {
                string genre1 = this.comboBox1.GetItemText(this.comboBox5.SelectedItem);
                string rating1 = this.comboBox1.GetItemText(this.comboBox4.SelectedItem);

                int i = 0;
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE Movie SET movieTitle = '" + textBox13.Text + "', movieGenre = '" + comboBox5.Text + "', movieRating = '" + comboBox4.Text + "', releaseDate = '" + dateTimePicker2.Text + "', movieDirector= '" + textBox12.Text + "', moviePublisher= '" + textBox10.Text + "', movieActors= '" + textBox11.Text + "', movieDescription= '" + textBox9.Text + "', moviePicture=@moviePicture WHERE movieID = " + dataGridView1.SelectedRows[0].Cells[0].Value;
                }
                catch(ArgumentOutOfRangeException a)
                {
                    MessageBox.Show("Record is already Updated");
                }
                try
                {

                    MemoryStream stream = new MemoryStream();
                    pictureBox2.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] pic = stream.ToArray();
                    cmd.Parameters.AddWithValue("@moviePicture", pic);

                    i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        MessageBox.Show("Movie Record Updated " + i);
                    }
                    showData();
                }
                catch (NullReferenceException x)
                {
                    MessageBox.Show("Please insert an image for the movie");
                }
            }

            else
            {
                MessageBox.Show("Please insert movie title.");
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            int k = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM Movie WHERE movieID =" + dataGridView1.SelectedRows[0].Cells[0].Value;
                k = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (ArgumentOutOfRangeException b)
            {
                MessageBox.Show("Record is being Deleted");
            }

            if (k > 0)
            {
                MessageBox.Show("Movie Record Deleted");
                textBox13.Text = "";
                textBox12.Text = "";
                textBox11.Text = "";
                textBox10.Text = "";
                textBox9.Text = "";
                comboBox5.Text = "-select one-";
                comboBox4.Text = "-select one-";
                pictureBox2.Image = null;
                showData();

            }


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                label29.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                label30.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                label31.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                label32.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                label33.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                label34.Text = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                label35.Text = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
                label36.Text = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();



                con.Open();
                cmd.Connection = con;
                cmd.CommandText = " select moviePicture from Movie where movieID = " + dataGridView2.SelectedRows[0].Cells[0].Value;

                ad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                byte[] mydata = new byte[0];
                ad.Fill(ds, "Movie");
                DataRow myrow;
                myrow = ds.Tables["Movie"].Rows[0];
                mydata = (byte[])myrow["moviePicture"];
                MemoryStream s = new MemoryStream(mydata);
                pictureBox4.Image = Image.FromStream(s);
                con.Close();
            }
            catch (ArgumentOutOfRangeException m)
            {
                MessageBox.Show("Please Click on the (Silver Bar) Left End of the Row");
            }
          
        }

        private void button5_Click(object sender, EventArgs e)
        {

            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movie WHERE movieTitle  LIKE '%" + textBox6.Text.ToString()+ "%'";
            ad.SelectCommand = cmd;
            DataTable query = new DataTable();
            ad.Fill(query);
            dataGridView1.DataSource = query;
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movie WHERE movieGenre  LIKE '%" + comboBox3.Text.ToString() + "%'";
            ad.SelectCommand = cmd;
            DataTable query = new DataTable();
            ad.Fill(query);
            dataGridView1.DataSource = query;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movie WHERE movieTitle  LIKE '%" + textBox7.Text.ToString() + "%'";
            ad.SelectCommand = cmd;
            DataTable query = new DataTable();
            ad.Fill(query);
            dataGridView2.DataSource = query;
            con.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movie WHERE movieGenre  LIKE '%" + comboBox6.Text.ToString() + "%'";
            ad.SelectCommand = cmd;
            DataTable query = new DataTable();
            ad.Fill(query);
            dataGridView2.DataSource = query;
            con.Close();
        }
    }
}
