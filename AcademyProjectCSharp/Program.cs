using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyProjectCSharp
{
    abstract class Object
    {
        public string Name { get; set; }
    }
    abstract class Human : Object
    {
        public string Surname { get; set; }
        public string Email { get; set; }

    }
    class Exam:Object
    {
        public double Score { get; set; }
        public DateTime ExamDate { get; set; }=DateTime.Now;
    }
    class Student : Human
    {
        public Exam[] Exams { get; set; }
        public double AverageScore { get; set; }
        public double GetAverageScore() => AverageScore;
    }

    class Teacher : Human
    {
        public double Salary { get; set; }
    }

    class Group:Object
    {
        public Teacher[] Teachers { get; set; }
        public Student[] Students { get; set; }
    }
    class Academy
    {
        public Group[] Groups { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
