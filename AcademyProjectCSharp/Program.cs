using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace AcademyProjectCSharp
{
    abstract class Object
    {
        public static int ID = 1;
        public int id = ID;
        public string Name { get; set; }
        public Object()
        {
            ID++;
        }
        public void ShowId()
        {
            Console.WriteLine($"ID : {id}");
        }
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
            base.ShowId();
            Console.WriteLine($"Exam name : {Name}");
            Console.WriteLine($"Score : {Score}");
            Console.WriteLine($"Exam Date : {ExamDate.ToString()}");
        }
    }
    class Student : Human
    {
        public Exam[] Exams { get; set; }
        public int ExamCount { get; set; } = 0;
        public double AverageScore { get; set; } = 0;
        public double GetAverageScore() => AverageScore;
        public void ShowStudent()
        {
            base.ShowId();
            Console.WriteLine($"FullName : {Name} {Surname}");
            Console.WriteLine($"Email    : {Email}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    Exams : ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Exams != null)
            {
                foreach (var exam in Exams)
                {
                    exam.ShowExam();
                }
            }
            else Console.WriteLine("Student didn't take the exam");
        }
        public void AddExam(ref Exam exam)
        {
            Exam[] temp = new Exam[++ExamCount];
            if (Exams != null)
            {
                Exams.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = exam;
            Exams = temp;
            AverageScore = ((AverageScore * (ExamCount - 1)) + exam.Score) / ExamCount;

        }
    }

    class Teacher : Human
    {
        public double Salary { get; set; }
        public void ShowTeacher()
        {
            base.ShowId();
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
            base.ShowId();
            Console.WriteLine($"Group name : {Name}");
            Console.WriteLine($"Teacher : {Teacher.Name} {Teacher.Surname}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Students : ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Students != null)
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
            Console.ForegroundColor = ConsoleColor.White;
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
        public void ShowGroupInfo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("GROUPS : ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Groups != null)
            {
                foreach (var group in Groups)
                {
                    group.ShowId();
                    Console.WriteLine($"Name : {group.Name}");
                    Console.WriteLine("----------------------------------");
                }
            }
            else Console.WriteLine("No Group in Academy");
        }

        public Group GetGroupById(int id)
        {
            if (Groups != null)
            {
                foreach (var group in Groups)
                {
                    if (id == group.id) return group;
                }
            }
            return null;
        }
        public void ShowAllStudents()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Students : ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Groups != null)
            {
                foreach (var group in Groups)
                {
                    if (group.Students != null)
                    {
                        foreach (var student in group.Students)
                        {
                            Console.WriteLine($"Id : {student.id}");
                            Console.WriteLine($"FullName : {student.Name} {student.Surname}");
                        }
                    }
                }
            }
        }
        public Student GetStudentById(int id)
        {
            if (Groups != null)
            {
                foreach (var group in Groups)
                {
                    if (group.Students != null)
                    {
                        foreach (var student in group.Students)
                        {
                            if (id == student.id) return student;
                        }
                    }
                }
            }
            return null;
        }
    }

    class Controller
    {
        public static string GeneratePDFAndGetPath(Student student,Exam exam)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER,10,10,42,35);
            string PDFpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\' + student.Name + student.Surname + exam.Name + ".pdf";
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(PDFpath, FileMode.Create));
            doc.Open();
            Paragraph paraghraph = new Paragraph();
            paraghraph.SpacingBefore = 10;
            paraghraph.SpacingAfter = 10;
            string text = $@"Hello {student.Name} {student.Surname}. Your exam result is written below
Exam name : {exam.Name}
Score : {exam.Score}
Exam date : {exam.ExamDate.ToString()}

Good Luck!";

            paraghraph.Add(text);
            doc.Add(paraghraph);
            doc.Close();
            return PDFpath;
        }

        public static void email_send(string AttachmentFilePath, Student student)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mehemmedbayramov2004@gmail.com");
            mail.To.Add(student.Email);
            mail.Subject = "Exam Result";
            mail.Body = $"Hello {student.Name} {student.Surname}.Your exam result is in PDF. This is automatic mail. Don't reply!";
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(AttachmentFilePath);
            mail.Attachments.Add(attachment);
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("mehemmedbayramov2004@gmail.com", "zsvxzpxvztuoezkr");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

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
                academy.ShowGroupInfo();
                Console.WriteLine();
                Console.WriteLine("ShowGroup Info       [1] ");
                Console.WriteLine("Add Exam             [2] ");
                Console.WriteLine("Show Average Score   [3] ");
                string select = Console.ReadLine();
                if (select == "1")
                {
                    Console.WriteLine("Enter ID : ");
                    int id = int.Parse(Console.ReadLine());
                    var CurrentGroup = academy.GetGroupById(id);
                    if (CurrentGroup != null)
                    {
                        CurrentGroup.ShowGroup();
                    }
                    else
                    {
                        Console.WriteLine("There is not such group by this ID");
                    }
                }
                else if (select == "2")
                {
                    academy.ShowAllStudents();
                    Console.WriteLine("Enter Id : ");
                    int id = int.Parse(Console.ReadLine());
                    var CurrentStudent = academy.GetStudentById(id);
                    Console.WriteLine("Enter exam name : ");
                    string exam_name = Console.ReadLine();
                    Console.WriteLine("Enter exam score : ");
                    double score = double.Parse(Console.ReadLine());
                    Exam exam = new Exam
                    {
                        Name = exam_name,
                        Score = score
                    };
                    CurrentStudent.AddExam(ref exam);
                    string pdfpath = GeneratePDFAndGetPath(CurrentStudent,exam);
                    email_send(pdfpath, CurrentStudent);
                }
                else if (select == "3")
                {
                    academy.ShowAllStudents();
                    Console.WriteLine("Enter Id : ");
                    int id = int.Parse(Console.ReadLine());
                    var CurrentStudent = academy.GetStudentById(id);
                    Console.WriteLine($"{CurrentStudent.Name}'s average score is {CurrentStudent.AverageScore}");
                }

                else
                {
                    continue;
                }
                Console.ReadKey();
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
