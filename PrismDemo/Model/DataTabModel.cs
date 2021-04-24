using Prism.Commands;
using Prism.Mvvm;
using PrismDemo.Interface;
using PrismDemo.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PrismDemo.Model
{
    public class DataTabModel : BindableBase, ITab
    {
        #region Переменные

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        public bool DeleteEnable = true;

        public event EventHandler CloseRequested;

        private bool playVisibility;
        public bool PlayVisibility
        {
            get => playVisibility;
            set
            {
                playVisibility = value;
                OnPropertyChanged(nameof(PlayVisibility));
            }
        }

        private bool pauseVisibility;
        public bool PauseVisibility
        {
            get => pauseVisibility;
            set
            {
                pauseVisibility = value;
                OnPropertyChanged(nameof(PauseVisibility));
            }
        }

        private bool proceedVisibility;
        public bool ProceedVisibility
        {
            get => proceedVisibility;
            set
            {
                proceedVisibility = value;
                OnPropertyChanged(nameof(ProceedVisibility));
            }
        }

        private bool dischargeVisibility;
        public bool DischargeVisibility
        {
            get => dischargeVisibility;
            set
            {
                dischargeVisibility = value;
                OnPropertyChanged(nameof(DischargeVisibility));
            }
        }

        private string timerText;
        public string TimerText
        {
            get => timerText;
            set
            {
                timerText = value;
                OnPropertyChanged(nameof(TimerText));
            }
        }

        private bool dischargeEnbled;
        public bool DischargeEnbled
        {
            get => dischargeEnbled;
            set
            {
                dischargeEnbled = value;
                OnPropertyChanged(nameof(DischargeEnbled));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set => OnPropertyChanged(nameof(Name));
        }

        public ICommand CloseCommand { get; set; }

        public ICommand PlayTimerCommand { get; }
        public ICommand PauseTimerCommand { get; }
        public ICommand ProceedTimerCommand { get; }
        public ICommand DischargeTimerCommand { get; }
        #endregion

        #region Конструктор
        public DataTabModel()
        {
            try
            {
                StartVisibility();

                PlayTimerCommand = new DelegateCommand<object>(PlayTimerExecute);
                PauseTimerCommand = new DelegateCommand<object>(PauseTimerExecute);
                DischargeTimerCommand = new DelegateCommand<object>(DischargeTimerExecute);
                CloseCommand = new DelegateCommand<object>(p => CloseRequested?.Invoke(this, EventArgs.Empty));

                dt.Tick += new EventHandler(dt_Tick);
                dt.Interval = new TimeSpan(0, 0, 0, 0, 1);

                MainWindowViewModel.NumTab++;
                name = "Секундомер " + MainWindowViewModel.NumTab + ' ' + DateTime.Now.ToLongTimeString();
                TimerText = "00:00:00";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Неизвестная ошибка!");
            }
            
        }
        #endregion

        #region Функции 

        /// <summary>
        /// Сброс таймера
        /// </summary>
        /// <param name="obj"></param>
        private void DischargeTimerExecute(object obj)
        {
            try
            {
                sw.Reset();
                DeleteEnable = true;
                PauseVisibility = false;
                DischargeVisibility = false;
                DischargeEnbled = false;
                ProceedVisibility = false;
                PlayVisibility = true;
                TimerText = "00:00:00";
            }
            catch (Exception exp)
            {
                StartVisibility();
                MessageBox.Show(exp.Message, "Неизвестная ошибка!");
            }
        }

        /// <summary>
        /// Пауза для таймера
        /// </summary>
        /// <param name="obj"></param>
        private void PauseTimerExecute(object obj)
        {
            try
            {
                if (sw.IsRunning)
                {
                    sw.Stop();
                }
                DeleteEnable = false;
                ProceedVisibility = true;
                PauseVisibility = false;
                DischargeEnbled = true;
            }
            catch (Exception exp)
            {
                StartVisibility();
                MessageBox.Show(exp.Message, "Неизвестная ошибка!");
            }
            
        }

        /// <summary>
        /// Старт таймера
        /// </summary>
        /// <param name="obj"></param>
        private void PlayTimerExecute(object obj)
        {
            try
            {
                sw.Start();
                dt.Start();
                DeleteEnable = false;
                DischargeEnbled = false;
                PauseVisibility = true;
                DischargeVisibility = true;
                PlayVisibility = false;
                ProceedVisibility = false;
            }
            catch(Exception exp)
            {
                StartVisibility();
                MessageBox.Show(exp.Message, "Неизвестная ошибка!");
            }
            
        }

        /// <summary>
        /// Функция обработки Tick
        /// </summary>
        /// <param name="obj"></param>
        private void dt_Tick(object sender, EventArgs e)
        {
            try
            {
                if (sw.IsRunning)
                {
                    TimeSpan ts = sw.Elapsed;
                    currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    TimerText = currentTime;
                }
            }
            catch (Exception exp)
            {
                StartVisibility();
                MessageBox.Show(exp.Message, "Неизвестная ошибка!");
            }
        }

        /// <summary>
        /// Начальные настройки для Visibility UserControl
        /// </summary>
        /// <param name="obj"></param>
        private void StartVisibility()
        {
            PlayVisibility = true;
            PauseVisibility = false;
            ProceedVisibility = false;
            DischargeVisibility = false;
            DischargeEnbled = false;
        }

        #endregion
    }
}
