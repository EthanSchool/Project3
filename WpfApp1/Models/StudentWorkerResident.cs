namespace WpfApp1.Models
{
    class StudentWorkerResident : Resident
    {
        public float HourlyPayRate { get; set; }//Default to $14
        public float MonthlyHoursWorked { get; set; }
        public override float Rent => (float)(1_245 - 0.5 * HourlyPayRate * MonthlyHoursWorked);
    }
}
