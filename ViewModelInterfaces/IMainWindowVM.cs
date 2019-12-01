namespace ViewModelInterfaces
{
    public interface IMainWindowVM
    {
        bool EnableExtract { get; set; }
        string FileLocation { get; set; }
        int LoadData();
    }
}
