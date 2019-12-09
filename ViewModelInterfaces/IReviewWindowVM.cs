using DataStructures;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ViewModelInterfaces
{
    public interface IReviewWindowVM
    {
        event EventHandler HeadstoneChanged;

        int PageIndex { get; set; }
        string ImageSource1 { get; }
        string ImageSource2 { get; }
        Headstone CurrentPageData { get; set; }
        List<EmblemData> GetEmblemData { get; }
        List<CemeteryNameData> GetCemeteryNames { get; }
        List<LocationData> GetLocationData { get; }
        List<BranchData> GetBranchData { get; }
        List<AwardData> GetAwardData { get; }
        List<WarData> GetWarData { get; }
        int GetDatabaseCount { get; }

        List<bool> CheckMandatoryFields();
        void SetRecordsToReview();
        void NextRecord();
        void PreviousRecord();
        void FirstRecord();
        void LastRecord();
        bool GoToRecord(string input);
        void SaveRecord();
    }
}
