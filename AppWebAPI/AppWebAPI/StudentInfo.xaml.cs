using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentInfo : TabbedPage
{
    public Student? Student { get; set; }

    public StudentInfo(Student? student)
    {
        InitializeComponent();

        if (student is null)
        {
            Navigation.PopAsync();
            return;
        }

        Student = student;
        BindingContext = Student;
    }
}