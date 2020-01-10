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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Todo.Model;
using Todo.Utilities;
using System.Collections.ObjectModel;

namespace Todo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TodoItem> TodoList { get; set; } = new ObservableCollection<TodoItem>();

        private System.Windows.Media.Animation.Storyboard start;
        private System.Windows.Media.Animation.Storyboard end;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAnimation();
            InitializeCommands();           

            //listbox_todo.ItemsSource = TodoList;
        }

        #region Initialization
        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
            //CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeWindow, CanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow, CanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, RestoreWindow, CanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, ShowSystemMenu));
        }    
        
        private void InitializeAnimation()
        {
            start = this.TryFindResource("start") as System.Windows.Media.Animation.Storyboard;
            end = this.TryFindResource("end") as System.Windows.Media.Animation.Storyboard;
        }

        private void CanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void CanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            //SystemCommands.CloseWindow(this);
        }

        private void MaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void RestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }


        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null)
                return;

            var point = WindowState == WindowState.Maximized ? new Point(0, element.ActualHeight)
                : new Point(Left + BorderThickness.Left, element.ActualHeight + Top + BorderThickness.Top);
            point = element.TransformToAncestor(this).Transform(point);
            SystemCommands.ShowSystemMenu(this, point);
        }

        #endregion

        #region Event

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewTodoDialog addNewTodoDialog = new AddNewTodoDialog(this);
            if(addNewTodoDialog.ShowDialog() == true)
            {
                var content = addNewTodoDialog.tbox_Content.Text;
                var time = addNewTodoDialog.combox_Time.Text;

                TodoItemControl todoItemControl = new TodoItemControl();
                todoItemControl.TodoContent = content;
                todoItemControl.TodoTime = time;

                todoItemControl.MouseDoubleClick += (a, b) => 
                {
                    if (MessageBox.Show("是否移除该Todo", "确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        this.stack_ContentList.Children.Remove(todoItemControl);
                    }
                };

                this.stack_ContentList.Children.Add(todoItemControl);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
        }

        private void Setting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grid_Setting.Height > 0)
            {
                HideSettingPanel();
            }
            else
            {
                ShowSettingPanel();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Background.Opacity = e.NewValue;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlurWindow();
        }

        private void WrapPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = e.Source as Border;
            if(border != null)
            {
                var opacity = this.Background.Opacity;
                this.Background = border.Background;
                this.Background.Opacity = opacity;
            }
        }
        #endregion

        #region Method
        private void EnableBlurWindow()
        {
            WindowHelper.BlurWindow(this);
        }

        private void ShowSettingPanel()
        {
            if (start != null)
                start.Begin();
        }

        private void HideSettingPanel()
        {
            if (end != null)
                end.Begin();
        }
        #endregion
    }
}
