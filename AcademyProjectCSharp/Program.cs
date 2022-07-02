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
    class Exam : Object
    {
        public double Score { get; set; }
        public DateTime ExamDate { get; set; } = DateTime.Now;
        public void ShowExam()
        {
            Console.WriteLine($"Exam name : {Name}");
            Console.WriteLine($"Score : {Score}");
            Console.WriteLine($"Exam Date : {ExamDate.ToString()}");
        }
    }
    class Student : Human
    {
        public Exam[] Exams { get; set; }
        public double AverageScore { get; set; } = 0;
        public double GetAverageScore() => AverageScore;
        public void ShowStudent()
        {
            Console.WriteLine($"FullName : {Name} {Surname}");
            Console.WriteLine($"Email    : {Email}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    Exams : ");
            Console.ForegroundColor = ConsoleColor.White;
            if(Exams != null)
            {
                foreach (var exam in Exams)
                {
                    exam.ShowExam();
                }
            }
            else Console.WriteLine("Student didn't take the exam");
        }
    }

    class Teacher : Human
    {
        public double Salary { get; set; }
        public void ShowTeacher()
        {
            Console.WriteLine($"FullName : {Name} {Surname}");
            Console.WriteLine($"Email    : {Email}");
            Console.WriteLine($"Salary   : {Salary}");
        }

    }

    class Group : Object
    {
        public Teacher Teacher { get; set; }
        public Student[] Students { get; set; }
        public void ShowGroup()
        {
            Console.WriteLine($"Group name : {Name}");
            Console.WriteLine($"Teacher : {Teacher.Name} {Teacher.Surname}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Students : ");
            Console.ForegroundColor = ConsoleColor.White;
            if(Students != null)
            {
                foreach (var student in Students)
                {
                    student.ShowStudent();
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("No Student in Group");
        }
    }
    class Academy : Object
    {
        public Group[] Groups { get; set; }
        public void ShowGroups()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("GROUPS : ");
            Console.ForegroundColor=ConsoleColor.White;
            if (Groups != null)
            { 
                foreach (var group in Groups)
                {
                    group.ShowGroup();
                    Console.WriteLine("----------------------------------");
                }
            }
            else Console.WriteLine("No Group in Academy");
        }
    }

    class Controller
    {
        public static void Start()
        {
            Student s1 = new Student()
            {
                Name = "Nurlan",
                Email = "nurlan.shirinov1998@gmail.com",
                Surname = "Shirinov"
            };
            Student s2 = new Student()
            {
                Name = "Ilkin",
                Email = "ilkinsuleymanov902@gmail.com",
                Surname = "Suleymanov"
            };
            Student s3 = new Student()
            {
                Name = "Tunay",
                Email = "tunayhuseynli458@gmail.com",
                Surname = "Huseynli"
            };
            Student s4 = new Student()
            {
                Name = "Remzi",
                Email = "remzihesenov245@gmail.com",
                Surname = "Hesenov"
            };

            Teacher t1 = new Teacher()
            {
                Name = "Elvin",
                Surname = "Shirinov",
                Email = "elvincamalzade1999@gmail.com",
                Salary = 2300
            };
            Teacher t2 = new Teacher()
            {
                Name = "Ibrahim",
                Surname = "Nebiyev",
                Email = "ibrahimnebiyev1953@@gmail.com",
                Salary = 1200
            };
            Group g1 = new Group()
            {
                Name = "FBES-3212",
                Teacher = t1,
                Students = new Student[] { s1, s2 }
            };
            Group g2 = new Group()
            {
                Name = "TK-92",
                Teacher = t2,
                Students = new Student[] { s3, s4 }
            };

            Academy academy = new Academy
            {
                Name = "StepIT ACADEMY",
                Groups = new Group[] { g1, g2 }
            };
            //  PROJECT START

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"                          {academy.Name}");
                academy.ShowGroups();
                int select = int.Parse(Console.ReadLine());


            }//while true close


        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Controller.Start();
        }
    }
}
