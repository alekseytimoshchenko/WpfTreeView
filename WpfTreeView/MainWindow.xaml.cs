using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfTreeView
{
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem
                {
                    Header = drive,
                    Tag = drive
                };

                //Add a dummy item
                item.Items.Add(null);

                //Add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }

        #endregion


    }
}
