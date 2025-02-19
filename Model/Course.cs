namespace MangmentSystemUnivercity.Model;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

public class Course : Person
{
    //public int Id { get; set; }
    //public string Name { get; set; }
    private string _dep = "";
    private string _name = "";
    private short _grade = 0;
    private short _year = 0;
    

    public string Department 
    { 
        get
        {
            return _dep;
        }
        set
        {
            if (value.ToUpper() == "ICT".ToUpper() ||
                value.ToUpper() == "Autotronics".ToUpper() ||
                value.ToUpper() == "Energy".ToUpper() ||
                value.ToUpper() == "Mechatronics".ToUpper() ||
                value.ToUpper() == "Prosthetics".ToUpper())
            {
                this._dep = value;
            }
            else
                throw new Exception(@"Department not valid, Available departments are: 
                          (ICT - Autotronics - Energy - Mechatronics - Prosthetics)");
        } 
    }
    public short Grade
    {
        get
        {
            return _grade;
        }
        set
        {
            if (value >= 50 && value <= 200)
                this._grade = value;
            else
                throw new Exception("Grade must be between 50 and 200");
        }
    }
    public short Year
    {
        get
        {
            return _year;
        }
        set
        {
            if (value >= 1 && value <= 4)
                this._year = value;
            else
                throw new Exception("Year must be between 1 and 4");
        }
    }
    public string Name 
    {
        get
        {
            return _name;
        }
        set
        {
            if(value != null)  this._name = value;
        }
    }

    public Course(){}
    public Course(int searchId) : base(searchId)
    {
        //if (searchId > 0)
        //    this.Id = searchId;
        //else
        //    throw new Exception("ID input is not valid");
}
    public Course(int id, string name, string dep, short grade, short year) : base(id)
    {
        this.Name = name;
        this.Department = dep;
        this.Grade = grade;
        this.Year = year;
    }
}
