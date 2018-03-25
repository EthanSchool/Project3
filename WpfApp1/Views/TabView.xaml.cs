using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Utils;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for TabView.xaml
    /// </summary>
    public partial class TabView : UserControl
    {
        public TabView()
        {
            InitializeComponent();
        }

        private void NewResident(object sender, RoutedEventArgs e)
        {
            Resident res;
            string error = "";

            if (TypeBox.Text.Length == 0 || !Enum.TryParse<ResidentType>(TypeBox.Text.Replace(" ", string.Empty), true, out var type))
            {
                type = (ResidentType) 999;
            }

            if (Id.Text.Length == 0 || !uint.TryParse(Id.Text, out var id))
            {
                error += "Invalid Id given\n";
                id = 0;
            }

            string firstName = FirstName.Text;
            if (firstName.Length == 0)
                error += "Invalid first name given\n";
            string lastName = LastName.Text;
            if (lastName.Length == 0)
                error += "Invalid last name given\n";

            if (Floor.Text.Length == 0 || !uint.TryParse(Floor.Text, out var floor))
            {
                error += "Invalid floor given\n";
                floor = 0;
            }
            if (Room.Text.Length == 0 || !uint.TryParse(Room.Text, out var room))
            {
                error += "Invalid room given\n";
                room = 0;
            }

            switch (type)
            {
                case ResidentType.Scholarship:
                    res = new ScholarshipResident
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        DormFloor = floor,
                        DormRoom = room
                    };
                    break;
                case ResidentType.Athlete:
                    res = new AthleteResident()
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        DormFloor = floor,
                        DormRoom = room
                    };
                    break;
                case ResidentType.StudentWorker:
                    if (HourlyRate.Text.Length == 0 || !float.TryParse(HourlyRate.Text, out var payRate) || payRate < 0)
                    {
                        error += "Invalid hourly rate\n";
                        payRate = 0;
                    }
                    if (Hours.Text.Length == 0 || !float.TryParse(Hours.Text, out var hoursWorked) || hoursWorked < 0)
                    {
                        error += "Invalid hours worked\n";
                        hoursWorked = 0;
                    }
                    res = new StudentWorkerResident()
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        DormFloor = floor,
                        DormRoom = room,
                        HourlyPayRate = payRate,
                        MonthlyHoursWorked = hoursWorked
                    };
                    break;
                default:
                    error += $"Invalid Resident type '{TypeBox.Text}'\n";
                    MessageBox.Show(error);
                    return;
            }

            if (error != "")
            {
                MessageBox.Show(error);
                return;
            }

            error = Database.AddResident(res);

            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }
            else
            {
                MessageBox.Show("Sucessfully added resident!");
                FirstName.Clear();
                LastName.Clear();

            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var data = Database.SearchResidents(SearchBox.Text) ?? new Resident[0];
            DataGrid.ItemsSource = data;
        }

        private void TypeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBox) sender).SelectedIndex)
            {
                case 0://Scholarship
                    Floor.ItemsSource = new [] {"7", "8"};
                    StudentWorkFields(false);
                    break;
                case 1://Athlete
                    Floor.ItemsSource = new [] { "4", "5", "6" };
                    StudentWorkFields(false);
                    break;
                case 2://Student worker
                    Floor.ItemsSource = new [] { "1", "2", "3" };
                    StudentWorkFields(true);
                    break;
                default:
                    Floor.ItemsSource = new string[0];
                    break;
            }

            void StudentWorkFields(bool enabled)
            {
                HourlyRate.IsEnabled = enabled;
                Hours.IsEnabled = enabled;
                HourlyRate.Text = "";
                Hours.Text = "";
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnalyticsTab.IsSelected)
            {
                Resident[] residents = Database.GetResidents();
                AverageRent.Text = "Average Rent: $" + residents.Average(i => i.Rent);
                TotalResidents.Text = "Total Residents: " + residents.Length;
            }
        }
    }
}
