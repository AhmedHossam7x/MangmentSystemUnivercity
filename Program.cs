using MangmentSystemUnivercity.controller;
using MangmentSystemUnivercity.Model;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MangmentSystemUnivercity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            Teacher teacher = new Teacher();
            Student student = new Student();
            Course course = new Course();

            if (!File.Exists("adminDatabase.txt"))
            {
                Admin.CreateFile("adminDatabase.txt");
                Admin.insertAdminData("adminDatabase.txt", 1, "ahmed", "Ah@1234567");
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome in Application Mangment System for Univercity");
                Console.WriteLine("===============================================");
                Console.ForegroundColor = ConsoleColor.Gray;
                InnerLoop:
                Console.Write("Who ara you (1:Admin) (2:Teacher) (3:Student) to Exit press (0) ");
                var selectWho = Console.ReadLine()[0];
                switch (selectWho)
                {
                    case '0':
                        return;
                    case '1':
                        Console.Write("Pls, Enter username: ");
                        admin.UserName = Console.ReadLine();
                        Console.Write("Your Password is: ");
                        admin.Password = Console.ReadLine();
                        if (Admin.login("adminDatabase.txt", admin.UserName, admin.Password))
                        {
                            InnerLoopInsideSelectAdmin:
                            Console.Write("(1:Upgrade)  (2:List Data)  (3:Create Teacher) (4:Delete)  Exit(0): ");
                            var selectAdmin = Console.ReadLine()[0];
                            Console.WriteLine("=============================================");
                            switch (selectAdmin)
                            {
                                case '0':
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto InnerLoop;
                                case '1':
                                    while (true)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("1)-Add admin  2)-Add Teacher  3)_Add student  4)-Add Courses");
                                        Console.WriteLine("5)-Update admin 6)-Update teacher 7)-Update student 8)-Update Courses");
                                        Console.WriteLine("0)-To Exit");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        var inputAdmin = Console.ReadLine()[0];
                                        if (inputAdmin == '1')
                                        {
                                            Console.Write("Enter id for adnim: ");
                                            admin.Id = int.Parse(Console.ReadLine());
                                            Console.Write("Admin Name: ");
                                            admin.UserName = Console.ReadLine();
                                            Console.Write("password is: ");
                                            admin.Password = Console.ReadLine();
                                            Admin.insertAdminData("adminDatabase.txt", admin.Id, admin.UserName, admin.Password);
                                        }
                                        else if (inputAdmin == '2')
                                        {
                                            Console.Write("Enter id for teacher: ");
                                            teacher.Id = int.Parse(Console.ReadLine());
                                            Console.Write("Enter NatId for teacher the number contain 14 digit number: ");
                                            teacher.NatId = long.Parse(Console.ReadLine());
                                            Console.Write("Enter name for teacher: ");
                                            teacher.Name = Console.ReadLine();
                                            Console.Write("Enter email for teacher: ");
                                            teacher.Email = Console.ReadLine();
                                            Console.Write("Enter age for teacher: ");
                                            teacher.Age = short.Parse(Console.ReadLine());
                                            Console.Write("Enter phone for teacher: ");
                                            teacher.Phone = Console.ReadLine();
                                            Console.Write("Enter gender for teacher 1)-male 2)-female: ");
                                            var gender = byte.Parse(Console.ReadLine());
                                            Console.Write("Enter salary for teacher: ");
                                            teacher.Salary = double.Parse(Console.ReadLine());
                                            Console.Write("Enter address for teacher: ");
                                            teacher.Address = Console.ReadLine();

                                            Admin.insertTeacherData(
                                                "teacherDatabase.txt",
                                                teacher.Id,
                                                teacher.NatId,
                                                teacher.Age,
                                                teacher.Phone,
                                                teacher.Email,
                                                teacher.Name,
                                                gender,
                                                teacher.Salary,
                                                teacher.Address);
                                        }
                                        else if (inputAdmin == '3')
                                        {
                                            Console.Write("Enter id for student: ");
                                            student.Id = int.Parse(Console.ReadLine());
                                            Console.Write("Enter NatId for student the number contain 14 digit number: ");
                                            student.NatId = long.Parse(Console.ReadLine());
                                            Console.Write("Enter name for student: ");
                                            student.Name = Console.ReadLine();
                                            Console.Write("Enter email for student: ");
                                            student.Email = Console.ReadLine();
                                            Console.Write("Enter age for student: ");
                                            student.Age = short.Parse(Console.ReadLine());
                                            Console.Write("Enter phone for student: ");
                                            student.Phone = Console.ReadLine();
                                            Console.Write("Enter gender for student 1)-male 2)-female: ");
                                            var gender = byte.Parse(Console.ReadLine());
                                            Console.Write("Enter address for student: ");
                                            student.Address = Console.ReadLine();
                                            Console.Write("Enter current year of student: ");
                                            student.CurrentYear = short.Parse(Console.ReadLine());
                                            Console.Write("Department 1)-ict 2)-'Mechatronics' 3)'Prosthetics' 4)-'Autotronics' 5)-'Energy': ");
                                            var Depart = Console.ReadLine();
                                            student.Dep = SelectDepartment(Depart, true);

                                            Admin.insertStudentData(
                                                "studentDatabase.txt",
                                                student.Id,
                                                student.NatId,
                                                student.Age,
                                                student.Phone,
                                                student.Email,
                                                student.Name,
                                                gender,
                                                student.Address,
                                                student.CurrentYear,
                                                student.Dep
                                            );
                                        }
                                        else if (inputAdmin == '4')
                                        {
                                            Console.Write("Enter id for Course: ");
                                            course.Id = int.Parse(Console.ReadLine());
                                            Console.Write("Enter Name of Course: ");
                                            course.Name = Console.ReadLine();
                                            Console.Write("Department: 1)-'ict' 2)-'Mechatronics' 3)'Prosthetics' 4)-'Autotronics' 5)-'Energy': ");
                                            var Depart = Console.ReadLine();
                                            course.Department = SelectDepartment(Depart, true);
                                            Console.Write("Enter Grade for Course between 50 to 200: ");
                                            course.Grade = short.Parse(Console.ReadLine());
                                            Console.Write("Enter Year for Course: ");
                                            course.Year = short.Parse(Console.ReadLine());
                                            Admin.insertCourseData("courseDatabase.txt", course.Id, course.Name, course.Department, course.Grade, course.Year);
                                        }
                                        else if (inputAdmin == '5')
                                        {
                                            Console.Write("Enter id for adnim want update: ");
                                            admin.Id = int.Parse(Console.ReadLine());
                                            Console.Write("What field want update (id,username,password): ");
                                            var feild = Console.ReadLine();
                                            Console.Write("Value is: ");
                                            var value = Console.ReadLine();
                                            Admin.UpdateAdminData("adminDatabase.txt", admin.Id, feild, value);
                                        }
                                        else if (inputAdmin == '6')
                                        {
                                            Console.Write("Enter id for teacher want update: ");
                                            teacher.Id = int.Parse(Console.ReadLine());
                                            Console.Write("What field want update (name-email-phone-age-gender-natId-addr-salary): ");
                                            var feild = Console.ReadLine();
                                            Console.Write("Value is: ");
                                            var value = Console.ReadLine();
                                            Admin.UpdateTeacherData("teacherDatabase.txt", teacher.Id, feild, value);
                                        }
                                        else if (inputAdmin == '7')
                                        {
                                            Console.Write("Enter id for student want update: ");
                                            student.Id = int.Parse(Console.ReadLine());
                                            Console.Write("What field want update (name-email-phone-age-gender-natId-addr-year-dep): ");
                                            var feild = Console.ReadLine();
                                            var value = "";
                                            if (feild == "dep")
                                            {
                                                Console.Write("1)-ict 2)-'Mechatronics' 3)'Prosthetics' 4)-'Autotronics' 5)-'Energy': ");
                                                var inputDep = Console.ReadLine();
                                                value = SelectDepartment(inputDep);
                                            }
                                            else
                                            {
                                                Console.Write("Value is: ");
                                                value = Console.ReadLine();
                                            }
                                            Admin.UpdateStudentData("studentDatabase.txt", student.Id, feild, value);
                                        }
                                        else if(inputAdmin == '8')
                                        {
                                            Console.Write("Enter id for Course want update: ");
                                            course.Id = int.Parse(Console.ReadLine());
                                            Console.Write("What field want update (name-dep-grade-year): ");
                                            var feild = Console.ReadLine();
                                            var value = "";
                                            if (feild == "dep")
                                            {
                                                Console.Write("1)-ict 2)-'Mechatronics' 3)'Prosthetics' 4)-'Autotronics' 5)-'Energy': ");
                                                var inputDep = Console.ReadLine();
                                                value = SelectDepartment(inputDep);
                                            }
                                            else
                                            {
                                                Console.Write("Value is: ");
                                                value = Console.ReadLine();
                                            }
                                            Admin.UpdateCourseData("courseDatabase.txt", course.Id, feild, value);
                                        }
                                        else if (inputAdmin == '0')
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            goto InnerLoopInsideSelectAdmin;
                                        }
                                    }
                                case '2':
                                    while (true)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("1)listTeacher 2)listStudent 3)listCourses 4)listAdmin");
                                        //Console.WriteLine("4)Specific Teacher 5)Specific Student 6)Specific Course");
                                        Console.Write("0)-To Exit: ");
                                        var choose = Console.ReadLine()[0];
                                        Console.WriteLine("=====================================================");
                                        switch (choose)
                                        {
                                            case '0':
                                                goto InnerLoopInsideSelectAdmin;
                                            case '1':
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("All Teacher Data");
                                                Admin.List("teacherDatabase.txt");
                                                break;
                                            case '2':
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("All Student Data");
                                                Admin.List("studentDatabase.txt");
                                                break;
                                            case '3':
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("All Courses Data");
                                                Admin.List("courseDatabase.txt");
                                                break;
                                            case '4':
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("All Admin Data");
                                                Admin.List("adminDatabase.txt");
                                                break;
                                        }
                                    }
                                    break;
                                case '3':
                                    AdminCreateTeacher();
                                    break;
                                case '4':
                                    while (true)
                                    {
                                        Console.Write("1)-Delete admin  2)-Delete teacher  3)-Delete student 4)-Delete Course 5)-Delete Teacher Account  0)-Exit: ");
                                        var inputAdminDelete = Console.ReadLine()[0];
                                        if (inputAdminDelete == '1')
                                        {
                                            Console.Write("Enter id for adnim want delete: ");
                                            admin.Id = int.Parse(Console.ReadLine());
                                            Admin.Delete("adminDatabase.txt", admin.Id);
                                        }
                                        else if (inputAdminDelete == '2')
                                        {
                                            Console.Write("Enter id for teacher want delete: ");
                                            teacher.Id = int.Parse(Console.ReadLine());
                                            Admin.Delete("teacherDatabase.txt", teacher.Id);
                                        }
                                        else if (inputAdminDelete == '3')
                                        {
                                            Console.Write("Enter id for student want delete: ");
                                            student.Id = int.Parse(Console.ReadLine());
                                            Admin.Delete("studentDatabase.txt", student.Id);
                                        }
                                        else if (inputAdminDelete == '4')
                                        {
                                            Console.Write("Enter id for Course want delete: ");
                                            course.Id = int.Parse(Console.ReadLine());
                                            Admin.Delete("courseDatabase.txt", course.Id);
                                        }
                                        else if (inputAdminDelete == '5')
                                        {
                                            Console.Write("Enter id for Teacher want delete: ");
                                            teacher.Id = int.Parse(Console.ReadLine());
                                            Admin.Delete("teacherLogin.txt", teacher.Id);
                                        }
                                        else if (inputAdminDelete == '0')
                                            break;
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Invaild input");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                    }
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invaild input");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }
                        }
                        break;
                    case '2':
                        Console.Write("Pls, Enter username: ");
                        teacher.UserName = Console.ReadLine();
                        Console.Write("Your Password is: ");
                        teacher.Password = Console.ReadLine();
                        if(Admin.login("teacherLogin.txt", teacher.UserName, teacher.Password))
                        {
                            while (true)
                            {
                                Console.Write("Attendance(1) - Quiz(2) - Midterm(3) - Final(4) - Go back(0): ");
                                byte chooseTeacher = byte.Parse(Console.ReadLine());
                                switch (chooseTeacher)
                                {
                                    case 1:
                                        Teacher.TeacherAttendanceGrade();
                                        break;
                                    case 2:
                                        Teacher.TeacherQuizGrade();
                                        break;
                                    case 3:
                                        Teacher.TeacherMidTermGrade();
                                        break;
                                    case 4:
                                        Teacher.TeacherFinalGrade();
                                        break;
                                    case 0:
                                        goto InnerLoop;
                                }
                            }
                        }
                        break;
                    case '3':
                        Console.Write("Pls, Enter Id: ");
                        student.Id = int.Parse(Console.ReadLine());
                        if (Student.CheckStudentId("studentDatabase.txt", student.Id))
                        {
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("View My Courses(1) - View My Grades(2) -  Go back(0): ");
                                byte chooseStudent = byte.Parse(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.White;
                                switch (chooseStudent)
                                {
                                    case 1:
                                        Console.Write("Enter Current Year: ");
                                        student.CurrentYear = short.Parse(Console.ReadLine());
                                        string[] DEP = { "ICT", "Energy", "Mechatronics", "Prosthetics", "Autotronics" };
                                        Console.Write("1-ICT 2-Mechtroniecs 3-autotroniecs 4-anergy 5-Prosthetics: ");
                                        byte currentDept = byte.Parse(Console.ReadLine());
                                        if (currentDept == 1)
                                            student.Dep = "ICT";
                                        else if (currentDept == 2)
                                            student.Dep = "Mechatronics";
                                        else if (currentDept == 3)
                                            student.Dep = "Autotronics";
                                        else if (currentDept == 4)
                                            student.Dep = "Energy";
                                        else if (currentDept == 5)
                                            student.Dep = "Prosthetics";
                                        Student.myCourses(student.Dep, student.CurrentYear);
                                        break;
                                    case 2:
                                        Student.StudentGrades(student.Id);
                                        break;
                                    case 0:
                                        goto InnerLoop;
                                }
                            }
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invaild input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }
        private static string SelectDepartment(string value, bool flag = false)
        {
            switch (value)
            {
                case "1":
                    value = "ict";
                    break;
                case "2":
                    value = "Mechatronics";
                    break;
                case "3":
                    value = "Prosthetics";
                    break;
                case "4":
                    value = "Autotronics";
                    break;
                case "5":
                    value = "Energy";
                    break;
                default:
                    if (flag)
                        value = "ict";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invaild Input");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            return value;
        }
        public static void AdminCreateTeacher()
        {
            Admin admin = new Admin();
            try
            {
                Console.Write("Enter teacher id: ");
                admin.Id = int.Parse(Console.ReadLine());
                Console.Write("Enter teacher userName: ");
                admin.UserName = Console.ReadLine();
                Console.Write("Enter teacher Password: ");
                admin.Password = Console.ReadLine();
                Admin.CreateTeacherAccount("teacherLogin.txt", admin.Id, admin.UserName, admin.Password);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=======================================");
                Console.WriteLine(e);
                Console.WriteLine("=======================================");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
