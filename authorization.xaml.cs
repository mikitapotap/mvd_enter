using System;
using System.Threading;
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
        long error = 0;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
        }

        private void OnTimerTick(object sender, object e)
        {
            //Label_info.Content = _initState;
            _initState -= 1;

            if (_initState == 2)
            {
                Button_enter.Content = "ПОДТВЕРЖДЁН";
                var username = Login_reg.Text.ToString();
                Label_info.Content = $"Добро пожаловать, {username}";

                Login_reg.BorderBrush = Brushes.GreenYellow;
                Login_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                Pass_reg.BorderBrush = Brushes.GreenYellow;
                Pass_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                Label_info.Foreground = Brushes.GreenYellow;
                Thread.Sleep(2000);
            }

            else if (_initState < 0)
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

            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0),
                IsEnabled = true
            };
            timer.Tick += (o, t) => { Label_info_time.Content = DateTime.Now.ToString("U"); };
            timer.Start();
        }

        private void Button_enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login_reg.Text == "polska_92" && Pass_reg.Password == "555")
                {
                    Button_enter.Content = "ПРОВЕРКА...";
                    Label_info.Content = " ";
                    Pass_reg.IsEnabled = false;
                    Login_reg.IsEnabled = false;
                    Button_enter.IsEnabled = false;

                    _initState = 3;
                    _timer.Interval = TimeSpan.FromSeconds(1);
                    _timer.Tick += OnTimerTick;
                    _timer.Start();
                }
                else
                {
                    error++;
                    Login_reg.BorderBrush = Brushes.Red;
                    Login_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                    Pass_reg.BorderBrush = Brushes.Red;
                    Pass_reg.BorderThickness = new Thickness(3, 3, 3, 3);

                    Label_info.Content = "Ошибка идентификационных данных";
                    Label_info.Foreground = Brushes.Red;

                    Login_reg.SelectAll();
                    Login_reg.Focus();
                    Pass_reg.Clear();

                    Button_enter.Content = "ВОЙТИ";
                }

                if (error == 3)
                {
                    string title = "Системное оповещение безопасности";
                    string sms = "Ошибка авторизации. Программа будет завершена автоматически по нажатию клавиши <OK>.\nДанная ошибка может возникать из-за неверного сочетания логина/пароля или " +
                        "проблем с подключением к базе данных, вызванных различными причинами.\n\n" +
                        "Для устранения данной проблемы советуем вам:\n\n1. Проверить выбранную раскладку на клавиатуре. Убедитесь, не включен ли CapsLock.\n" +
                        "\n\r2. Введите логин и пароль в текстовый документ, убедитесь, что они написаны верно и в них отсутствуют лишние символы, например пробелы в начале" +
                        "или в конце,  после этого скопируйте их (Ctrl+C, Ctrl+V) в соответствующие поля ввода.";
                    Label_info.Content = "";
                    var message = MessageBox.Show(sms, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    if (message == MessageBoxResult.OK)
                    {
                        Application.Current.Shutdown();
                        Pass_reg.IsEnabled = false;
                        Login_reg.IsEnabled = false;
                        Button_enter.IsEnabled = false;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Системное оповещение безопасности", MessageBoxButton.OK, MessageBoxImage.Error);
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
