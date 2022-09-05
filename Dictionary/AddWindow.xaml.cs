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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        readonly Locals.Local Local = LocalService.Instance.GetLocal;
        
        public AddWindow()
        {
            InitializeComponent();

            this.Title = Local.AddWindowTitle;

            LabelWord.Content = Local.LabelWordText;

            LabelTranslation.Content = Local.LabelTranslationText;

            WordBox.Focus();
        }

        private void CloseTheWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(WordBox.Text)) ||
                !(string.IsNullOrWhiteSpace(TranslationBox.Text)))
            {
                DictService.AddWord(new Entry
                {
                    Word = WordBox.Text,
                    Translation = TranslationBox.Text
                });
                MainWindow.DataChanged = true;
            }
            Close();
        }
    }
}
