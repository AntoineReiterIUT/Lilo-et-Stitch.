using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.View.PatternsViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Interaction logic for WindowModelsStitch.xaml
    /// </summary>
    public partial class WindowModelsStitch : Window, INotifyPropertyChanged
    {
        #region attributes

        private int page = 0;
        private Manager manager;
        private ObservableCollection<TemplateUserModel> patterns;

        #endregion

        #region properties

        public int Page
        {
            get { return page; }
            set
            {
                if (page != value)
                {
                    page = value;
                    OnPropertyChanged(nameof(Page));
                }
            }
        }

        public ObservableCollection<TemplateUserModel> Patterns
        {
            get { return patterns; }
            set
            {
                if (patterns != value)
                {
                    patterns = value;
                    OnPropertyChanged(nameof(Patterns));
                }
            }
        }

        #endregion

        #region operations

        public WindowModelsStitch(Manager manager)
        {
            InitializeComponent();
            manager = manager;
            Patterns = new ObservableCollection<TemplateUserModel>();
            ImageLoader();
            DataContext = this;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowMenuStitch stitchMenu = new WindowMenuStitch();
            stitchMenu.Show();
            Close();
        }

        private void ImageLoader()
        {
            Patterns.Clear();
            Dictionary<int, Dictionary<string, BitmapImage>> images = manager.GetImages(Page);
            numPage.Text = (Page + 1).ToString();

            int row = 0;
            int column = 0;

            foreach (int i in images.Keys)
            {
                foreach (string name in images[i].Keys)
                {
                    TemplateUserModel pattern = new TemplateUserModel();
                    pattern.PatternImage.Source = images[i][name];
                    pattern.PatternName.Text = name;

                    // Set different row and column for each TemplateUserModel
                    Grid.SetRow(pattern, row);
                    Grid.SetColumn(pattern, column);

                    Patterns.Add(pattern);

                    // Update row and column for the next iteration
                    column++;
                    if (column >= 3)  // Assuming 3 columns
                    {
                        column = 0;
                        row++;
                    }
                }
            }
        }

        private void Button_NextClick(object sender, RoutedEventArgs e)
        {
            Page++;
            ImageLoader();
        }

        private void Button_PreviousClick(object sender, RoutedEventArgs e)
        {
            if (Page > 0)
            {
                Page--;
                ImageLoader();
            }
        }

        #endregion

        #region PropertyChanged Event

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
