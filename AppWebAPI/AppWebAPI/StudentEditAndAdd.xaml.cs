using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentEditAndAdd : ContentPage
{
    private Student Student { get; set; }
    private bool _edit;

    public StudentEditAndAdd(Student? student)
    {
        InitializeComponent();

        if (student is null)
        {
            Student = new Student();
            _edit = false;
        }
        else
        {
            Student = student;
            _edit = true;
        }

        Title = _edit ? "Редактирование студента" : "Добавление нового студента";
        
        BindingContext = Student;
    }

    protected override async void OnAppearing()
    {
        var gr = new GroupCont();
        var items = (await gr.GetAll())?.OrderBy(group => group.GroupName).ToList();
        Picker1.ItemsSource = items;
        if (Student.Groups is not null)
        {
            Picker1.SelectedItem = items?.SingleOrDefault(s => s.GroupId == Student.GroupsId);
        }
    }

    private async void StudAdd(object sender, EventArgs e)
    {
        var db = new StudCont();
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
            await db.Add(Student);
        }
        else
        {
            //var student = await db.Students.FindAsync(Student.StudentId);
            //db.Entry(student).CurrentValues.SetValues(Student);
            await db.Update(Student);
        }

        //await db.SaveChangesAsync();
        await Navigation.PopAsync();
        Button.IsEnabled = true;
    }

    private void NameTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!e.OldTextValue.IsNullOrEmpty()) return;
        Button.BackgroundColor = Color.Default;
        Name.Placeholder = "Введите имя";
    }
}