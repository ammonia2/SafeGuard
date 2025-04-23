// --- START OF UPDATED Form1.Designer.cs ---

using System.Data.SqlClient;
using System.Windows.Forms; // Ensure this is included for Control positioning
using System.Drawing; // Ensure this is included for Color definitions

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            panelHeader = new Panel();
            linkLabel6 = new LinkLabel();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            headingNav = new Label();
            panelContent = new Panel();
            panelDropPasteTarget = new Panel();
            lblDropPasteHint = new Label();
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
            txtEncryptSelection = new TextBox();
            btnEncryptDropdown = new Button();
            checkedListBoxEncrypt = new CheckedListBox();
            encryptButton = new Button();
            label8 = new Label();
            encryptionKeyBox = new TextBox();
            label7 = new Label();
            label = new Label();
            encryptionMethodSelection = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            decryptionPanel = new Panel();
            txtDecryptSelection = new TextBox();
            btnDecryptDropdown = new Button();
            checkedListBoxDecrypt = new CheckedListBox();
            decryptButton = new Button();
            label6 = new Label();
            decryptionKey = new TextBox();
            label9 = new Label();
            label10 = new Label();
            decryptionMethodSelection = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            compressionPanel = new Panel();
            txtCompressSelection = new TextBox();
            btnCompressDropdown = new Button();
            checkedListBoxCompress = new CheckedListBox();
            compressButton = new Button();
            compressionFileLabel = new Label();
            compressionDescLabel = new Label();
            compressionTitleLabel = new Label();
            decompressionPanel = new Panel();
            txtDecompressSelection = new TextBox();
            btnDecompressDropdown = new Button();
            checkedListBoxDecompress = new CheckedListBox();
            decompressButton = new Button();
            decompressionFileLabel = new Label();
            decompressionDescLabel = new Label();
            decompressionTitleLabel = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelDropPasteTarget.SuspendLayout();
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
            panelHeader.BackColor = Color.FromArgb(30, 30, 30);
            panelHeader.Controls.Add(linkLabel6);
            panelHeader.Controls.Add(linkLabel5);
            panelHeader.Controls.Add(linkLabel4);
            panelHeader.Controls.Add(linkLabel3);
            panelHeader.Controls.Add(linkLabel2);
            panelHeader.Controls.Add(linkLabel1);
            panelHeader.Controls.Add(headingNav);
            panelHeader.Dock = DockStyle.Left;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(10, 20, 10, 10);
            panelHeader.Size = new Size(160, 532);
            panelHeader.TabIndex = 0;
            //
            // linkLabel6
            //
            linkLabel6.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel6.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel6.Font = new Font("Segoe UI", 10F);
            linkLabel6.ForeColor = Color.White;
            linkLabel6.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel6.LinkColor = Color.White;
            linkLabel6.Location = new Point(25, 250);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(104, 35);
            linkLabel6.TabIndex = 0;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "Decompression";
            linkLabel6.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel6.VisitedLinkColor = Color.White;
            linkLabel6.LinkClicked += linkLabel6_LinkClicked;
            //
            // linkLabel5
            //
            linkLabel5.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel5.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel5.Font = new Font("Segoe UI", 10F);
            linkLabel5.ForeColor = Color.White;
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.White;
            linkLabel5.Location = new Point(24, 204);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(105, 35);
            linkLabel5.TabIndex = 1;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "Compression";
            linkLabel5.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel5.VisitedLinkColor = Color.White;
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            //
            // linkLabel4
            //
            linkLabel4.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel4.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel4.Font = new Font("Segoe UI", 10F);
            linkLabel4.ForeColor = Color.White;
            linkLabel4.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel4.LinkColor = Color.White;
            linkLabel4.Location = new Point(23, 295);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(106, 35);
            linkLabel4.TabIndex = 2;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Files";
            linkLabel4.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel4.VisitedLinkColor = Color.White;
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            //
            // linkLabel3
            //
            linkLabel3.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel3.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel3.Font = new Font("Segoe UI", 10F);
            linkLabel3.ForeColor = Color.White;
            linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel3.LinkColor = Color.White;
            linkLabel3.Location = new Point(25, 116);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(104, 35);
            linkLabel3.TabIndex = 3;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Encryption";
            linkLabel3.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel3.VisitedLinkColor = Color.White;
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            //
            // linkLabel2
            //
            linkLabel2.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel2.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel2.Font = new Font("Segoe UI", 10F);
            linkLabel2.ForeColor = Color.White;
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.White;
            linkLabel2.Location = new Point(25, 160);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(104, 35);
            linkLabel2.TabIndex = 9;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Decryption";
            linkLabel2.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel2.VisitedLinkColor = Color.White;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            //
            // linkLabel1
            //
            linkLabel1.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel1.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel1.Font = new Font("Segoe UI", 10F);
            linkLabel1.ForeColor = Color.White;
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(24, 71);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(105, 35);
            linkLabel1.TabIndex = 9;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Home";
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.VisitedLinkColor = Color.White;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            //
            // headingNav
            //
            headingNav.AutoSize = true;
            headingNav.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            headingNav.ForeColor = Color.White;
            headingNav.Location = new Point(23, 20);
            headingNav.Name = "headingNav";
            headingNav.Size = new Size(106, 25);
            headingNav.TabIndex = 0;
            headingNav.Text = "SafeGuard";
            //
            // panelContent
            //
            panelContent.BackColor = Color.FromArgb(45, 45, 48); // Dark Background
            panelContent.ForeColor = Color.White; // Default Text Color
            panelContent.Controls.Add(panelDropPasteTarget);
            panelContent.Controls.Add(viewAllFilesButton);
            panelContent.Controls.Add(label13);
            panelContent.Controls.Add(flowLayoutPanelRecentFiles);
            panelContent.Controls.Add(label3);
            panelContent.Controls.Add(btnUpload);
            panelContent.Controls.Add(label2);
            panelContent.Controls.Add(label1);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(160, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(703, 532);
            panelContent.TabIndex = 1;
            //
            // panelDropPasteTarget
            //
            panelDropPasteTarget.AllowDrop = true;
            panelDropPasteTarget.BackColor = Color.FromArgb(60, 60, 60); // Slightly Lighter Dark
            panelDropPasteTarget.BorderStyle = BorderStyle.FixedSingle;
            panelDropPasteTarget.Controls.Add(lblDropPasteHint);
            panelDropPasteTarget.ForeColor = Color.DarkGray; // Hint Text Color
            panelDropPasteTarget.Location = new Point(247, 236);
            panelDropPasteTarget.Name = "panelDropPasteTarget";
            panelDropPasteTarget.Size = new Size(224, 53);
            panelDropPasteTarget.TabIndex = 7;
            panelDropPasteTarget.DragDrop += panelDropPasteTarget_DragDrop;
            panelDropPasteTarget.DragEnter += panelDropPasteTarget_DragEnter;
            //
            // lblDropPasteHint
            //
            lblDropPasteHint.Dock = DockStyle.Fill;
            lblDropPasteHint.Enabled = false;
            lblDropPasteHint.ForeColor = Color.DarkGray; // Hint Text Color
            lblDropPasteHint.Location = new Point(0, 0);
            lblDropPasteHint.Name = "lblDropPasteHint";
            lblDropPasteHint.Size = new Size(222, 51);
            lblDropPasteHint.TabIndex = 0;
            lblDropPasteHint.Text = "or drag && drop / paste image from clipboard here";
            lblDropPasteHint.TextAlign = ContentAlignment.MiddleCenter;
            //
            // viewAllFilesButton
            //
            viewAllFilesButton.BackColor = Color.Black;
            viewAllFilesButton.FlatStyle = FlatStyle.Flat; // Modern Look
            viewAllFilesButton.FlatAppearance.BorderColor = Color.DimGray;
            viewAllFilesButton.Font = new Font("Segoe UI", 12F);
            viewAllFilesButton.ForeColor = Color.White;
            viewAllFilesButton.Location = new Point(308, 471);
            viewAllFilesButton.Name = "viewAllFilesButton";
            viewAllFilesButton.Size = new Size(110, 37);
            viewAllFilesButton.TabIndex = 6;
            viewAllFilesButton.Text = "View All";
            viewAllFilesButton.UseVisualStyleBackColor = false;
            viewAllFilesButton.Click += viewAllFilesButton_Click;
            //
            // label13
            //
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.White; // Text Color
            label13.Location = new Point(275, 144);
            label13.Name = "label13";
            label13.Size = new Size(176, 28);
            label13.TabIndex = 5;
            label13.Text = "File Management";
            //
            // flowLayoutPanelRecentFiles
            //
            flowLayoutPanelRecentFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            flowLayoutPanelRecentFiles.AutoScroll = true;
            flowLayoutPanelRecentFiles.BackColor = Color.FromArgb(45, 45, 48); // Match Panel Background
            flowLayoutPanelRecentFiles.ForeColor = Color.White; // Default Text Color Inside
            flowLayoutPanelRecentFiles.Location = new Point(134, 335);
            flowLayoutPanelRecentFiles.Name = "flowLayoutPanelRecentFiles";
            flowLayoutPanelRecentFiles.Size = new Size(422, 130);
            flowLayoutPanelRecentFiles.TabIndex = 4;
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White; // Text Color
            label3.Location = new Point(301, 304);
            label3.Name = "label3";
            label3.Size = new Size(125, 28);
            label3.TabIndex = 3;
            label3.Text = "Recent Files";
            //
            // btnUpload
            //
            btnUpload.BackColor = Color.Black;
            btnUpload.FlatStyle = FlatStyle.Flat; // Modern Look
            btnUpload.FlatAppearance.BorderColor = Color.DimGray;
            btnUpload.Font = new Font("Segoe UI", 12F);
            btnUpload.ForeColor = Color.White;
            btnUpload.Location = new Point(308, 184);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(110, 37);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White; // Text Color
            label2.Location = new Point(261, 95);
            label2.Name = "label2";
            label2.Size = new Size(205, 21);
            label2.TabIndex = 1;
            label2.Text = "Manage your files efficiently";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White; // Text Color
            label1.Location = new Point(159, 45);
            label1.Name = "label1";
            label1.Size = new Size(409, 32);
            label1.TabIndex = 0;
            label1.Text = "Welcome To SafeGuard Dashboard";
            //
            // panelFileManagement
            //
            panelFileManagement.BackColor = Color.FromArgb(45, 45, 48); // Dark Background
            panelFileManagement.ForeColor = Color.White; // Default Text Color
            panelFileManagement.Controls.Add(label15);
            panelFileManagement.Controls.Add(cmbTables);
            panelFileManagement.Controls.Add(dataGridViewFiles);
            panelFileManagement.Controls.Add(label14);
            panelFileManagement.Dock = DockStyle.Fill;
            panelFileManagement.Location = new Point(160, 0);
            panelFileManagement.Name = "panelFileManagement";
            panelFileManagement.Size = new Size(703, 532);
            panelFileManagement.TabIndex = 7;
            panelFileManagement.Visible = false;
            //
            // label15
            //
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.White; // Text Color
            label15.Location = new Point(300, 100);
            label15.Name = "label15";
            label15.Size = new Size(127, 21);
            label15.TabIndex = 4;
            label15.Text = "Select File Type";
            //
            // cmbTables
            //
            cmbTables.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            cmbTables.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTables.FlatStyle = FlatStyle.Flat; // Modern Look
            cmbTables.ForeColor = Color.White; // Text Color
            cmbTables.FormattingEnabled = true;
            cmbTables.Location = new Point(303, 138);
            cmbTables.Name = "cmbTables";
            cmbTables.Size = new Size(121, 23);
            cmbTables.TabIndex = 3;
            //
            // dataGridViewFiles
            //
            dataGridViewFiles.AllowUserToAddRows = false;
            dataGridViewFiles.AllowUserToDeleteRows = false;
            dataGridViewFiles.BackgroundColor = Color.FromArgb(45, 45, 48); // Match Panel Background
            dataGridViewFiles.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewFiles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black; // Header Background
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White; // Header Text
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(60, 60, 60); // Cell Background
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White; // Cell Text
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue; // Selection Background
            dataGridViewCellStyle2.SelectionForeColor = Color.White; // Selection Text
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewFiles.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewFiles.EnableHeadersVisualStyles = false; // Important for custom header colors
            dataGridViewFiles.GridColor = Color.DimGray; // Grid Lines
            dataGridViewFiles.Location = new Point(50, 192);
            dataGridViewFiles.Name = "dataGridViewFiles";
            dataGridViewFiles.ReadOnly = true;
            dataGridViewFiles.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.Black; // Row Header Background
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White; // Row Header Text
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridViewFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewFiles.RowHeadersWidth = 51; // Keep size if needed
            dataGridViewCellStyle4.BackColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewFiles.RowsDefaultCellStyle = dataGridViewCellStyle4; // Ensure rows style too
            dataGridViewFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFiles.Size = new Size(627, 261);
            dataGridViewFiles.TabIndex = 2;
            dataGridViewFiles.CellDoubleClick += dataGridViewFiles_CellDoubleClick;
            //
            // label14
            //
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.White; // Text Color
            label14.Location = new Point(258, 45);
            label14.Name = "label14";
            label14.Size = new Size(211, 32);
            label14.TabIndex = 1;
            label14.Text = "File Management";
            //
            // panelEncryptionSettings
            //
            panelEncryptionSettings.BackColor = Color.FromArgb(45, 45, 48); // Dark Background
            panelEncryptionSettings.ForeColor = Color.White; // Default Text Color
            panelEncryptionSettings.BorderStyle = BorderStyle.FixedSingle; // Cleaner Border
            panelEncryptionSettings.Controls.Add(txtEncryptSelection);
            panelEncryptionSettings.Controls.Add(btnEncryptDropdown);
            panelEncryptionSettings.Controls.Add(checkedListBoxEncrypt);
            panelEncryptionSettings.Controls.Add(encryptButton);
            panelEncryptionSettings.Controls.Add(label8);
            panelEncryptionSettings.Controls.Add(encryptionKeyBox);
            panelEncryptionSettings.Controls.Add(label7);
            panelEncryptionSettings.Controls.Add(label);
            panelEncryptionSettings.Controls.Add(encryptionMethodSelection);
            panelEncryptionSettings.Controls.Add(label5);
            panelEncryptionSettings.Controls.Add(label4);
            panelEncryptionSettings.Dock = DockStyle.Fill;
            panelEncryptionSettings.Location = new Point(160, 0);
            panelEncryptionSettings.Name = "panelEncryptionSettings";
            panelEncryptionSettings.Size = new Size(703, 532);
            panelEncryptionSettings.TabIndex = 5;
            panelEncryptionSettings.Visible = false;
            //
            // txtEncryptSelection
            //
            txtEncryptSelection.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            txtEncryptSelection.ForeColor = Color.White; // Text Color
            txtEncryptSelection.BorderStyle = BorderStyle.FixedSingle;
            txtEncryptSelection.Location = new Point(103, 222);
            txtEncryptSelection.Margin = new Padding(3, 2, 3, 2);
            txtEncryptSelection.Name = "txtEncryptSelection";
            txtEncryptSelection.ReadOnly = true;
            txtEncryptSelection.Size = new Size(106, 23);
            txtEncryptSelection.TabIndex = 7;
            txtEncryptSelection.TabStop = false;
            txtEncryptSelection.Text = "No selection";
            //
            // btnEncryptDropdown
            //
            btnEncryptDropdown.BackColor = Color.FromArgb(70, 70, 70); // Slightly lighter dark
            btnEncryptDropdown.FlatStyle = FlatStyle.Flat;
            btnEncryptDropdown.FlatAppearance.BorderSize = 0;
            btnEncryptDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEncryptDropdown.ForeColor = Color.White;
            btnEncryptDropdown.Location = new Point(210, 221);
            btnEncryptDropdown.Margin = new Padding(3, 2, 3, 2);
            btnEncryptDropdown.Name = "btnEncryptDropdown";
            btnEncryptDropdown.Size = new Size(26, 22);
            btnEncryptDropdown.TabIndex = 10;
            btnEncryptDropdown.Text = "▼";
            btnEncryptDropdown.UseVisualStyleBackColor = false;
            btnEncryptDropdown.Click += btnDropdown_Click;
            //
            // checkedListBoxEncrypt
            //
            checkedListBoxEncrypt.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            checkedListBoxEncrypt.ForeColor = Color.White; // Text Color
            checkedListBoxEncrypt.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxEncrypt.CheckOnClick = true;
            checkedListBoxEncrypt.FormattingEnabled = true;
            checkedListBoxEncrypt.Location = new Point(103, 244);
            checkedListBoxEncrypt.Margin = new Padding(3, 2, 3, 2);
            checkedListBoxEncrypt.Name = "checkedListBoxEncrypt";
            checkedListBoxEncrypt.Size = new Size(134, 58);
            checkedListBoxEncrypt.TabIndex = 11;
            checkedListBoxEncrypt.Visible = false;
            checkedListBoxEncrypt.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxEncrypt.Leave += checkedListBox_Leave;
            //
            // encryptButton
            //
            encryptButton.BackColor = Color.Black;
            encryptButton.FlatStyle = FlatStyle.Flat;
            encryptButton.FlatAppearance.BorderColor = Color.DimGray;
            encryptButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            encryptButton.ForeColor = Color.White;
            encryptButton.Location = new Point(321, 330);
            encryptButton.Name = "encryptButton";
            encryptButton.Size = new Size(95, 41);
            encryptButton.TabIndex = 9;
            encryptButton.Text = "Encrypt";
            encryptButton.UseVisualStyleBackColor = false;
            encryptButton.Click += encryptButton_Click;
            //
            // label8
            //
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White; // Text Color
            label8.Location = new Point(122, 192);
            label8.Name = "label8";
            label8.Size = new Size(87, 21);
            label8.TabIndex = 8;
            label8.Text = "Select File";
            //
            // encryptionKeyBox
            //
            encryptionKeyBox.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            encryptionKeyBox.ForeColor = Color.White; // Text Color
            encryptionKeyBox.BorderStyle = BorderStyle.FixedSingle;
            encryptionKeyBox.Location = new Point(495, 222);
            encryptionKeyBox.Name = "encryptionKeyBox";
            encryptionKeyBox.Size = new Size(100, 23);
            encryptionKeyBox.TabIndex = 6;
            //
            // label7
            //
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White; // Text Color
            label7.Location = new Point(488, 192);
            label7.Name = "label7";
            label7.Size = new Size(126, 21);
            label7.TabIndex = 5;
            label7.Text = "Encryption Key\r\n";
            //
            // label
            //
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.ForeColor = Color.White; // Text Color
            label.Location = new Point(288, 192);
            label.Name = "label";
            label.Size = new Size(158, 21);
            label.TabIndex = 3;
            label.Text = "Encryption Method";
            //
            // encryptionMethodSelection
            //
            encryptionMethodSelection.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            encryptionMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            encryptionMethodSelection.FlatStyle = FlatStyle.Flat;
            encryptionMethodSelection.ForeColor = Color.White; // Text Color
            encryptionMethodSelection.FormattingEnabled = true;
            encryptionMethodSelection.Items.AddRange(new object[] { "AES", "Pixel Scrambling" });
            encryptionMethodSelection.Location = new Point(303, 222);
            encryptionMethodSelection.Name = "encryptionMethodSelection";
            encryptionMethodSelection.Size = new Size(121, 23);
            encryptionMethodSelection.TabIndex = 2;
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.White; // Text Color
            label5.Location = new Point(227, 95);
            label5.Name = "label5";
            label5.Size = new Size(272, 21);
            label5.TabIndex = 1;
            label5.Text = "Select Encryption Method and Set Key";
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White; // Text Color
            label4.Location = new Point(271, 45);
            label4.Name = "label4";
            label4.Size = new Size(185, 32);
            label4.TabIndex = 0;
            label4.Text = "File Encryption";
            //
            // decryptionPanel
            //
            decryptionPanel.BackColor = Color.FromArgb(45, 45, 48); // Dark Background
            decryptionPanel.ForeColor = Color.White; // Default Text Color
            decryptionPanel.BorderStyle = BorderStyle.FixedSingle; // Cleaner Border
            decryptionPanel.Controls.Add(txtDecryptSelection);
            decryptionPanel.Controls.Add(btnDecryptDropdown);
            decryptionPanel.Controls.Add(checkedListBoxDecrypt);
            decryptionPanel.Controls.Add(decryptButton);
            decryptionPanel.Controls.Add(label6);
            decryptionPanel.Controls.Add(decryptionKey);
            decryptionPanel.Controls.Add(label9);
            decryptionPanel.Controls.Add(label10);
            decryptionPanel.Controls.Add(decryptionMethodSelection);
            decryptionPanel.Controls.Add(label11);
            decryptionPanel.Controls.Add(label12);
            decryptionPanel.Dock = DockStyle.Fill;
            decryptionPanel.Location = new Point(160, 0);
            decryptionPanel.Name = "decryptionPanel";
            decryptionPanel.Size = new Size(703, 532);
            decryptionPanel.TabIndex = 6;
            decryptionPanel.Visible = false;
            //
            // txtDecryptSelection
            //
            txtDecryptSelection.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            txtDecryptSelection.ForeColor = Color.White; // Text Color
            txtDecryptSelection.BorderStyle = BorderStyle.FixedSingle;
            txtDecryptSelection.Location = new Point(96, 220);
            txtDecryptSelection.Margin = new Padding(3, 2, 3, 2);
            txtDecryptSelection.Name = "txtDecryptSelection";
            txtDecryptSelection.ReadOnly = true;
            txtDecryptSelection.Size = new Size(106, 23);
            txtDecryptSelection.TabIndex = 7;
            txtDecryptSelection.TabStop = false;
            txtDecryptSelection.Text = "No selection";
            //
            // btnDecryptDropdown
            //
            btnDecryptDropdown.BackColor = Color.FromArgb(70, 70, 70); // Slightly lighter dark
            btnDecryptDropdown.FlatStyle = FlatStyle.Flat;
            btnDecryptDropdown.FlatAppearance.BorderSize = 0;
            btnDecryptDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDecryptDropdown.ForeColor = Color.White;
            btnDecryptDropdown.Location = new Point(203, 219);
            btnDecryptDropdown.Margin = new Padding(3, 2, 3, 2);
            btnDecryptDropdown.Name = "btnDecryptDropdown";
            btnDecryptDropdown.Size = new Size(26, 22);
            btnDecryptDropdown.TabIndex = 10;
            btnDecryptDropdown.Text = "▼";
            btnDecryptDropdown.UseVisualStyleBackColor = false;
            btnDecryptDropdown.Click += btnDropdown_Click;
            //
            // checkedListBoxDecrypt
            //
            checkedListBoxDecrypt.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            checkedListBoxDecrypt.ForeColor = Color.White; // Text Color
            checkedListBoxDecrypt.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxDecrypt.CheckOnClick = true;
            checkedListBoxDecrypt.FormattingEnabled = true;
            checkedListBoxDecrypt.Location = new Point(96, 242);
            checkedListBoxDecrypt.Margin = new Padding(3, 2, 3, 2);
            checkedListBoxDecrypt.Name = "checkedListBoxDecrypt";
            checkedListBoxDecrypt.Size = new Size(134, 58);
            checkedListBoxDecrypt.TabIndex = 11;
            checkedListBoxDecrypt.Visible = false;
            checkedListBoxDecrypt.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxDecrypt.Leave += checkedListBox_Leave;
            //
            // decryptButton
            //
            decryptButton.BackColor = Color.Black;
            decryptButton.FlatStyle = FlatStyle.Flat;
            decryptButton.FlatAppearance.BorderColor = Color.DimGray;
            decryptButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            decryptButton.ForeColor = Color.White;
            decryptButton.Location = new Point(312, 329);
            decryptButton.Name = "decryptButton";
            decryptButton.Size = new Size(95, 41);
            decryptButton.TabIndex = 9;
            decryptButton.Text = "Decrypt";
            decryptButton.UseVisualStyleBackColor = false;
            decryptButton.Click += decryptButton_Click;
            //
            // label6
            //
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White; // Text Color
            label6.Location = new Point(115, 190);
            label6.Name = "label6";
            label6.Size = new Size(87, 21);
            label6.TabIndex = 8;
            label6.Text = "Select File";
            //
            // decryptionKey
            //
            decryptionKey.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            decryptionKey.ForeColor = Color.White; // Text Color
            decryptionKey.BorderStyle = BorderStyle.FixedSingle;
            decryptionKey.Location = new Point(488, 220);
            decryptionKey.Name = "decryptionKey";
            decryptionKey.Size = new Size(100, 23);
            decryptionKey.TabIndex = 6;
            //
            // label9
            //
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White; // Text Color
            label9.Location = new Point(481, 190);
            label9.Name = "label9";
            label9.Size = new Size(128, 21);
            label9.TabIndex = 5;
            label9.Text = "Decryption Key\r\n";
            //
            // label10
            //
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White; // Text Color
            label10.Location = new Point(281, 190);
            label10.Name = "label10";
            label10.Size = new Size(160, 21);
            label10.TabIndex = 3;
            label10.Text = "Decryption Method";
            //
            // decryptionMethodSelection
            //
            decryptionMethodSelection.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            decryptionMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            decryptionMethodSelection.FlatStyle = FlatStyle.Flat;
            decryptionMethodSelection.ForeColor = Color.White; // Text Color
            decryptionMethodSelection.FormattingEnabled = true;
            decryptionMethodSelection.Location = new Point(296, 220);
            decryptionMethodSelection.Name = "decryptionMethodSelection";
            decryptionMethodSelection.Size = new Size(121, 23);
            decryptionMethodSelection.TabIndex = 2;
            //
            // label11
            //
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = Color.White; // Text Color
            label11.Location = new Point(211, 95);
            label11.Name = "label11";
            label11.Size = new Size(305, 21);
            label11.TabIndex = 1;
            label11.Text = "Select Decryption Method and Provide Key";
            //
            // label12
            //
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White; // Text Color
            label12.Location = new Point(269, 45);
            label12.Name = "label12";
            label12.Size = new Size(188, 32);
            label12.TabIndex = 0;
            label12.Text = "File Decryption";
            //
            // compressionPanel
            //
            compressionPanel.BackColor = Color.FromArgb(45, 45, 48); // Dark Background
            compressionPanel.ForeColor = Color.White; // Default Text Color
            compressionPanel.BorderStyle = BorderStyle.FixedSingle; // Cleaner Border
            compressionPanel.Controls.Add(txtCompressSelection);
            compressionPanel.Controls.Add(btnCompressDropdown);
            compressionPanel.Controls.Add(checkedListBoxCompress);
            compressionPanel.Controls.Add(compressButton);
            compressionPanel.Controls.Add(compressionFileLabel);
            compressionPanel.Controls.Add(compressionDescLabel);
            compressionPanel.Controls.Add(compressionTitleLabel);
            compressionPanel.Dock = DockStyle.Fill;
            compressionPanel.Location = new Point(160, 0);
            compressionPanel.Name = "compressionPanel";
            compressionPanel.Size = new Size(703, 532);
            compressionPanel.TabIndex = 8;
            compressionPanel.Visible = false;
            //
            // txtCompressSelection
            //
            txtCompressSelection.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            txtCompressSelection.ForeColor = Color.White; // Text Color
            txtCompressSelection.BorderStyle = BorderStyle.FixedSingle;
            txtCompressSelection.Location = new Point(297, 234);
            txtCompressSelection.Margin = new Padding(3, 2, 3, 2);
            txtCompressSelection.Name = "txtCompressSelection";
            txtCompressSelection.ReadOnly = true;
            txtCompressSelection.Size = new Size(106, 23);
            txtCompressSelection.TabIndex = 3;
            txtCompressSelection.TabStop = false;
            txtCompressSelection.Text = "No selection";
            //
            // btnCompressDropdown
            //
            btnCompressDropdown.BackColor = Color.FromArgb(70, 70, 70); // Slightly lighter dark
            btnCompressDropdown.FlatStyle = FlatStyle.Flat;
            btnCompressDropdown.FlatAppearance.BorderSize = 0;
            btnCompressDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCompressDropdown.ForeColor = Color.White;
            btnCompressDropdown.Location = new Point(403, 233);
            btnCompressDropdown.Margin = new Padding(3, 2, 3, 2);
            btnCompressDropdown.Name = "btnCompressDropdown";
            btnCompressDropdown.Size = new Size(26, 22);
            btnCompressDropdown.TabIndex = 5;
            btnCompressDropdown.Text = "▼";
            btnCompressDropdown.UseVisualStyleBackColor = false;
            btnCompressDropdown.Click += btnDropdown_Click;
            //
            // checkedListBoxCompress
            //
            checkedListBoxCompress.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            checkedListBoxCompress.ForeColor = Color.White; // Text Color
            checkedListBoxCompress.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxCompress.CheckOnClick = true;
            checkedListBoxCompress.FormattingEnabled = true;
            checkedListBoxCompress.Location = new Point(297, 257);
            checkedListBoxCompress.Margin = new Padding(3, 2, 3, 2);
            checkedListBoxCompress.Name = "checkedListBoxCompress";
            checkedListBoxCompress.Size = new Size(134, 58);
            checkedListBoxCompress.TabIndex = 6;
            checkedListBoxCompress.Visible = false;
            checkedListBoxCompress.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxCompress.Leave += checkedListBox_Leave;
            //
            // compressButton
            //
            compressButton.BackColor = Color.Black;
            compressButton.FlatStyle = FlatStyle.Flat;
            compressButton.FlatAppearance.BorderColor = Color.DimGray;
            compressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressButton.ForeColor = Color.White;
            compressButton.Location = new Point(307, 330);
            compressButton.Margin = new Padding(3, 2, 3, 2);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(113, 38);
            compressButton.TabIndex = 4;
            compressButton.Text = "Optimize";
            compressButton.UseVisualStyleBackColor = false;
            compressButton.Click += compressButton_Click;
            //
            // compressionFileLabel
            //
            compressionFileLabel.AutoSize = true;
            compressionFileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressionFileLabel.ForeColor = Color.White; // Text Color
            compressionFileLabel.Location = new Point(320, 190);
            compressionFileLabel.Name = "compressionFileLabel";
            compressionFileLabel.Size = new Size(87, 21);
            compressionFileLabel.TabIndex = 2;
            compressionFileLabel.Text = "Select File";
            //
            // compressionDescLabel
            //
            compressionDescLabel.AutoSize = true;
            compressionDescLabel.Font = new Font("Segoe UI", 12F);
            compressionDescLabel.ForeColor = Color.White; // Text Color
            compressionDescLabel.Location = new Point(210, 95);
            compressionDescLabel.Name = "compressionDescLabel";
            compressionDescLabel.Size = new Size(306, 21);
            compressionDescLabel.TabIndex = 1;
            compressionDescLabel.Text = "Optimize your image files (JPG, PNG, BMP)";
            //
            // compressionTitleLabel
            //
            compressionTitleLabel.AutoSize = true;
            compressionTitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            compressionTitleLabel.ForeColor = Color.White; // Text Color
            compressionTitleLabel.Location = new Point(259, 45);
            compressionTitleLabel.Name = "compressionTitleLabel";
            compressionTitleLabel.Size = new Size(209, 32);
            compressionTitleLabel.TabIndex = 0;
            compressionTitleLabel.Text = "File Compression";
            //
            // decompressionPanel
            //
            decompressionPanel.BackColor = Color.FromArgb(45, 45, 48); // Dark Background
            decompressionPanel.ForeColor = Color.White; // Default Text Color
            decompressionPanel.BorderStyle = BorderStyle.FixedSingle; // Cleaner Border
            decompressionPanel.Controls.Add(txtDecompressSelection);
            decompressionPanel.Controls.Add(btnDecompressDropdown);
            decompressionPanel.Controls.Add(checkedListBoxDecompress);
            decompressionPanel.Controls.Add(decompressButton);
            decompressionPanel.Controls.Add(decompressionFileLabel);
            decompressionPanel.Controls.Add(decompressionDescLabel);
            decompressionPanel.Controls.Add(decompressionTitleLabel);
            decompressionPanel.Dock = DockStyle.Fill;
            decompressionPanel.Location = new Point(160, 0);
            decompressionPanel.Name = "decompressionPanel";
            decompressionPanel.Size = new Size(703, 532);
            decompressionPanel.TabIndex = 9;
            decompressionPanel.Visible = false;
            //
            // txtDecompressSelection
            //
            txtDecompressSelection.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            txtDecompressSelection.ForeColor = Color.White; // Text Color
            txtDecompressSelection.BorderStyle = BorderStyle.FixedSingle;
            txtDecompressSelection.Location = new Point(297, 234);
            txtDecompressSelection.Margin = new Padding(3, 2, 3, 2);
            txtDecompressSelection.Name = "txtDecompressSelection";
            txtDecompressSelection.ReadOnly = true;
            txtDecompressSelection.Size = new Size(106, 23);
            txtDecompressSelection.TabIndex = 3;
            txtDecompressSelection.TabStop = false;
            txtDecompressSelection.Text = "No selection";
            //
            // btnDecompressDropdown
            //
            btnDecompressDropdown.BackColor = Color.FromArgb(70, 70, 70); // Slightly lighter dark
            btnDecompressDropdown.FlatStyle = FlatStyle.Flat;
            btnDecompressDropdown.FlatAppearance.BorderSize = 0;
            btnDecompressDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDecompressDropdown.ForeColor = Color.White;
            btnDecompressDropdown.Location = new Point(403, 233);
            btnDecompressDropdown.Margin = new Padding(3, 2, 3, 2);
            btnDecompressDropdown.Name = "btnDecompressDropdown";
            btnDecompressDropdown.Size = new Size(26, 22);
            btnDecompressDropdown.TabIndex = 5;
            btnDecompressDropdown.Text = "▼";
            btnDecompressDropdown.UseVisualStyleBackColor = false;
            btnDecompressDropdown.Click += btnDropdown_Click;
            //
            // checkedListBoxDecompress
            //
            checkedListBoxDecompress.BackColor = Color.FromArgb(60, 60, 60); // Control Background
            checkedListBoxDecompress.ForeColor = Color.White; // Text Color
            checkedListBoxDecompress.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxDecompress.CheckOnClick = true;
            checkedListBoxDecompress.FormattingEnabled = true;
            checkedListBoxDecompress.Location = new Point(297, 257);
            checkedListBoxDecompress.Margin = new Padding(3, 2, 3, 2);
            checkedListBoxDecompress.Name = "checkedListBoxDecompress";
            checkedListBoxDecompress.Size = new Size(134, 58);
            checkedListBoxDecompress.TabIndex = 6;
            checkedListBoxDecompress.Visible = false;
            checkedListBoxDecompress.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxDecompress.Leave += checkedListBox_Leave;
            //
            // decompressButton
            //
            decompressButton.BackColor = Color.Black;
            decompressButton.FlatStyle = FlatStyle.Flat;
            decompressButton.FlatAppearance.BorderColor = Color.DimGray;
            decompressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            decompressButton.ForeColor = Color.White;
            decompressButton.Location = new Point(295, 330);
            decompressButton.Margin = new Padding(3, 2, 3, 2);
            decompressButton.Name = "decompressButton";
            decompressButton.Size = new Size(136, 38);
            decompressButton.TabIndex = 4;
            decompressButton.Text = "Decompress";
            decompressButton.UseVisualStyleBackColor = false;
            decompressButton.Click += decompressButton_Click;
            //
            // decompressionFileLabel
            //
            decompressionFileLabel.AutoSize = true;
            decompressionFileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            decompressionFileLabel.ForeColor = Color.White; // Text Color
            decompressionFileLabel.Location = new Point(278, 190);
            decompressionFileLabel.Name = "decompressionFileLabel";
            decompressionFileLabel.Size = new Size(170, 21);
            decompressionFileLabel.TabIndex = 2;
            decompressionFileLabel.Text = "Select Optimised File";
            //
            // decompressionDescLabel
            //
            decompressionDescLabel.AutoSize = true;
            decompressionDescLabel.Font = new Font("Segoe UI", 12F);
            decompressionDescLabel.ForeColor = Color.White; // Text Color
            decompressionDescLabel.Location = new Point(198, 95);
            decompressionDescLabel.Name = "decompressionDescLabel";
            decompressionDescLabel.Size = new Size(330, 21);
            decompressionDescLabel.TabIndex = 1;
            decompressionDescLabel.Text = "Restore optimized files to their original format";
            //
            // decompressionTitleLabel
            //
            decompressionTitleLabel.AutoSize = true;
            decompressionTitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            decompressionTitleLabel.ForeColor = Color.White; // Text Color
            decompressionTitleLabel.Location = new Point(245, 45);
            decompressionTitleLabel.Name = "decompressionTitleLabel";
            decompressionTitleLabel.Size = new Size(237, 32);
            decompressionTitleLabel.TabIndex = 0;
            decompressionTitleLabel.Text = "File Decompression";
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48); // Set Form background too
            ClientSize = new Size(863, 532);
            Controls.Add(panelContent); // Ensure panelContent is added first to be the default visible
            Controls.Add(panelEncryptionSettings);
            Controls.Add(decryptionPanel);
            Controls.Add(decompressionPanel);
            Controls.Add(compressionPanel);
            Controls.Add(panelFileManagement);
            Controls.Add(panelHeader); // panelHeader should be behind the content panels if docked Left
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
        private Button encryptButton;
        private Panel decryptionPanel;
        private Button decryptButton;
        private Label label6;
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
        private LinkLabel linkLabel6;
        private Panel compressionPanel;
        private Button compressButton;
        private Label compressionTitleLabel;
        private Label compressionDescLabel;
        private Label compressionFileLabel;
        private Panel decompressionPanel;
        private Label decompressionTitleLabel;
        private Label decompressionDescLabel;
        private Label decompressionFileLabel;
        private Button decompressButton;
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
