using System.Collections.ObjectModel;
using System.Linq;

namespace WpfTreeView.Directory.ViewModels.Base
{
    /// <summary>
    /// The view model for the applications main Directory view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// A list of all directoried on the machine 
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //Get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            //Create view models from data
            Items = new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }
        #endregion

        #region 
        #endregion
    }
}
