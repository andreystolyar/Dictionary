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
using Dictionary.Models;
using Dictionary.Services;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for Trainer.xaml
    /// </summary>


    public partial class Trainer : Window
    {
        readonly Locals.Local Local = LocalService.Instance.GetLocal;

        readonly List<Entry> learningList = DictService.GetLearningList();

        readonly bool RepeatOn;

        int index = 0;
        
        public Trainer(bool repeat)
        {
            InitializeComponent();

            RepeatOn = repeat;

            Title = Local.WindowTrainerTitle;
            WordLabel.Content = learningList[index].Word;
            TranslationLabel.Content =
                learningList[index].Translation;

            ButtonNextTextBlock.Text = Local.ButtonNextText;
            ButtonNext.ToolTip = Local.ButtonNextTooltip;
            ButtonPreviousTextBlock.Text = Local.ButtonPreviousText;
            ButtonPrevious.ToolTip = Local.ButtonPreviousTooltip;
            WordCheckBox.ToolTip = Local.WordCheckBoxTooltip;
            TranslationCheckBox.ToolTip = Local.TranslationCheckBoxTooltip;

            if (learningList.Count > 1)
            {
                ButtonNext.IsEnabled = true;
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            //First two ifs are required for correct cycling
            if (index == learningList.Count)
            {
                index = 0;
            }

            if (index == learningList.Count - 1)
            {
                index = -1;
            }

            if (index < learningList.Count - 1)
            {
                index++;
                WordLabel.Content = learningList[index].Word;
                TranslationLabel.Content =
                    learningList[index].Translation;

                ButtonPrevious.IsEnabled = true;

                if (index == learningList.Count - 1)
                {
                    if (RepeatOn)
                    {
                        index = -1;
                    }
                    else
                    {
                        ButtonNext.IsEnabled = false;
                    }
                }
            }
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (index == -1)
            {
                index = learningList.Count - 1;
            }

            if (index == 0)
            {
                index = learningList.Count;
            }

            if (index > 0)
            {
                index--;
                WordLabel.Content = learningList[index].Word;
                TranslationLabel.Content =
                    learningList[index].Translation;

                ButtonNext.IsEnabled = true;

                if (index == 0)
                {
                    if (RepeatOn)
                    {
                        index = learningList.Count;
                    }
                    else
                    {
                        ButtonPrevious.IsEnabled = false;
                    }
                }
            }
        }

        private void WordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WordLabel.Visibility = Visibility.Visible;
        }

        private void WordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            WordLabel.Visibility = Visibility.Hidden;
        }

        private void TranslationCheckBox_Checked(object sender,
            RoutedEventArgs e)
        {
            TranslationLabel.Visibility = Visibility.Visible;
        }

        private void TranslationCheckBox_Unchecked(object sender,
            RoutedEventArgs e)
        {
            TranslationLabel.Visibility = Visibility.Hidden;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Space:
                case Key.Right:
                    if (ButtonNext.IsEnabled)
                    {
                        ButtonNext_Click(sender, e);
                    }
                    break;
                case Key.Back:
                case Key.Left:
                    if (ButtonPrevious.IsEnabled)
                    {
                        ButtonPrevious_Click(sender, e);
                    }
                    break;
                case Key.Escape:
                    Close();
                    break;
            }
        }
    }
}
