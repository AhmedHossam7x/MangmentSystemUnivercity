using MangmentSystemUnivercity.controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MangmentSystemUnivercity.Model
{
    public class Teacher : Person
    {
        private double _salary;

        public double Salary
        {
            get { return _salary; }
            set
            {
                if(value <= 0)
                    Console.WriteLine("Can`t assigned salary");
                else
                    _salary = value;
            }
        }

        public Teacher(){}
        public Teacher(string username, string password) : base(username, password) { }
        public Teacher(int id, string username, string password) : base(id, username, password) { }
        public Teacher(int searchId) : base(searchId) { }
        public Teacher(
            int id, 
            long nId, 
            string name, 
            short age, 
            byte gender, 
            string phone, 
            string email, 
            string addr, 
            double salary) : base(id,nId,name,age,gender,phone,email,addr) 
        {
            Salary = salary;
        }

        private static bool CheckIdExit(string filePath, short id, string course = "same")
        {
            var lineAllText = File.ReadAllText(filePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (id != 0)
                    {
                        if (id.ToString().Equals(fields[0]))
                            return true;
                    }
                    else if (!course.Equals("same"))
                    {
                        if (course.ToLower().Equals(fields[1]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static void TeacherAttendanceGrade()
        {
            TeacherProsses(out short id, out string course, out double grade);
            //MarkAttendance(id, course, grade);
            Mark("studentAttendanceGrades.txt", id, course, grade);
        }
        public static void TeacherQuizGrade()
        {
            TeacherProsses(out short id, out string course, out double grade);
            //MarkQuiz(id, course, grade);
            Mark("studentQuizGrades.txt", id, course, grade);
        }
        public static void TeacherMidTermGrade()
        {
            TeacherProsses(out short id, out string course, out double grade);
            Mark("studentMidTermGrades.txt", id, course, grade);
        }
        public static void TeacherFinalGrade()
        {
            TeacherProsses(out short id, out string course, out double grade);
            Mark("studentFinalGrades.txt", id, course, grade);
        }
        private static void TeacherProsses(out short id, out string course, out double grade)
        {
            Console.Write("Enter student Id: ");
            id = short.Parse(Console.ReadLine());
            if (!CheckIdExit("studentDatabase.txt", id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Id was not found in student database file");
            }
            Console.Write("Enter Course: ");
            course = Console.ReadLine().ToUpper();
            if (!CheckIdExit("courseDatabase.txt", 0, course))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Course was not found in course database file");
            }

            Console.Write("Enter student grade: ");
            grade = double.Parse(Console.ReadLine());
        }
        private static void Mark(string filePath, short id, string? courseName, double grade)
        {
            Student student = new Student();
            Course course = new Course();

            student.Id = id;
            course.Name = courseName;
            student.AttendanceGrade = grade;

            Admin.CreateFile(filePath);

            var lineAllText = File.ReadAllText(filePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (id.ToString().Equals(fields[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Id is Already exist!");
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{id},{courseName},{grade}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("************************** Inserted **************************");
                Console.WriteLine("Record added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                writer.Close();
            }
        }
    }
}
