using MINIGAMES.Classes;
using System;
using System.Windows;
using MINIGAMES.Pages;

namespace MINIGAMES.Windows.Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            User.OpenUser();
            WinMain.window = this;
            FrameMain.frame = fMain;
            FrameMain.frame.Navigate(new MinigamesPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            User.SaveUser();
        }
    }
}
