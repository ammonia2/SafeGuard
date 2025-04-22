using System;
using System.Collections.Generic; // Needed for List
using System.Drawing;
using System.IO;
using System.Linq; // Needed for Any()
using System.Windows.Forms;

namespace SafeGuard
{
    public partial class ImageViewerForm : Form
    {
        private readonly List<string> _imagePaths; // Changed from single path
        private int _currentIndex;
        private readonly string _baseTitle; // Store the original title part

        // Constructor accepts a LIST of paths and title
        public ImageViewerForm(List<string> imagePaths, string windowTitle)
        {
            InitializeComponent();

            if (imagePaths == null || !imagePaths.Any())
            {
                // Handle cases with no images passed
                MessageBox.Show("No valid image paths were provided to the viewer.", "Viewer Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _imagePaths = new List<string>(); // Ensure list is not null
                _baseTitle = windowTitle ?? "Image Viewer";
                // Close(); // Or disable controls if preferred
            }
            else
            {
                _imagePaths = imagePaths;
                _baseTitle = windowTitle ?? "Image Viewer";
            }
            _currentIndex = 0; // Start at the first image
        }

        private void ImageViewerForm_Load(object sender, EventArgs e)
        {
            if (_imagePaths.Any())
            {
                LoadImageAtIndex(_currentIndex);
            }
            else
            {
                // No images, disable everything or close
                pictureBoxDisplay.Image = null;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                this.Text = _baseTitle + " (No images)";
            }
        }

        private void LoadImageAtIndex(int index)
        {
            if (index < 0 || index >= _imagePaths.Count)
            {
                // Index out of bounds, shouldn't normally happen if buttons are managed correctly
                return;
            }

            string imagePath = _imagePaths[index];
            Image loadedImage = null; // Temporary variable

            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                // File missing for this specific index
                MessageBox.Show($"Image file not found or path is invalid:\n{imagePath}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBoxDisplay.Image?.Dispose(); // Dispose previous image if any
                pictureBoxDisplay.Image = null; // Clear display
                // Optionally: Display a placeholder "File Not Found" image
            }
            else
            {
                try
                {
                    // Load image into memory stream to avoid locking file
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    using (var ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0; // Reset stream position
                        loadedImage = Image.FromStream(ms); // Load from memory
                    }

                    // Dispose the old image BEFORE assigning the new one
                    pictureBoxDisplay.Image?.Dispose();
                    pictureBoxDisplay.Image = loadedImage; // Assign the new image loaded from memory
                }
                catch (OutOfMemoryException) // Common for invalid formats
                {
                    MessageBox.Show($"Failed to load image. File may be corrupt or not a supported format:\n{imagePath}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pictureBoxDisplay.Image?.Dispose();
                    pictureBoxDisplay.Image = null;
                    loadedImage?.Dispose(); // Ensure disposal if assignment failed
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred loading image:\n{imagePath}\n\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pictureBoxDisplay.Image?.Dispose();
                    pictureBoxDisplay.Image = null;
                    loadedImage?.Dispose(); // Ensure disposal on other errors
                }
            }

            _currentIndex = index; // Update current index *after* trying to load
            UpdateNavigation(); // Update buttons and title
        }

        private void UpdateNavigation()
        {
            // Enable/Disable Buttons
            btnPrevious.Enabled = (_currentIndex > 0);
            btnNext.Enabled = (_currentIndex < _imagePaths.Count - 1);

            // Update Title
            if (_imagePaths.Any() && _currentIndex >= 0 && _currentIndex < _imagePaths.Count)
            {
                this.Text = $"{_baseTitle} ({_currentIndex + 1}/{_imagePaths.Count}) - {Path.GetFileName(_imagePaths[_currentIndex])}";
            }
            else if (_imagePaths.Any()) // Handle case where index might be invalid temporarily
            {
                this.Text = $"{_baseTitle} ({_currentIndex + 1}/{_imagePaths.Count})";
            }
            else
            {
                this.Text = _baseTitle + " (No images)";
            }
        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                LoadImageAtIndex(_currentIndex - 1);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _imagePaths.Count - 1)
            {
                LoadImageAtIndex(_currentIndex + 1);
            }
        }

        // Ensure the current image is disposed when form closes
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            pictureBoxDisplay?.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}