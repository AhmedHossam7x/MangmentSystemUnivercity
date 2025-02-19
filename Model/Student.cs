using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MangmentSystemUnivercity.Model
{
    internal class Student : Person
    {
        private string[] DEP = { "ICT", "Energy", "Mechatronics", "Prosthetics", "Autotronics" };
        private short _currentYear;
        private string _department = "";

        //private bool studyFees = false;
        //private string[][] examTable;
        private double _attendanceGrade;
        //private float quizGrade;
        //private float midTermGrade;
        //private float finalGrade;

        public short CurrentYear
        {
            get { return _currentYear; }
            set
            {
                if (value > 0 && value <= 4)
                    _currentYear = value;
                else
                    Console.WriteLine("The input year must to between (1:4)");
            }
        }
        public string Dep
        {
            get { return _department; }
            set
            {
                if (Array.Exists(DEP, element => element.Equals(value, StringComparison.OrdinalIgnoreCase)))
                    _department = value;
                else
                    Console.WriteLine("The department must be one of the following: ICT, Energy, Mechatronics, Prosthetics, Autotronics.");
            }
        }
        public double AttendanceGrade
        {
            get { return _attendanceGrade; }
            set
            {
                if (value <= 200)
                    _attendanceGrade = value;
            }
        }

        //public Student(short year, string dep) 
        //{
        //   CurrentYear = year;
        //    Dep = dep;
        //}
        public Student() { }
        public Student(string username, string password) : base(username, password) { }
        public Student(int id, string username, string password) : base(id, username, password) { }
        public Student(int searchId) : base(searchId) { }
        public Student(
            int id,
            long nId,
            string name,
            short age,
            byte gender,
            string phone,
            string email,
            string addr,
            short currentYear,
            string dep) : base(id, nId, name, age, gender, phone, email, addr) 
        {
            CurrentYear = currentYear;
            Dep = dep;
        }

        public static bool CheckStudentId(string databasePath, int id)
        {
            var lineAllText = File.ReadAllText(databasePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] data = line.Split(',');
                    if (data[0].Equals(id.ToString()))
                        return true;
                }
            }
            return false;
        }
        public static void myCourses(string Dept, short year)
        {
            var lineAllText = File.ReadAllText("courseDatabase.txt");
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (Dept.Equals(fields[2], StringComparison.OrdinalIgnoreCase) && year.ToString().Equals(fields[4], StringComparison.OrdinalIgnoreCase))
                        Console.WriteLine($"The Current Year is {fields[4]} and Courses is {fields[1]}");
                }
            }
        }
        public static void StudentGrades(int id)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(@"1)- View My Attendance Grade 2)- View My  Quiz Grade
3)- View My MidTerm Grade    4)- View My Final Grade   0)- Exit:  ");
                byte choice = byte.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        viewAttendanceGrade("studentAttendanceGrades.txt", id);
                        break;
                    case 2:
                        viewQuizGrade("studentQuizGrades.txt", id);
                        break;
                    case 3:
                        viewMidTermGrade("studentMidTermGrades.txt", id);
                        break;
                    case 4:
                        viewFinalGrade("studentFinalGrades.txt", id);
                        break;
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter one of the available choices");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void viewFinalGrade(string filePath, int id)
        {
            var lineAllText = File.ReadAllText(filePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (id.ToString().Equals(fields[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        //foreach(var field in fields)
                        //    Console.Write(field+",");
                        Console.WriteLine($"Your finel grade is {fields[2]}");
                        //Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        private static void viewMidTermGrade(string filePath, int id)
        {
            var lineAllText = File.ReadAllText(filePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (id.ToString().Equals(fields[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        //foreach(var field in fields)
                        //    Console.Write(field+",");
                        Console.WriteLine($"Your midterm grade is {fields[2]}");
                        //Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        private static void viewQuizGrade(string filePath, int id)
        {
            var lineAllText = File.ReadAllText(filePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (id.ToString().Equals(fields[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        //foreach(var field in fields)
                        //    Console.Write(field+",");
                        Console.WriteLine($"Your Quiz grade is {fields[2]}");
                        //Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        private static void viewAttendanceGrade(string filePath, int id)
        {
            var lineAllText = File.ReadAllText(filePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (id.ToString().Equals(fields[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        //foreach(var field in fields)
                        //    Console.Write(field+",");
                        Console.WriteLine($"Your Attendance grade is {fields[2]}");
                        //Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
    }
}
