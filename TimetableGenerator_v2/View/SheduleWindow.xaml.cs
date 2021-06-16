using System.Data;
using System.Windows;

namespace Generator.View
{
    /// <summary>
    /// Логика взаимодействия для SheduleWindow.xaml
    /// </summary>
    public partial class SheduleWindow : Window
    {
        public SheduleWindow(DataTable table)
        {
            InitializeComponent();

            DataView = table.DefaultView;

            DataContext = this;
        }

        public DataView DataView { get; private set; }
    }
}
