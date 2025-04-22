// --- START OF UPDATED Form1.Designer.cs ---

using System.Data.SqlClient;
namespace SafeGuard
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 

        private Panel panelDropPasteTarget;
        private Label lblDropPasteHint;

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            linkLabel6 = new LinkLabel();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            headingNav = new Label();
            panelContent = new Panel();
            viewAllFilesButton = new Button();
            label13 = new Label();
            flowLayoutPanelRecentFiles = new FlowLayoutPanel();
            label3 = new Label();
            btnUpload = new Button();
            label2 = new Label();
            label1 = new Label();
            panelFileManagement = new Panel();
            label15 = new Label();
            cmbTables = new ComboBox();
            dataGridViewFiles = new DataGridView();
            label14 = new Label();
            panelEncryptionSettings = new Panel();
            encryptButton = new Button();
            label8 = new Label();
            //fileSelectionEncryption = new ComboBox();
            encryptionKeyBox = new TextBox();
            label7 = new Label();
            label = new Label();
            encryptionMethodSelection = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            decryptionPanel = new Panel();
            decryptButton = new Button();
            label6 = new Label();
            //decryptionFileSelection = new ComboBox();
            decryptionKey = new TextBox();
            label9 = new Label();
            label10 = new Label();
            decryptionMethodSelection = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            compressionPanel = new Panel();
            compressButton = new Button();
            //fileSelectionCompression = new ComboBox();
            compressionFileLabel = new Label();
            compressionDescLabel = new Label();
            compressionTitleLabel = new Label();
            decompressionPanel = new Panel();
            decompressButton = new Button();
            //fileSelectionDecompression = new ComboBox();
            decompressionFileLabel = new Label();
            decompressionDescLabel = new Label();
            decompressionTitleLabel = new Label();
            lblDropPasteHint = new Label();
            panelDropPasteTarget = new Panel();

            checkedListBoxEncrypt = new CheckedListBox();
            checkedListBoxDecrypt = new CheckedListBox();
            checkedListBoxCompress = new CheckedListBox();
            checkedListBoxDecompress = new CheckedListBox();
            txtEncryptSelection = new TextBox();
            btnEncryptDropdown = new Button();
            txtDecryptSelection = new TextBox();
            btnDecryptDropdown = new Button();
            txtCompressSelection = new TextBox();
            btnCompressDropdown = new Button();
            txtDecompressSelection = new TextBox();
            btnDecompressDropdown = new Button();

            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelFileManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).BeginInit();
            panelEncryptionSettings.SuspendLayout();
            decryptionPanel.SuspendLayout();
            compressionPanel.SuspendLayout();
            decompressionPanel.SuspendLayout();
            SuspendLayout();

            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.LightGray;
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(linkLabel6);
            panelHeader.Controls.Add(linkLabel5);
            panelHeader.Controls.Add(linkLabel4);
            panelHeader.Controls.Add(linkLabel3);
            panelHeader.Controls.Add(linkLabel2);
            panelHeader.Controls.Add(linkLabel1);
            panelHeader.Controls.Add(headingNav);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(986, 62);
            panelHeader.TabIndex = 0;
            //
            //
            // linkLabel6
            // 
            linkLabel6.AutoSize = true;
            linkLabel6.Cursor = Cursors.Hand;
            linkLabel6.LinkColor = Color.Black;
            linkLabel6.Location = new Point(770, 22);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(112, 20);
            linkLabel6.TabIndex = 8;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "Decompression";
            linkLabel6.LinkClicked += linkLabel6_LinkClicked;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Cursor = Cursors.Hand;
            linkLabel5.LinkColor = Color.Black;
            linkLabel5.Location = new Point(663, 22);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(95, 20);
            linkLabel5.TabIndex = 7;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "Compression";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.LinkColor = Color.Black;
            linkLabel4.Location = new Point(889, 22);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(38, 20);
            linkLabel4.TabIndex = 6;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Files";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Cursor = Cursors.Hand;
            linkLabel3.LinkColor = Color.Black;
            linkLabel3.Location = new Point(468, 22);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(79, 20);
            linkLabel3.TabIndex = 3;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Encryption";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Cursor = Cursors.Hand;
            linkLabel2.LinkColor = Color.Black;
            linkLabel2.Location = new Point(568, 22);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(82, 20);
            linkLabel2.TabIndex = 2;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Decryption";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Cursor = Cursors.Hand;
            linkLabel1.LinkColor = Color.Black;
            linkLabel1.Location = new Point(401, 22);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(50, 20);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Home";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // headingNav
            // 
            headingNav.AutoSize = true;
            headingNav.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            headingNav.Location = new Point(49, 12);
            headingNav.Name = "headingNav";
            headingNav.Size = new Size(132, 32);
            headingNav.TabIndex = 0;
            headingNav.Text = "SafeGuard";
            // 
            // panelContent
            // 
            panelContent.Controls.Add(viewAllFilesButton);
            panelContent.Controls.Add(label13);
            panelContent.Controls.Add(flowLayoutPanelRecentFiles);
            panelContent.Controls.Add(label3);
            panelContent.Controls.Add(btnUpload);
            panelContent.Controls.Add(label2);
            panelContent.Controls.Add(label1);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 62);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(986, 647);
            panelContent.TabIndex = 1;

            panelContent.Controls.Add(this.panelDropPasteTarget); // Add the new panel HERE
            panelContent.Controls.Add(viewAllFilesButton);
            panelContent.Controls.Add(label13);
            panelContent.Controls.Add(flowLayoutPanelRecentFiles);
            panelContent.Controls.Add(label3);
            panelContent.Controls.Add(btnUpload);
            panelContent.Controls.Add(label2);
            panelContent.Controls.Add(label1);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 62);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(986, 647);
            panelContent.TabIndex = 1;
            // 
            // viewAllFilesButton
            // 
            viewAllFilesButton.BackColor = Color.Black;
            viewAllFilesButton.Font = new Font("Segoe UI", 12F);
            viewAllFilesButton.ForeColor = Color.White;
            viewAllFilesButton.Location = new Point(425, 560);
            viewAllFilesButton.Margin = new Padding(3, 4, 3, 4);
            viewAllFilesButton.Name = "viewAllFilesButton";
            viewAllFilesButton.Size = new Size(126, 49);
            viewAllFilesButton.TabIndex = 6;
            viewAllFilesButton.Text = "View All";
            viewAllFilesButton.UseVisualStyleBackColor = false;
            viewAllFilesButton.Click += viewAllFilesButton_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(385, 219);
            label13.Name = "label13";
            label13.Size = new Size(219, 35);
            label13.TabIndex = 5;
            label13.Text = "File Management";
            // 
            // flowLayoutPanelRecentFiles
            // 
            flowLayoutPanelRecentFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            flowLayoutPanelRecentFiles.AutoScroll = true;
            flowLayoutPanelRecentFiles.BackColor = SystemColors.Control;
            flowLayoutPanelRecentFiles.Location = new Point(245, 428);
            flowLayoutPanelRecentFiles.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelRecentFiles.Name = "flowLayoutPanelRecentFiles";
            flowLayoutPanelRecentFiles.Size = new Size(482, 111);
            flowLayoutPanelRecentFiles.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(421, 367);
            label3.Name = "label3";
            label3.Size = new Size(154, 35);
            label3.TabIndex = 3;
            label3.Text = "Recent Files";
            // 
            // btnUpload
            // 
            btnUpload.BackColor = Color.Black;
            btnUpload.Font = new Font("Segoe UI", 12F);
            btnUpload.ForeColor = Color.White;
            btnUpload.Location = new Point(425, 270);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(126, 49);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;

            panelDropPasteTarget.AllowDrop = true;
            panelDropPasteTarget.BackColor = SystemColors.ControlLight;
            panelDropPasteTarget.BorderStyle = BorderStyle.FixedSingle;
            panelDropPasteTarget.Controls.Add(this.lblDropPasteHint); // Add label inside
            panelDropPasteTarget.Location = new Point(363, 330); // Position below btnUpload (adjust X, Y)
            panelDropPasteTarget.Margin = new Padding(3, 4, 3, 4);
            panelDropPasteTarget.Name = "panelDropPasteTarget";
            panelDropPasteTarget.Size = new Size(256, 70); // Adjust Width, Height
            panelDropPasteTarget.TabIndex = 7; // Adjust TabIndex if needed
            // Add event handlers for Drag/Drop
            panelDropPasteTarget.DragEnter += new DragEventHandler(panelDropPasteTarget_DragEnter);
            panelDropPasteTarget.DragDrop += new DragEventHandler(panelDropPasteTarget_DragDrop);

            //
            // lblDropPasteHint (Inside panelDropPasteTarget)
            //
            lblDropPasteHint.Dock = DockStyle.Fill; // Fill the parent panel
            lblDropPasteHint.ForeColor = SystemColors.ControlDarkDark;
            lblDropPasteHint.Location = new Point(0, 0);
            lblDropPasteHint.Name = "lblDropPasteHint";
            lblDropPasteHint.Size = new Size(254, 68); // Slightly smaller than parent
            lblDropPasteHint.TabIndex = 0;
            lblDropPasteHint.Text = "or drag && drop / paste image from clipboard here";
            lblDropPasteHint.TextAlign = ContentAlignment.MiddleCenter;
            // Make label passive for drag/drop events - parent panel handles them
            lblDropPasteHint.Enabled = false;


            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(363, 123);
            label2.Name = "label2";
            label2.Size = new Size(256, 28);
            label2.TabIndex = 1;
            label2.Text = "Manage your files efficiently";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(258, 57);
            label1.Name = "label1";
            label1.Size = new Size(504, 41);
            label1.TabIndex = 0;
            label1.Text = "Welcome To SafeGuard Dashboard";
            // 
            // panelFileManagement
            // 
            panelFileManagement.Controls.Add(label15);
            panelFileManagement.Controls.Add(cmbTables);
            panelFileManagement.Controls.Add(dataGridViewFiles);
            panelFileManagement.Controls.Add(label14);
            panelFileManagement.Dock = DockStyle.Fill;
            panelFileManagement.Location = new Point(0, 62);
            panelFileManagement.Margin = new Padding(3, 4, 3, 4);
            panelFileManagement.Name = "panelFileManagement";
            panelFileManagement.Size = new Size(986, 647);
            panelFileManagement.TabIndex = 7;
            panelFileManagement.Visible = false;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(434, 133);
            label15.Name = "label15";
            label15.Size = new Size(159, 28);
            label15.TabIndex = 4;
            label15.Text = "Select File Type";
            // 
            // cmbTables
            // 
            cmbTables.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTables.FormattingEnabled = true;
            cmbTables.Location = new Point(438, 184);
            cmbTables.Margin = new Padding(3, 4, 3, 4);
            cmbTables.Name = "cmbTables";
            cmbTables.Size = new Size(138, 28);
            cmbTables.TabIndex = 3;
            // 
            // dataGridViewFiles
            // 
            dataGridViewFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFiles.Location = new Point(198, 256);
            dataGridViewFiles.Margin = new Padding(3, 4, 3, 4);
            dataGridViewFiles.Name = "dataGridViewFiles";
            dataGridViewFiles.ReadOnly = true;
            dataGridViewFiles.RowHeadersWidth = 51;
            dataGridViewFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Optional but nice
            dataGridViewFiles.Size = new Size(626, 348);
            dataGridViewFiles.TabIndex = 2;
            dataGridViewFiles.CellDoubleClick += new DataGridViewCellEventHandler(dataGridViewFiles_CellDoubleClick);
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(385, 57);
            label14.Name = "label14";
            label14.Size = new Size(263, 41);
            label14.TabIndex = 1;
            label14.Text = "File Management";
            // 
            // panelEncryptionSettings
            //
            panelEncryptionSettings.Controls.Add(this.txtEncryptSelection);      // ADD TextBox
            panelEncryptionSettings.Controls.Add(this.btnEncryptDropdown);       // ADD Button

            panelEncryptionSettings.Controls.Add(this.checkedListBoxEncrypt);

            panelEncryptionSettings.BorderStyle = BorderStyle.Fixed3D;
            panelEncryptionSettings.Controls.Add(encryptButton);
            panelEncryptionSettings.Controls.Add(label8);
            panelEncryptionSettings.Controls.Add(fileSelectionEncryption);
            panelEncryptionSettings.Controls.Add(encryptionKeyBox);
            panelEncryptionSettings.Controls.Add(label7);
            panelEncryptionSettings.Controls.Add(label);
            panelEncryptionSettings.Controls.Add(encryptionMethodSelection);
            panelEncryptionSettings.Controls.Add(label5);
            panelEncryptionSettings.Controls.Add(label4);
            panelEncryptionSettings.Dock = DockStyle.Fill;
            panelEncryptionSettings.Location = new Point(0, 62);
            panelEncryptionSettings.Margin = new Padding(3, 4, 3, 4);
            panelEncryptionSettings.Name = "panelEncryptionSettings";
            panelEncryptionSettings.Size = new Size(986, 647);
            panelEncryptionSettings.TabIndex = 5;
            panelEncryptionSettings.Visible = false;
            //
            // txtEncryptSelection (Where the ComboBox/Old ListBox was)
            //
            txtEncryptSelection.Location = new Point(207, 312); // Original position
            txtEncryptSelection.Name = "txtEncryptSelection";
            txtEncryptSelection.ReadOnly = true; // Display only
            txtEncryptSelection.Size = new Size(120, 27); // Adjust width (leave space for button)
            txtEncryptSelection.TabIndex = 7; // Original TabIndex
            txtEncryptSelection.Text = "No selection"; // Initial text
            txtEncryptSelection.TabStop = false; // Don't tab into read-only text
            //
            //
            // btnEncryptDropdown (Next to the TextBox)
            //
            btnEncryptDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEncryptDropdown.Location = new Point(329, 311); // Position next to textbox (adjust X)
            btnEncryptDropdown.Name = "btnEncryptDropdown";
            btnEncryptDropdown.Size = new Size(30, 29); // Small button size
            btnEncryptDropdown.TabIndex = 10; // New TabIndex
            btnEncryptDropdown.Text = "▼"; // Down arrow symbol
            btnEncryptDropdown.UseVisualStyleBackColor = true;
            btnEncryptDropdown.Click += new EventHandler(this.btnDropdown_Click);
            //
            // checkedListBoxEncrypt (Position BELOW TextBox/Button, initially hidden)
            //
            checkedListBoxEncrypt.CheckOnClick = true;
            checkedListBoxEncrypt.FormattingEnabled = true;
            checkedListBoxEncrypt.Location = new Point(207, 342); // Position BELOW the textbox
            checkedListBoxEncrypt.Name = "checkedListBoxEncrypt";
            checkedListBoxEncrypt.Size = new Size(152, 90); // Adjust size as needed for dropdown items
            checkedListBoxEncrypt.TabIndex = 11; // New TabIndex
            checkedListBoxEncrypt.Visible = false; // <<< INITIALLY HIDDEN
            checkedListBoxEncrypt.ItemCheck += new ItemCheckEventHandler(this.checkedListBox_ItemCheck); // Keep selection limit handler
            checkedListBoxEncrypt.Leave += new EventHandler(this.checkedListBox_Leave); // Add Leave handler
            //
            // encryptButton
            // 
            encryptButton.BackColor = SystemColors.ActiveCaptionText;
            encryptButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            encryptButton.ForeColor = Color.Snow;
            encryptButton.Location = new Point(453, 383);
            encryptButton.Margin = new Padding(3, 4, 3, 4);
            encryptButton.Name = "encryptButton";
            encryptButton.Size = new Size(109, 55);
            encryptButton.TabIndex = 9;
            encryptButton.Text = "Encrypt";
            encryptButton.UseVisualStyleBackColor = false;
            encryptButton.Click += encryptButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(229, 253);
            label8.Name = "label8";
            label8.Size = new Size(108, 28);
            label8.TabIndex = 8;
            label8.Text = "Select File";
            // 
            // fileSelectionEncryption
            // 
            //fileSelectionEncryption.DropDownStyle = ComboBoxStyle.DropDownList;
            //fileSelectionEncryption.FormattingEnabled = true;
            //fileSelectionEncryption.Location = new Point(207, 312);
           // fileSelectionEncryption.Margin = new Padding(3, 4, 3, 4);
            //fileSelectionEncryption.Name = "fileSelectionEncryption";
           // fileSelectionEncryption.Size = new Size(138, 28);
            //fileSelectionEncryption.TabIndex = 7;
            // 
            // encryptionKeyBox
            // 
            encryptionKeyBox.Location = new Point(655, 311);
            encryptionKeyBox.Margin = new Padding(3, 4, 3, 4);
            encryptionKeyBox.Name = "encryptionKeyBox";
            encryptionKeyBox.Size = new Size(114, 27);
            encryptionKeyBox.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(647, 253);
            label7.Name = "label7";
            label7.Size = new Size(156, 28);
            label7.TabIndex = 5;
            label7.Text = "Encryption Key\r\n";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.Location = new Point(418, 253);
            label.Name = "label";
            label.Size = new Size(195, 28);
            label.TabIndex = 3;
            label.Text = "Encryption Method";
            // 
            // encryptionMethodSelection
            // 
            encryptionMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            encryptionMethodSelection.FormattingEnabled = true;
            encryptionMethodSelection.Items.AddRange(new object[] { "AES", "Pixel Scrambling" });
            encryptionMethodSelection.Location = new Point(435, 311);
            encryptionMethodSelection.Margin = new Padding(3, 4, 3, 4);
            encryptionMethodSelection.Name = "encryptionMethodSelection";
            encryptionMethodSelection.Size = new Size(138, 28);
            encryptionMethodSelection.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(327, 123);
            label5.Name = "label5";
            label5.Size = new Size(346, 28);
            label5.TabIndex = 1;
            label5.Text = "Select Encryption Method and Set Key";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(397, 57);
            label4.Name = "label4";
            label4.Size = new Size(229, 41);
            label4.TabIndex = 0;
            label4.Text = "File Encryption";
            // 
            // decryptionPanel
            // 
            decryptionPanel.Controls.Add(this.txtDecryptSelection);
            decryptionPanel.Controls.Add(this.btnDecryptDropdown);
            decryptionPanel.Controls.Add(this.checkedListBoxDecrypt); // ADD

            decryptionPanel.BorderStyle = BorderStyle.Fixed3D;
            decryptionPanel.Controls.Add(decryptButton);
            decryptionPanel.Controls.Add(label6);
            decryptionPanel.Controls.Add(decryptionFileSelection);
            decryptionPanel.Controls.Add(decryptionKey);
            decryptionPanel.Controls.Add(label9);
            decryptionPanel.Controls.Add(label10);
            decryptionPanel.Controls.Add(decryptionMethodSelection);
            decryptionPanel.Controls.Add(label11);
            decryptionPanel.Controls.Add(label12);
            decryptionPanel.Dock = DockStyle.Fill;
            decryptionPanel.Location = new Point(0, 62);
            decryptionPanel.Margin = new Padding(3, 4, 3, 4);
            decryptionPanel.Name = "decryptionPanel";
            decryptionPanel.Size = new Size(986, 647);
            decryptionPanel.TabIndex = 6;
            decryptionPanel.Visible = false;
            // txtDecryptSelection setup... (Location: 207, 312 etc.)
            txtDecryptSelection.Location = new Point(207, 312);
            txtDecryptSelection.Name = "txtDecryptSelection";
            txtDecryptSelection.ReadOnly = true;
            txtDecryptSelection.Size = new Size(120, 27);
            txtDecryptSelection.TabIndex = 7;
            txtDecryptSelection.Text = "No selection";
            txtDecryptSelection.TabStop = false;
            // btnDecryptDropdown setup... (Location: 329, 311 etc.)
            btnDecryptDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDecryptDropdown.Location = new Point(329, 311);
            btnDecryptDropdown.Name = "btnDecryptDropdown";
            btnDecryptDropdown.Size = new Size(30, 29);
            btnDecryptDropdown.TabIndex = 10;
            btnDecryptDropdown.Text = "▼";
            btnDecryptDropdown.UseVisualStyleBackColor = true;
            btnDecryptDropdown.Click += new EventHandler(this.btnDropdown_Click); // SHARED
                                                                                  // checkedListBoxDecrypt setup... (Location: 207, 342 etc., Visible: false)
            checkedListBoxDecrypt.CheckOnClick = true;
            checkedListBoxDecrypt.FormattingEnabled = true;
            checkedListBoxDecrypt.Location = new Point(207, 342);
            checkedListBoxDecrypt.Name = "checkedListBoxDecrypt";
            checkedListBoxDecrypt.Size = new Size(152, 90);
            checkedListBoxDecrypt.TabIndex = 11;
            checkedListBoxDecrypt.Visible = false; // Initially hidden
            checkedListBoxDecrypt.ItemCheck += new ItemCheckEventHandler(this.checkedListBox_ItemCheck); // Keep limit handler
            checkedListBoxDecrypt.Leave += new EventHandler(this.checkedListBox_Leave); // Add Leave handler
            // 
            // decryptButton
            // 
            decryptButton.BackColor = SystemColors.ActiveCaptionText;
            decryptButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            decryptButton.ForeColor = Color.Snow;
            decryptButton.Location = new Point(453, 383);
            decryptButton.Margin = new Padding(3, 4, 3, 4);
            decryptButton.Name = "decryptButton";
            decryptButton.Size = new Size(109, 55);
            decryptButton.TabIndex = 9;
            decryptButton.Text = "Decrypt";
            decryptButton.UseVisualStyleBackColor = false;
            decryptButton.Click += decryptButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(229, 253);
            label6.Name = "label6";
            label6.Size = new Size(108, 28);
            label6.TabIndex = 8;
            label6.Text = "Select File";
            // 
            // decryptionFileSelection
            // 
            //decryptionFileSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            //decryptionFileSelection.FormattingEnabled = true;
            //decryptionFileSelection.Location = new Point(207, 312);
            //decryptionFileSelection.Margin = new Padding(3, 4, 3, 4);
            //decryptionFileSelection.Name = "decryptionFileSelection";
            //decryptionFileSelection.Size = new Size(138, 28);
            //decryptionFileSelection.TabIndex = 7;
            //// 
            // decryptionKey
            // 
            decryptionKey.Location = new Point(655, 311);
            decryptionKey.Margin = new Padding(3, 4, 3, 4);
            decryptionKey.Name = "decryptionKey";
            decryptionKey.Size = new Size(114, 27);
            decryptionKey.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(647, 253);
            label9.Name = "label9";
            label9.Size = new Size(159, 28);
            label9.TabIndex = 5;
            label9.Text = "Decryption Key\r\n";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(418, 253);
            label10.Name = "label10";
            label10.Size = new Size(198, 28);
            label10.TabIndex = 3;
            label10.Text = "Decryption Method";
            // 
            // decryptionMethodSelection
            // 
            decryptionMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            decryptionMethodSelection.FormattingEnabled = true;
            decryptionMethodSelection.Location = new Point(435, 311);
            decryptionMethodSelection.Margin = new Padding(3, 4, 3, 4);
            decryptionMethodSelection.Name = "decryptionMethodSelection";
            decryptionMethodSelection.Size = new Size(138, 28);
            decryptionMethodSelection.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.Location = new Point(327, 123);
            label11.Name = "label11";
            label11.Size = new Size(388, 28);
            label11.TabIndex = 1;
            label11.Text = "Select Decryption Method and Provide Key";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(397, 57);
            label12.Name = "label12";
            label12.Size = new Size(233, 41);
            label12.TabIndex = 0;
            label12.Text = "File Decryption";
            // 
            // compressionPanel
            //
            compressionPanel.Controls.Add(this.txtCompressSelection);
            compressionPanel.Controls.Add(this.btnCompressDropdown);
            compressionPanel.Controls.Add(this.checkedListBoxCompress); // ADD

            compressionPanel.BorderStyle = BorderStyle.Fixed3D;
            compressionPanel.Controls.Add(compressButton);
            compressionPanel.Controls.Add(fileSelectionCompression);
            compressionPanel.Controls.Add(compressionFileLabel);
            compressionPanel.Controls.Add(compressionDescLabel);
            compressionPanel.Controls.Add(compressionTitleLabel);
            compressionPanel.Dock = DockStyle.Fill;
            compressionPanel.Location = new Point(0, 62);
            compressionPanel.Margin = new Padding(3, 4, 3, 4);
            compressionPanel.Name = "compressionPanel";
            compressionPanel.Size = new Size(986, 647);
            compressionPanel.TabIndex = 8;
            compressionPanel.Visible = false;
            // txtCompressSelection setup... (Location: 425, 312 etc.)
            txtCompressSelection.Location = new Point(425, 312);
            txtCompressSelection.Name = "txtCompressSelection";
            txtCompressSelection.ReadOnly = true;
            txtCompressSelection.Size = new Size(120, 27); // Adjust size as needed
            txtCompressSelection.TabIndex = 3; // Original TabIndex
            txtCompressSelection.Text = "No selection";
            txtCompressSelection.TabStop = false;
            // btnCompressDropdown setup... (Location: 547, 311 etc.)
            btnCompressDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCompressDropdown.Location = new Point(547, 311); // Position next to textbox
            btnCompressDropdown.Name = "btnCompressDropdown";
            btnCompressDropdown.Size = new Size(30, 29);
            btnCompressDropdown.TabIndex = 5; // Adjust TabIndex
            btnCompressDropdown.Text = "▼";
            btnCompressDropdown.UseVisualStyleBackColor = true;
            btnCompressDropdown.Click += new EventHandler(this.btnDropdown_Click); // SHARED
                                                                                   // checkedListBoxCompress setup... (Location: 425, 342 etc., Visible: false)
            checkedListBoxCompress.CheckOnClick = true;
            checkedListBoxCompress.FormattingEnabled = true;
            checkedListBoxCompress.Location = new Point(425, 342); // Position BELOW the textbox
            checkedListBoxCompress.Name = "checkedListBoxCompress";
            checkedListBoxCompress.Size = new Size(152, 90); // Adjust size
            checkedListBoxCompress.TabIndex = 6; // Adjust TabIndex
            checkedListBoxCompress.Visible = false; // Initially hidden
            checkedListBoxCompress.ItemCheck += new ItemCheckEventHandler(this.checkedListBox_ItemCheck); // Keep limit handler
            checkedListBoxCompress.Leave += new EventHandler(this.checkedListBox_Leave); // Add Leave handler
            //
            // compressButton
            // 
            compressButton.BackColor = SystemColors.ActiveCaptionText;
            compressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressButton.ForeColor = Color.Snow;
            compressButton.Location = new Point(435, 383);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(129, 51);
            compressButton.TabIndex = 4;
            compressButton.Text = "Optimize";
            compressButton.UseVisualStyleBackColor = false;
            compressButton.Click += compressButton_Click;
            // 
            // fileSelectionCompression
            // 
            //fileSelectionCompression.DropDownStyle = ComboBoxStyle.DropDownList;
            //fileSelectionCompression.FormattingEnabled = true;
            //fileSelectionCompression.Location = new Point(425, 312);
            //fileSelectionCompression.Name = "fileSelectionCompression";
            //fileSelectionCompression.Size = new Size(150, 28);
            //fileSelectionCompression.TabIndex = 3;
            // 
            // compressionFileLabel
            // 
            compressionFileLabel.AutoSize = true;
            compressionFileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressionFileLabel.Location = new Point(443, 253);
            compressionFileLabel.Name = "compressionFileLabel";
            compressionFileLabel.Size = new Size(108, 28);
            compressionFileLabel.TabIndex = 2;
            compressionFileLabel.Text = "Select File";
            // 
            // compressionDescLabel
            // 
            compressionDescLabel.AutoSize = true;
            compressionDescLabel.Font = new Font("Segoe UI", 12F);
            compressionDescLabel.Location = new Point(317, 123);
            compressionDescLabel.Name = "compressionDescLabel";
            compressionDescLabel.Size = new Size(382, 28);
            compressionDescLabel.TabIndex = 1;
            compressionDescLabel.Text = "Optimize your image files (JPG, PNG, BMP)";
            // 
            // compressionTitleLabel
            // 
            compressionTitleLabel.AutoSize = true;
            compressionTitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            compressionTitleLabel.Location = new Point(377, 57);
            compressionTitleLabel.Name = "compressionTitleLabel";
            compressionTitleLabel.Size = new Size(258, 41);
            compressionTitleLabel.TabIndex = 0;
            compressionTitleLabel.Text = "File Compression";
            // 
            // decompressionPanel
            //
            decompressionPanel.Controls.Add(this.txtDecompressSelection);
            decompressionPanel.Controls.Add(this.btnDecompressDropdown);
            decompressionPanel.Controls.Add(this.checkedListBoxDecompress); // ADD

            decompressionPanel.BorderStyle = BorderStyle.Fixed3D;
            decompressionPanel.Controls.Add(decompressButton);
            decompressionPanel.Controls.Add(fileSelectionDecompression);
            decompressionPanel.Controls.Add(decompressionFileLabel);
            decompressionPanel.Controls.Add(decompressionDescLabel);
            decompressionPanel.Controls.Add(decompressionTitleLabel);
            decompressionPanel.Dock = DockStyle.Fill;
            decompressionPanel.Location = new Point(0, 62);
            decompressionPanel.Margin = new Padding(3, 4, 3, 4);
            decompressionPanel.Name = "decompressionPanel";
            decompressionPanel.Size = new Size(986, 647);
            decompressionPanel.TabIndex = 9;
            decompressionPanel.Visible = false;
            // txtDecompressSelection setup... (Location: 425, 312 etc.)
            txtDecompressSelection.Location = new Point(425, 312);
            txtDecompressSelection.Name = "txtDecompressSelection";
            txtDecompressSelection.ReadOnly = true;
            txtDecompressSelection.Size = new Size(120, 27); // Adjust size as needed
            txtDecompressSelection.TabIndex = 3; // Original TabIndex
            txtDecompressSelection.Text = "No selection";
            txtDecompressSelection.TabStop = false;
            // btnDecompressDropdown setup... (Location: 547, 311 etc.)
            btnDecompressDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDecompressDropdown.Location = new Point(547, 311); // Position next to textbox
            btnDecompressDropdown.Name = "btnDecompressDropdown";
            btnDecompressDropdown.Size = new Size(30, 29);
            btnDecompressDropdown.TabIndex = 5; // Adjust TabIndex
            btnDecompressDropdown.Text = "▼";
            btnDecompressDropdown.UseVisualStyleBackColor = true;
            btnDecompressDropdown.Click += new EventHandler(this.btnDropdown_Click); // SHARED
            // checkedListBoxDecompress setup... (Location: 425, 342 etc., Visible: false)
            checkedListBoxDecompress.CheckOnClick = true;
            checkedListBoxDecompress.FormattingEnabled = true;
            checkedListBoxDecompress.Location = new Point(425, 342); // Position BELOW the textbox
            checkedListBoxDecompress.Name = "checkedListBoxDecompress";
            checkedListBoxDecompress.Size = new Size(152, 90); // Adjust size
            checkedListBoxDecompress.TabIndex = 6; // Adjust TabIndex
            checkedListBoxDecompress.Visible = false; // Initially hidden
            checkedListBoxDecompress.ItemCheck += new ItemCheckEventHandler(this.checkedListBox_ItemCheck); // Keep limit handler
            checkedListBoxDecompress.Leave += new EventHandler(this.checkedListBox_Leave); // Add Leave handler
            //
            // decompressButton
            // 
            decompressButton.BackColor = SystemColors.ActiveCaptionText;
            decompressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            decompressButton.ForeColor = Color.Snow;
            decompressButton.Location = new Point(421, 383);
            decompressButton.Name = "decompressButton";
            decompressButton.Size = new Size(155, 51);
            decompressButton.TabIndex = 4;
            decompressButton.Text = "Decompress";
            decompressButton.UseVisualStyleBackColor = false;
            decompressButton.Click += decompressButton_Click;
            // 
            // fileSelectionDecompression
            // 
            //fileSelectionDecompression.DropDownStyle = ComboBoxStyle.DropDownList;
            //fileSelectionDecompression.FormattingEnabled = true;
            //fileSelectionDecompression.Location = new Point(425, 312);
            //fileSelectionDecompression.Name = "fileSelectionDecompression";
            //fileSelectionDecompression.Size = new Size(150, 28);
            //fileSelectionDecompression.TabIndex = 3;
            // 
            // decompressionFileLabel
            // 
            decompressionFileLabel.AutoSize = true;
            decompressionFileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            decompressionFileLabel.Location = new Point(400, 253);
            decompressionFileLabel.Name = "decompressionFileLabel";
            decompressionFileLabel.Size = new Size(211, 28);
            decompressionFileLabel.TabIndex = 2;
            decompressionFileLabel.Text = "Select Optimised File";
            // 
            // decompressionDescLabel
            // 
            decompressionDescLabel.AutoSize = true;
            decompressionDescLabel.Font = new Font("Segoe UI", 12F);
            decompressionDescLabel.Location = new Point(317, 123);
            decompressionDescLabel.Name = "decompressionDescLabel";
            decompressionDescLabel.Size = new Size(415, 28);
            decompressionDescLabel.TabIndex = 1;
            decompressionDescLabel.Text = "Restore optimized files to their original format";
            // 
            // decompressionTitleLabel
            // 
            decompressionTitleLabel.AutoSize = true;
            decompressionTitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            decompressionTitleLabel.Location = new Point(377, 57);
            decompressionTitleLabel.Name = "decompressionTitleLabel";
            decompressionTitleLabel.Size = new Size(291, 41);
            decompressionTitleLabel.TabIndex = 0;
            decompressionTitleLabel.Text = "File Decompression";

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 709);
            Controls.Add(decompressionPanel);
            Controls.Add(compressionPanel);
            Controls.Add(panelFileManagement);
            Controls.Add(decryptionPanel);
            Controls.Add(panelEncryptionSettings);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "SafeGuard File Manager";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            panelDropPasteTarget.ResumeLayout(false);
            panelFileManagement.ResumeLayout(false);
            panelFileManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).EndInit();
            panelEncryptionSettings.ResumeLayout(false);
            panelEncryptionSettings.PerformLayout();
            decryptionPanel.ResumeLayout(false);
            decryptionPanel.PerformLayout();
            compressionPanel.ResumeLayout(false);
            compressionPanel.PerformLayout();
            decompressionPanel.ResumeLayout(false);
            decompressionPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label headingNav;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private Panel panelContent;
        private Label label1;
        private Label label2;
        private Button btnUpload;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanelRecentFiles;
        private Panel panelEncryptionSettings;
        private Label label4;
        private ComboBox encryptionMethodSelection;
        private Label label5;
        private TextBox encryptionKeyBox;
        private Label label7;
        private Label label;
        private Label label8;
        private ComboBox fileSelectionEncryption;
        private Button encryptButton;
        private Panel decryptionPanel;
        private Button decryptButton;
        private Label label6; // This label exists but wasn't being used in the Decompression panel before
        private ComboBox decryptionFileSelection;
        private TextBox decryptionKey;
        private Label label9;
        private Label label10;
        private ComboBox decryptionMethodSelection;
        private Label label11;
        private Label label12;
        private LinkLabel linkLabel4;
        private Label label13;
        private Button viewAllFilesButton;
        private Panel panelFileManagement;
        private Label label15;
        private ComboBox cmbTables;
        private DataGridView dataGridViewFiles;
        private Label label14;
        private LinkLabel linkLabel5;
        // Compression Controls
        private Panel compressionPanel;
        private ComboBox fileSelectionCompression;
        private Button compressButton;
        private Label compressionTitleLabel; // Renamed from compressionLabel for clarity
        private Label compressionDescLabel;
        private Label compressionFileLabel;
        // Decompression Controls (NEW)
        private LinkLabel linkLabel6; // Link in header
        private Panel decompressionPanel;
        private Label decompressionTitleLabel;
        private Label decompressionDescLabel;
        private Label decompressionFileLabel;
        private ComboBox fileSelectionDecompression;
        private Button decompressButton;
        //batch
        private CheckedListBox checkedListBoxEncrypt;
        private CheckedListBox checkedListBoxDecrypt;
        private CheckedListBox checkedListBoxCompress;
        private CheckedListBox checkedListBoxDecompress;
        private TextBox txtEncryptSelection;
        private Button btnEncryptDropdown;
        private TextBox txtDecryptSelection;
        private Button btnDecryptDropdown;
        private TextBox txtCompressSelection;
        private Button btnCompressDropdown;
        private TextBox txtDecompressSelection;
        private Button btnDecompressDropdown;

    }
}
// --- END OF UPDATED Form1.Designer.cs ---