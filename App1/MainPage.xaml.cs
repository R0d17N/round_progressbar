using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace App1
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private bool isRunning = false;
        private int totalTime = 60; // Время в секундах
        private int currentTime;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;

                currentTime = totalTime;
                progressBar.Progress = 1;
                timerLabel.Text = FormatTime(currentTime);

                while (currentTime > 0 && isRunning)
                {
                    await Task.Delay(1000);
                    currentTime--;
                    progressBar.Progress = (double)currentTime / totalTime;
                    timerLabel.Text = FormatTime(currentTime);
                }

                if (isRunning)
                {
                    // Таймер завершился
                    await DisplayAlert("Таймер", "Время истекло!", "OK");
                }

                isRunning = false;
            }
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            isRunning = false;
        }

        private string FormatTime(int timeInSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
            return string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
