using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Input;
using DataForest.Utilities;
using System.Collections.ObjectModel;

namespace DataForest.ViewModel.Dialogs
{
    public class FileDialogModel : BaseDialogModel
    {

        
        public FileDialogModel()
        {
            string links = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Links");
            DirectoryInfo fav = new DirectoryInfo(links);
            Favorites.Add(new MyComputer{ Name = "Computer" });
            if (fav.Exists)
            {
                IWshRuntimeLibrary.IWshShell shell = new IWshRuntimeLibrary.WshShell();
                foreach (FileInfo lnk in fav.GetFiles("*.lnk", SearchOption.TopDirectoryOnly))
                {
                    string path = ((IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(lnk.FullName)).TargetPath;
                    if (!String.IsNullOrEmpty(path))
                    {
                        DirectoryInfo directory = new DirectoryInfo(path);
                        if (directory.Exists)
                        {
                            Favorites.Add(directory);
                        }
                    }
                }
               
            }

            

            this.Filters = "Alle |*";
            this.SelectedFilter = filters.FirstOrDefault();

            if (startDirectory != null)
            {
                CurrentDirectory = new DirectoryInfo(startDirectory);
            }
            else
            {
                CurrentDirectory = null;
            }

        }
        
        #region private Member

        static private string startDirectory = null;

        private Object[] currentEntries;
        private DirectoryInfo currentDirectory;
        private ICommand folderSelectedCommand;
        private ICommand fileSelectedCommand;
        private ICommand driveSelectedCommand;
        private ICommand folderUpCommand;
        private ICommand folderBackCommand;
        private ICommand folderForwardCommand;
        private ICommand newFolderCommand;
        private ICommand jumpToDirectoryCommand;
        private Object selectedFileSystemInfo;
        private ObservableCollection<Object> folderPath = new ObservableCollection<Object>();
        private FileInfo selectedFile;
        private DirectoryInfo selectedFolder;
        private DriveInfo selectedDrive;
        private Dictionary<string, string[]> filters = new Dictionary<string, string[]>();
        private Stack<DirectoryInfo> lastFolders = new Stack<DirectoryInfo>();
        private Stack<DirectoryInfo> forwardFolders = new Stack<DirectoryInfo>();
        private KeyValuePair<string, string[]> selectedFilter;
        private List<Object> favorites = new List<Object>();

        #endregion

        #region public Properties 

        static public string StartDirectory
        {
            get
            {
                return startDirectory;
            }
            set
            {
                if (value != startDirectory)
                {
                    startDirectory = value;
                }
            }
        }

        public List<Object> Favorites
        {
            get
            {
                return favorites;
            }
            set
            {
                favorites = value;
                OnPropertyChanged("Favorites");
            }
        }


        public bool AskOverrideFile { get; set; }
        public bool CheckFileExists { get; set; }
        public ObservableCollection<Object> FolderPath
        {
            get
            {
                return folderPath;
            }
        }
        public DirectoryInfo CurrentDirectory
        {
            get
            {
                return currentDirectory;
            }
            set
            {
                try
                {
                    DirectoryInfo lastFolder = CurrentDirectory;
                    SetCurrentDirectory(value);
                    lastFolders.Push(lastFolder);
                    RefreshFolderPath();
                }
                catch (Exception ex)
                {
                    this.ShowMessageDialog("Zugriff verweigert", "Der gewählte Ordner konnte nicht geöffnet werden.\n\n" + ex.Message);
                }
            }
        }
        public Object[] CurrentEntries
        {
            get
            {
                return currentEntries;
            }
            set
            {
                if (currentEntries != value)
                {
                    currentEntries = value;
                    OnPropertyChanged("CurrentEntries");
                }
            }
        }
        public string FileName
        {
            get
            {
                if (selectedFile != null)
                {
                    return selectedFile.Name;
                }
                return String.Empty;
            }
        }
        public string FullFileName
        {
            get
            {
                if (selectedFile != null)
                {
                    return selectedFile.FullName;
                }
                return String.Empty;
            }
        }
        public string Filters
        {
            set
            {
                if (value != null)
                {
                    string[] tmp = value.Split('|');
                    try {
                        for (int i = 0; i < tmp.Length; i+=2)
                        {
                            filters.Add(tmp[i], tmp[i + 1].Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries));
                        }

                    } catch (Exception ex) {
                        throw new ArgumentException("Wrong Number of Pipe Seperators, Filter without Name", "Filters", ex);
                    }
                }
            }
        }
        public Dictionary<string, string[]> FilterList
        {
            get
            {
                return filters;
            }
        }
        public bool ShowHiddenEntries { get; set; }

