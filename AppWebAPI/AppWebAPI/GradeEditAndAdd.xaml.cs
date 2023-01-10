using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GradeEditAndAdd : ContentPage
{
    public Grade Grade { set; get; }
    public Student Student { set; get; }
    private bool _edit;

    public GradeEditAndAdd(Grade? grade, Student student)
    {
        InitializeComponent();

        Student = student;
        Name.Text = $"{student.StudentName}  {student.Groups?.GroupName}";
        if (grade is null)
        {
            Grade = new Grade
            {
                StudentsId = student.StudentId
            };
            _edit = false;
            DelBut.IsVisible = false;
        }
        else
        {
            Grade = grade;
            _edit = true;
            DelBut.IsVisible = true;
        }

        Title = _edit ? "Редактирование оценок" : "Выставление оценок";

        BindingContext = Grade;
    }

    private async void GradeAdd(object sender, EventArgs e)
    {
        if (Rus.Text.IsNullOrEmpty() && Eng.Text.IsNullOrEmpty() && Dis.Text.IsNullOrEmpty() &&
            Phy.Text.IsNullOrEmpty() && Alg.Text.IsNullOrEmpty())
        {
            Button.BackgroundColor = Color.Red;
            Error.Text = "Введите хотя бы одно поле";
            return;
        }

        Button.IsEnabled = false;
        var grade = new GradeCont();
        Grade.Russian = int.TryParse(Rus.Text, out _) ? Convert.ToInt32(Rus.Text) : null;
        Grade.English = int.TryParse(Eng.Text, out _) ? Convert.ToInt32(Eng.Text) : null;
        Grade.DiscreteMath = int.TryParse(Dis.Text, out _) ? Convert.ToInt32(Dis.Text) : null;
        Grade.Physics = int.TryParse(Phy.Text, out _) ? Convert.ToInt32(Phy.Text) : null;
        Grade.Algorithms = int.TryParse(Alg.Text, out _) ? Convert.ToInt32(Alg.Text) : null;
        if (!_edit)
        {
            await grade.Add(Grade);
        }
        else
        {
            await grade.Update(Grade);
        }

        await Navigation.PopAsync();
        Button.IsEnabled = true;
    }

    private void CheckText(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (entry.Text != "" && (!Regex.IsMatch(entry.Text, @"^\d+$") || Convert.ToInt32(entry.Text) > 100))
        {
            Device.BeginInvokeOnMainThread(() => entry.Text = e.OldTextValue ?? "");
        }
        else
        {
            Button.BackgroundColor = Color.Default;
        }
    }

    private async void DeleteButton(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Подтверждение действия", "Точно хотите удалить оценки?", "Да", "Нет");
        if (!result) return;
        var grad = new GradeCont();
        var grade = BindingContext as Grade;
        await grad.Delete(grade!.GradeId);
        await Navigation.PopAsync();
    }
}