using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionBD_Jobs
{
    public partial class Form1 : Form
    {
        decimal? minSalary;
        decimal? maxSalary;
        
        static string ConnectionString = @"Data source = 79.143.90.12,54321;
                                        Initial Catalog = GermanEmployees;
                                        Persist Security Info = true;
                                        User Id = sa;
                                        Password = 123456789";

        private SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            MostrarLista();
        }
        private void JobInsert()
        {
            Job newJob = new Job(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
            try
            {
                string query = $"INSERT INTO jobs (job_title, min_salary, max_salary)" +
                    $"VALUES ('{newJob.JobTitle}',{newJob.JobMinSalary}, {newJob.JobMaxSalary});" +
                    $"SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                object id = command.ExecuteScalar();
                newJob.JobId = int.Parse(id.ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private List<Job> SelectJobs()
        {
            List<Job> jobList = new List<Job>();

            string query = "SELECT * FROM jobs";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int jobId = (int)reader["job_id"];
                string jobTitle = (string)reader["job_title"];
                decimal? minSalary = (decimal)reader["min_salary"];
                decimal? maxSalary = (decimal)reader["max_salary"];

                foreach (Job j in jobList)
                {
                    if (j.JobId == jobId)
                        return jobList;
                }

                Job job = new Job(jobId, jobTitle, minSalary, maxSalary);

                jobList.Add(job);


            }
            return jobList;
        }
        private void MostrarLista()
        {
            listBox1.Items.Clear();
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                foreach (Job job in SelectJobs())
                {
                    listBox1.Items.Add(job);
                }
                connection.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                label1.Text = "Conectado";
            }
            catch(SqlException ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Close();
            label1.Text = "No Conectado";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            JobInsert();
        }
    }
}
