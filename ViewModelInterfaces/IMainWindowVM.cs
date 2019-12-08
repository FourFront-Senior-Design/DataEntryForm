namespace ViewModelInterfaces
{
    public interface IMainWindowVM
    {
        bool EnableExtract { get; set; }
        string FileLocation { get; set; }
        string Message { get; set; }
        int LoadData();
    }
}
