using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Students : ContentPage
{
    public Students()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        var stud = new StudCont();
        var items = await stud.GetAll();
        StudList.ItemsSource = items;
        base.OnAppearing();
    }

    private async void OnItemSelected(object sender, EventArgs e)
    {
        if (StudList.SelectedItem is not Student student) return;
        StudList.SelectedItem = null;
        await Navigation.PushAsync(new StudentInfo(student));
    }

    private async void StudentAdd(object sender, EventArgs e)
    {
        var page = new StudentEditAndAdd(null);
        await Navigation.PushAsync(page);
    }
}