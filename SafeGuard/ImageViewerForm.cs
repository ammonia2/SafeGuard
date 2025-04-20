using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SafeGuard
{
    public partial class ImageViewerForm : Form
    {
        private string _imagePath;

        // Constructor accepts the path and title
        public ImageViewerForm(string imagePath, string windowTitle)
        {
            InitializeComponent();
            _imagePath = imagePath;
            this.Text = windowTitle; // Set the window title
        }

        private void ImageViewerForm_Load(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void LoadImage()
        {
            if (string.IsNullOrEmpty(_imagePath) || !File.Exists(_imagePath))
            {
                MessageBox.Show($"Image file not found:\n{_imagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBoxDisplay.Image = null; // Clear image
                this.Close(); // Close the viewer if the file isn't there
                return;
            }

            try
            {
                // Load the image into a temporary bitmap to release the file lock immediately
                using (var bmpTemp = new Bitmap(_imagePath))
                {
                    // Create a new Bitmap object from the temporary one for the PictureBox
                    // This ensures the PictureBox has its own copy and doesn't lock the file
                    pictureBoxDisplay.Image = new Bitmap(bmpTemp);
                }
            }
            catch (OutOfMemoryException) // Specific exception for invalid image formats
            {
                MessageBox.Show($"Failed to load image. The file may be corrupt or not a valid image format:\n{_imagePath}", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pictureBoxDisplay.Image = null;
                this.Close();
            }
            catch (Exception ex) // Catch other potential exceptions (IO, etc.)
            {
                MessageBox.Show($"An error occurred while loading the image:\n{_imagePath}\n\n{ex.Message}", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBoxDisplay.Image = null;
                this.Close();
            }
        }

        // Optional: Ensure the image is disposed when the form closes
        // Although PictureBox usually handles this, being explicit is safe.
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            pictureBoxDisplay?.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}