using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD_Jobs
{
    internal class Job
    {
        private int jobId;
        private string jobTitle;
        private decimal? minSalary;
        private decimal? maxSalary;

        public int JobId { get { return jobId; } set { jobId = value; } }
        public string JobTitle { get { return jobTitle; } set { jobTitle = value; } }
        public decimal? JobMinSalary { get { return minSalary; } set { minSalary = value; } }
        public decimal? JobMaxSalary { get { return maxSalary; } set { maxSalary = value; } }
        public Job() { }
        public Job(int id, string jobName, decimal? minSal, decimal? maxSal)
        {
            jobId = id;
            jobTitle = jobName;
            minSalary = minSal;
            maxSalary = maxSal;
        }
        public Job(string jobName, decimal? minSal, decimal? maxSal)
        {
            jobTitle = jobName;
            minSalary = minSal;
            maxSalary = maxSal;
        }
        public override string ToString()
        {
            return $"ID {JobId}," +
                   $"{JobTitle}," +
                   $"Salary {JobMinSalary} - {JobMaxSalary}";
        }
    }
}
