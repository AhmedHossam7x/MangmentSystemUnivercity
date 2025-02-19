namespace MangmentSystemUnivercity.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public enum Gender : byte
{
    Male = 1, //the defuilte is (0) but can change it by this way
    Femal
}

public class Person
{
    private int _id;
    private long _naId;
    private string _name;
    private short _age;
    private Gender _gender;

    public int Id
    {
        get { return _id; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Can`t input id number less then zero");
            else
                _id = value;
        }
    }
    public long NatId 
    { 
        get { return _naId; }
        set
        {
            if (natIdValidation(value))
                _naId = value;
            else
                Console.WriteLine("The number is not contain 14 digit");
        }
    }
    public string Name 
    {
        get { return _name; }
        set
        {
            if(nameValidation(value))
                _name = value;
        }
    }
    public short Age
    {
        get { return _age; }
        set
        {
            if(value > 0 && value < 55)
                _age = value;
            else
                Console.WriteLine("Wrong input age (max input is 55)");
        }

    }
    public string Address { get; set; }
    public Gender gender{ get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string[] Lectures { get; set; }
    public string[,] Table { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    //================================\\
    public Person() { }
    public Person(int id)
    {
        if (id <= 0)
            throw new Exception("ID is not valid");
        else
            this.Id = id;
    }
    public Person(int id, string name)
    {
        if (id <= 0)
            throw new Exception("ID is not valid");
        else
            this.Id = id;

        if (nameValidation(name))
            this.Name = name;
        else
            throw new Exception("Name is not valid, Accepts only letters");
    }
    public Person(int id, string username, string password)
    {
        if (id <= 0)
            throw new Exception("ID is not valid");
        else
            this.Id = id;
        if(userNameValidation(username))
            this.UserName = username;
        else
            throw new Exception("UserName is not valid");
        if(passwordValidation(password))
            this.Password = password;
        else
            throw new Exception(@"Password must contain at least one digit [0-9]. +
            Password must contain at least one lowercase Latin character [a-z] +
            Password must contain at least one uppercase Latin character [A-Z]. +
            Password must contain at least one special character like ! @ # & ( ). +
            Password must contain a length of at least 8 characters and a maximum of 20 characters.");
    }
    public Person(int id,long nId,string name,short age,byte gender,string phone,string email,string addr)
    {
        if(id <= 0)
            throw new Exception("ID is not valid");
        else
            this.Id = id;
        if(natIdValidation(nId))
            this.NatId = nId;
        else
            throw new Exception("national id must have 14 digits");
        if(nameValidation(name))
            this.Name = name;
        else
            throw new Exception("Name is not valid, Accepts only letters");
        if (age > 18 & age < 50)
            this.Age = age;
        else
            throw new Exception("age input must be between 19 and 50");
        if (gender == 1 || gender == 2)
            this.gender = (Gender)gender;
        else
            throw new Exception("Accepts only (\"male\" or \"female\")");
        if(phoneValidation(phone))
            this.Phone = phone;
        else
            throw new Exception("Phone must have 11 digits");
        if(emailValidation(email))
            this.Email = email;
        else
            throw new Exception("Email is not valid");
        if(addr != null)
            this.Address = addr;
        else
            throw new Exception("Address not valid, Accepts only letters");
    }       
    public Person(string userName, string password)
    {
        if (userNameValidation(userName))
            this.UserName = userName;
        else
            throw new Exception("Username must be between 8 and 20 characters");
        if (passwordValidation(password))
            this.Password = password;
        else
            throw new Exception(
            @"Password must contain at least one digit [0-9].
              Password must contain at least one lowercase Latin character [a-z].
              Password must contain at least one uppercase Latin character [A-Z].
              Password must contain at least one special character like ! @ # & ( ).
              Password must contain a length of at least 8 characters and a maximum of 20 characters.");
    }
    //================================\\
    private static bool emailValidation(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var addr = new MailAddress(email);
            return true;
        }
        catch
        {
            throw new Exception("The mail is not valid");
        }

    }
    private static bool userNameValidation(string userName)
    {
        String regexUser = @"^[A-Za-z0-9_-]{3,20}$";
        if (String.IsNullOrEmpty(userName))
            return false;
        Regex regex = new Regex(regexUser);
        return regex.IsMatch(userName);
    }
    private static bool passwordValidation(string password)
    {
        String regexPass = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
        Regex regex = new Regex(regexPass);
        return regex.IsMatch(password);
    }
    private static bool natIdValidation(long id)
    {
        if (id <= 0)
            return false;
        int countIdDigits = 0;
        while (id != 0)
        {
            id /= 10;
            ++countIdDigits;
        }
        return (countIdDigits == 14);
    }
    private static bool nameValidation(string name)
    {
        string pattern = @"(?i)(^[A-Za-z])((?![ .,'-]$)[A-Za-z .,'-]){0,24}$";
        return Regex.IsMatch(name, pattern);
    }
    private static bool phoneValidation(string phone)
    {
        string pattern = @"^(\+?\d{1,3}[-\s]?)?(\(?\d{1,4}\)?[-\s]?)?[\d\s\-]{11}$";
        return Regex.IsMatch(phone, pattern);
    }
    //public static boolean lecturesValidation(String[] lectures)
    //{
    //    for (String value : lectures)
    //        if (!value.matches("(?i)(^[a-z])((?![ .,'-]$)[a-z .,'-]){0,24}$"))
    //            return false;
    //    return true;
    //}
    //public static boolean tableValidation(String[][] table)
    //{
    //    for (String[] innerArray : table)
    //        for (String value : innerArray)
    //            if (!value.matches("(?i)(^[a-z])((?![ .,'-]$)[a-z .,'-]){0,24}$"))
    //                return false;
    //    return true;
    //}
    //================================\\
    public static bool login(string databasePath, string userName, string password)
    {
        bool login = false;
        try
        {
            var lineAllText = File.ReadAllText(databasePath);
            foreach (var line in lineAllText.Split(Environment.NewLine))
            {
                string[] data = line.Split(',');
                string id = data[0];
                string userNameScan = data[1].Trim();
                string passwordScan = data[2].Trim();

                if(userNameScan.Equals(userName) && passwordScan.Equals(password))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=======================");
                    Console.WriteLine($"****Welcome In System Mr/Mrs:({userNameScan}).****");
                    Console.WriteLine("=======================");
                    Console.ForegroundColor = ConsoleColor.White;
                    return login=true;
                }
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=====================");
            Console.WriteLine($"{e} >>>>>>> Login field <<<<<<<");
            Console.WriteLine("\">>>>>>>Username or password are not valid.<<<<<<<\"");
            Console.WriteLine("=====================");
            Console.ForegroundColor = ConsoleColor.White;
        }
        return login;
    }
}
