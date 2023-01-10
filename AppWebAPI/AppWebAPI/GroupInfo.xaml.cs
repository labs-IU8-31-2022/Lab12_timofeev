using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AppWebAPI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GroupInfo : ContentPage
{
    public Group? Group { get; set; }

    public GroupInfo(Group? group)
    {
        InitializeComponent();

        if (group is null)
        {
            Navigation.PopAsync();
            return;
        }

        Group = group;
        BindingContext = Group;
    }

    protected override async void OnAppearing()
    {
        var db = new GroupCont();
        BindingContext = await db.Get((BindingContext as Group)!.GroupId);
        base.OnAppearing();
    }

    private async void OnItemSelected(object sender, EventArgs e)
    {
        if (StudList.SelectedItem is not Student student) return;
        StudList.SelectedItem = null;
        await Navigation.PushAsync(new StudentInfo(student));
    }

    private async void EditButton(object sender, EventArgs e)
    {
        EditBut.IsEnabled = false;
        var page = new GroupEditAndAdd(BindingContext as Group);
        await Navigation.PushAsync(page);
        EditBut.IsEnabled = true;
    }

    private async void DeleteButton(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Подтверждение действия", "Точно хотите удалить группу?", "Да", "Нет");
        if (!result) return;
        var db = new GroupCont();
        var group = BindingContext as Group;
        await db.Delete(group!.GroupId);
        await Navigation.PopAsync();
        BindingContext = null;
    }

    private void ToggleSwitch(object sender, ToggledEventArgs e)
    {
        DelBut.IsEnabled = e.Value;
    }
}