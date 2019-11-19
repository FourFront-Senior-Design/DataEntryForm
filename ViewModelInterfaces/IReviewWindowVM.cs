using DataStructures;
using System;
using System.Collections.Generic;

namespace ViewModelInterfaces
{
    public interface IReviewWindowVM
    {
        FieldData CurrentPageData { get; set; }
        void SetImagesToReview(List<FieldData> images);
        bool SaveToDatabase();
        void NextImage();
        void PreviousImage();
    }
}
