using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.Utils
{
    /// Note:
    /// - Example taken from: https://kmatyaszek.github.io/wpf%20validation/2019/03/13/wpf-validation-using-inotifydataerrorinfo.html
    /// - And combined with ObservableBase
    public abstract class ObservableAndValidationalBase : ObservableBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors { get => _errorsByPropertyName.Count > 0; }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrWhiteSpace(propertyName) &&
                _errorsByPropertyName.ContainsKey(propertyName))
            {
                return _errorsByPropertyName[propertyName];
            }

            return null;
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }
}
