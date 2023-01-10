using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentInfoGeneral : ContentPage
{
    public StudentInfoGeneral()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        var db = new StudCont();
        BindingContext = await db.Get((BindingContext as Student)!.StudentId);
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
        var stud = new StudCont();
        var student = BindingContext as Student;
        await stud.Delete(student!.StudentId);
        await Navigation.PopAsync();
        BindingContext = null;
    }

    private void ToggleSwitch(object sender, ToggledEventArgs e)
    {
        DelBut.IsEnabled = e.Value;
    }
}