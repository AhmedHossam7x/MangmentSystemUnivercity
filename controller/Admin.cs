using System.Diagnostics;
using System.Xml.Linq;

namespace MangmentSystemUnivercity.controller;

using MangmentSystemUnivercity.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

[Serializable]
public class Admin : Person
{
    public Admin() { }
    public Admin(string username, string password) : base(username, password) {}
    public Admin(int id, string username, string password) : base (id, username, password){}
    public Admin(int searchId) : base(searchId) {}
    //==========================================\\
    public static void CreateFile(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("==========================");
                    Console.WriteLine("File created successfully!");
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.White;
                    fs.Close();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occurred while trying to create a file:\n{e}");
        }
    }
    //public static void DeleteFile(string fileName)
    //{
    //    try
    //    {
    //        string path = fileName;
    //        if (File.Exists(path))
    //        {
    //            File.Delete(path);
    //            Console.ForegroundColor = ConsoleColor.Green;
    //            Console.WriteLine("==========================");
    //            Console.WriteLine("File deleted successfully.");
    //        }
    //        else
    //        {
    //            Console.ForegroundColor = ConsoleColor.Yellow;
    //            Console.WriteLine("==========================");
    //            Console.WriteLine("The file does not exist.");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine($"Error occurred while trying to delete a file:\n{e}");
    //    }
    //    finally
    //    {
    //        Console.WriteLine("==========================");
    //        Console.ForegroundColor = ConsoleColor.White;
    //    }
    //}
    //==========================================\\
    public static void insertAdminData(string filePath, int id, string userName, string password)
    {
        Admin admin = new Admin(id, userName, password);
        try
        {
            CheckIdExit(filePath, admin.Id);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{admin.Id.ToString()},{admin.UserName},{admin.Password}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("************************** Inserted Admin **************************");
                Console.WriteLine("Record added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                writer.Close();
            }
        }
        catch(Exception e){    Console.WriteLine(e);    }
    }
    public static void UpdateAdminData(string filePath, int searchId, string field, string value)
    {
        Admin admin = new Admin(searchId);

        //if (!File.Exists(filePath))
        //    CreateFile(filePath);
        try
        {   // Check if the ID exists
            string tempFile = "temp.txt";
            FileInfo oldFile = new FileInfo(filePath);
            FileInfo newFile = new FileInfo(tempFile);
            //*******************************Update********************************\\
            var lineAllText = File.ReadAllText(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    string id = fields[0];
                    string userName = fields[1];
                    string password = fields[2];

                    if (id == searchId.ToString())
                    {
                        if (field.ToLower() == "username" || field.ToLower() == "name")
                        {
                            admin.UserName = value;
                            sb.AppendLine($"{id},{value},{password}");
                        }
                        else if (field.ToLower() == "password")
                        {
                            admin.Password = value;
                            sb.AppendLine($"{id},{userName},{value}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Admin fields are: (username - password)");
                        }
                    }
                    else
                        sb.AppendLine($"{id},{userName},{password}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Record updated successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            File.WriteAllText(tempFile, sb.ToString());
            /////////////////////////////////////////////////////////////////////////////////////
            oldFile.Delete();
            newFile.MoveTo(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void insertTeacherData(
        string filePath, 
        int id, 
        long nationalId, 
        short age, 
        string phone, 
        string email, 
        string fName,
        byte gender,
        double salary,
        string address)
    {
        Teacher teacher = new Teacher(id, nationalId, fName, age, gender, phone, email, address, salary);
        try
        {
            if (File.Exists(filePath))
                CheckIdExit(filePath, teacher.Id);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{teacher.Id.ToString()},{teacher.Name},{teacher.Email},{teacher.Phone},{teacher.Age},{teacher.gender},{teacher.NatId.ToString()},{teacher.Address},{teacher.Salary}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("************************** Inserted Teacher **************************");
                Console.WriteLine("Record added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                writer.Close();
            }
        }
        catch (Exception e) { Console.WriteLine(e); }
    }
    public static void UpdateTeacherData(string filePath, int searchId, string field, string value)
    {
        Teacher teacher = new Teacher(searchId);
        //if (!File.Exists(filePath))
        //    CreateFile(filePath);
        try
        {
            string tempFile = "temp.txt";
            FileInfo oldFile = new FileInfo(filePath);
            FileInfo newFile = new FileInfo(tempFile);
            //*******************************Update********************************\\
            var lineAllText = File.ReadAllText(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    string id = fields[0];
                    string userName = fields[1];
                    string email = fields[2];
                    string phone = fields[3];
                    string age = fields[4];
                    string gender = fields[5];
                    string natId = fields[6];
                    string addr = fields[7];
                    string salary = fields[8];

                    if (id == searchId.ToString())
                    {
                        if (field.ToLower() == "username" || field.ToLower() == "name")
                        {
                            teacher.UserName = value;
                            sb.AppendLine($"{id},{value},{email},{phone},{age},{gender},{natId},{addr},{salary}");
                        }
                        else if (field.ToLower() == "email")
                        {
                            teacher.Email = value;
                            sb.AppendLine($"{id},{userName},{value},{phone},{age},{gender},{natId},{addr},{salary}");
                        }
                        else if (field.ToLower() == "phone")
                        {
                            teacher.Phone = value;
                            sb.AppendLine($"{id},{userName},{email},{value},{age},{gender},{natId},{addr},{salary}");
                        }
                        else if (field.ToLower() == "age")
                        {
                            teacher.Age = short.Parse(value);
                            sb.AppendLine($"{id},{userName},{email},{phone},{value},{gender},{natId},{addr},{salary}");
                        }
                        else if (field.ToLower() == "gender")
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{value},{natId},{addr},{salary}");
                        else if (field.ToLower() == "natId")
                        {
                            teacher.NatId = long.Parse(value);
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{value},{addr},{salary}");
                        }
                        else if (field.ToLower() == "address")
                        {
                            teacher.Address = value;
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{value},{salary}");
                        }
                        else if (field.ToLower() == "salary")
                        {
                            teacher.Salary = double.Parse(value);
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{value}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Admin fields are: (id-name-email-phone-age-gender-natId-addr-salary)");
                        }
                    }
                    else
                        sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{salary}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Record updated successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            File.WriteAllText(tempFile, sb.ToString());
            /////////////////////////////////////////////////////////////////////////////////////
            oldFile.Delete();
            newFile.MoveTo(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void insertStudentData(
        string filePath,
        int id,
        long nationalId,
        short age,
        string phone,
        string email,
        string fName,
        byte gender,
        string address,
        short year,
        string dep)
    {
        Student st = new Student(id, nationalId, fName, age, gender, phone, email, address, year, dep);
        try
        {
            if (File.Exists(filePath))
                CheckIdExit(filePath, st.Id);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{st.Id.ToString()},{st.Name},{st.Email},{st.Phone},{st.Age},{st.gender},{st.NatId.ToString()},{st.Address},{st.CurrentYear},{st.Dep}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("************************** Inserted Teacher **************************");
                Console.WriteLine("Record added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                writer.Close();
            }
        }
        catch (Exception e) { Console.WriteLine(e); }
    }
    public static void UpdateStudentData(string filePath, int searchId, string field, string value) 
    {
        Student student = new Student(searchId);
        //if (!File.Exists(filePath))
        //    CreateFile(filePath);
        try
        {
            string tempFile = "temp.txt";
            FileInfo oldFile = new FileInfo(filePath);
            FileInfo newFile = new FileInfo(tempFile);
            //*******************************Update********************************\\
            var lineAllText = File.ReadAllText(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    string id = fields[0];
                    string userName = fields[1];
                    string email = fields[2];
                    string phone = fields[3];
                    string age = fields[4];
                    string gender = fields[5];
                    string natId = fields[6];
                    string addr = fields[7];
                    string Year = fields[8];
                    string Dep = fields[9];

                    if (id == searchId.ToString())
                    {
                        if (field.ToLower() == "username" || field.ToLower() == "name")
                        {
                            student.UserName = value;
                            sb.AppendLine($"{id},{value},{email},{phone},{age},{gender},{natId},{addr},{Year},{Dep}");
                        }
                        else if (field.ToLower() == "email")
                        {
                            student.Email = value;
                            sb.AppendLine($"{id},{userName},{value},{phone},{age},{gender},{natId},{addr},{Year},{Dep}");
                        }
                        else if (field.ToLower() == "phone")
                        {
                            student.Phone = value;
                            sb.AppendLine($"{id},{userName},{email},{value},{age},{gender},{natId},{addr},{Year},{Dep}");
                        }
                        else if (field.ToLower() == "age")
                        {
                            student.Age = short.Parse(value);
                            sb.AppendLine($"{id},{userName},{email},{phone},{value},{gender},{natId},{addr},{Year},{Dep}");
                        }
                        else if (field.ToLower() == "gender")
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{value},{natId},{addr},{Year},{Dep}");
                        else if (field.ToLower() == "natId")
                        {
                            student.NatId = long.Parse(value);
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{value},{addr},{Year},{Dep}");
                        }
                        else if (field.ToLower() == "address")
                        {
                            student.Address = value;
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{value},{Year},{Dep}");
                        }
                        else if (field.ToLower() == "year")
                        {
                            student.CurrentYear = short.Parse(value);
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{value},{Dep}");
                        }
                        else if (field.ToLower() == "dep")
                        {
                            student.Dep = value;
                            sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{Year},{value}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Admin fields are: (id-name-email-phone-age-gender-natId-addr-year-dep)");
                        }
                    }
                    else
                        sb.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{Year},{Dep}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Record updated successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            File.WriteAllText(tempFile, sb.ToString());
            /////////////////////////////////////////////////////////////////////////////////////
            oldFile.Delete();
            newFile.MoveTo(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void insertCourseData(string filePath, int id, string name, string dep, short grade, short year)
    {
        Course course = new Course(id, name, dep, grade, year);
        try
        {
            if (File.Exists(filePath))
                CheckIdExit(filePath, course.Id);

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{course.Id},{course.Name},{course.Department},{course.Grade},{course.Year}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("************************** Inserted Admin **************************");
                Console.WriteLine("Record added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                writer.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    public static void UpdateCourseData(string filePath, int searchId, string field, string value)
    {
        Course course = new Course(searchId);
        //if (!File.Exists(filePath))
        //    CreateFile(filePath);
        try
        {
            string tempFile = "temp.txt";
            FileInfo oldFile = new FileInfo(filePath);
            FileInfo newFile = new FileInfo(tempFile);
            //*******************************Update********************************\\
            var lineAllText = File.ReadAllText(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    string id = fields[0];
                    string name = fields[1];
                    string dep = fields[2];
                    string grade = fields[3];
                    string year = fields[4];

                    if (id == searchId.ToString())
                    {
                        if (field.ToLower() == "username" || field.ToLower() == "name")
                        {
                            course.Name = value;
                            sb.AppendLine($"{id},{value},{dep},{grade},{year}");
                        }
                        else if (field.ToLower() == "dep")
                        {
                            course.Department = value;
                            sb.AppendLine($"{id},{name},{value},{grade},{year}");
                        }
                        else if (field.ToLower() == "grade")
                        {
                            course.Grade = short.Parse(value);
                            sb.AppendLine($"{id},{name},{dep},{value},{year}");
                        }
                        else if (field.ToLower() == "year")
                        {
                            course.Year = short.Parse(value);
                            sb.AppendLine($"{id},{name},{dep},{grade},{value}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Admin fields are: (id-name-dep-grade-year)");
                        }
                    }
                    else
                        sb.AppendLine($"{id},{name},{dep},{grade},{year}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Record updated successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            File.WriteAllText(tempFile, sb.ToString());
            /////////////////////////////////////////////////////////////////////////////////////
            oldFile.Delete();
            newFile.MoveTo(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void Delete(string filePath, int searchId)
    {
        string tempFile = "temp.txt";
        FileInfo oldFile = new FileInfo(filePath);
        FileInfo newFile = new FileInfo(tempFile);
        StringBuilder stringBuilder = new StringBuilder();

        string lineAlltext = File.ReadAllText(filePath);
        foreach (var line in lineAlltext.Split(Environment.NewLine))
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] fields = line.Split(',');

                if(fields.Length == 5)
                {
                    string id = fields[0];
                    string name = fields[1];
                    string dep = fields[2];
                    string grade = fields[3];
                    string year = fields[4];

                    if (id == searchId.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("field deleted successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        stringBuilder.AppendLine($"{id},{name},{dep},{grade},{year}");
                }
                else if(fields.Length == 3)
                {
                    string id = fields[0];
                    string name = fields[1];
                    string password = fields[2];

                    if (id == searchId.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("field deleted successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        stringBuilder.AppendLine($"{id},{name},{password}");
                }
                else if(fields.Length == 9)
                {
                    string id = fields[0];
                    string userName = fields[1];
                    string email = fields[2];
                    string phone = fields[3];
                    string age = fields[4];
                    string gender = fields[5];
                    string natId = fields[6];
                    string addr = fields[7];
                    string salary = fields[8];

                    if (id == searchId.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("field deleted successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        stringBuilder.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{salary}");
                }
                else if(fields.Length == 10)
                {
                    string id = fields[0];
                    string userName = fields[1];
                    string email = fields[2];
                    string phone = fields[3];
                    string age = fields[4];
                    string gender = fields[5];
                    string natId = fields[6];
                    string addr = fields[7];
                    string Year = fields[8];
                    string Dep = fields[9];

                    if (id == searchId.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("field deleted successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        stringBuilder.AppendLine($"{id},{userName},{email},{phone},{age},{gender},{natId},{addr},{Year},{Dep}");
                }
            }
        }
        File.WriteAllText(tempFile, stringBuilder.ToString());
        oldFile.Delete();
        newFile.MoveTo(filePath);
    } 

    public static void CreateTeacherAccount(string path, int id, string username, string password)
    {
        Admin admin = new Admin(id, username, password);

        try
        {
            if (File.Exists(path))
                CheckIdExit(path, admin.Id, "teacherDatabase.txt", username, true);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"{admin.Id},{admin.UserName},{admin.Password}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("************************** Inserted Teacher Account **************************");
                Console.WriteLine("Record added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                writer.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }


    public static void List(string path)
    {
        var lineAllText = File.ReadAllText(path);
        foreach (var line in lineAllText.Split(Environment.NewLine))
        {
            if (!string.IsNullOrEmpty(line))
            {
                Console.WriteLine($"{line}");
            }
        }
    }
    private static void CheckIdExit(string filePath, int id, string second = "", string name = "", bool check = false)
    {
        var lineAllText = "";

        if (check)
        {
            lineAllText = File.ReadAllText(second);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(',');
                    if (fields.Length > 1)
                    {
                        if (fields[0] == id.ToString())
                        {
                            if(fields[1] != name)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new Exception("\">>>>>>>ID or Name is not exist in teacher database!<<<<<<<\"");
                            }
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }



        lineAllText = File.ReadAllText(filePath);
        foreach (var line in lineAllText.Split(Environment.NewLine))
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] fields = line.Split(',');
                if (fields.Length > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (fields[0] == id.ToString())
                        throw new Exception("\">>>>>>>ID is already exist!<<<<<<<\"");
                    //else if (fields[2] == admin.UserName)
                    //    throw new Exception("\">>>>>>>UserName is already exist!<<<<<<<\"");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
