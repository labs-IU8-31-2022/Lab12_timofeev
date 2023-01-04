using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Groups : ContentPage
{
    public Groups()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        var db = new University();
        GroupList.ItemsSource = db.Groups.ToList().OrderBy(group => group.GroupName);
        base.OnAppearing();
    }
    
    private async void OnItemSelected(object sender, EventArgs e)
    {
        if (GroupList.SelectedItem is not Group group) return;
        GroupList.SelectedItem = null;
        await Navigation.PushAsync(new GroupInfo(group));
    }
    
    private async void GroupAdd(object sender, EventArgs e)
    {
        var page = new GroupEditAndAdd(null);
        await Navigation.PushAsync(page);
    }
}