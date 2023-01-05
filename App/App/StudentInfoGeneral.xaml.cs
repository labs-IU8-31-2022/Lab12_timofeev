using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentInfoGeneral : ContentPage
{
    public StudentInfoGeneral()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        var db = new University();
        BindingContext = db.Students.FindAsync((BindingContext as Student)?.StudentId).Result;
        base.OnAppearing();
    }

    private async void EditButton(object sender, EventArgs e)
    {
        EditBut.IsEnabled = false;
        var page = new StudentEditAndAdd(BindingContext as Student);
        await Navigation.PushAsync(page);
        EditBut.IsEnabled = true;
    }

    private async void GradeAddButton(object sender, EventArgs e)
    {
        GradeBut.IsEnabled = false;
        var page = new GradeEditAndAdd(null, BindingContext as Student ?? throw new InvalidOperationException());
        await Navigation.PushAsync(page);
        GradeBut.IsEnabled = true;
    }

    private async void DeleteButton(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Подтверждение действия", "Точно хотите удалить студента?", "Да", "Нет");
        if (!result) return;
        var db = new University();
        var student = BindingContext as Student;
        db.Grades.RemoveRange(db.Grades.Where(g => g.StudentsId == student!.StudentId));
        db.Students.Remove(student!);
        await db.SaveChangesAsync();
        await Navigation.PopAsync();
        BindingContext = null;
    }

    private void ToggleSwitch(object sender, ToggledEventArgs e)
    {
        DelBut.IsEnabled = e.Value;
    }
}