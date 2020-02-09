using System;
using System.Collections.Generic;
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
                    //Set the header
                    Header = drive,

                    //Add the full path
                    Tag = drive
                };

                //Add a dummy item
                item.Items.Add(null);

                // Listen our for item being expanded
                item.Expanded += Folder_Expanded;

                //Add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            //If the item only conteins the dummy date 
            if (item.Items.Count != 1
                && item.Items[0] != null)
            {
                return;
            }

            //Clear dummy data
            item.Items.Clear();

            //Get folder name
            var fullPaht = (string)item.Tag;

            //Create a blank list for directories
            List<string> directories = new List<string>();

            //Try and get directories from the folder
            //ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPaht);

                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch
            {

            }

            directories.ForEach(directoryPaht =>
            {
                //Create directory item
                var subItem = new TreeViewItem()
                {
                    //Set header as folder name
                    Header = GetFileFolderName(directoryPaht),

                    //And tag as full path
                    Tag = directoryPaht
                };

                //Add dummy item so we can expand folder
                subItem.Items.Add(null);

                //Handle expanding
                subItem.Expanded += Folder_Expanded;

                //Add this item to parant
                item.Items.Add(subItem);
            });

            
        }

        //Find the file or folder name from a full path
        private static string GetFileFolderName(string path)
        {
            //If we have no path, return empty
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // Make all alshed back slashes
            var normalizedPath = path.Replace('/', '\\');

            //Find a last back slash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //If we don't find a back slash return the path itself
            if (lastIndex <= 0)
                return path;

            //Return the name after the last backslash
            return path.Substring(lastIndex + 1);
        }

        #endregion


    }
}
