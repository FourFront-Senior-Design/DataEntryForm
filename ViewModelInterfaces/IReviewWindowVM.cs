using DataStructures;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ViewModelInterfaces
{
    public interface IReviewWindowVM
    {
        int PageIndex { get; set; }
        Headstone CurrentPageData { get; set; }
        void SetRecordsToReview();
        void NextRecord();
        void PreviousRecord();
        List<EmblemData> GetEmblemData { get; }
    }
}
