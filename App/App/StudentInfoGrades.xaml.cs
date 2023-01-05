using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentInfoGrades : ContentPage
{
    public StudentInfoGrades()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        var db = new University();
        BindingContext = db.Students.FindAsync((BindingContext as Student)?.StudentId).Result;
        base.OnAppearing();
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (GradeList.SelectedItem is not Grade grade) return;
        GradeList.SelectedItem = null;
        await Navigation.PushAsync(new GradeEditAndAdd(grade,
            BindingContext as Student ?? throw new InvalidOperationException()));
    }
}