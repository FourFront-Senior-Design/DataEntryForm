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
        string SectionFilePath { get; }
        Headstone CurrentPageData { get; set; }
        void SetRecordsToReview();
        void NextRecord();
        void PreviousRecord();
        List<EmblemData> GetEmblemData { get; }
        //List<CemeteryNameData> GetCemeteryNames { get; }
        int GetDatabaseCount { get; }
    }
}
