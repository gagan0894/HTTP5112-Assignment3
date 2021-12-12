using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5112_Assignment3.Models;
using MySql.Data.MySqlClient;

namespace HTTP5112_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        [Route("api/TeacherData/ListTeachers")]
        [HttpGet]
        public List<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string Salary = ResultSet["salary"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();

                Teacher NewTeacher = new Teacher();
                NewTeacher.Id = TeacherId;
                NewTeacher.Fname = TeacherFname;
                NewTeacher.Lname = TeacherLname;
                NewTeacher.Salary = Salary;
                NewTeacher.Enum = EmployeeNumber;

                //Add the Author Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }
        /// <summary>
        /// Returns the list of teachers with salary greater than input salary
        /// </summary>
        /// <param name="salary">input from the user to get the teachers with salary greater than input</param>
        /// <returns>list of teachers with salary greater than input salary</returns>
        [Route("api/TeacherData/ListTeachers/{salary}")]
        [HttpGet]
        public List<string> ListTeachersWithSalary(int salary)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where salary>"+salary;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<String> TeacherNames = new List<string> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                //Add the Teacher Name to the List
                TeacherNames.Add(TeacherName);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return TeacherNames;
        }

        [HttpPost]
        public void AddTeacher(Teacher newTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL QUERY
            int NextId = getNextId();
            cmd.CommandText = "insert into teachers(teacherid,teacherfname,teacherlname,employeenumber,hiredate,salary) values(@Teacherid,@Teacherfname,@Teacherlname,@EmployeeNumber,CURRENT_DATE(),@Salary)";
            cmd.Parameters.AddWithValue("@Teacherid", NextId);
            cmd.Parameters.AddWithValue("@Teacherfname", newTeacher.Fname);
            cmd.Parameters.AddWithValue("@Teacherlname", newTeacher.Lname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", newTeacher.Enum);
            cmd.Parameters.AddWithValue("@Salary", newTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        private int getNextId()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "select max(teacherid) as id from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            int TeacherId =0;
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                TeacherId = (int)ResultSet["id"] ;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();
            return TeacherId + 1;
        }

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
    }
}
