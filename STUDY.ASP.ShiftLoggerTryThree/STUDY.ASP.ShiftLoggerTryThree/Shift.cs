namespace STUDY.ASP.ShiftLoggerTryThree
{
    // Shift.cs
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

    // Employee.cs
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add more properties as needed
    }

}
