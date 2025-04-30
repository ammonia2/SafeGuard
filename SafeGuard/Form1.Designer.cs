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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panelHeader = new Panel();
            linkLabel7 = new LinkLabel();
            linkLabel6 = new LinkLabel();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            headingNav = new Label();
            panelContent = new Panel();
            viewAllFilesButton = new Button();
            panelDropPasteTarget = new Panel();
            lblDropPasteHint = new Label();
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
            panelRemoveFiles = new Panel();
            buttonDeleteSelectedFiles = new Button();
            dropdownRemoveSelector = new Button();
            txtRemoveSelect = new TextBox();
            checkedListBoxRemove = new CheckedListBox();
            label17 = new Label();
            label16 = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelDropPasteTarget.SuspendLayout();
            panelFileManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).BeginInit();
            panelEncryptionSettings.SuspendLayout();
            decryptionPanel.SuspendLayout();
            compressionPanel.SuspendLayout();
            decompressionPanel.SuspendLayout();
            panelRemoveFiles.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(30, 30, 30);
            panelHeader.Controls.Add(linkLabel7);
            panelHeader.Controls.Add(linkLabel6);
            panelHeader.Controls.Add(linkLabel5);
            panelHeader.Controls.Add(linkLabel4);
            panelHeader.Controls.Add(linkLabel3);
            panelHeader.Controls.Add(linkLabel2);
            panelHeader.Controls.Add(linkLabel1);
            panelHeader.Controls.Add(headingNav);
            panelHeader.Dock = DockStyle.Left;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(11, 27, 11, 13);
            panelHeader.Size = new Size(192, 709);
            panelHeader.TabIndex = 0;
            // 
            // linkLabel7
            // 
            linkLabel7.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel7.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel7.Font = new Font("Segoe UI", 10F);
            linkLabel7.ForeColor = Color.White;
            linkLabel7.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel7.LinkColor = Color.White;
            linkLabel7.Location = new Point(14, 478);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new Size(163, 47);
            linkLabel7.TabIndex = 10;
            linkLabel7.TabStop = true;
            linkLabel7.Text = "Drop Files";
            linkLabel7.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel7.VisitedLinkColor = Color.White;
            linkLabel7.LinkClicked += linkRemoveFiles_LinkClicked;
            // 
            // linkLabel6
            // 
            linkLabel6.ActiveLinkColor = Color.DeepSkyBlue;
            linkLabel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel6.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel6.Font = new Font("Segoe UI", 10F);
            linkLabel6.ForeColor = Color.White;
            linkLabel6.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel6.LinkColor = Color.White;
            linkLabel6.Location = new Point(14, 352);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(163, 49);
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
            linkLabel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel5.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel5.Font = new Font("Segoe UI", 10F);
            linkLabel5.ForeColor = Color.White;
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.White;
            linkLabel5.Location = new Point(14, 291);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(164, 47);
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
            linkLabel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel4.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel4.Font = new Font("Segoe UI", 10F);
            linkLabel4.ForeColor = Color.White;
            linkLabel4.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel4.LinkColor = Color.White;
            linkLabel4.Location = new Point(14, 416);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(163, 45);
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
            linkLabel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel3.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel3.Font = new Font("Segoe UI", 10F);
            linkLabel3.ForeColor = Color.White;
            linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel3.LinkColor = Color.White;
            linkLabel3.Location = new Point(14, 172);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(163, 47);
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
            linkLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel2.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel2.Font = new Font("Segoe UI", 10F);
            linkLabel2.ForeColor = Color.White;
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.White;
            linkLabel2.Location = new Point(14, 232);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(163, 47);
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
            linkLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLabel1.BackColor = Color.FromArgb(45, 45, 48);
            linkLabel1.Font = new Font("Segoe UI", 10F);
            linkLabel1.ForeColor = Color.White;
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(14, 112);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(164, 47);
            linkLabel1.TabIndex = 9;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Home";
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.VisitedLinkColor = Color.White;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // headingNav
            // 
            headingNav.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headingNav.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            headingNav.ForeColor = Color.White;
            headingNav.Location = new Point(11, 27);
            headingNav.Name = "headingNav";
            headingNav.Size = new Size(167, 32);
            headingNav.TabIndex = 0;
            headingNav.Text = "SafeGuard";
            headingNav.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(45, 45, 48);
            panelContent.Controls.Add(viewAllFilesButton);
            panelContent.Controls.Add(panelDropPasteTarget);
            panelContent.Controls.Add(label13);
            panelContent.Controls.Add(flowLayoutPanelRecentFiles);
            panelContent.Controls.Add(label3);
            panelContent.Controls.Add(btnUpload);
            panelContent.Controls.Add(label2);
            panelContent.Controls.Add(label1);
            panelContent.Dock = DockStyle.Fill;
            panelContent.ForeColor = Color.White;
            panelContent.Location = new Point(192, 0);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(794, 709);
            panelContent.TabIndex = 1;
            // 
            // viewAllFilesButton
            // 
            viewAllFilesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            viewAllFilesButton.BackColor = Color.Black;
            viewAllFilesButton.FlatAppearance.BorderColor = Color.DimGray;
            viewAllFilesButton.FlatStyle = FlatStyle.Flat;
            viewAllFilesButton.Font = new Font("Segoe UI", 12F);
            viewAllFilesButton.ForeColor = Color.White;
            viewAllFilesButton.Location = new Point(332, 607);
            viewAllFilesButton.Margin = new Padding(3, 4, 3, 4);
            viewAllFilesButton.Name = "viewAllFilesButton";
            viewAllFilesButton.Size = new Size(117, 49);
            viewAllFilesButton.TabIndex = 6;
            viewAllFilesButton.Text = "View All";
            viewAllFilesButton.UseVisualStyleBackColor = false;
            viewAllFilesButton.Click += viewAllFilesButton_Click;
            // 
            // panelDropPasteTarget
            // 
            panelDropPasteTarget.AllowDrop = true;
            panelDropPasteTarget.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelDropPasteTarget.BackColor = Color.FromArgb(60, 60, 60);
            panelDropPasteTarget.BorderStyle = BorderStyle.FixedSingle;
            panelDropPasteTarget.Controls.Add(lblDropPasteHint);
            panelDropPasteTarget.ForeColor = Color.DarkGray;
            panelDropPasteTarget.Location = new Point(267, 312);
            panelDropPasteTarget.Margin = new Padding(3, 4, 3, 4);
            panelDropPasteTarget.Name = "panelDropPasteTarget";
            panelDropPasteTarget.Size = new Size(247, 70);
            panelDropPasteTarget.TabIndex = 7;
            panelDropPasteTarget.DragDrop += panelDropPasteTarget_DragDrop;
            panelDropPasteTarget.DragEnter += panelDropPasteTarget_DragEnter;
            // 
            // lblDropPasteHint
            // 
            lblDropPasteHint.Dock = DockStyle.Fill;
            lblDropPasteHint.Enabled = false;
            lblDropPasteHint.ForeColor = Color.DarkGray;
            lblDropPasteHint.Location = new Point(0, 0);
            lblDropPasteHint.Name = "lblDropPasteHint";
            lblDropPasteHint.Size = new Size(245, 68);
            lblDropPasteHint.TabIndex = 0;
            lblDropPasteHint.Text = "or drag && drop / paste image from clipboard here";
            lblDropPasteHint.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label13.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.White;
            label13.Location = new Point(0, 189);
            label13.Name = "label13";
            label13.Size = new Size(782, 35);
            label13.TabIndex = 5;
            label13.Text = "File Management";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelRecentFiles
            // 
            flowLayoutPanelRecentFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelRecentFiles.AutoScroll = true;
            flowLayoutPanelRecentFiles.BackColor = Color.FromArgb(45, 45, 48);
            flowLayoutPanelRecentFiles.ForeColor = Color.White;
            flowLayoutPanelRecentFiles.Location = new Point(187, 478);
            flowLayoutPanelRecentFiles.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelRecentFiles.Name = "flowLayoutPanelRecentFiles";
            flowLayoutPanelRecentFiles.Size = new Size(485, 112);
            flowLayoutPanelRecentFiles.TabIndex = 4;
            flowLayoutPanelRecentFiles.WrapContents = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(0, 425);
            label3.Name = "label3";
            label3.Size = new Size(782, 35);
            label3.TabIndex = 3;
            label3.Text = "Recent Files";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnUpload
            // 
            btnUpload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnUpload.BackColor = Color.Black;
            btnUpload.FlatAppearance.BorderColor = Color.DimGray;
            btnUpload.FlatStyle = FlatStyle.Flat;
            btnUpload.Font = new Font("Segoe UI", 12F);
            btnUpload.ForeColor = Color.White;
            btnUpload.Location = new Point(332, 242);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(117, 49);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(0, 124);
            label2.Name = "label2";
            label2.Size = new Size(782, 28);
            label2.TabIndex = 1;
            label2.Text = "Manage your files efficiently";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 57);
            label1.Name = "label1";
            label1.Size = new Size(782, 41);
            label1.TabIndex = 0;
            label1.Text = "Dashboard";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelFileManagement
            // 
            panelFileManagement.BackColor = Color.FromArgb(45, 45, 48);
            panelFileManagement.Controls.Add(label15);
            panelFileManagement.Controls.Add(cmbTables);
            panelFileManagement.Controls.Add(dataGridViewFiles);
            panelFileManagement.Controls.Add(label14);
            panelFileManagement.Dock = DockStyle.Fill;
            panelFileManagement.ForeColor = Color.White;
            panelFileManagement.Location = new Point(192, 0);
            panelFileManagement.Margin = new Padding(3, 4, 3, 4);
            panelFileManagement.Name = "panelFileManagement";
            panelFileManagement.Size = new Size(794, 709);
            panelFileManagement.TabIndex = 7;
            panelFileManagement.Visible = false;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label15.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.White;
            label15.Location = new Point(6, 133);
            label15.Name = "label15";
            label15.Size = new Size(782, 28);
            label15.TabIndex = 4;
            label15.Text = "Select File Type";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbTables
            // 
            cmbTables.Anchor = AnchorStyles.Top;
            cmbTables.BackColor = Color.FromArgb(60, 60, 60);
            cmbTables.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTables.FlatStyle = FlatStyle.Flat;
            cmbTables.ForeColor = Color.White;
            cmbTables.FormattingEnabled = true;
            cmbTables.Location = new Point(328, 184);
            cmbTables.Margin = new Padding(3, 4, 3, 4);
            cmbTables.Name = "cmbTables";
            cmbTables.Size = new Size(138, 28);
            cmbTables.TabIndex = 3;
            // 
            // dataGridViewFiles
            // 
            dataGridViewFiles.AllowUserToAddRows = false;
            dataGridViewFiles.AllowUserToDeleteRows = false;
            dataGridViewFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewFiles.BackgroundColor = Color.FromArgb(45, 45, 48);
            dataGridViewFiles.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewFiles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewFiles.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewFiles.EnableHeadersVisualStyles = false;
            dataGridViewFiles.GridColor = Color.DimGray;
            dataGridViewFiles.Location = new Point(40, 256);
            dataGridViewFiles.Margin = new Padding(40, 4, 40, 4);
            dataGridViewFiles.Name = "dataGridViewFiles";
            dataGridViewFiles.ReadOnly = true;
            dataGridViewFiles.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.Black;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridViewFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewFiles.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewFiles.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFiles.Size = new Size(714, 409);
            dataGridViewFiles.TabIndex = 2;
            dataGridViewFiles.CellDoubleClick += dataGridViewFiles_CellDoubleClick;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label14.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.White;
            label14.Location = new Point(6, 60);
            label14.Name = "label14";
            label14.Size = new Size(782, 41);
            label14.TabIndex = 1;
            label14.Text = "File Management";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelEncryptionSettings
            // 
            panelEncryptionSettings.BackColor = Color.FromArgb(45, 45, 48);
            panelEncryptionSettings.BorderStyle = BorderStyle.FixedSingle;
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
            panelEncryptionSettings.ForeColor = Color.White;
            panelEncryptionSettings.Location = new Point(192, 0);
            panelEncryptionSettings.Margin = new Padding(3, 4, 3, 4);
            panelEncryptionSettings.Name = "panelEncryptionSettings";
            panelEncryptionSettings.Size = new Size(794, 709);
            panelEncryptionSettings.TabIndex = 5;
            panelEncryptionSettings.Visible = false;
            // 
            // txtEncryptSelection
            // 
            txtEncryptSelection.Anchor = AnchorStyles.Top;
            txtEncryptSelection.BackColor = Color.FromArgb(60, 60, 60);
            txtEncryptSelection.BorderStyle = BorderStyle.FixedSingle;
            txtEncryptSelection.ForeColor = Color.White;
            txtEncryptSelection.Location = new Point(114, 296);
            txtEncryptSelection.Name = "txtEncryptSelection";
            txtEncryptSelection.ReadOnly = true;
            txtEncryptSelection.Size = new Size(121, 27);
            txtEncryptSelection.TabIndex = 7;
            txtEncryptSelection.TabStop = false;
            txtEncryptSelection.Text = "No selection";
            // 
            // btnEncryptDropdown
            // 
            btnEncryptDropdown.Anchor = AnchorStyles.Top;
            btnEncryptDropdown.BackColor = Color.FromArgb(70, 70, 70);
            btnEncryptDropdown.FlatAppearance.BorderSize = 0;
            btnEncryptDropdown.FlatStyle = FlatStyle.Flat;
            btnEncryptDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEncryptDropdown.ForeColor = Color.White;
            btnEncryptDropdown.Location = new Point(236, 295);
            btnEncryptDropdown.Name = "btnEncryptDropdown";
            btnEncryptDropdown.Size = new Size(30, 29);
            btnEncryptDropdown.TabIndex = 10;
            btnEncryptDropdown.Text = "▼";
            btnEncryptDropdown.UseVisualStyleBackColor = false;
            btnEncryptDropdown.Click += btnDropdown_Click;
            // 
            // checkedListBoxEncrypt
            // 
            checkedListBoxEncrypt.Anchor = AnchorStyles.Top;
            checkedListBoxEncrypt.BackColor = Color.FromArgb(60, 60, 60);
            checkedListBoxEncrypt.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxEncrypt.CheckOnClick = true;
            checkedListBoxEncrypt.ForeColor = Color.White;
            checkedListBoxEncrypt.FormattingEnabled = true;
            checkedListBoxEncrypt.Location = new Point(114, 325);
            checkedListBoxEncrypt.Name = "checkedListBoxEncrypt";
            checkedListBoxEncrypt.Size = new Size(153, 68);
            checkedListBoxEncrypt.TabIndex = 11;
            checkedListBoxEncrypt.Visible = false;
            checkedListBoxEncrypt.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxEncrypt.Leave += checkedListBox_Leave;
            // 
            // encryptButton
            // 
            encryptButton.Anchor = AnchorStyles.Top;
            encryptButton.BackColor = Color.Black;
            encryptButton.FlatAppearance.BorderColor = Color.DimGray;
            encryptButton.FlatStyle = FlatStyle.Flat;
            encryptButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            encryptButton.ForeColor = Color.White;
            encryptButton.Location = new Point(343, 440);
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
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(137, 256);
            label8.Name = "label8";
            label8.Size = new Size(108, 28);
            label8.TabIndex = 8;
            label8.Text = "Select File";
            // 
            // encryptionKeyBox
            // 
            encryptionKeyBox.Anchor = AnchorStyles.Top;
            encryptionKeyBox.BackColor = Color.FromArgb(60, 60, 60);
            encryptionKeyBox.BorderStyle = BorderStyle.FixedSingle;
            encryptionKeyBox.ForeColor = Color.White;
            encryptionKeyBox.Location = new Point(548, 296);
            encryptionKeyBox.Margin = new Padding(3, 4, 3, 4);
            encryptionKeyBox.Name = "encryptionKeyBox";
            encryptionKeyBox.Size = new Size(114, 27);
            encryptionKeyBox.TabIndex = 6;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(526, 256);
            label7.Name = "label7";
            label7.Size = new Size(156, 28);
            label7.TabIndex = 5;
            label7.Text = "Encryption Key\r\n";
            // 
            // label
            // 
            label.Anchor = AnchorStyles.Top;
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.ForeColor = Color.White;
            label.Location = new Point(302, 256);
            label.Name = "label";
            label.Size = new Size(195, 28);
            label.TabIndex = 3;
            label.Text = "Encryption Method";
            // 
            // encryptionMethodSelection
            // 
            encryptionMethodSelection.Anchor = AnchorStyles.Top;
            encryptionMethodSelection.BackColor = Color.FromArgb(60, 60, 60);
            encryptionMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            encryptionMethodSelection.FlatStyle = FlatStyle.Flat;
            encryptionMethodSelection.ForeColor = Color.White;
            encryptionMethodSelection.FormattingEnabled = true;
            encryptionMethodSelection.Items.AddRange(new object[] { "AES", "Pixel Scrambling" });
            encryptionMethodSelection.Location = new Point(330, 296);
            encryptionMethodSelection.Margin = new Padding(3, 4, 3, 4);
            encryptionMethodSelection.Name = "encryptionMethodSelection";
            encryptionMethodSelection.Size = new Size(138, 28);
            encryptionMethodSelection.TabIndex = 2;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.White;
            label5.Location = new Point(6, 127);
            label5.Name = "label5";
            label5.Size = new Size(782, 28);
            label5.TabIndex = 1;
            label5.Text = "Select Encryption Method and Set Key";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(6, 60);
            label4.Name = "label4";
            label4.Size = new Size(782, 41);
            label4.TabIndex = 0;
            label4.Text = "File Encryption";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // decryptionPanel
            // 
            decryptionPanel.BackColor = Color.FromArgb(45, 45, 48);
            decryptionPanel.BorderStyle = BorderStyle.FixedSingle;
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
            decryptionPanel.ForeColor = Color.White;
            decryptionPanel.Location = new Point(192, 0);
            decryptionPanel.Margin = new Padding(3, 4, 3, 4);
            decryptionPanel.Name = "decryptionPanel";
            decryptionPanel.Size = new Size(794, 709);
            decryptionPanel.TabIndex = 6;
            decryptionPanel.Visible = false;
            // 
            // txtDecryptSelection
            // 
            txtDecryptSelection.Anchor = AnchorStyles.Top;
            txtDecryptSelection.BackColor = Color.FromArgb(60, 60, 60);
            txtDecryptSelection.BorderStyle = BorderStyle.FixedSingle;
            txtDecryptSelection.ForeColor = Color.White;
            txtDecryptSelection.Location = new Point(106, 293);
            txtDecryptSelection.Name = "txtDecryptSelection";
            txtDecryptSelection.ReadOnly = true;
            txtDecryptSelection.Size = new Size(121, 27);
            txtDecryptSelection.TabIndex = 7;
            txtDecryptSelection.TabStop = false;
            txtDecryptSelection.Text = "No selection";
            // 
            // btnDecryptDropdown
            // 
            btnDecryptDropdown.Anchor = AnchorStyles.Top;
            btnDecryptDropdown.BackColor = Color.FromArgb(70, 70, 70);
            btnDecryptDropdown.FlatAppearance.BorderSize = 0;
            btnDecryptDropdown.FlatStyle = FlatStyle.Flat;
            btnDecryptDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDecryptDropdown.ForeColor = Color.White;
            btnDecryptDropdown.Location = new Point(228, 292);
            btnDecryptDropdown.Name = "btnDecryptDropdown";
            btnDecryptDropdown.Size = new Size(30, 29);
            btnDecryptDropdown.TabIndex = 10;
            btnDecryptDropdown.Text = "▼";
            btnDecryptDropdown.UseVisualStyleBackColor = false;
            btnDecryptDropdown.Click += btnDropdown_Click;
            // 
            // checkedListBoxDecrypt
            // 
            checkedListBoxDecrypt.Anchor = AnchorStyles.Top;
            checkedListBoxDecrypt.BackColor = Color.FromArgb(60, 60, 60);
            checkedListBoxDecrypt.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxDecrypt.CheckOnClick = true;
            checkedListBoxDecrypt.ForeColor = Color.White;
            checkedListBoxDecrypt.FormattingEnabled = true;
            checkedListBoxDecrypt.Location = new Point(106, 323);
            checkedListBoxDecrypt.Name = "checkedListBoxDecrypt";
            checkedListBoxDecrypt.Size = new Size(153, 68);
            checkedListBoxDecrypt.TabIndex = 11;
            checkedListBoxDecrypt.Visible = false;
            checkedListBoxDecrypt.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxDecrypt.Leave += checkedListBox_Leave;
            // 
            // decryptButton
            // 
            decryptButton.Anchor = AnchorStyles.Top;
            decryptButton.BackColor = Color.Black;
            decryptButton.FlatAppearance.BorderColor = Color.DimGray;
            decryptButton.FlatStyle = FlatStyle.Flat;
            decryptButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            decryptButton.ForeColor = Color.White;
            decryptButton.Location = new Point(343, 439);
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
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(129, 253);
            label6.Name = "label6";
            label6.Size = new Size(108, 28);
            label6.TabIndex = 8;
            label6.Text = "Select File";
            // 
            // decryptionKey
            // 
            decryptionKey.Anchor = AnchorStyles.Top;
            decryptionKey.BackColor = Color.FromArgb(60, 60, 60);
            decryptionKey.BorderStyle = BorderStyle.FixedSingle;
            decryptionKey.ForeColor = Color.White;
            decryptionKey.Location = new Point(540, 293);
            decryptionKey.Margin = new Padding(3, 4, 3, 4);
            decryptionKey.Name = "decryptionKey";
            decryptionKey.Size = new Size(114, 27);
            decryptionKey.TabIndex = 6;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(518, 253);
            label9.Name = "label9";
            label9.Size = new Size(159, 28);
            label9.TabIndex = 5;
            label9.Text = "Decryption Key\r\n";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(294, 253);
            label10.Name = "label10";
            label10.Size = new Size(198, 28);
            label10.TabIndex = 3;
            label10.Text = "Decryption Method";
            // 
            // decryptionMethodSelection
            // 
            decryptionMethodSelection.Anchor = AnchorStyles.Top;
            decryptionMethodSelection.BackColor = Color.FromArgb(60, 60, 60);
            decryptionMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            decryptionMethodSelection.FlatStyle = FlatStyle.Flat;
            decryptionMethodSelection.ForeColor = Color.White;
            decryptionMethodSelection.FormattingEnabled = true;
            decryptionMethodSelection.Location = new Point(322, 293);
            decryptionMethodSelection.Margin = new Padding(3, 4, 3, 4);
            decryptionMethodSelection.Name = "decryptionMethodSelection";
            decryptionMethodSelection.Size = new Size(138, 28);
            decryptionMethodSelection.TabIndex = 2;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = Color.White;
            label11.Location = new Point(6, 127);
            label11.Name = "label11";
            label11.Size = new Size(782, 28);
            label11.TabIndex = 1;
            label11.Text = "Select Decryption Method and Provide Key";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label12.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White;
            label12.Location = new Point(6, 60);
            label12.Name = "label12";
            label12.Size = new Size(782, 41);
            label12.TabIndex = 0;
            label12.Text = "File Decryption";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // compressionPanel
            // 
            compressionPanel.BackColor = Color.FromArgb(45, 45, 48);
            compressionPanel.BorderStyle = BorderStyle.FixedSingle;
            compressionPanel.Controls.Add(txtCompressSelection);
            compressionPanel.Controls.Add(btnCompressDropdown);
            compressionPanel.Controls.Add(checkedListBoxCompress);
            compressionPanel.Controls.Add(compressButton);
            compressionPanel.Controls.Add(compressionFileLabel);
            compressionPanel.Controls.Add(compressionDescLabel);
            compressionPanel.Controls.Add(compressionTitleLabel);
            compressionPanel.Dock = DockStyle.Fill;
            compressionPanel.ForeColor = Color.White;
            compressionPanel.Location = new Point(192, 0);
            compressionPanel.Margin = new Padding(3, 4, 3, 4);
            compressionPanel.Name = "compressionPanel";
            compressionPanel.Size = new Size(794, 709);
            compressionPanel.TabIndex = 8;
            compressionPanel.Visible = false;
            // 
            // txtCompressSelection
            // 
            txtCompressSelection.Anchor = AnchorStyles.Top;
            txtCompressSelection.BackColor = Color.FromArgb(60, 60, 60);
            txtCompressSelection.BorderStyle = BorderStyle.FixedSingle;
            txtCompressSelection.ForeColor = Color.White;
            txtCompressSelection.Location = new Point(322, 312);
            txtCompressSelection.Name = "txtCompressSelection";
            txtCompressSelection.ReadOnly = true;
            txtCompressSelection.Size = new Size(121, 27);
            txtCompressSelection.TabIndex = 3;
            txtCompressSelection.TabStop = false;
            txtCompressSelection.Text = "No selection";
            // 
            // btnCompressDropdown
            // 
            btnCompressDropdown.Anchor = AnchorStyles.Top;
            btnCompressDropdown.BackColor = Color.FromArgb(70, 70, 70);
            btnCompressDropdown.FlatAppearance.BorderSize = 0;
            btnCompressDropdown.FlatStyle = FlatStyle.Flat;
            btnCompressDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCompressDropdown.ForeColor = Color.White;
            btnCompressDropdown.Location = new Point(444, 311);
            btnCompressDropdown.Name = "btnCompressDropdown";
            btnCompressDropdown.Size = new Size(30, 29);
            btnCompressDropdown.TabIndex = 5;
            btnCompressDropdown.Text = "▼";
            btnCompressDropdown.UseVisualStyleBackColor = false;
            btnCompressDropdown.Click += btnDropdown_Click;
            // 
            // checkedListBoxCompress
            // 
            checkedListBoxCompress.Anchor = AnchorStyles.Top;
            checkedListBoxCompress.BackColor = Color.FromArgb(60, 60, 60);
            checkedListBoxCompress.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxCompress.CheckOnClick = true;
            checkedListBoxCompress.ForeColor = Color.White;
            checkedListBoxCompress.FormattingEnabled = true;
            checkedListBoxCompress.Location = new Point(322, 343);
            checkedListBoxCompress.Name = "checkedListBoxCompress";
            checkedListBoxCompress.Size = new Size(153, 68);
            checkedListBoxCompress.TabIndex = 6;
            checkedListBoxCompress.Visible = false;
            checkedListBoxCompress.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxCompress.Leave += checkedListBox_Leave;
            // 
            // compressButton
            // 
            compressButton.Anchor = AnchorStyles.Top;
            compressButton.BackColor = Color.Black;
            compressButton.FlatAppearance.BorderColor = Color.DimGray;
            compressButton.FlatStyle = FlatStyle.Flat;
            compressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressButton.ForeColor = Color.White;
            compressButton.Location = new Point(333, 440);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(129, 51);
            compressButton.TabIndex = 4;
            compressButton.Text = "Optimize";
            compressButton.UseVisualStyleBackColor = false;
            compressButton.Click += compressButton_Click;
            // 
            // compressionFileLabel
            // 
            compressionFileLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            compressionFileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressionFileLabel.ForeColor = Color.White;
            compressionFileLabel.Location = new Point(6, 253);
            compressionFileLabel.Name = "compressionFileLabel";
            compressionFileLabel.Size = new Size(782, 28);
            compressionFileLabel.TabIndex = 2;
            compressionFileLabel.Text = "Select File";
            compressionFileLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // compressionDescLabel
            // 
            compressionDescLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            compressionDescLabel.Font = new Font("Segoe UI", 12F);
            compressionDescLabel.ForeColor = Color.White;
            compressionDescLabel.Location = new Point(6, 127);
            compressionDescLabel.Name = "compressionDescLabel";
            compressionDescLabel.Size = new Size(782, 28);
            compressionDescLabel.TabIndex = 1;
            compressionDescLabel.Text = "Optimize your image files (JPG, PNG, BMP)";
            compressionDescLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // compressionTitleLabel
            // 
            compressionTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            compressionTitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            compressionTitleLabel.ForeColor = Color.White;
            compressionTitleLabel.Location = new Point(6, 60);
            compressionTitleLabel.Name = "compressionTitleLabel";
            compressionTitleLabel.Size = new Size(782, 41);
            compressionTitleLabel.TabIndex = 0;
            compressionTitleLabel.Text = "File Compression";
            compressionTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // decompressionPanel
            // 
            decompressionPanel.BackColor = Color.FromArgb(45, 45, 48);
            decompressionPanel.BorderStyle = BorderStyle.FixedSingle;
            decompressionPanel.Controls.Add(txtDecompressSelection);
            decompressionPanel.Controls.Add(btnDecompressDropdown);
            decompressionPanel.Controls.Add(checkedListBoxDecompress);
            decompressionPanel.Controls.Add(decompressButton);
            decompressionPanel.Controls.Add(decompressionFileLabel);
            decompressionPanel.Controls.Add(decompressionDescLabel);
            decompressionPanel.Controls.Add(decompressionTitleLabel);
            decompressionPanel.Dock = DockStyle.Fill;
            decompressionPanel.ForeColor = Color.White;
            decompressionPanel.Location = new Point(192, 0);
            decompressionPanel.Margin = new Padding(3, 4, 3, 4);
            decompressionPanel.Name = "decompressionPanel";
            decompressionPanel.Size = new Size(794, 709);
            decompressionPanel.TabIndex = 9;
            decompressionPanel.Visible = false;
            // 
            // txtDecompressSelection
            // 
            txtDecompressSelection.Anchor = AnchorStyles.Top;
            txtDecompressSelection.BackColor = Color.FromArgb(60, 60, 60);
            txtDecompressSelection.BorderStyle = BorderStyle.FixedSingle;
            txtDecompressSelection.ForeColor = Color.White;
            txtDecompressSelection.Location = new Point(322, 312);
            txtDecompressSelection.Name = "txtDecompressSelection";
            txtDecompressSelection.ReadOnly = true;
            txtDecompressSelection.Size = new Size(121, 27);
            txtDecompressSelection.TabIndex = 3;
            txtDecompressSelection.TabStop = false;
            txtDecompressSelection.Text = "No selection";
            // 
            // btnDecompressDropdown
            // 
            btnDecompressDropdown.Anchor = AnchorStyles.Top;
            btnDecompressDropdown.BackColor = Color.FromArgb(70, 70, 70);
            btnDecompressDropdown.FlatAppearance.BorderSize = 0;
            btnDecompressDropdown.FlatStyle = FlatStyle.Flat;
            btnDecompressDropdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDecompressDropdown.ForeColor = Color.White;
            btnDecompressDropdown.Location = new Point(444, 311);
            btnDecompressDropdown.Name = "btnDecompressDropdown";
            btnDecompressDropdown.Size = new Size(30, 29);
            btnDecompressDropdown.TabIndex = 5;
            btnDecompressDropdown.Text = "▼";
            btnDecompressDropdown.UseVisualStyleBackColor = false;
            btnDecompressDropdown.Click += btnDropdown_Click;
            // 
            // checkedListBoxDecompress
            // 
            checkedListBoxDecompress.Anchor = AnchorStyles.Top;
            checkedListBoxDecompress.BackColor = Color.FromArgb(60, 60, 60);
            checkedListBoxDecompress.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxDecompress.CheckOnClick = true;
            checkedListBoxDecompress.ForeColor = Color.White;
            checkedListBoxDecompress.FormattingEnabled = true;
            checkedListBoxDecompress.Location = new Point(322, 343);
            checkedListBoxDecompress.Name = "checkedListBoxDecompress";
            checkedListBoxDecompress.Size = new Size(153, 68);
            checkedListBoxDecompress.TabIndex = 6;
            checkedListBoxDecompress.Visible = false;
            checkedListBoxDecompress.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxDecompress.Leave += checkedListBox_Leave;
            // 
            // decompressButton
            // 
            decompressButton.Anchor = AnchorStyles.Top;
            decompressButton.BackColor = Color.Black;
            decompressButton.FlatAppearance.BorderColor = Color.DimGray;
            decompressButton.FlatStyle = FlatStyle.Flat;
            decompressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            decompressButton.ForeColor = Color.White;
            decompressButton.Location = new Point(320, 440);
            decompressButton.Name = "decompressButton";
            decompressButton.Size = new Size(155, 51);
            decompressButton.TabIndex = 4;
            decompressButton.Text = "Decompress";
            decompressButton.UseVisualStyleBackColor = false;
            decompressButton.Click += decompressButton_Click;
            // 
            // decompressionFileLabel
            // 
            decompressionFileLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            decompressionFileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            decompressionFileLabel.ForeColor = Color.White;
            decompressionFileLabel.Location = new Point(6, 253);
            decompressionFileLabel.Name = "decompressionFileLabel";
            decompressionFileLabel.Size = new Size(782, 28);
            decompressionFileLabel.TabIndex = 2;
            decompressionFileLabel.Text = "Select Optimised File";
            decompressionFileLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // decompressionDescLabel
            // 
            decompressionDescLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            decompressionDescLabel.Font = new Font("Segoe UI", 12F);
            decompressionDescLabel.ForeColor = Color.White;
            decompressionDescLabel.Location = new Point(6, 127);
            decompressionDescLabel.Name = "decompressionDescLabel";
            decompressionDescLabel.Size = new Size(782, 28);
            decompressionDescLabel.TabIndex = 1;
            decompressionDescLabel.Text = "Restore optimized files to their original format";
            decompressionDescLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // decompressionTitleLabel
            // 
            decompressionTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            decompressionTitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            decompressionTitleLabel.ForeColor = Color.White;
            decompressionTitleLabel.Location = new Point(6, 60);
            decompressionTitleLabel.Name = "decompressionTitleLabel";
            decompressionTitleLabel.Size = new Size(782, 41);
            decompressionTitleLabel.TabIndex = 0;
            decompressionTitleLabel.Text = "File Decompression";
            decompressionTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRemoveFiles
            // 
            panelRemoveFiles.BackColor = Color.FromArgb(45, 45, 48);
            panelRemoveFiles.Controls.Add(buttonDeleteSelectedFiles);
            panelRemoveFiles.Controls.Add(dropdownRemoveSelector);
            panelRemoveFiles.Controls.Add(txtRemoveSelect);
            panelRemoveFiles.Controls.Add(checkedListBoxRemove);
            panelRemoveFiles.Controls.Add(label17);
            panelRemoveFiles.Controls.Add(label16);
            panelRemoveFiles.Dock = DockStyle.Fill;
            panelRemoveFiles.ForeColor = Color.White;
            panelRemoveFiles.Location = new Point(192, 0);
            panelRemoveFiles.Margin = new Padding(3, 4, 3, 4);
            panelRemoveFiles.Name = "panelRemoveFiles";
            panelRemoveFiles.Size = new Size(794, 709);
            panelRemoveFiles.TabIndex = 10;
            panelRemoveFiles.Visible = false;
            // 
            // buttonDeleteSelectedFiles
            // 
            buttonDeleteSelectedFiles.Anchor = AnchorStyles.Top;
            buttonDeleteSelectedFiles.BackColor = Color.Black;
            buttonDeleteSelectedFiles.FlatAppearance.BorderColor = Color.DimGray;
            buttonDeleteSelectedFiles.FlatStyle = FlatStyle.Flat;
            buttonDeleteSelectedFiles.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonDeleteSelectedFiles.ForeColor = Color.White;
            buttonDeleteSelectedFiles.Location = new Point(343, 417);
            buttonDeleteSelectedFiles.Margin = new Padding(3, 4, 3, 4);
            buttonDeleteSelectedFiles.Name = "buttonDeleteSelectedFiles";
            buttonDeleteSelectedFiles.Size = new Size(109, 55);
            buttonDeleteSelectedFiles.TabIndex = 15;
            buttonDeleteSelectedFiles.Text = "Remove";
            buttonDeleteSelectedFiles.UseVisualStyleBackColor = false;
            buttonDeleteSelectedFiles.Click += btnDeleteSelectedFiles_Click;
            // 
            // dropdownRemoveSelector
            // 
            dropdownRemoveSelector.Anchor = AnchorStyles.Top;
            dropdownRemoveSelector.BackColor = Color.FromArgb(70, 70, 70);
            dropdownRemoveSelector.FlatAppearance.BorderSize = 0;
            dropdownRemoveSelector.FlatStyle = FlatStyle.Flat;
            dropdownRemoveSelector.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dropdownRemoveSelector.ForeColor = Color.White;
            dropdownRemoveSelector.Location = new Point(444, 288);
            dropdownRemoveSelector.Name = "dropdownRemoveSelector";
            dropdownRemoveSelector.Size = new Size(30, 29);
            dropdownRemoveSelector.TabIndex = 14;
            dropdownRemoveSelector.Text = "▼";
            dropdownRemoveSelector.UseVisualStyleBackColor = false;
            dropdownRemoveSelector.Click += btnDropdown_Click;
            // 
            // txtRemoveSelect
            // 
            txtRemoveSelect.Anchor = AnchorStyles.Top;
            txtRemoveSelect.BackColor = Color.FromArgb(60, 60, 60);
            txtRemoveSelect.BorderStyle = BorderStyle.FixedSingle;
            txtRemoveSelect.ForeColor = Color.White;
            txtRemoveSelect.Location = new Point(322, 289);
            txtRemoveSelect.Name = "txtRemoveSelect";
            txtRemoveSelect.ReadOnly = true;
            txtRemoveSelect.Size = new Size(121, 27);
            txtRemoveSelect.TabIndex = 13;
            txtRemoveSelect.TabStop = false;
            txtRemoveSelect.Text = "No selection";
            // 
            // checkedListBoxRemove
            // 
            checkedListBoxRemove.Anchor = AnchorStyles.Top;
            checkedListBoxRemove.BackColor = Color.FromArgb(60, 60, 60);
            checkedListBoxRemove.BorderStyle = BorderStyle.FixedSingle;
            checkedListBoxRemove.CheckOnClick = true;
            checkedListBoxRemove.ForeColor = Color.White;
            checkedListBoxRemove.FormattingEnabled = true;
            checkedListBoxRemove.Location = new Point(322, 319);
            checkedListBoxRemove.Name = "checkedListBoxRemove";
            checkedListBoxRemove.Size = new Size(153, 68);
            checkedListBoxRemove.TabIndex = 12;
            checkedListBoxRemove.Visible = false;
            checkedListBoxRemove.ItemCheck += checkedListBox_ItemCheck;
            checkedListBoxRemove.Leave += checkedListBox_Leave;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label17.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.White;
            label17.Location = new Point(6, 91);
            label17.Name = "label17";
            label17.Size = new Size(782, 41);
            label17.TabIndex = 6;
            label17.Text = "Remove a File";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.ForeColor = Color.White;
            label16.Location = new Point(6, 229);
            label16.Name = "label16";
            label16.Size = new Size(782, 28);
            label16.TabIndex = 5;
            label16.Text = "Select the File";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(986, 709);
            Controls.Add(panelContent);
            Controls.Add(panelRemoveFiles);
            Controls.Add(decompressionPanel);
            Controls.Add(compressionPanel);
            Controls.Add(decryptionPanel);
            Controls.Add(panelEncryptionSettings);
            Controls.Add(panelFileManagement);
            Controls.Add(panelHeader);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(800, 600);
            Name = "Form1";
            Text = "SafeGuard File Manager";
            Load += Form1_Load;
            panelHeader.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelDropPasteTarget.ResumeLayout(false);
            panelFileManagement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).EndInit();
            panelEncryptionSettings.ResumeLayout(false);
            panelEncryptionSettings.PerformLayout();
            decryptionPanel.ResumeLayout(false);
            decryptionPanel.PerformLayout();
            compressionPanel.ResumeLayout(false);
            compressionPanel.PerformLayout();
            decompressionPanel.ResumeLayout(false);
            decompressionPanel.PerformLayout();
            panelRemoveFiles.ResumeLayout(false);
            panelRemoveFiles.PerformLayout();
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
        private LinkLabel linkLabel7;
        private Panel panelRemoveFiles;
        private Label label17;
        private Label label16;
        private CheckedListBox checkedListBoxRemove;
        private Button buttonDeleteSelectedFiles;
        private Button dropdownRemoveSelector;
        private TextBox txtRemoveSelect;
    }
}
// --- END OF UPDATED Form1.Designer.cs ---