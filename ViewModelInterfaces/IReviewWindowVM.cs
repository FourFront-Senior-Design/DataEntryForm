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
        MacroCommand UnknownFieldMacro { get; }
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

    public class MacroCommand : ICommand
    {
        public Headstone _stone { get; }
        private string _fill;

        public MacroCommand(Headstone stone, string fill)
        {
            _stone = stone;
            _fill = fill;
        }

        public void Execute(object parameter)
        {
            string param = parameter.ToString();
            object obj = _stone;
            object prev = obj;
            var info = obj.GetType().GetProperty(param);
            foreach (String part in param.Split('.'))
            {
                Type type = obj.GetType();
                info = type.GetProperty(part);
                if (info == null) { return; }
                System.Diagnostics.Trace.WriteLine(info);
                prev = obj;
                obj = info.GetValue(obj, null);
            }

            System.Diagnostics.Trace.WriteLine(prev);
            System.Diagnostics.Trace.WriteLine(_stone.PrimaryDecedent);
            System.Diagnostics.Trace.WriteLine(_stone.PrimaryDecedent.Inscription);
            
            info.SetValue(prev, _fill);
        }

        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