        #endregion




        #region Selected.. Properties

        public KeyValuePair<string, string[]> SelectedFilter {
            get
            {
                return (KeyValuePair<string, string[]>) selectedFilter;
            }
            set
            {
                selectedFilter = value;
                OnPropertyChanged("SelectedFilter");
            }
        }

        public FileInfo SelectedFile
        {
            get
            {
                return selectedFile;
            }
            set
            {
                if (value != selectedFile)
                {
                    selectedFile = value;
                    OnPropertyChanged("SelectedFile");
                    OnPropertyChanged("FileName");
                    OnPropertyChanged("FullFileName");
                }
            }
        }
        public DriveInfo SelectedDrive
        {
            get
            {
                return SelectedDrive;
            }
            set
            {
                if (value != selectedDrive)
                {
                    selectedDrive = SelectedDrive;
                    OnPropertyChanged("SelectedDrive");
                }
            }
        }
        public DirectoryInfo SelectedFolder
        {
            get
            {
                return selectedFolder;
            }
            set
            {
                if (value != selectedFolder)
                {
                    selectedFolder = value;
                    OnPropertyChanged("SelectedFolder");
                }
            }
        }
        public Object SelectedFileSystemInfo
        {
            get
            {
                return selectedFileSystemInfo;
            }
            set
            {
                if (value != selectedFileSystemInfo)
                {
                    selectedFileSystemInfo = value;
                    if (value is FileInfo)
                    {
                        SelectedFile = value as FileInfo;
                    }
                    if (value is DirectoryInfo)
                    {
                        SelectedFolder = value as DirectoryInfo;
                    }
                    if (value is DriveInfo)
                    {
                        selectedDrive = value as DriveInfo;
                    }
                    OnPropertyChanged("SelectedFileSystemInfo");
                }
            }
        }
        #endregion

        #region ICommand Properties

