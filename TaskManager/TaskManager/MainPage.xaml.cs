using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskManager
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetTasks();
            var iconTap = new TapGestureRecognizer();
            iconTap.Tapped += addB_Clicked;
            addI.GestureRecognizers.Add(iconTap);

            var iconTap1 = new TapGestureRecognizer();
            iconTap1.Tapped += settingsI_Clicked;
            settingsI.GestureRecognizers.Add(iconTap1);
        }

        private async void settingsI_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Settings());
        }

        private async void addB_Clicked(object sender, EventArgs e)
        {
            Button b = new Button();
            string result = await DisplayPromptAsync("Задача", "");
            b.Text = result;
            b.Clicked += taskB_Clicked;
            b.BackgroundColor = Color.Silver;
            mainSV.Children.Add(b);
        }

        private void taskB_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackgroundColor == Color.Red)
                b.BackgroundColor = Color.Green;
            else if(b.BackgroundColor == Color.Green)
                b.BackgroundColor = Color.Silver;
            else
                b.BackgroundColor = Color.Red;
        }

        private void SaveTasks()
        {
            int i = 0;
            App.Current.Properties.Clear();
            foreach (Button b in mainSV.Children)
            {
                App.Current.Properties.Add($"task{i}-Text", b.Text);
                App.Current.Properties.Add($"task{i}-Color", b.BackgroundColor.ToHex());
                i++;
            }
            App.Current.Properties.Add("TasksCount", $"{i}");
        }

        private void GetTasks()
        {
            if (App.Current.Properties.Count == 0)
                return;
            int count = int.Parse(App.Current.Properties["TasksCount"].ToString());
            for(int i = 0; i < count; i++)
            {
                Button b = new Button();
                object x = "";
                if(App.Current.Properties.TryGetValue($"task{i}-Text", out x))
                    b.Text = x.ToString();
                if(App.Current.Properties.TryGetValue($"task{i}-Color", out x))
                    b.BackgroundColor = Color.FromHex(x.ToString());
                b.Clicked += taskB_Clicked;
                mainSV.Children.Add(b);
            }
        }

        private void saveB_Clicked(object sender, EventArgs e)
        {
            SaveTasks();
        }

        private void sendReportB_Clicked(object sender, EventArgs e)
        {
            Telegram.A();
            //App.Current.Properties.Clear();
        }
    }
}
