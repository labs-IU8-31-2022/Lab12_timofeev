using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Groups : ContentPage
{
    public Groups()
    {
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        var group = new GroupCont();
        GroupList.ItemsSource = (await group.GetAll())?.OrderBy(gr => gr.GroupName).ToList();
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