using System.Collections.ObjectModel;
using System.Linq;

namespace WpfTreeView.Directory.ViewModels
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel: BaseViewModel
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path to the item
        /// </summary>
        public  string FullPath { get; set; }

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

            }
        }
    }
}
