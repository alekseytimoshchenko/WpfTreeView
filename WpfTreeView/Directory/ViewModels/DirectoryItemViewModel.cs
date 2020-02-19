using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfTreeView.Directory.ViewModels
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name => Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath);

        /// <summary>
        /// A list of all childn conteined inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Childen { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand => Type != DirectoryItemType.File;

        public bool IsExpanded
        {
            get => Childen?.Count(f => f != null) > 0;

            set
            {
                //If the UI tells us to expand...
                if (value)
                {
                    //Find all children
                    Expand();
                }
                else
                {
                    ClearChildren();
                }
            }
        }
        #endregion

        #region Public Commands
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">Full path to this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            //Create commands
            ExpandCommand = new RelayCommand(Expand);

            //Set path and type
            FullPath = fullPath;
            Type = type;

            //Setup the children as needed
            ClearChildren();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if requred
        /// </summary>
        private void ClearChildren()
        {
            //Clear items
            Childen = new ObservableCollection<DirectoryItemViewModel>();

            //Show the expand arrow if we are not a file
            if (Type != DirectoryItemType.File)
            {
                Childen.Add(null);
            }
        }
        #endregion

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            if (Type == DirectoryItemType.File)
            {
                return;
            }

            //Find all children
            var children = DirectoryStructure.GetDirectoryContents(FullPath);
            Childen = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
