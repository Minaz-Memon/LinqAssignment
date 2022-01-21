// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Program
    {
        IList<Employee> employeeList;
        IList<Salary> salaryList;

        public Program()
        {
            employeeList = new List<Employee>() { 
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}			
		};

            salaryList = new List<Salary>() {
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
        }

        public static void Main()
       {
            Program program = new Program();

            program.Task1();

            program.Task2();

            program.Task3();
            Console.ReadKey();
        }

        public void Task1()
        {


            Console.Write("\n******\nTask 1\n******\n");
            //group salary on id and calc sum of salary
            var salary_emp_grp = from slist in salaryList
                                 group slist by slist.EmployeeID into slist_grp
                                 select new 
                                 {
                                     EmpId = slist_grp.First().EmployeeID,
                                     Total = slist_grp.Sum(s => s.Amount),
                                 };
            //join two list and sort by total salary
            var final_result = from emplist in employeeList
                         join sal in salary_emp_grp on emplist.EmployeeID equals sal.EmpId
                         orderby sal.Total
                         select new
                         {
                             Name = emplist.EmployeeFirstName + " " + emplist.EmployeeLastName,
                             Total_salary = sal.Total                        
                         };

            foreach (var r in final_result)
            {
                Console.WriteLine(r.Name + "\t " + r.Total_salary);
            }
            
        }

        public void Task2()
        {
            Console.Write("\n******\nTask 2\n******\n");
            //Sort emp info by age and get the second oldest age using  ElementAt
            var emp_result = (from emplist in employeeList
                             orderby emplist.Age descending
                             select emplist).ElementAt(1);

            //using emp info extracted in emp_result get the monhtly salary
            var m_sal = (from slist in salaryList
                        where slist.EmployeeID == emp_result.EmployeeID
                        where slist.Type == SalaryType.Monthly
                        select slist).First();
            Console.Write(emp_result.EmployeeID + " " + emp_result.EmployeeFirstName + " " + emp_result.EmployeeLastName + " " + emp_result.Age + " " + m_sal.Amount);
            
        }

        public void Task3()
        {
            Console.Write("\n\n******\nTask 3\n******\n");
            //get emp info of all the emp above 30  and than group them based on salary type
            // calculate average of each grouped salary type and result result
            var emp_result = from emplist in employeeList
                             where emplist.Age > 30
                             join slist in salaryList on emplist.EmployeeID equals slist.EmployeeID
                             group slist by slist.Type into slist_grp
                             select new
                             {
                                 Type = slist_grp.First().Type,
                                 mean = slist_grp.Average(s => s.Amount)
                             };

            foreach (var e in emp_result) {
                Console.Write(e.Type+" "+e.mean+"\n");
            }
            
        }
    }

    public enum SalaryType
    {
        Monthly,
        Performance,
        Bonus
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int Age { get; set; }
    }

    public class Salary
    {
        public int EmployeeID { get; set; }
        public int Amount { get; set; }
        public SalaryType Type { get; set; }
    }
}