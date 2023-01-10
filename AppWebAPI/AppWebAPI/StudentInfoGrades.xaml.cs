using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StudentInfoGrades : ContentPage
{
    public StudentInfoGrades()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        var db = new StudCont();
        BindingContext = await db.Get((BindingContext as Student)!.StudentId);
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