namespace ViewModelInterfaces
{
    public interface IMainWindowVM
    {
        bool EnableExtract { get; set; }
        string FileLocation { get; set; }
        string Message { get; set; }
        string Copyright { get; }
        int LoadData();
    }
}
