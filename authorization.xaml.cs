using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace mvd_enter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _initState;
        private readonly DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
        }

        private void OnTimerTick(object sender, object e)
        {
            Label_info.Content = _initState;
            _initState -= 1;
            if (_initState < 0)
            {
                _timer.Stop();
                mainForm forma_new = new mainForm();
                forma_new.Show();
                Close();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login_reg.Focus();
            Login_reg.SelectionStart = Login_reg.Text.Length;
            Login_reg.SelectAll();

            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0),
                IsEnabled = true
            };
            timer.Tick += (o, t) => { Label_info_time.Content = DateTime.Now.ToString("U"); };
            timer.Start();
        }

        private void Button_enter_Click(object sender, RoutedEventArgs e)
        {
            if (Login_reg.Text.Trim() == "polska_92" && Pass_reg.Password == "555")
            {
                //отрисовка контура и появление уровня досупа.
                Login_reg.BorderBrush = Brushes.GreenYellow;
                Login_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                Pass_reg.BorderBrush = Brushes.GreenYellow;
                Pass_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                Label_info.Content = "Доступ разрешён";
                Label_info.Foreground = Brushes.GreenYellow;

                _initState = 3;
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += OnTimerTick;
                _timer.Start();
            }
            else
            {
                //отрисовка контура и появление уровня досупа.
                Login_reg.BorderBrush = Brushes.Red;
                Login_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                Pass_reg.BorderBrush = Brushes.Red;
                Pass_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                Label_info.Content = "Ошибка идентификационных данных";
                Label_info.Foreground = Brushes.Red;

                Login_reg.SelectAll();
                Login_reg.Focus();
                Pass_reg.Clear();
            }
        }
        private void Login_reg_TextChanged(object sender, TextChangedEventArgs e)
        {
            //визуальное отображение подсказки.
            if (String.IsNullOrEmpty(Login_reg.Text.Trim()))
            {
                Login_reg.Text = "логин сотрудника";
                Login_reg.SelectionStart = Login_reg.Text.Length;
                Login_reg.FontSize = 19;
                Login_reg.SelectAll();
            }
        }
    }
}
