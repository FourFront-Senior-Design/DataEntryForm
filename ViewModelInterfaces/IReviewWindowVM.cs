using DataStructures;
using System;
using System.Collections.Generic;

namespace ViewModelInterfaces
{
    public class EmblemData
    {
        public string Photo { get; set; }
        public string Name { get; set; }
    }

    public class LocationData
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
    }

    public interface IReviewWindowVM
    {
        int PageIndex { get; set; }
        Headstone CurrentPageData { get; set; }
        void SetImagesToReview();
        void NextImage();
        void PreviousImage();
        List<LocationData> GetLocation { get; }
        List<EmblemData> GetEmblemData { get; }
    }
}
