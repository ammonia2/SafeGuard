﻿using System.Data.SqlClient;
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
        private void InitializeComponent()
        {
            panelHeader = new Panel();
            linkLabel4 = new LinkLabel();
            searchBtn = new Button();
            txtSearch = new TextBox();
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
            fileSelectionEncryption = new ComboBox();
            encryptionKeyBox = new TextBox();
            label7 = new Label();
            label = new Label();
            encryptionMethodSelection = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            decryptionPanel = new Panel();
            decryptButton = new Button();
            label6 = new Label();
            decryptionFileSelection = new ComboBox();
            decryptionKey = new TextBox();
            label9 = new Label();
            label10 = new Label();
            decryptionMethodSelection = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            linkLabel5 = new LinkLabel();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelFileManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).BeginInit();
            panelEncryptionSettings.SuspendLayout();
            decryptionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.LightGray;
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(linkLabel5);
            panelHeader.Controls.Add(linkLabel4);
            panelHeader.Controls.Add(searchBtn);
            panelHeader.Controls.Add(txtSearch);
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
            panelHeader.Paint += panelHeader_Paint;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.LinkColor = Color.Black;
            linkLabel4.Location = new Point(706, 24);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(38, 20);
            linkLabel4.TabIndex = 6;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Files";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            // 
            // searchBtn
            // 
            searchBtn.Location = new Point(890, 17);
            searchBtn.Margin = new Padding(3, 4, 3, 4);
            searchBtn.Name = "searchBtn";
            searchBtn.Size = new Size(67, 31);
            searchBtn.TabIndex = 5;
            searchBtn.Text = "search";
            searchBtn.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(769, 17);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(114, 27);
            txtSearch.TabIndex = 4;
            txtSearch.Text = "Search File";
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Cursor = Cursors.Hand;
            linkLabel3.LinkColor = Color.Black;
            linkLabel3.Location = new Point(410, 24);
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
            linkLabel2.Location = new Point(510, 24);
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
            linkLabel1.Location = new Point(343, 24);
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
            btnUpload.Location = new Point(425, 288);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(126, 49);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;
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
            cmbTables.Items.AddRange(new object[] { "All Files", "Encrypted Files" });
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
            dataGridViewFiles.RowHeadersWidth = 51;
            dataGridViewFiles.Size = new Size(626, 348);
            dataGridViewFiles.TabIndex = 2;
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
            fileSelectionEncryption.DropDownStyle = ComboBoxStyle.DropDownList;
            fileSelectionEncryption.FormattingEnabled = true;
            fileSelectionEncryption.Location = new Point(207, 312);
            fileSelectionEncryption.Margin = new Padding(3, 4, 3, 4);
            fileSelectionEncryption.Name = "fileSelectionEncryption";
            fileSelectionEncryption.Size = new Size(138, 28);
            fileSelectionEncryption.TabIndex = 7;
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
            decryptionFileSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            decryptionFileSelection.FormattingEnabled = true;
            decryptionFileSelection.Location = new Point(207, 312);
            decryptionFileSelection.Margin = new Padding(3, 4, 3, 4);
            decryptionFileSelection.Name = "decryptionFileSelection";
            decryptionFileSelection.Size = new Size(138, 28);
            decryptionFileSelection.TabIndex = 7;
            // 
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
            decryptionMethodSelection.Items.AddRange(new object[] { "AES", "Pixel Scrambling" });
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
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Cursor = Cursors.Hand;
            linkLabel5.LinkColor = Color.Black;
            linkLabel5.Location = new Point(605, 24);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(95, 20);
            linkLabel5.TabIndex = 7;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "Compression";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 709);
            Controls.Add(panelFileManagement);
            Controls.Add(panelContent);
            Controls.Add(decryptionPanel);
            Controls.Add(panelEncryptionSettings);
            Controls.Add(panelHeader);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            panelFileManagement.ResumeLayout(false);
            panelFileManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).EndInit();
            panelEncryptionSettings.ResumeLayout(false);
            panelEncryptionSettings.PerformLayout();
            decryptionPanel.ResumeLayout(false);
            decryptionPanel.PerformLayout();
            ResumeLayout(false);

            // Initialize compression panel
            compressionPanel = new Panel();
            compressionPanel.Dock = DockStyle.Fill;
            compressionPanel.BorderStyle = BorderStyle.Fixed3D;
            compressionPanel.Visible = false;
            this.Controls.Add(compressionPanel);

            // Title label
            Label titleLabel = new Label();
            titleLabel.Text = "File Compression";
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            titleLabel.Location = new Point(397, 97);
            titleLabel.AutoSize = true;
            compressionPanel.Controls.Add(titleLabel);

            // Description label
            Label descLabel = new Label();
            descLabel.Text = "Compress your files to save storage space";
            descLabel.Font = new Font("Segoe UI", 12F);
            descLabel.Location = new Point(327, 163);
            descLabel.AutoSize = true;
            compressionPanel.Controls.Add(descLabel);

            // File selection label
            Label fileLabel = new Label();
            fileLabel.Text = "Select File";
            fileLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            fileLabel.Location = new Point(453, 293);
            fileLabel.AutoSize = true;
            compressionPanel.Controls.Add(fileLabel);

            // File selection dropdown
            fileSelectionCompression = new ComboBox();
            fileSelectionCompression.Location = new Point(443, 352);
            fileSelectionCompression.Size = new Size(138, 28);
            fileSelectionCompression.DropDownStyle = ComboBoxStyle.DropDownList;
            compressionPanel.Controls.Add(fileSelectionCompression);

            // Compression button
            compressButton = new Button();
            compressButton.Text = "Compress";
            compressButton.BackColor = SystemColors.ActiveCaptionText;
            compressButton.ForeColor = Color.Snow;
            compressButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            compressButton.Location = new Point(453, 423);
            compressButton.Size = new Size(119, 50);
            compressButton.Click += new EventHandler(compressButton_Click);
            compressionPanel.Controls.Add(compressButton);

           
        }

        #endregion

        private Panel panelHeader;
        private Label headingNav;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private TextBox txtSearch;
        private Button searchBtn;
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
        private Label label6;
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
        private Panel compressionPanel;
        private ComboBox fileSelectionCompression;
        private Button compressButton;
        private Label compressionLabel;
    }
}
