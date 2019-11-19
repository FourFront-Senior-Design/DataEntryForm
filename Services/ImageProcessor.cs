using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DataStructures;
using ServicesInterfaces;
using Newtonsoft.Json;

namespace Services
{
    public class ImageProcessor: IImageProcessor
    {
        private ITextExtractor _textExtractor;
        private ITextClassifier _textClassifier;
        private List<FieldData> _data;
        private ResultsPageData _results;
        private string _jsonLoc = @"C:\Python\FourFrontScripts\results\";

        public ImageProcessor(ITextExtractor textExtractor, ITextClassifier textClassifier)
        {
            _textExtractor = textExtractor;
            _textClassifier = textClassifier;
        }
        
        public bool ProcessImages(List<string> imageLoc) 
        {
            string filePath = Path.GetDirectoryName(imageLoc[0]);
            if (_textExtractor.ReadText(filePath) == false)
                return false;

            if (_textClassifier.ClassifyText() == false)
                return false;

            _data = new List<FieldData>();
            foreach (String path in imageLoc)
            {
                FieldData info = new FieldData();
                info.FileName = path;
                _data.Add(info);
            }

            string[] pathToJson = Directory.GetFiles(_jsonLoc, "*.data", SearchOption.TopDirectoryOnly);

            if (pathToJson.Length == 0)
            {
                Trace.WriteLine(_jsonLoc);
                Trace.WriteLine(pathToJson.Length);
                return false;
            }

            foreach (string path in pathToJson)
            {
                string image_name = Path.GetFileNameWithoutExtension(path);

                for (int i=0; i< _data.Count; i++)
                {
                    Trace.WriteLine(image_name + " " + (Path.GetFileNameWithoutExtension(_data[i].FileName)));
                    if (Path.GetFileNameWithoutExtension(_data[i].FileName) == image_name)
                    {
                        string file = _data[i].FileName;
                        _data[i] = JsonConvert.DeserializeObject<FieldData>(File.ReadAllText(path));
                        _data[i].FileName = file;
                        _data[i].PageNumber = string.Concat((i + 1).ToString(), "/", _data.Count.ToString());
                        break;
                    }
                }
            }
            
            return true;
        }

        public ResultsPageData GetSummary()
        {
            _results = new ResultsPageData();
            _results.Total = _data.Count;

            Trace.WriteLine("Get Summary Total: ");
            Trace.WriteLine(_results.Total);
            return _results;
        }

        public List<FieldData> GetHighConfImages()
        {
            List<FieldData> highConfImages = _data;
            return highConfImages;
        }

        public List<FieldData> GetLowConfImages()
        {
            List<FieldData> lowConfImages = _data;
            return lowConfImages;
        }
    }
}
