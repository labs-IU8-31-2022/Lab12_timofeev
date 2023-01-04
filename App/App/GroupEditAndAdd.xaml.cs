using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GroupEditAndAdd : ContentPage
{
    private Group Group { get; set; }
    private bool _edit;
    public GroupEditAndAdd(Group? group)
    {
        InitializeComponent();
        
        if (group is null)
        {
            Group = new Group();
            _edit = false;
        }
        else
        {
            Group = group;
            _edit = true;
        }
        Title = _edit ? "Редактирование группы" : "Добавление новой группы";

        BindingContext = Group;
    }

    private async void GrAdd(object sender, EventArgs e)
    {
        var db = new University();
        if (Name.Text is null || Name.Text.Length == 0)
        {
            Button.BackgroundColor = Color.Red;
            Name.PlaceholderColor = Color.Red;
            return;
        }

        Button.IsEnabled = false;
        Group.GroupName = Name.Text;
        if (!_edit)
        {
            await db.Groups.AddAsync(Group);
        }
        else
        {
            var group = await db.Groups.FindAsync(Group.GroupId);
            db.Entry(group).CurrentValues.SetValues(Group);
        }
        await db.SaveChangesAsync();
        await Navigation.PopAsync();
        Button.IsEnabled = true;
    }
}