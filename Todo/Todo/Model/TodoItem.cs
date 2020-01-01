using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Todo.Model
{
    public class TodoItem : INotifyPropertyChanged
    {
        private string todoContent;
        private bool todoFininishFlag;
        private string todoDate;
        private Visibility todoEditFlag;

        public string TodoContent { get => todoContent; set { todoContent = value;RaiseChangee("TodoContent"); } }
        public bool TodoFininishFlag { get => todoFininishFlag; set { todoFininishFlag = value; RaiseChangee("TodoFininishFlag"); } }
        public string TodoDate { get => todoDate; set { todoDate = value; RaiseChangee("TodoDate"); } }
        public Visibility TodoEditFlag { get => todoEditFlag; set { todoEditFlag = value; RaiseChangee("TodoEditFlag"); } }

        public void RaiseChangee(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