        new public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new RelayCommand(param => DialogReturned());
                }
                return acceptCommand;
            }
        }

        public ICommand JumpToDirectoryCommand
        {
            get
            {
                if (jumpToDirectoryCommand == null)
                {
                    jumpToDirectoryCommand = new RelayCommand(param => JumpToDirectory(param));
                }
                return jumpToDirectoryCommand;
            }
        }
        public ICommand NewFolderCommand
        {
            get
            {
                if (newFolderCommand == null)
                {
                    newFolderCommand = new RelayCommand(param => NewFolder());
                }
                return newFolderCommand;
            }
        }
        public ICommand FileSelectedCommand
        {
            get
            {
                if (fileSelectedCommand == null)
                {
                    fileSelectedCommand = new RelayCommand(param => FileSelected());
                }
                return fileSelectedCommand;
            }
        }
        public ICommand DriveSelectedCommand
        {
            get
            {
                if (driveSelectedCommand == null)
                {
                    driveSelectedCommand = new RelayCommand(param => DriveSelected());
                }
                return driveSelectedCommand;
            }
        }
        public ICommand FolderUpCommand
        {
            get
            {
                if (folderUpCommand == null)
                {
                    folderUpCommand = new RelayCommand(param => FolderUp());
                }
                return folderUpCommand;
            }
        }
        public ICommand FolderBackCommand
        {
            get
            {
                if (folderBackCommand == null)
                {
                    folderBackCommand = new RelayCommand(param => FolderBack());
                }
                return folderBackCommand;
            }
        }
        public ICommand FolderForwardCommand
        {
            get
            {
                if (folderForwardCommand == null)
                {
                    folderForwardCommand = new RelayCommand(param => FolderForward());
                }
                return folderBackCommand;
            }
        }
        public ICommand FolderSelectedCommand
        {
            get
            {
                if (folderSelectedCommand == null)
                {
                    folderSelectedCommand = new RelayCommand(param => ChangeFolder());
                }
                return folderSelectedCommand;
            }
        }
        #endregion

        #region ICommand Handler Methods

        private void JumpToDirectory(object param)
        {
            DirectoryInfo folder = param as DirectoryInfo;
            CurrentDirectory = folder;
        }

        private void NewFolder()
        {
            throw new NotImplementedException();
        }

        private void DriveSelected()
        {
            DriveInfo drive = SelectedFileSystemInfo as DriveInfo;
            if (drive != null)
            {
                CurrentDirectory = drive.RootDirectory;
            }
        }

        private void FolderForward()
        {
            if (forwardFolders.Count != 0)
            {
                lastFolders.Push(currentDirectory);
                CurrentDirectory = forwardFolders.Pop();

            }
        }

        private void FolderBack()
        {
            if (lastFolders.Count != 0)
            {

                forwardFolders.Push(currentDirectory);
                CurrentDirectory = lastFolders.Pop();
            }
        }

        private void FolderUp()
        {
            if (CurrentDirectory != null)
            {
                CurrentDirectory = CurrentDirectory.Parent;
            }
        }

        private void FileSelected()
        {
            FileInfo file = SelectedFileSystemInfo as FileInfo;
            if (file != null)
            {
                SelectedFile = file;
            }
        }

        private void ChangeFolder()
        {
            DirectoryInfo folder = SelectedFileSystemInfo as DirectoryInfo;
            if (folder != null)
            {
                CurrentDirectory = folder;
            }
        }

        new protected void DialogReturned()
        {
            if (CheckFileExists)
            {
                if (!File.Exists(SelectedFile.FullName))
                {
                    this.ShowMessageDialog("Datei nicht vorhanden", "Die ausgewählte Datei konnte nicht gefunden werden. Bitte geben Sie eine vorhandene Datei an!");
                    return;
                }

            }

            if (AskOverrideFile)
            {
                if (File.Exists(SelectedFile.FullName))
                {
                    this.ShowMessageDialog("Datei bereits vorhanden", "Soll die vorhandene Datei überschrieben werden!");
                    return;
                }
            }
            base.DialogReturned();
        }

        #endregion

        private void SetCurrentDirectory(DirectoryInfo value)
        {
            if (value == null)
            {
                CurrentEntries = DriveInfo.GetDrives();
                
            }
            else
            {

                List<FileSystemInfo> entries = new List<FileSystemInfo>();
                if (filters.Count != 0)
                {
                    entries.AddRange(value.GetDirectories());
                    foreach (string searchpattern in selectedFilter.Value) {
                        entries.AddRange(value.GetFileSystemInfos(searchpattern, SearchOption.TopDirectoryOnly));
                    }
                }
                else
                {
                    entries = value.GetFileSystemInfos().ToList();
                }
                if (!ShowHiddenEntries)
                {
                    entries = entries.Where(f => (f.Attributes & FileAttributes.Hidden) == 0).ToList();
                }
                CurrentEntries = entries.ToArray();
            }
            currentDirectory = value;
            SelectedFile = null;
            RefreshFolderPath();
            OnPropertyChanged("CurrentDirectory");
        }

        private void RefreshFolderPath()
        {
            folderPath.Clear();
            for (DirectoryInfo dir = CurrentDirectory; dir != null; dir = dir.Parent)
            {
                folderPath.Insert(0, dir);
            }
            folderPath.Insert(0, new { Name = "Computer" });
            OnPropertyChanged("FolderPath");
        }

        

        
        
    }

    public class MyComputer
    {
        public string Name { get; set; }
    }
}
