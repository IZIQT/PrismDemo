using System.Windows;

namespace PrismDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
