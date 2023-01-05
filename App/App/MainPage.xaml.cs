using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var db = new University();
            StudNum.Text = $"Число студентов {db.Students.Count()}";
            GroupNum.Text = $"Число групп {db.Groups.Count()}";

            base.OnAppearing();
        }

        private async void OnItemSelected(object sender, EventArgs e)
        {
        }

        private async void CreateStudent(object sender, EventArgs e)
        {
        }

        private async void StudList(object sender, EventArgs e)
        {
            var page = new Students();
            await Navigation.PushAsync(page);
        }

        private async void GroupList(object sender, EventArgs e)
        {
            var page = new Groups();
            await Navigation.PushAsync(page);
        }
    }
}