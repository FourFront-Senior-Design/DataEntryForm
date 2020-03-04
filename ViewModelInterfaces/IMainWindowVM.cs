namespace ViewModelInterfaces
{
    public interface IMainWindowVM
    {
        bool ReviewButtonEnabled { get;  }
        string Message { get; }
        string Copyright { get; }
        string Title { get; }
        string FileLocation { get; set; }

        void SetFilePath(string path);
        bool LoadData();
        void ResetWindow();
    }
}
