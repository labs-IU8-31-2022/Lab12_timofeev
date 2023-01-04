using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentEditAndAdd : ContentPage
{
    private Student Student { get; set; }
    private bool _edit;
    public StudentEditAndAdd(Student? student)
    {
        InitializeComponent();

        var db = new University();
        var items = db.Groups.OrderBy(group => group.GroupName).ToList();
        Picker1.ItemsSource = items;
        if (student is null)
        {
            Student = new Student();
            _edit = false;
        }
        else
        {
            Student = student;
            _edit = true;
            if (student.Groups is not null)
            {
                Picker1.SelectedItem = items.SingleOrDefault(s => s.GroupId == Student.GroupsId);
            }
        }
        Title = _edit ? "Редактирование студента" : "Добавление нового студента";

        BindingContext = Student;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void StudAdd(object sender, EventArgs e)
    {
        var db = new University();
        if (Name.Text is null || Name.Text.Length == 0)
        {
            Button.BackgroundColor = Color.Red;
            Name.Placeholder = "Введите имя !!!";
            return;
        }

        Button.IsEnabled = false;
        Student.StudentName = Name.Text;
        if (Picker1.SelectedItem is not null)
            Student.GroupsId = ((Group)Picker1.SelectedItem).GroupId;
        if (Picker2.SelectedItem is not null)
            Student.Type = Picker2.SelectedItem.ToString();
        if (!_edit)
        {
            await db.Students.AddAsync(Student);
        }
        else
        {
            var student = await db.Students.FindAsync(Student.StudentId);
            db.Entry(student).CurrentValues.SetValues(Student);
        }
        await db.SaveChangesAsync();
        await Navigation.PopAsync();
        Button.IsEnabled = true;
    }
}