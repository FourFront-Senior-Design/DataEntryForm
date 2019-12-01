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
        ICommand PreviousRecordMacro { get; }
        ICommand NextRecordMacro { get; }
        void SetRecordsToReview();
        void NextRecord();
        void PreviousRecord();
        List<EmblemData> GetEmblemData { get; }
    }

    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
