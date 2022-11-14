using System;
using System.IO;
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
using Microsoft.Win32;

using Dictionary.Services;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Locals.Local Local = LocalService.Instance.GetLocal;

        string? StatusBarText { get; set; }

        bool DataChanged { get; set; }
        bool IsNewFile { get; set; }
        bool ShuffleOn { get; set; }
        bool RepeatOn { get; set; }

		void DictInit()
        {
            if (DictService.Init())
            {
                DataChanged = false;
                ButtonSave.IsEnabled = false;
                StatusBarText = DictService.Path;
                StatusBarTextBlock.Text = StatusBarText;
            }
            else
            {
                DictCreate();
            }
        }

        void DictCreate()
        {
            DictService.Create();
			Refresh();
			StatusBarText = Local.NewDictionary;
			StatusBarTextBlock.Text = StatusBarText;
            IsNewFile = true;
            DataChanged = false;
            ShuffleOn = false;
            ButtonShuffle.Background = SystemColors.ControlLightBrush;
            RepeatOn = false;
            ButtonRepeat.Background = SystemColors.ControlLightBrush;
            ButtonSave.IsEnabled = true;
            ButtonLearn.IsEnabled = false;
            ButtonDelete.IsEnabled = false;
        }

        void DictOpen()
        {
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
            {
                DictService.Open(openFileDialog.FileName);

				Refresh();

				StatusBarTextBlock.Text = DictService.Path;

                ButtonSave.IsEnabled = false;
				ButtonDelete.IsEnabled = false;
				ButtonLearn.IsEnabled = false;
				IsNewFile = false;
            }
        }

        void DictSave()
        {
            var result = MessageBox.Show(Local.MessageBoxSaveText,
                Local.MessageBoxSaveCaption, MessageBoxButton.YesNo,
                MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                if (IsNewFile)
                {
                    SaveFileDialog saveFileDialog = new();
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        DictService.Save(saveFileDialog.FileName);
                    }
                    IsNewFile = false;
                }
                else
                {
					DictService.Save();
				}

				StatusBarTextBlock.Text = DictService.Path;

                ButtonSave.IsEnabled = false;

                DataChanged = false;
            }
        }

        void ConfirmAction(Action X)
        {
            if (DataChanged)
            {
                MessageBoxResult result =
                    MessageBox.Show(Local.MessageBoxUnsavedDataText,
                    Local.MessageBoxUnsavedDataCaption, MessageBoxButton.YesNo,
                    MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    X();
                }
            }
            else
            {
                X();
            }
        }

        void Refresh()
        {
            DictionaryGrid.ItemsSource = DictService.GetWords();
			DictionaryGrid.Items.Refresh();
		}

		public MainWindow()
        {
            InitializeComponent();

            DictInit();

            //Link the grid with the dictionary
            DictionaryGrid.ItemsSource = DictService.GetWords();

            EntriesNumberTextBox.Text = DictService.SelectNumber;

            //Set main window title
            Title = Local.MainWindowTitle;

            //Set column headers in the grid
            WordColumn.Header = Local.WordColumnHeader;
            TranslationColumn.Header = Local.TranslationColumnHeader;

            //Set names for buttons
            ButtonLearn.Content = Local.ButtonLearnText;
            ButtonShuffleTextBlock.Text = Local.ButtonShuffleText;
            ButtonRepeat.Content = Local.ButtonRepeatText;
            SelectAllTextBlock.Text = Local.SelectAllButtonText;
            UnselectAllTextBlock.Text = Local.UnselectAllButtonText;
            SelectLastTextBlock.Text = Local.SelectLastButtonText + DictService.SelectNumber;
            SelectRandomTextBlock.Text = Local.SelectRandomButtonText + DictService.SelectNumber;

            //Set tooltips

            //Upper buttons' tooltips
            ButtonNew.ToolTip = Local.ButtonNewTooltip;
            ButtonOpen.ToolTip = Local.ButtonOpenTooltip;
            ButtonSave.ToolTip = Local.ButtonSaveTooltip;
            ButtonAdd.ToolTip = Local.ButtonAddTooltip;
            ButtonDelete.ToolTip = Local.ButtonDeleteTooltip;
            ButtonSearch.ToolTip = Local.ButtonSearchTooltip;

            //Right buttons' tooltips
            ButtonLearn.ToolTip = Local.ButtonLearnTooltip;
            ButtonShuffle.ToolTip = Local.ButtonShuffleTooltip;
            ButtonRepeat.ToolTip = Local.ButtonRepeatTooltip;

            EntriesNumberTextBox.ToolTip = Local.EntriesNumberTextBoxTooltip;
        }

        //Right side buttons
        private void ButtonLearn_Click(object sender, RoutedEventArgs e)
        {
            DictService.FillLearningList
                (DictionaryGrid.SelectedItems, ShuffleOn);

            new Trainer(RepeatOn).ShowDialog();
        }

        private void ButtonShuffle_Click(object sender, RoutedEventArgs e)
        {
            ShuffleOn = !ShuffleOn;

            ButtonShuffle.Background = ShuffleOn ?
                SystemColors.ActiveCaptionBrush : SystemColors.ControlLightBrush;
        }

        private void ButtonRepeat_Click(object sender, RoutedEventArgs e)
        {
            RepeatOn = !RepeatOn;

            ButtonRepeat.Background = RepeatOn ?
				SystemColors.ActiveCaptionBrush : SystemColors.ControlLightBrush;
        }

        private void EntriesNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DictService.SelectNumber = EntriesNumberTextBox.Text;
                SelectLastTextBlock.Text =
                    Local.SelectLastButtonText + DictService.SelectNumber;
                SelectRandomTextBlock.Text =
                    Local.SelectRandomButtonText + DictService.SelectNumber;
                DictionaryGrid.Focus();
            }
        }

        private void EntriesNumberTextBox_LostKeyboardFocus
            (object sender, KeyboardFocusChangedEventArgs e)
        {
			EntriesNumberTextBox.Text = DictService.SelectNumber;
		}

		//Upper buttons
		private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            ConfirmAction(DictCreate);
        }
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            ConfirmAction(DictOpen);
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DictSave();
        }

        //Activate Save button when the editing ends
        void DictionaryGrid_CellEditEnding
            (object sender, DataGridCellEditEndingEventArgs e)
        {
            DataChanged = true;
            ButtonSave.IsEnabled = true;
        }

        //Activate Delete and Learn buttons if at least one entry was selected
        //Activate Shuffle and Repeat buttons if more than two were selected
        void DictionaryGrid_SelectionChanged
            (object sender, SelectionChangedEventArgs e)
        {
            ButtonDelete.IsEnabled = true;
            ButtonLearn.IsEnabled = true;
            if (DictionaryGrid.SelectedItems.Count > 2)
            {
                ButtonShuffle.IsEnabled = true;
                ButtonRepeat.IsEnabled = true;
                StatusBarTextBlock.Text =
                    Local.Selected + DictionaryGrid.SelectedItems.Count;
            }
            else
            {
                ButtonShuffle.IsEnabled = false;
                ButtonRepeat.IsEnabled = false;
                StatusBarTextBlock.Text = StatusBarText;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            DataChanged = new AddWindow().ShowDialog() ?? false;

            if(DataChanged)
            {
				Refresh();
				ButtonSave.IsEnabled = true;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Local.MessageBoxDeleteText,
                Local.MessageBoxDeleteCaption, MessageBoxButton.YesNo,
                MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                int count = DictionaryGrid.SelectedItems.Count;
                for(int i = 0; i < count; i++)
                {
					DictService.RemoveWords(DictionaryGrid.SelectedIndex);
					Refresh();
                }
				DataChanged = true;
                ButtonSave.IsEnabled = true;
                ButtonDelete.IsEnabled = false;
                ButtonLearn.IsEnabled = false;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            UIElement? ButtonSearchParrent =
                VisualTreeHelper.GetParent(ButtonSearch) as UIElement;
            Point ButtonSearchPosition =
                ButtonSearch.TranslatePoint(new Point(), ButtonSearchParrent);
            SearchBox.Margin =
                new Thickness(ButtonSearchPosition.X + 90, 0, 0, 0);
            SearchBox.Text = string.Empty;
            SearchBox.Visibility = Visibility.Visible;
            SearchBox.Focus();
        }
        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Visibility = Visibility.Hidden;
        }
        //Search
        string LastFound { get; set; } = string.Empty;
        int LastSearchIndex { get; set; } = 0;
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            var tmp = DictService.GetWords();

            if (e.Key == Key.Enter)
            {
                int i = (SearchBox.Text == LastFound) ? LastSearchIndex + 1 : 0;
                if (i == tmp.Count)
                {
                    i = 0;
                }
                for (; i < tmp.Count; i++)
                {
                    if ((tmp[i].Word == SearchBox.Text) ||
                        (tmp[i].Translation == SearchBox.Text))
                    {
                        DictionaryGrid.ScrollIntoView(tmp[i]);
                        LastFound = SearchBox.Text;
                        LastSearchIndex = i;
                        break;
                    }
                    LastFound = string.Empty;
                }
            }
            if (e.Key == Key.Escape)
            {
                DictionaryGrid.Focus();
            }
        }

        //Lower buttons
        private void ButtonSelectAll_Click(object sender, RoutedEventArgs e)
        {
            DictionaryGrid.SelectAllCells();
        }

        private void ButtonUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            DictionaryGrid.UnselectAllCells();
            ButtonDelete.IsEnabled = false;
            ButtonLearn.IsEnabled = false;
        }

        private void ButtonSelectLast_Click(object sender, RoutedEventArgs e)
        {
            DictionaryGrid.UnselectAll();

            var tmp = DictService.GetWords();
            int i = tmp.Count - int.Parse(DictService.SelectNumber);
            while(i < tmp.Count)
            {
                DictionaryGrid.SelectedItems.Add(DictionaryGrid.Items[i]);
                i++;
            }
            if (tmp.Count != 0)
            {
                DictionaryGrid.ScrollIntoView(DictionaryGrid.Items[i - 1]);
            }
        }

        private void ButtonSelectRandom_Click(object sender, RoutedEventArgs e)
        {
            Random random = new();
            List <int> RandomIndices = new();
            int index;
            for (int i = 0; i < int.Parse(DictService.SelectNumber); i++)
            {
                do
                {
                    index = random.Next(DictionaryGrid.Items.Count);
                } while (RandomIndices.Contains(index));
                RandomIndices.Add(index);
            }

            //Select rows by index
            DictionaryGrid.UnselectAll();
            for (int i = 0; i < RandomIndices.Count; i++)
            {
            DictionaryGrid.SelectedItems.Add
                    (DictionaryGrid.Items[RandomIndices[i]]);
            }
        }

        //Hide Searchbox if the window size is changed
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DictionaryGrid.Focus();
        }

        private void Window_Closing(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (DataChanged)
            {
                MessageBoxResult result =
                    MessageBox.Show(Local.MessageBoxUnsavedExitText,
                    Local.MessageBoxUnsavedExitCaption, MessageBoxButton.YesNo,
                    MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            DictService.UpdateSettings();
        }
    }
}
