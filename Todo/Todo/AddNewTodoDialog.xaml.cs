using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Todo
{
    /// <summary>
    /// AddNewTodoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewTodoDialog : Window
    {
        public AddNewTodoDialog(Window window)
        {
            InitializeComponent();
            InitializeCommands();

            this.Owner = window;
        }

        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));        
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbox_Content.Focus();
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty( this.tbox_Content.Text.Trim()))
            {
                MessageBox.Show("请输入内容");
                return;
            }

            this.DialogResult = true;
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
