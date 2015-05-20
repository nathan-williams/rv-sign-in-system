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

using System.IO;
using Thriple.Controls;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for FlipImageGrid.xaml
    /// </summary>
    public partial class FlipImageGrid : UserControl
    {
        public FlipImageGrid()
        {
            InitializeComponent();

            this.Loaded += ForcePopulateImageListWithImageDirectoryAndRemeasure;
        }

        void ForcePopulateImageListWithImageDirectoryAndRemeasure(object sender, RoutedEventArgs e)
        {
            UpdateImageListWithDirectory();
            UpdateImageGrid(this.RenderSize);
        }

        public string ImageDirectory
        {
            get
            {
                return (string)GetValue(ImageDirectoryProperty);
            }
            set
            {
                SetValue(ImageDirectoryProperty, value);
                UpdateImageListWithDirectory();
            }
        }
        private void UpdateImageListWithDirectory()
        {
            List<String> filenames = new List<String>();
            List<String> possibleFileExtensions = new List<String> { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp" };
            foreach (String pattern in possibleFileExtensions)
                filenames.AddRange(Directory.GetFiles(ImageDirectory, pattern));

            List<String> newImageList = new List<String>();
            foreach (String imageUri in filenames)
            {
                newImageList.Add(imageUri);
            }

            newImageList = Shuffle(new Random(), newImageList);

            ImageList = newImageList;
        }


        public static readonly DependencyProperty ImageDirectoryProperty = DependencyProperty.Register(
            "ImageDirectory", typeof(string), typeof(FlipImageGrid), new PropertyMetadata(String.Empty));

        public List<String> ImageList
        {
            get
            {
                return (List<String>)GetValue(ImageListProperty);
            }
            set
            {
                if (!Enumerable.SequenceEqual<String>(ImageList.OrderBy(q => q), value.OrderBy(q => q)))
                {
                    SetValue(ImageListProperty, value);
                    foreach (FlipImage flipImage in ImageGrid.Children)
                    {
                        flipImage.StopTimer();
                        flipImage.ChangeImage();
                        flipImage.StartTimer();
                    }
                }
            }
        }
        public static readonly DependencyProperty ImageListProperty = DependencyProperty.Register(
            "ImageList", typeof(List<String>), typeof(FlipImageGrid), new PropertyMetadata(new List<String>()));

        private List<String> Shuffle(Random rng, List<String> list)
        {
            String[] array = list.ToArray<String>();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                String temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
            return array.ToList<String>();
        }

        public Boolean? OneImageBool
        {
            get
            {
                return (Boolean?)GetValue(OneImageBoolProperty);
            }
            set
            {
                // Program does not actually go here, everything refreshed after changing settings is done by Loaded event.
                SetValue(OneImageBoolProperty, value);
                UpdateImageGrid(this.RenderSize);
            }
        }
        public static readonly DependencyProperty OneImageBoolProperty = DependencyProperty.Register(
            "OneImageBool", typeof(Boolean?), typeof(FlipImageGrid), new PropertyMetadata(false));

        private void ThisFlipImageGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => UpdateImageGrid(e.NewSize)));
        }
        private Stack<FlipImage> flipImageStorage = new Stack<FlipImage>();
        private void UpdateImageGrid(Size newSize)
        {
            GridLength sideLength;
            int numCols = 0, numRows = 0;
            if ((bool)OneImageBool)
            {
                sideLength = new GridLength(Math.Min(newSize.Width, newSize.Height));
                numCols = 1;
                numRows = 1;
            }
            else
            {
                double targetSideLength = 225;
                numCols = (int)(newSize.Width / targetSideLength);
                if (newSize.Width % targetSideLength > (targetSideLength / 2) || numCols == 0)
                {
                    numCols += 1;
                }
                numRows = (int)(newSize.Height / targetSideLength);
                if (newSize.Height % targetSideLength > (targetSideLength / 2) || numRows == 0)
                {
                    numRows += 1;
                }
                double sideLengthDouble = (double)Math.Min(newSize.Width / numCols, newSize.Height / numRows);
                sideLength = new GridLength(sideLengthDouble);
            }

            // Make sure ImageGrid has the right number of FlipImage children.
            if (numCols * numRows == ImageGrid.Children.Count)
            {
                // This will effect all row and column definitions in the grid.
                ImageGrid.RowDefinitions.FirstOrDefault().Height = sideLength;
                return;
            }
            if (numCols * numRows > ImageGrid.Children.Count)
            {
                while (numCols * numRows > ImageGrid.Children.Count)
                {
                    if (flipImageStorage.Count > 0)
                    {
                        FlipImage addMe = flipImageStorage.Pop();
                        addMe.StartTimer();
                        ImageGrid.Children.Add(addMe);
                        continue;
                    }

                    FlipImage flipImage = new FlipImage();

                    // Force the FlipImages to do initial flip if they're new
                    flipImage.ImageList = this.ImageList;

                    flipImage.SetResourceReference(FlipImage.TransitionTypeProperty, "TransitionType");
                    
                    Binding binding = new Binding
                    {
                        Path = new PropertyPath("ImageList"),
                        RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(FlipImageGrid), 1)
                    };
                    flipImage.SetBinding(FlipImage.ImageListProperty, binding);

                    ImageGrid.Children.Add(flipImage);
                }
            }
            if (numCols * numRows < ImageGrid.Children.Count)
            {
                while (numCols * numRows < ImageGrid.Children.Count)
                {
                    FlipImage toStore = ImageGrid.Children[ImageGrid.Children.Count - 1] as FlipImage;
                    ImageGrid.Children.RemoveAt(ImageGrid.Children.Count - 1);
                    toStore.StopTimer();
                    flipImageStorage.Push(toStore);
                }
            }

            // Ensure proper number of ColumnDefinitions and RowDefinitions.
            ColumnDefinitionCollection colDefs = ImageGrid.ColumnDefinitions;
            RowDefinitionCollection rowDefs = ImageGrid.RowDefinitions;
            colDefs.Clear(); rowDefs.Clear();
            for (int x = 0; x < numCols; x++)
                for (int y = 0; y < numRows; y++)
                {
                    if (y == 0) colDefs.Add(new ColumnDefinition() { Width = sideLength, SharedSizeGroup = "FlipImageSharedSizeGroup" });
                    if (x == 0) rowDefs.Add(new RowDefinition() { Height = sideLength, SharedSizeGroup = "FlipImageSharedSizeGroup" });
                }

            // Set grid coordinates of each child in ImageGrid
            int count = 0;
            foreach (FlipImage flipImage in ImageGrid.Children)
            {
                Grid.SetColumn(flipImage, count % numCols);
                Grid.SetRow(flipImage, count / numCols);
                count++;
            }
        }
    }
}
