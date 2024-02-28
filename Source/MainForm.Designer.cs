using SkiaSharp.Views.Desktop;

namespace eft_dma_radar
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            chkNoVisorDebug = new CheckBox();
            chkOpticThermalVisionDebug = new CheckBox();
            chkNightVisionDebug = new CheckBox();
            chkThermalVisionDebug = new CheckBox();
            grpLoot = new GroupBox();
            btnRefreshLoot = new Button();
            btnApplyLoot = new Button();
            lblLootItem = new Label();
            txtLootFilter = new TextBox();
            label8 = new Label();
            label7 = new Label();
            txtImportantLootValue = new TextBox();
            txtRegularLootValue = new TextBox();
            lblLootValue = new Label();
            grpMapSetup = new GroupBox();
            btnLoot = new Button();
            chkMapFree = new CheckBox();
            btnMapSetupApply = new Button();
            txtMapSetupScale = new TextBox();
            lblMapScale = new Label();
            txtMapSetupY = new TextBox();
            lblMapXY = new Label();
            txtMapSetupX = new TextBox();
            lblMapCoords = new Label();
            tabPage2 = new TabPage();
            grpMemoryWriting = new GroupBox();
            chkNoVisor = new CheckBox();
            chkOpticThermalVision = new CheckBox();
            chkNightVision = new CheckBox();
            chkThermalVision = new CheckBox();
            grpConfig = new GroupBox();
            lblUIScale = new Label();
            trkUIScale = new TrackBar();
            chkHideNames = new CheckBox();
            txtTeammateID = new TextBox();
            lblPrimaryTeammate = new Label();
            chkShowAimview = new CheckBox();
            btnRestartGame = new Button();
            chkShowMapSetup = new CheckBox();
            chkShowLoot = new CheckBox();
            lblZoom = new Label();
            trkZoom = new TrackBar();
            lblAimline = new Label();
            trkAimLength = new TrackBar();
            btnToggleMap = new Button();
            tabPage3 = new TabPage();
            rchTxtPlayerInfo = new RichTextBox();
            tabPage4 = new TabPage();
            lstViewPMCHistory = new ListView();
            columnHeader_Entry = new ColumnHeader();
            columnHeader_ID = new ColumnHeader();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            grpLoot.SuspendLayout();
            grpMapSetup.SuspendLayout();
            tabPage2.SuspendLayout();
            grpMemoryWriting.SuspendLayout();
            grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkUIScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkZoom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkAimLength).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Controls.Add(tabPage4);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1086, 666);
            tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(chkNoVisorDebug);
            tabPage1.Controls.Add(chkOpticThermalVisionDebug);
            tabPage1.Controls.Add(chkNightVisionDebug);
            tabPage1.Controls.Add(chkThermalVisionDebug);
            tabPage1.Controls.Add(grpLoot);
            tabPage1.Controls.Add(grpMapSetup);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1078, 638);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Radar";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkNoVisorDebug
            // 
            chkNoVisorDebug.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkNoVisorDebug.AutoSize = true;
            chkNoVisorDebug.Location = new Point(968, 82);
            chkNoVisorDebug.Name = "chkNoVisorDebug";
            chkNoVisorDebug.Size = new Size(71, 19);
            chkNoVisorDebug.TabIndex = 17;
            chkNoVisorDebug.Text = "No Visor";
            chkNoVisorDebug.UseVisualStyleBackColor = true;
            chkNoVisorDebug.CheckedChanged += chkNoVisor_CheckedChanged;
            // 
            // chkOpticThermalVisionDebug
            // 
            chkOpticThermalVisionDebug.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkOpticThermalVisionDebug.AutoSize = true;
            chkOpticThermalVisionDebug.Location = new Point(968, 56);
            chkOpticThermalVisionDebug.Name = "chkOpticThermalVisionDebug";
            chkOpticThermalVisionDebug.Size = new Size(101, 19);
            chkOpticThermalVisionDebug.TabIndex = 16;
            chkOpticThermalVisionDebug.Text = "Optic Thermal";
            chkOpticThermalVisionDebug.UseVisualStyleBackColor = true;
            chkOpticThermalVisionDebug.CheckedChanged += chkOpticThermalVision_CheckedChanged;
            // 
            // chkNightVisionDebug
            // 
            chkNightVisionDebug.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkNightVisionDebug.AutoSize = true;
            chkNightVisionDebug.Location = new Point(968, 6);
            chkNightVisionDebug.Name = "chkNightVisionDebug";
            chkNightVisionDebug.Size = new Size(94, 19);
            chkNightVisionDebug.TabIndex = 14;
            chkNightVisionDebug.Text = "Night Vision ";
            chkNightVisionDebug.UseVisualStyleBackColor = true;
            chkNightVisionDebug.CheckedChanged += chkNightVision_CheckedChanged;
            // 
            // chkThermalVisionDebug
            // 
            chkThermalVisionDebug.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkThermalVisionDebug.AutoSize = true;
            chkThermalVisionDebug.Location = new Point(968, 31);
            chkThermalVisionDebug.Name = "chkThermalVisionDebug";
            chkThermalVisionDebug.Size = new Size(104, 19);
            chkThermalVisionDebug.TabIndex = 15;
            chkThermalVisionDebug.Text = "Thermal Vision";
            chkThermalVisionDebug.UseVisualStyleBackColor = true;
            chkThermalVisionDebug.CheckedChanged += chkThermalVision_CheckedChanged;
            // 
            // grpLoot
            // 
            grpLoot.Controls.Add(btnRefreshLoot);
            grpLoot.Controls.Add(btnApplyLoot);
            grpLoot.Controls.Add(lblLootItem);
            grpLoot.Controls.Add(txtLootFilter);
            grpLoot.Controls.Add(label8);
            grpLoot.Controls.Add(label7);
            grpLoot.Controls.Add(txtImportantLootValue);
            grpLoot.Controls.Add(txtRegularLootValue);
            grpLoot.Controls.Add(lblLootValue);
            grpLoot.Location = new Point(3, 11);
            grpLoot.Name = "grpLoot";
            grpLoot.Size = new Size(256, 202);
            grpLoot.TabIndex = 12;
            grpLoot.TabStop = false;
            grpLoot.Text = "Loot";
            grpLoot.Visible = false;
            // 
            // btnRefreshLoot
            // 
            btnRefreshLoot.Location = new Point(178, 34);
            btnRefreshLoot.Name = "btnRefreshLoot";
            btnRefreshLoot.Size = new Size(55, 49);
            btnRefreshLoot.TabIndex = 21;
            btnRefreshLoot.Text = "Refresh Loot";
            btnRefreshLoot.UseVisualStyleBackColor = true;
            btnRefreshLoot.Click += btnRefreshLoot_Click;
            // 
            // btnApplyLoot
            // 
            btnApplyLoot.Enabled = false;
            btnApplyLoot.Location = new Point(82, 147);
            btnApplyLoot.Name = "btnApplyLoot";
            btnApplyLoot.Size = new Size(61, 46);
            btnApplyLoot.TabIndex = 7;
            btnApplyLoot.Text = "Apply";
            btnApplyLoot.UseVisualStyleBackColor = true;
            btnApplyLoot.Click += btnApplyLoot_Click;
            // 
            // lblLootItem
            // 
            lblLootItem.AutoSize = true;
            lblLootItem.Location = new Point(6, 100);
            lblLootItem.Name = "lblLootItem";
            lblLootItem.Size = new Size(210, 15);
            lblLootItem.TabIndex = 6;
            lblLootItem.Text = "Find Item(s) by Name (sep by comma)";
            // 
            // txtLootFilter
            // 
            txtLootFilter.Location = new Point(6, 118);
            txtLootFilter.MaxLength = 512;
            txtLootFilter.Name = "txtLootFilter";
            txtLootFilter.Size = new Size(227, 23);
            txtLootFilter.TabIndex = 5;
            txtLootFilter.TextChanged += txtLootFilter_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(77, 34);
            label8.Name = "label8";
            label8.Size = new Size(60, 15);
            label8.TabIndex = 4;
            label8.Text = "Important";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 34);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 3;
            label7.Text = "Regular";
            // 
            // txtImportantLootValue
            // 
            txtImportantLootValue.Location = new Point(86, 52);
            txtImportantLootValue.MaxLength = 7;
            txtImportantLootValue.Name = "txtImportantLootValue";
            txtImportantLootValue.Size = new Size(57, 23);
            txtImportantLootValue.TabIndex = 2;
            txtImportantLootValue.Text = "300000";
            txtImportantLootValue.TextChanged += txtImportantLootValue_TextChanged;
            // 
            // txtRegularLootValue
            // 
            txtRegularLootValue.Location = new Point(21, 52);
            txtRegularLootValue.MaxLength = 6;
            txtRegularLootValue.Name = "txtRegularLootValue";
            txtRegularLootValue.Size = new Size(50, 23);
            txtRegularLootValue.TabIndex = 1;
            txtRegularLootValue.Text = "70000";
            txtRegularLootValue.TextChanged += txtRegularLootValue_TextChanged;
            // 
            // lblLootValue
            // 
            lblLootValue.AutoSize = true;
            lblLootValue.Location = new Point(6, 19);
            lblLootValue.Name = "lblLootValue";
            lblLootValue.Size = new Size(137, 15);
            lblLootValue.TabIndex = 0;
            lblLootValue.Text = "Minimum Value to Show";
            // 
            // grpMapSetup
            // 
            grpMapSetup.Controls.Add(btnLoot);
            grpMapSetup.Controls.Add(chkMapFree);
            grpMapSetup.Controls.Add(btnMapSetupApply);
            grpMapSetup.Controls.Add(txtMapSetupScale);
            grpMapSetup.Controls.Add(lblMapScale);
            grpMapSetup.Controls.Add(txtMapSetupY);
            grpMapSetup.Controls.Add(lblMapXY);
            grpMapSetup.Controls.Add(txtMapSetupX);
            grpMapSetup.Controls.Add(lblMapCoords);
            grpMapSetup.Location = new Point(8, 11);
            grpMapSetup.Name = "grpMapSetup";
            grpMapSetup.Size = new Size(327, 175);
            grpMapSetup.TabIndex = 11;
            grpMapSetup.TabStop = false;
            grpMapSetup.Text = "Map Setup";
            grpMapSetup.Visible = false;
            // 
            // btnLoot
            // 
            btnLoot.Location = new Point(85, 0);
            btnLoot.Name = "btnLoot";
            btnLoot.Size = new Size(44, 25);
            btnLoot.TabIndex = 12;
            btnLoot.Text = "Loot";
            btnLoot.UseVisualStyleBackColor = true;
            btnLoot.Visible = false;
            btnLoot.Click += btnLoot_Click;
            // 
            // chkMapFree
            // 
            chkMapFree.Appearance = Appearance.Button;
            chkMapFree.AutoSize = true;
            chkMapFree.Location = new Point(0, 0);
            chkMapFree.Name = "chkMapFree";
            chkMapFree.Size = new Size(66, 25);
            chkMapFree.TabIndex = 17;
            chkMapFree.Text = "Map Free";
            chkMapFree.UseVisualStyleBackColor = true;
            chkMapFree.CheckedChanged += chkMapFree_CheckedChanged;
            // 
            // btnMapSetupApply
            // 
            btnMapSetupApply.Location = new Point(6, 143);
            btnMapSetupApply.Name = "btnMapSetupApply";
            btnMapSetupApply.Size = new Size(75, 23);
            btnMapSetupApply.TabIndex = 16;
            btnMapSetupApply.Text = "Apply";
            btnMapSetupApply.UseVisualStyleBackColor = true;
            btnMapSetupApply.Click += btnMapSetupApply_Click;
            // 
            // txtMapSetupScale
            // 
            txtMapSetupScale.Location = new Point(46, 101);
            txtMapSetupScale.Name = "txtMapSetupScale";
            txtMapSetupScale.Size = new Size(50, 23);
            txtMapSetupScale.TabIndex = 15;
            // 
            // lblMapScale
            // 
            lblMapScale.AutoSize = true;
            lblMapScale.Location = new Point(6, 104);
            lblMapScale.Name = "lblMapScale";
            lblMapScale.Size = new Size(34, 15);
            lblMapScale.TabIndex = 14;
            lblMapScale.Text = "Scale";
            // 
            // txtMapSetupY
            // 
            txtMapSetupY.Location = new Point(102, 67);
            txtMapSetupY.Name = "txtMapSetupY";
            txtMapSetupY.Size = new Size(50, 23);
            txtMapSetupY.TabIndex = 13;
            // 
            // lblMapXY
            // 
            lblMapXY.AutoSize = true;
            lblMapXY.Location = new Point(6, 70);
            lblMapXY.Name = "lblMapXY";
            lblMapXY.Size = new Size(24, 15);
            lblMapXY.TabIndex = 12;
            lblMapXY.Text = "X,Y";
            // 
            // txtMapSetupX
            // 
            txtMapSetupX.Location = new Point(46, 67);
            txtMapSetupX.Name = "txtMapSetupX";
            txtMapSetupX.Size = new Size(50, 23);
            txtMapSetupX.TabIndex = 11;
            // 
            // lblMapCoords
            // 
            lblMapCoords.AutoSize = true;
            lblMapCoords.Location = new Point(7, 19);
            lblMapCoords.Margin = new Padding(4, 0, 4, 0);
            lblMapCoords.Name = "lblMapCoords";
            lblMapCoords.Size = new Size(43, 15);
            lblMapCoords.TabIndex = 10;
            lblMapCoords.Text = "coords";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(grpMemoryWriting);
            tabPage2.Controls.Add(grpConfig);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1328, 638);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Settings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpMemoryWriting
            // 
            grpMemoryWriting.Controls.Add(chkNoVisor);
            grpMemoryWriting.Controls.Add(chkOpticThermalVision);
            grpMemoryWriting.Controls.Add(chkNightVision);
            grpMemoryWriting.Controls.Add(chkThermalVision);
            grpMemoryWriting.Location = new Point(487, 3);
            grpMemoryWriting.Name = "grpMemoryWriting";
            grpMemoryWriting.Size = new Size(335, 629);
            grpMemoryWriting.TabIndex = 9;
            grpMemoryWriting.TabStop = false;
            grpMemoryWriting.Text = "Memory Writing";
            // 
            // chkNoVisor
            // 
            chkNoVisor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkNoVisor.AutoSize = true;
            chkNoVisor.Location = new Point(166, 47);
            chkNoVisor.Name = "chkNoVisor";
            chkNoVisor.Size = new Size(71, 19);
            chkNoVisor.TabIndex = 21;
            chkNoVisor.Text = "No Visor";
            chkNoVisor.UseVisualStyleBackColor = true;
            chkNoVisor.CheckedChanged += chkNoVisor_CheckedChanged;
            // 
            // chkOpticThermalVision
            // 
            chkOpticThermalVision.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkOpticThermalVision.AutoSize = true;
            chkOpticThermalVision.Location = new Point(166, 22);
            chkOpticThermalVision.Name = "chkOpticThermalVision";
            chkOpticThermalVision.Size = new Size(101, 19);
            chkOpticThermalVision.TabIndex = 20;
            chkOpticThermalVision.Text = "Optic Thermal";
            chkOpticThermalVision.UseVisualStyleBackColor = true;
            chkOpticThermalVision.CheckedChanged += chkOpticThermalVision_CheckedChanged;
            // 
            // chkNightVision
            // 
            chkNightVision.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkNightVision.AutoSize = true;
            chkNightVision.Location = new Point(6, 22);
            chkNightVision.Name = "chkNightVision";
            chkNightVision.Size = new Size(144, 19);
            chkNightVision.TabIndex = 18;
            chkNightVision.Text = "Night Vision (Ctrl + N)";
            chkNightVision.UseVisualStyleBackColor = true;
            chkNightVision.CheckedChanged += chkNightVision_CheckedChanged;
            // 
            // chkThermalVision
            // 
            chkThermalVision.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkThermalVision.AutoSize = true;
            chkThermalVision.Location = new Point(6, 47);
            chkThermalVision.Name = "chkThermalVision";
            chkThermalVision.Size = new Size(154, 19);
            chkThermalVision.TabIndex = 19;
            chkThermalVision.Text = "Thermal Vision (Ctrl + T)";
            chkThermalVision.UseVisualStyleBackColor = true;
            chkThermalVision.CheckedChanged += chkThermalVision_CheckedChanged;
            // 
            // grpConfig
            // 
            grpConfig.Controls.Add(lblUIScale);
            grpConfig.Controls.Add(trkUIScale);
            grpConfig.Controls.Add(chkHideNames);
            grpConfig.Controls.Add(txtTeammateID);
            grpConfig.Controls.Add(lblPrimaryTeammate);
            grpConfig.Controls.Add(chkShowAimview);
            grpConfig.Controls.Add(btnRestartGame);
            grpConfig.Controls.Add(chkShowMapSetup);
            grpConfig.Controls.Add(chkShowLoot);
            grpConfig.Controls.Add(lblZoom);
            grpConfig.Controls.Add(trkZoom);
            grpConfig.Controls.Add(lblAimline);
            grpConfig.Controls.Add(trkAimLength);
            grpConfig.Controls.Add(btnToggleMap);
            grpConfig.Dock = DockStyle.Left;
            grpConfig.Location = new Point(3, 3);
            grpConfig.Margin = new Padding(4, 3, 4, 3);
            grpConfig.Name = "grpConfig";
            grpConfig.Padding = new Padding(4, 3, 4, 3);
            grpConfig.Size = new Size(483, 632);
            grpConfig.TabIndex = 8;
            grpConfig.TabStop = false;
            grpConfig.Text = "Radar Config";
            // 
            // lblUIScale
            // 
            lblUIScale.AutoSize = true;
            lblUIScale.Location = new Point(83, 350);
            lblUIScale.Name = "lblUIScale";
            lblUIScale.Size = new Size(66, 15);
            lblUIScale.TabIndex = 28;
            lblUIScale.Text = "UI Scale 1.0";
            lblUIScale.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trkUIScale
            // 
            trkUIScale.LargeChange = 10;
            trkUIScale.Location = new Point(192, 334);
            trkUIScale.Maximum = 200;
            trkUIScale.Minimum = 50;
            trkUIScale.Name = "trkUIScale";
            trkUIScale.Size = new Size(277, 45);
            trkUIScale.TabIndex = 27;
            trkUIScale.Value = 100;
            // 
            // chkHideNames
            // 
            chkHideNames.AutoSize = true;
            chkHideNames.Location = new Point(326, 151);
            chkHideNames.Name = "chkHideNames";
            chkHideNames.Size = new Size(114, 19);
            chkHideNames.TabIndex = 26;
            chkHideNames.Text = "Hide Names (F6)";
            chkHideNames.UseVisualStyleBackColor = true;
            // 
            // txtTeammateID
            // 
            txtTeammateID.Location = new Point(44, 97);
            txtTeammateID.MaxLength = 12;
            txtTeammateID.Name = "txtTeammateID";
            txtTeammateID.Size = new Size(147, 23);
            txtTeammateID.TabIndex = 25;
            // 
            // lblPrimaryTeammate
            // 
            lblPrimaryTeammate.AutoSize = true;
            lblPrimaryTeammate.Location = new Point(44, 79);
            lblPrimaryTeammate.Name = "lblPrimaryTeammate";
            lblPrimaryTeammate.Size = new Size(147, 15);
            lblPrimaryTeammate.TabIndex = 22;
            lblPrimaryTeammate.Text = "Primary Teammate Acct ID";
            // 
            // chkShowAimview
            // 
            chkShowAimview.AutoSize = true;
            chkShowAimview.Location = new Point(193, 151);
            chkShowAimview.Name = "chkShowAimview";
            chkShowAimview.Size = new Size(127, 19);
            chkShowAimview.TabIndex = 19;
            chkShowAimview.Text = "Show Aimview (F4)";
            chkShowAimview.UseVisualStyleBackColor = true;
            // 
            // btnRestartGame
            // 
            btnRestartGame.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnRestartGame.Location = new Point(359, 33);
            btnRestartGame.Name = "btnRestartGame";
            btnRestartGame.Size = new Size(81, 61);
            btnRestartGame.TabIndex = 18;
            btnRestartGame.Text = "Restart Game";
            btnRestartGame.UseVisualStyleBackColor = true;
            btnRestartGame.Click += btnRestartGame_Click;
            // 
            // chkShowMapSetup
            // 
            chkShowMapSetup.AutoSize = true;
            chkShowMapSetup.Location = new Point(44, 176);
            chkShowMapSetup.Name = "chkShowMapSetup";
            chkShowMapSetup.Size = new Size(153, 19);
            chkShowMapSetup.TabIndex = 9;
            chkShowMapSetup.Text = "Show Map Setup Helper";
            chkShowMapSetup.UseVisualStyleBackColor = true;
            chkShowMapSetup.CheckedChanged += chkShowMapSetup_CheckedChanged;
            // 
            // chkShowLoot
            // 
            chkShowLoot.AutoSize = true;
            chkShowLoot.Location = new Point(44, 151);
            chkShowLoot.Name = "chkShowLoot";
            chkShowLoot.Size = new Size(105, 19);
            chkShowLoot.TabIndex = 17;
            chkShowLoot.Text = "Show Loot (F3)";
            chkShowLoot.UseVisualStyleBackColor = true;
            chkShowLoot.CheckedChanged += chkShowLoot_CheckedChanged;
            // 
            // lblZoom
            // 
            lblZoom.AutoSize = true;
            lblZoom.Location = new Point(44, 273);
            lblZoom.Margin = new Padding(4, 0, 4, 0);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(136, 45);
            lblZoom.TabIndex = 16;
            lblZoom.Text = "Zoom\r\nF1/Mouse Whl Up = In\r\nF2/Mouse Whl Dn = Out";
            lblZoom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trkZoom
            // 
            trkZoom.LargeChange = 1;
            trkZoom.Location = new Point(192, 273);
            trkZoom.Maximum = 200;
            trkZoom.Name = "trkZoom";
            trkZoom.Size = new Size(277, 45);
            trkZoom.TabIndex = 15;
            trkZoom.Value = 100;
            // 
            // lblAimline
            // 
            lblAimline.AutoSize = true;
            lblAimline.Location = new Point(63, 214);
            lblAimline.Margin = new Padding(4, 0, 4, 0);
            lblAimline.Name = "lblAimline";
            lblAimline.Size = new Size(99, 30);
            lblAimline.TabIndex = 13;
            lblAimline.Text = "Player/Teammate\r\nAimline";
            lblAimline.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trkAimLength
            // 
            trkAimLength.LargeChange = 50;
            trkAimLength.Location = new Point(193, 214);
            trkAimLength.Margin = new Padding(4, 3, 4, 3);
            trkAimLength.Maximum = 1000;
            trkAimLength.Minimum = 10;
            trkAimLength.Name = "trkAimLength";
            trkAimLength.Size = new Size(276, 45);
            trkAimLength.SmallChange = 5;
            trkAimLength.TabIndex = 11;
            trkAimLength.Value = 500;
            // 
            // btnToggleMap
            // 
            btnToggleMap.Location = new Point(44, 33);
            btnToggleMap.Margin = new Padding(4, 3, 4, 3);
            btnToggleMap.Name = "btnToggleMap";
            btnToggleMap.Size = new Size(107, 27);
            btnToggleMap.TabIndex = 7;
            btnToggleMap.Text = "Toggle Map (F5)";
            btnToggleMap.UseVisualStyleBackColor = true;
            btnToggleMap.Click += btnToggleMap_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(rchTxtPlayerInfo);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1328, 638);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Player Loadouts";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // rchTxtPlayerInfo
            // 
            rchTxtPlayerInfo.Dock = DockStyle.Fill;
            rchTxtPlayerInfo.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            rchTxtPlayerInfo.Location = new Point(0, 0);
            rchTxtPlayerInfo.Name = "rchTxtPlayerInfo";
            rchTxtPlayerInfo.ReadOnly = true;
            rchTxtPlayerInfo.Size = new Size(1328, 638);
            rchTxtPlayerInfo.TabIndex = 0;
            rchTxtPlayerInfo.Text = "";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(lstViewPMCHistory);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1328, 638);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Player History";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // lstViewPMCHistory
            // 
            lstViewPMCHistory.AutoArrange = false;
            lstViewPMCHistory.Columns.AddRange(new ColumnHeader[] { columnHeader_Entry, columnHeader_ID });
            lstViewPMCHistory.Dock = DockStyle.Fill;
            lstViewPMCHistory.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lstViewPMCHistory.FullRowSelect = true;
            lstViewPMCHistory.GridLines = true;
            lstViewPMCHistory.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstViewPMCHistory.Location = new Point(0, 0);
            lstViewPMCHistory.MultiSelect = false;
            lstViewPMCHistory.Name = "lstViewPMCHistory";
            lstViewPMCHistory.Size = new Size(1328, 638);
            lstViewPMCHistory.TabIndex = 0;
            lstViewPMCHistory.UseCompatibleStateImageBehavior = false;
            lstViewPMCHistory.View = View.Details;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1086, 666);
            Controls.Add(tabControl);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            Text = "EFT Radar";
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            grpLoot.ResumeLayout(false);
            grpLoot.PerformLayout();
            grpMapSetup.ResumeLayout(false);
            grpMapSetup.PerformLayout();
            tabPage2.ResumeLayout(false);
            grpMemoryWriting.ResumeLayout(false);
            grpMemoryWriting.PerformLayout();
            grpConfig.ResumeLayout(false);
            grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkUIScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkZoom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkAimLength).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox grpConfig;
        private Label lblAimline;
        private TrackBar trkAimLength;
        private Button btnToggleMap;
        private Label lblMapCoords;
        private Label lblZoom;
        private TrackBar trkZoom;
        private CheckBox chkShowLoot;
        private CheckBox chkShowMapSetup;
        private Button btnRestartGame;
        private GroupBox grpMapSetup;
        private Button btnMapSetupApply;
        private TextBox txtMapSetupScale;
        private Label lblMapScale;
        private TextBox txtMapSetupY;
        private Label lblMapXY;
        private TextBox txtMapSetupX;
        private CheckBox chkShowAimview;
        private CheckBox chkMapFree;
        private TabPage tabPage3;
        private RichTextBox rchTxtPlayerInfo;
        private TabPage tabPage4;
        private ListView lstViewPMCHistory;
        private ColumnHeader columnHeader_Entry;
        private ColumnHeader columnHeader_ID;
        private Label lblPrimaryTeammate;
        private TextBox txtTeammateID;
        private CheckBox chkHideNames;
        private GroupBox grpLoot;
        private Button btnApplyLoot;
        private Label lblLootItem;
        private TextBox txtLootFilter;
        private Label label8;
        private Label label7;
        private TextBox txtImportantLootValue;
        private TextBox txtRegularLootValue;
        private Label lblLootValue;
        private Button btnLoot;
        private Button btnRefreshLoot;
        private Label lblUIScale;
        private TrackBar trkUIScale;
        private GroupBox grpMemoryWriting;
        private CheckBox chkThermalVisionDebug;
        private CheckBox chkNightVisionDebug;
        private CheckBox chkOpticThermalVisionDebug;
        private CheckBox chkNoVisorDebug;
        private CheckBox chkNoVisor;
        private CheckBox chkOpticThermalVision;
        private CheckBox chkNightVision;
        private CheckBox chkThermalVision;
    }
}

