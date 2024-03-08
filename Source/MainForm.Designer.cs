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
            tabLootFilter = new TabPage();
            chkLootFilterActive = new CheckBox();
            lblActiveFilter = new Label();
            cboFilters = new ComboBox();
            btnLootFilterRemoveItem = new Button();
            btnLootFilterAddItem = new Button();
            txtItemFilter = new TextBox();
            cboLootItems = new ComboBox();
            lstViewLootFilter = new ListView();
            colHeadBSGId = new ColumnHeader();
            colHeadFullName = new ColumnHeader();
            colHeadShortName = new ColumnHeader();
            colHeadValue = new ColumnHeader();
            tabPlayerHistory = new TabPage();
            lstViewPMCHistory = new ListView();
            columnHeader_Entry = new ColumnHeader();
            columnHeader_ID = new ColumnHeader();
            tabPlayerLoadouts = new TabPage();
            rchTxtPlayerInfo = new RichTextBox();
            tabSettings = new TabPage();
            grpConfig = new GroupBox();
            grpColors = new GroupBox();
            picImportantLootColor = new PictureBox();
            lblImportantLootColor = new Label();
            picUSECColor = new PictureBox();
            picRegularLootColor = new PictureBox();
            lblRegularLootColor = new Label();
            picTeamHoverColor = new PictureBox();
            lblTeamHoverColor = new Label();
            picTeammateColor = new PictureBox();
            lblTeammateColor = new Label();
            picLocalPlayerColor = new PictureBox();
            lblLocalPlayerColor = new Label();
            picBEARColor = new PictureBox();
            picBossColor = new PictureBox();
            picAIRaiderColor = new PictureBox();
            picPScavColor = new PictureBox();
            lblBEARColor = new Label();
            lblUSECColor = new Label();
            lblBossColor = new Label();
            lblAIRaiderColor = new Label();
            lblPScavColor = new Label();
            picAIScavColor = new PictureBox();
            lblAIScavColor = new Label();
            grpLootFilters = new GroupBox();
            lstEditLootFilters = new ListBox();
            btnCancelEditFilter = new Button();
            btnEditSaveFilter = new Button();
            chkLootFilterEditActive = new CheckBox();
            btnAddNewFilter = new Button();
            btnRemoveFilter = new Button();
            lblLootFilterColor = new Label();
            btnFilterPriorityDown = new Button();
            btnFilterPriorityUp = new Button();
            lblFilterEditName = new Label();
            txtLootFilterEditName = new TextBox();
            picLootFilterEditColor = new PictureBox();
            grpLoot = new GroupBox();
            grpLootValues = new GroupBox();
            lblImportantLootDisplay = new Label();
            lblRegularLootDisplay = new Label();
            lblImportantLootSlider = new Label();
            trkImportantLootValue = new TrackBar();
            lblRegularLoot = new Label();
            trkRegularLootValue = new TrackBar();
            btnRefreshLoot = new Button();
            chkHideLootValue = new CheckBox();
            chkImportantLootOnly = new CheckBox();
            grpUserInterface = new GroupBox();
            chkShowLoot = new CheckBox();
            trkAimLength = new TrackBar();
            lblAimline = new Label();
            txtTeammateID = new TextBox();
            lblPrimaryTeammate = new Label();
            trkZoom = new TrackBar();
            lblUIScale = new Label();
            lblZoom = new Label();
            trkUIScale = new TrackBar();
            chkShowAimview = new CheckBox();
            chkHideNames = new CheckBox();
            grpMemoryWriting = new GroupBox();
            chkChams = new CheckBox();
            chkNoVisor = new CheckBox();
            chkOpticThermalVision = new CheckBox();
            chkNightVision = new CheckBox();
            chkThermalVision = new CheckBox();
            grpRadar = new GroupBox();
            btnRestartRadar = new Button();
            chkShowMapSetup = new CheckBox();
            btnToggleMap = new Button();
            tabRadar = new TabPage();
            chkNoVisorDebug = new CheckBox();
            chkOpticThermalVisionDebug = new CheckBox();
            chkNightVisionDebug = new CheckBox();
            chkThermalVisionDebug = new CheckBox();
            grpMapSetup = new GroupBox();
            chkMapFree = new CheckBox();
            txtMapSetupScale = new TextBox();
            lblMapScale = new Label();
            txtMapSetupY = new TextBox();
            lblMapXY = new Label();
            txtMapSetupX = new TextBox();
            lblMapCoords = new Label();
            tabControl = new TabControl();
            colDialog = new ColorDialog();
            tabLootFilter.SuspendLayout();
            tabPlayerHistory.SuspendLayout();
            tabPlayerLoadouts.SuspendLayout();
            tabSettings.SuspendLayout();
            grpConfig.SuspendLayout();
            grpColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picImportantLootColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUSECColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picRegularLootColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picTeamHoverColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picTeammateColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLocalPlayerColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBEARColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBossColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAIRaiderColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPScavColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAIScavColor).BeginInit();
            grpLootFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLootFilterEditColor).BeginInit();
            grpLoot.SuspendLayout();
            grpLootValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkImportantLootValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkRegularLootValue).BeginInit();
            grpUserInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkAimLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkZoom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkUIScale).BeginInit();
            grpMemoryWriting.SuspendLayout();
            grpRadar.SuspendLayout();
            tabRadar.SuspendLayout();
            grpMapSetup.SuspendLayout();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // tabLootFilter
            // 
            tabLootFilter.Controls.Add(chkLootFilterActive);
            tabLootFilter.Controls.Add(lblActiveFilter);
            tabLootFilter.Controls.Add(cboFilters);
            tabLootFilter.Controls.Add(btnLootFilterRemoveItem);
            tabLootFilter.Controls.Add(btnLootFilterAddItem);
            tabLootFilter.Controls.Add(txtItemFilter);
            tabLootFilter.Controls.Add(cboLootItems);
            tabLootFilter.Controls.Add(lstViewLootFilter);
            tabLootFilter.Location = new Point(4, 24);
            tabLootFilter.Name = "tabLootFilter";
            tabLootFilter.Padding = new Padding(3);
            tabLootFilter.Size = new Size(1168, 638);
            tabLootFilter.TabIndex = 4;
            tabLootFilter.Text = "Loot Filter";
            tabLootFilter.UseVisualStyleBackColor = true;
            // 
            // chkLootFilterActive
            // 
            chkLootFilterActive.AutoSize = true;
            chkLootFilterActive.Location = new Point(1011, 8);
            chkLootFilterActive.Name = "chkLootFilterActive";
            chkLootFilterActive.Size = new Size(59, 19);
            chkLootFilterActive.TabIndex = 7;
            chkLootFilterActive.Text = "Active";
            chkLootFilterActive.UseVisualStyleBackColor = true;
            chkLootFilterActive.CheckedChanged += chkLootFilterActive_CheckedChanged;
            // 
            // lblActiveFilter
            // 
            lblActiveFilter.AutoSize = true;
            lblActiveFilter.Location = new Point(780, 10);
            lblActiveFilter.Name = "lblActiveFilter";
            lblActiveFilter.Size = new Size(44, 15);
            lblActiveFilter.TabIndex = 6;
            lblActiveFilter.Text = "Profile:";
            // 
            // cboFilters
            // 
            cboFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilters.FormattingEnabled = true;
            cboFilters.Location = new Point(830, 6);
            cboFilters.Name = "cboFilters";
            cboFilters.Size = new Size(175, 23);
            cboFilters.TabIndex = 5;
            cboFilters.SelectedValueChanged += cboFilters_SelectedValueChanged;
            // 
            // btnLootFilterRemoveItem
            // 
            btnLootFilterRemoveItem.Location = new Point(689, 6);
            btnLootFilterRemoveItem.Name = "btnLootFilterRemoveItem";
            btnLootFilterRemoveItem.Size = new Size(75, 23);
            btnLootFilterRemoveItem.TabIndex = 4;
            btnLootFilterRemoveItem.Text = "Remove";
            btnLootFilterRemoveItem.UseVisualStyleBackColor = true;
            btnLootFilterRemoveItem.Click += btnLootFilterRemoveItem_Click;
            // 
            // btnLootFilterAddItem
            // 
            btnLootFilterAddItem.Location = new Point(608, 6);
            btnLootFilterAddItem.Name = "btnLootFilterAddItem";
            btnLootFilterAddItem.Size = new Size(75, 23);
            btnLootFilterAddItem.TabIndex = 3;
            btnLootFilterAddItem.Text = "Add";
            btnLootFilterAddItem.UseVisualStyleBackColor = true;
            btnLootFilterAddItem.Click += btnLootFilterAddItem_Click;
            // 
            // txtItemFilter
            // 
            txtItemFilter.Location = new Point(374, 6);
            txtItemFilter.Name = "txtItemFilter";
            txtItemFilter.Size = new Size(228, 23);
            txtItemFilter.TabIndex = 2;
            txtItemFilter.KeyDown += txtItemFilter_KeyDown;
            // 
            // cboLootItems
            // 
            cboLootItems.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLootItems.FormattingEnabled = true;
            cboLootItems.Location = new Point(8, 6);
            cboLootItems.Name = "cboLootItems";
            cboLootItems.Size = new Size(360, 23);
            cboLootItems.TabIndex = 1;
            // 
            // lstViewLootFilter
            // 
            lstViewLootFilter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstViewLootFilter.AutoArrange = false;
            lstViewLootFilter.Columns.AddRange(new ColumnHeader[] { colHeadBSGId, colHeadFullName, colHeadShortName, colHeadValue });
            lstViewLootFilter.FullRowSelect = true;
            lstViewLootFilter.GridLines = true;
            lstViewLootFilter.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstViewLootFilter.Location = new Point(3, 35);
            lstViewLootFilter.MultiSelect = false;
            lstViewLootFilter.Name = "lstViewLootFilter";
            lstViewLootFilter.Size = new Size(1162, 600);
            lstViewLootFilter.TabIndex = 0;
            lstViewLootFilter.UseCompatibleStateImageBehavior = false;
            lstViewLootFilter.View = View.Details;
            // 
            // colHeadBSGId
            // 
            colHeadBSGId.Text = "Item ID";
            colHeadBSGId.Width = 200;
            // 
            // colHeadFullName
            // 
            colHeadFullName.Text = "Name";
            colHeadFullName.Width = 400;
            // 
            // colHeadShortName
            // 
            colHeadShortName.Text = "Short Name";
            colHeadShortName.Width = 200;
            // 
            // colHeadValue
            // 
            colHeadValue.Text = "Value";
            colHeadValue.Width = 90;
            // 
            // tabPlayerHistory
            // 
            tabPlayerHistory.Controls.Add(lstViewPMCHistory);
            tabPlayerHistory.Location = new Point(4, 24);
            tabPlayerHistory.Name = "tabPlayerHistory";
            tabPlayerHistory.Size = new Size(1168, 638);
            tabPlayerHistory.TabIndex = 3;
            tabPlayerHistory.Text = "Player History";
            tabPlayerHistory.UseVisualStyleBackColor = true;
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
            lstViewPMCHistory.Size = new Size(1168, 638);
            lstViewPMCHistory.TabIndex = 0;
            lstViewPMCHistory.UseCompatibleStateImageBehavior = false;
            lstViewPMCHistory.View = View.Details;
            // 
            // columnHeader_Entry
            // 
            columnHeader_Entry.Text = "Entry";
            columnHeader_Entry.Width = 200;
            // 
            // columnHeader_ID
            // 
            columnHeader_ID.Text = "ID";
            columnHeader_ID.Width = 50;
            // 
            // tabPlayerLoadouts
            // 
            tabPlayerLoadouts.Controls.Add(rchTxtPlayerInfo);
            tabPlayerLoadouts.Location = new Point(4, 24);
            tabPlayerLoadouts.Name = "tabPlayerLoadouts";
            tabPlayerLoadouts.Size = new Size(1168, 638);
            tabPlayerLoadouts.TabIndex = 2;
            tabPlayerLoadouts.Text = "Player Loadouts";
            tabPlayerLoadouts.UseVisualStyleBackColor = true;
            // 
            // rchTxtPlayerInfo
            // 
            rchTxtPlayerInfo.Dock = DockStyle.Fill;
            rchTxtPlayerInfo.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            rchTxtPlayerInfo.Location = new Point(0, 0);
            rchTxtPlayerInfo.Name = "rchTxtPlayerInfo";
            rchTxtPlayerInfo.ReadOnly = true;
            rchTxtPlayerInfo.Size = new Size(1168, 638);
            rchTxtPlayerInfo.TabIndex = 0;
            rchTxtPlayerInfo.Text = "";
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(grpConfig);
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(1168, 638);
            tabSettings.TabIndex = 1;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // grpConfig
            // 
            grpConfig.Controls.Add(grpColors);
            grpConfig.Controls.Add(grpLootFilters);
            grpConfig.Controls.Add(grpLoot);
            grpConfig.Controls.Add(grpUserInterface);
            grpConfig.Controls.Add(grpMemoryWriting);
            grpConfig.Controls.Add(grpRadar);
            grpConfig.Dock = DockStyle.Fill;
            grpConfig.Location = new Point(3, 3);
            grpConfig.Margin = new Padding(4, 3, 4, 3);
            grpConfig.Name = "grpConfig";
            grpConfig.Padding = new Padding(4, 3, 4, 3);
            grpConfig.Size = new Size(1162, 632);
            grpConfig.TabIndex = 8;
            grpConfig.TabStop = false;
            grpConfig.Text = "Radar Config";
            // 
            // grpColors
            // 
            grpColors.Controls.Add(picImportantLootColor);
            grpColors.Controls.Add(lblImportantLootColor);
            grpColors.Controls.Add(picUSECColor);
            grpColors.Controls.Add(picRegularLootColor);
            grpColors.Controls.Add(lblRegularLootColor);
            grpColors.Controls.Add(picTeamHoverColor);
            grpColors.Controls.Add(lblTeamHoverColor);
            grpColors.Controls.Add(picTeammateColor);
            grpColors.Controls.Add(lblTeammateColor);
            grpColors.Controls.Add(picLocalPlayerColor);
            grpColors.Controls.Add(lblLocalPlayerColor);
            grpColors.Controls.Add(picBEARColor);
            grpColors.Controls.Add(picBossColor);
            grpColors.Controls.Add(picAIRaiderColor);
            grpColors.Controls.Add(picPScavColor);
            grpColors.Controls.Add(lblBEARColor);
            grpColors.Controls.Add(lblUSECColor);
            grpColors.Controls.Add(lblBossColor);
            grpColors.Controls.Add(lblAIRaiderColor);
            grpColors.Controls.Add(lblPScavColor);
            grpColors.Controls.Add(picAIScavColor);
            grpColors.Controls.Add(lblAIScavColor);
            grpColors.Location = new Point(888, 22);
            grpColors.Name = "grpColors";
            grpColors.Size = new Size(162, 283);
            grpColors.TabIndex = 28;
            grpColors.TabStop = false;
            grpColors.Text = "Colors";
            // 
            // picImportantLootColor
            // 
            picImportantLootColor.BackColor = Color.Transparent;
            picImportantLootColor.Location = new Point(108, 252);
            picImportantLootColor.Name = "picImportantLootColor";
            picImportantLootColor.Size = new Size(47, 18);
            picImportantLootColor.TabIndex = 35;
            picImportantLootColor.TabStop = false;
            picImportantLootColor.Click += picImportantLootColor_Click;
            // 
            // lblImportantLootColor
            // 
            lblImportantLootColor.AutoSize = true;
            lblImportantLootColor.Location = new Point(12, 252);
            lblImportantLootColor.Name = "lblImportantLootColor";
            lblImportantLootColor.Size = new Size(90, 15);
            lblImportantLootColor.TabIndex = 34;
            lblImportantLootColor.Text = "Important Loot:";
            // 
            // picUSECColor
            // 
            picUSECColor.BackColor = Color.Transparent;
            picUSECColor.Location = new Point(108, 108);
            picUSECColor.Name = "picUSECColor";
            picUSECColor.Size = new Size(47, 18);
            picUSECColor.TabIndex = 26;
            picUSECColor.TabStop = false;
            picUSECColor.Click += picUSECColor_Click;
            // 
            // picRegularLootColor
            // 
            picRegularLootColor.BackColor = Color.Transparent;
            picRegularLootColor.Location = new Point(108, 228);
            picRegularLootColor.Name = "picRegularLootColor";
            picRegularLootColor.Size = new Size(47, 18);
            picRegularLootColor.TabIndex = 33;
            picRegularLootColor.TabStop = false;
            picRegularLootColor.Click += picRegularLootColor_Click;
            // 
            // lblRegularLootColor
            // 
            lblRegularLootColor.AutoSize = true;
            lblRegularLootColor.Location = new Point(25, 228);
            lblRegularLootColor.Name = "lblRegularLootColor";
            lblRegularLootColor.Size = new Size(77, 15);
            lblRegularLootColor.TabIndex = 32;
            lblRegularLootColor.Text = "Regular Loot:";
            // 
            // picTeamHoverColor
            // 
            picTeamHoverColor.BackColor = Color.Transparent;
            picTeamHoverColor.Location = new Point(108, 204);
            picTeamHoverColor.Name = "picTeamHoverColor";
            picTeamHoverColor.Size = new Size(47, 18);
            picTeamHoverColor.TabIndex = 31;
            picTeamHoverColor.TabStop = false;
            picTeamHoverColor.Click += picTeamHoverColor_Click;
            // 
            // lblTeamHoverColor
            // 
            lblTeamHoverColor.AutoSize = true;
            lblTeamHoverColor.Location = new Point(29, 204);
            lblTeamHoverColor.Name = "lblTeamHoverColor";
            lblTeamHoverColor.Size = new Size(73, 15);
            lblTeamHoverColor.TabIndex = 30;
            lblTeamHoverColor.Text = "Team Hover:";
            // 
            // picTeammateColor
            // 
            picTeammateColor.BackColor = Color.Transparent;
            picTeammateColor.Location = new Point(108, 180);
            picTeammateColor.Name = "picTeammateColor";
            picTeammateColor.Size = new Size(47, 18);
            picTeammateColor.TabIndex = 29;
            picTeammateColor.TabStop = false;
            picTeammateColor.Click += picTeammateColor_Click;
            // 
            // lblTeammateColor
            // 
            lblTeammateColor.AutoSize = true;
            lblTeammateColor.Location = new Point(37, 180);
            lblTeammateColor.Name = "lblTeammateColor";
            lblTeammateColor.Size = new Size(65, 15);
            lblTeammateColor.TabIndex = 28;
            lblTeammateColor.Text = "Teammate:";
            // 
            // picLocalPlayerColor
            // 
            picLocalPlayerColor.BackColor = Color.Transparent;
            picLocalPlayerColor.Location = new Point(108, 156);
            picLocalPlayerColor.Name = "picLocalPlayerColor";
            picLocalPlayerColor.Size = new Size(47, 18);
            picLocalPlayerColor.TabIndex = 27;
            picLocalPlayerColor.TabStop = false;
            picLocalPlayerColor.Click += picLocalPlayerColor_Click;
            // 
            // lblLocalPlayerColor
            // 
            lblLocalPlayerColor.AutoSize = true;
            lblLocalPlayerColor.Location = new Point(32, 156);
            lblLocalPlayerColor.Name = "lblLocalPlayerColor";
            lblLocalPlayerColor.Size = new Size(70, 15);
            lblLocalPlayerColor.TabIndex = 26;
            lblLocalPlayerColor.Text = "LocalPlayer:";
            // 
            // picBEARColor
            // 
            picBEARColor.BackColor = Color.Transparent;
            picBEARColor.Location = new Point(108, 132);
            picBEARColor.Name = "picBEARColor";
            picBEARColor.Size = new Size(47, 18);
            picBEARColor.TabIndex = 25;
            picBEARColor.TabStop = false;
            picBEARColor.Click += picBEARColor_Click;
            // 
            // picBossColor
            // 
            picBossColor.BackColor = Color.Transparent;
            picBossColor.Location = new Point(108, 84);
            picBossColor.Name = "picBossColor";
            picBossColor.Size = new Size(47, 18);
            picBossColor.TabIndex = 24;
            picBossColor.TabStop = false;
            picBossColor.Click += picBossColor_Click;
            // 
            // picAIRaiderColor
            // 
            picAIRaiderColor.BackColor = Color.Transparent;
            picAIRaiderColor.Location = new Point(108, 60);
            picAIRaiderColor.Name = "picAIRaiderColor";
            picAIRaiderColor.Size = new Size(47, 18);
            picAIRaiderColor.TabIndex = 23;
            picAIRaiderColor.TabStop = false;
            picAIRaiderColor.Click += picAIRaiderColor_Click;
            // 
            // picPScavColor
            // 
            picPScavColor.BackColor = Color.Transparent;
            picPScavColor.Location = new Point(108, 36);
            picPScavColor.Name = "picPScavColor";
            picPScavColor.Size = new Size(47, 18);
            picPScavColor.TabIndex = 22;
            picPScavColor.TabStop = false;
            picPScavColor.Click += picPScavColor_Click;
            // 
            // lblBEARColor
            // 
            lblBEARColor.AutoSize = true;
            lblBEARColor.Location = new Point(64, 132);
            lblBEARColor.Name = "lblBEARColor";
            lblBEARColor.Size = new Size(38, 15);
            lblBEARColor.TabIndex = 21;
            lblBEARColor.Text = "BEAR:";
            // 
            // lblUSECColor
            // 
            lblUSECColor.AutoSize = true;
            lblUSECColor.Location = new Point(64, 108);
            lblUSECColor.Name = "lblUSECColor";
            lblUSECColor.Size = new Size(38, 15);
            lblUSECColor.TabIndex = 20;
            lblUSECColor.Text = "USEC:";
            // 
            // lblBossColor
            // 
            lblBossColor.AutoSize = true;
            lblBossColor.Location = new Point(68, 84);
            lblBossColor.Name = "lblBossColor";
            lblBossColor.Size = new Size(34, 15);
            lblBossColor.TabIndex = 19;
            lblBossColor.Text = "Boss:";
            // 
            // lblAIRaiderColor
            // 
            lblAIRaiderColor.AutoSize = true;
            lblAIRaiderColor.Location = new Point(20, 60);
            lblAIRaiderColor.Name = "lblAIRaiderColor";
            lblAIRaiderColor.Size = new Size(82, 15);
            lblAIRaiderColor.TabIndex = 18;
            lblAIRaiderColor.Text = "Raider/Rouge:";
            // 
            // lblPScavColor
            // 
            lblPScavColor.AutoSize = true;
            lblPScavColor.Location = new Point(33, 34);
            lblPScavColor.Name = "lblPScavColor";
            lblPScavColor.Size = new Size(69, 15);
            lblPScavColor.TabIndex = 17;
            lblPScavColor.Text = "Player Scav:";
            // 
            // picAIScavColor
            // 
            picAIScavColor.BackColor = Color.Transparent;
            picAIScavColor.Location = new Point(108, 12);
            picAIScavColor.Name = "picAIScavColor";
            picAIScavColor.Size = new Size(47, 18);
            picAIScavColor.TabIndex = 16;
            picAIScavColor.TabStop = false;
            picAIScavColor.Click += picAIScavColor_Click;
            // 
            // lblAIScavColor
            // 
            lblAIScavColor.AutoSize = true;
            lblAIScavColor.Location = new Point(54, 12);
            lblAIScavColor.Name = "lblAIScavColor";
            lblAIScavColor.Size = new Size(48, 15);
            lblAIScavColor.TabIndex = 15;
            lblAIScavColor.Text = "AI Scav:";
            // 
            // grpLootFilters
            // 
            grpLootFilters.Controls.Add(lstEditLootFilters);
            grpLootFilters.Controls.Add(btnCancelEditFilter);
            grpLootFilters.Controls.Add(btnEditSaveFilter);
            grpLootFilters.Controls.Add(chkLootFilterEditActive);
            grpLootFilters.Controls.Add(btnAddNewFilter);
            grpLootFilters.Controls.Add(btnRemoveFilter);
            grpLootFilters.Controls.Add(lblLootFilterColor);
            grpLootFilters.Controls.Add(btnFilterPriorityDown);
            grpLootFilters.Controls.Add(btnFilterPriorityUp);
            grpLootFilters.Controls.Add(lblFilterEditName);
            grpLootFilters.Controls.Add(txtLootFilterEditName);
            grpLootFilters.Controls.Add(picLootFilterEditColor);
            grpLootFilters.Location = new Point(474, 238);
            grpLootFilters.Name = "grpLootFilters";
            grpLootFilters.Size = new Size(408, 286);
            grpLootFilters.TabIndex = 27;
            grpLootFilters.TabStop = false;
            grpLootFilters.Text = "Loot Filters/Profiles";
            // 
            // lstEditLootFilters
            // 
            lstEditLootFilters.DisplayMember = "Name";
            lstEditLootFilters.FormattingEnabled = true;
            lstEditLootFilters.ItemHeight = 15;
            lstEditLootFilters.Location = new Point(6, 21);
            lstEditLootFilters.Name = "lstEditLootFilters";
            lstEditLootFilters.Size = new Size(168, 259);
            lstEditLootFilters.TabIndex = 14;
            lstEditLootFilters.SelectedIndexChanged += lstEditLootFilters_SelectedIndexChanged;
            // 
            // btnCancelEditFilter
            // 
            btnCancelEditFilter.Location = new Point(331, 102);
            btnCancelEditFilter.Name = "btnCancelEditFilter";
            btnCancelEditFilter.Size = new Size(71, 23);
            btnCancelEditFilter.TabIndex = 13;
            btnCancelEditFilter.Text = "Cancel";
            btnCancelEditFilter.UseVisualStyleBackColor = true;
            btnCancelEditFilter.Visible = false;
            btnCancelEditFilter.Click += btnCancelEditFilter_Click;
            // 
            // btnEditSaveFilter
            // 
            btnEditSaveFilter.Location = new Point(269, 102);
            btnEditSaveFilter.Name = "btnEditSaveFilter";
            btnEditSaveFilter.Size = new Size(56, 23);
            btnEditSaveFilter.TabIndex = 12;
            btnEditSaveFilter.Text = "Edit";
            btnEditSaveFilter.UseVisualStyleBackColor = true;
            btnEditSaveFilter.Click += btnEditSaveFilter_Click;
            // 
            // chkLootFilterEditActive
            // 
            chkLootFilterEditActive.AutoSize = true;
            chkLootFilterEditActive.Enabled = false;
            chkLootFilterEditActive.Location = new Point(269, 77);
            chkLootFilterEditActive.Name = "chkLootFilterEditActive";
            chkLootFilterEditActive.Size = new Size(70, 19);
            chkLootFilterEditActive.TabIndex = 11;
            chkLootFilterEditActive.Text = "Is Active";
            chkLootFilterEditActive.UseVisualStyleBackColor = true;
            // 
            // btnAddNewFilter
            // 
            btnAddNewFilter.Location = new Point(180, 228);
            btnAddNewFilter.Name = "btnAddNewFilter";
            btnAddNewFilter.Size = new Size(22, 23);
            btnAddNewFilter.TabIndex = 9;
            btnAddNewFilter.Text = "✚";
            btnAddNewFilter.UseVisualStyleBackColor = true;
            btnAddNewFilter.Click += btnAddNewFilter_Click;
            // 
            // btnRemoveFilter
            // 
            btnRemoveFilter.Location = new Point(180, 257);
            btnRemoveFilter.Name = "btnRemoveFilter";
            btnRemoveFilter.Size = new Size(22, 23);
            btnRemoveFilter.TabIndex = 8;
            btnRemoveFilter.Text = "✖";
            btnRemoveFilter.UseVisualStyleBackColor = true;
            btnRemoveFilter.Click += btnRemoveFilter_Click;
            // 
            // lblLootFilterColor
            // 
            lblLootFilterColor.AutoSize = true;
            lblLootFilterColor.Location = new Point(224, 52);
            lblLootFilterColor.Name = "lblLootFilterColor";
            lblLootFilterColor.Size = new Size(39, 15);
            lblLootFilterColor.TabIndex = 6;
            lblLootFilterColor.Text = "Color:";
            // 
            // btnFilterPriorityDown
            // 
            btnFilterPriorityDown.Location = new Point(180, 50);
            btnFilterPriorityDown.Name = "btnFilterPriorityDown";
            btnFilterPriorityDown.Size = new Size(22, 23);
            btnFilterPriorityDown.TabIndex = 5;
            btnFilterPriorityDown.Text = "▼";
            btnFilterPriorityDown.UseVisualStyleBackColor = true;
            btnFilterPriorityDown.Click += btnFilterPriorityDown_Click;
            // 
            // btnFilterPriorityUp
            // 
            btnFilterPriorityUp.Location = new Point(180, 21);
            btnFilterPriorityUp.Name = "btnFilterPriorityUp";
            btnFilterPriorityUp.Size = new Size(22, 23);
            btnFilterPriorityUp.TabIndex = 4;
            btnFilterPriorityUp.Text = "▲";
            btnFilterPriorityUp.UseVisualStyleBackColor = true;
            btnFilterPriorityUp.Click += btnFilterPriorityUp_Click;
            // 
            // lblFilterEditName
            // 
            lblFilterEditName.AutoSize = true;
            lblFilterEditName.Location = new Point(221, 23);
            lblFilterEditName.Name = "lblFilterEditName";
            lblFilterEditName.Size = new Size(42, 15);
            lblFilterEditName.TabIndex = 2;
            lblFilterEditName.Text = "Name:";
            // 
            // txtLootFilterEditName
            // 
            txtLootFilterEditName.Enabled = false;
            txtLootFilterEditName.Location = new Point(269, 20);
            txtLootFilterEditName.Name = "txtLootFilterEditName";
            txtLootFilterEditName.Size = new Size(133, 23);
            txtLootFilterEditName.TabIndex = 1;
            // 
            // picLootFilterEditColor
            // 
            picLootFilterEditColor.BackColor = Color.Transparent;
            picLootFilterEditColor.Enabled = false;
            picLootFilterEditColor.Location = new Point(269, 52);
            picLootFilterEditColor.Name = "picLootFilterEditColor";
            picLootFilterEditColor.Size = new Size(47, 18);
            picLootFilterEditColor.TabIndex = 7;
            picLootFilterEditColor.TabStop = false;
            picLootFilterEditColor.Click += picLootFilterPreview_Click;
            // 
            // grpLoot
            // 
            grpLoot.Controls.Add(grpLootValues);
            grpLoot.Controls.Add(btnRefreshLoot);
            grpLoot.Controls.Add(chkHideLootValue);
            grpLoot.Controls.Add(chkImportantLootOnly);
            grpLoot.Location = new Point(474, 22);
            grpLoot.Name = "grpLoot";
            grpLoot.Size = new Size(408, 210);
            grpLoot.TabIndex = 13;
            grpLoot.TabStop = false;
            grpLoot.Text = "Loot";
            // 
            // grpLootValues
            // 
            grpLootValues.Controls.Add(lblImportantLootDisplay);
            grpLootValues.Controls.Add(lblRegularLootDisplay);
            grpLootValues.Controls.Add(lblImportantLootSlider);
            grpLootValues.Controls.Add(trkImportantLootValue);
            grpLootValues.Controls.Add(lblRegularLoot);
            grpLootValues.Controls.Add(trkRegularLootValue);
            grpLootValues.Location = new Point(6, 18);
            grpLootValues.Name = "grpLootValues";
            grpLootValues.Size = new Size(306, 119);
            grpLootValues.TabIndex = 32;
            grpLootValues.TabStop = false;
            grpLootValues.Text = "Minimum Ruble Value";
            // 
            // lblImportantLootDisplay
            // 
            lblImportantLootDisplay.AutoSize = true;
            lblImportantLootDisplay.Location = new Point(68, 81);
            lblImportantLootDisplay.Name = "lblImportantLootDisplay";
            lblImportantLootDisplay.Size = new Size(25, 15);
            lblImportantLootDisplay.TabIndex = 34;
            lblImportantLootDisplay.Text = "xxx";
            // 
            // lblRegularLootDisplay
            // 
            lblRegularLootDisplay.AutoSize = true;
            lblRegularLootDisplay.Location = new Point(68, 37);
            lblRegularLootDisplay.Name = "lblRegularLootDisplay";
            lblRegularLootDisplay.Size = new Size(25, 15);
            lblRegularLootDisplay.TabIndex = 27;
            lblRegularLootDisplay.Text = "xxx";
            // 
            // lblImportantLootSlider
            // 
            lblImportantLootSlider.AutoSize = true;
            lblImportantLootSlider.Location = new Point(6, 66);
            lblImportantLootSlider.Name = "lblImportantLootSlider";
            lblImportantLootSlider.Size = new Size(87, 15);
            lblImportantLootSlider.TabIndex = 32;
            lblImportantLootSlider.Text = "Important Loot";
            // 
            // trkImportantLootValue
            // 
            trkImportantLootValue.BackColor = SystemColors.Control;
            trkImportantLootValue.LargeChange = 10;
            trkImportantLootValue.Location = new Point(99, 66);
            trkImportantLootValue.Maximum = 500;
            trkImportantLootValue.Minimum = 250;
            trkImportantLootValue.Name = "trkImportantLootValue";
            trkImportantLootValue.Size = new Size(207, 45);
            trkImportantLootValue.SmallChange = 10;
            trkImportantLootValue.TabIndex = 33;
            trkImportantLootValue.TickStyle = TickStyle.None;
            trkImportantLootValue.Value = 300;
            trkImportantLootValue.Scroll += trkImportantLootValue_Scroll;
            // 
            // lblRegularLoot
            // 
            lblRegularLoot.AutoSize = true;
            lblRegularLoot.Location = new Point(19, 22);
            lblRegularLoot.Name = "lblRegularLoot";
            lblRegularLoot.Size = new Size(74, 15);
            lblRegularLoot.TabIndex = 0;
            lblRegularLoot.Text = "Regular Loot";
            // 
            // trkRegularLootValue
            // 
            trkRegularLootValue.BackColor = SystemColors.Control;
            trkRegularLootValue.LargeChange = 10;
            trkRegularLootValue.Location = new Point(99, 20);
            trkRegularLootValue.Maximum = 249;
            trkRegularLootValue.Minimum = 10;
            trkRegularLootValue.Name = "trkRegularLootValue";
            trkRegularLootValue.Size = new Size(207, 45);
            trkRegularLootValue.SmallChange = 10;
            trkRegularLootValue.TabIndex = 31;
            trkRegularLootValue.TickStyle = TickStyle.None;
            trkRegularLootValue.Value = 85;
            trkRegularLootValue.Scroll += trkRegularLootValue_Scroll;
            // 
            // btnRefreshLoot
            // 
            btnRefreshLoot.Location = new Point(318, 27);
            btnRefreshLoot.Name = "btnRefreshLoot";
            btnRefreshLoot.Size = new Size(85, 41);
            btnRefreshLoot.TabIndex = 21;
            btnRefreshLoot.Text = "Refresh Loot";
            btnRefreshLoot.UseVisualStyleBackColor = true;
            btnRefreshLoot.Click += btnRefreshLoot_Click;
            // 
            // chkHideLootValue
            // 
            chkHideLootValue.AutoSize = true;
            chkHideLootValue.Location = new Point(6, 143);
            chkHideLootValue.Name = "chkHideLootValue";
            chkHideLootValue.Size = new Size(82, 19);
            chkHideLootValue.TabIndex = 29;
            chkHideLootValue.Text = "Hide Value";
            chkHideLootValue.UseVisualStyleBackColor = true;
            chkHideLootValue.CheckedChanged += chkHideLootValue_CheckedChanged;
            // 
            // chkImportantLootOnly
            // 
            chkImportantLootOnly.AutoSize = true;
            chkImportantLootOnly.Location = new Point(6, 165);
            chkImportantLootOnly.Name = "chkImportantLootOnly";
            chkImportantLootOnly.Size = new Size(151, 19);
            chkImportantLootOnly.TabIndex = 22;
            chkImportantLootOnly.Text = "Important/Filtered Only";
            chkImportantLootOnly.UseVisualStyleBackColor = true;
            // 
            // grpUserInterface
            // 
            grpUserInterface.Controls.Add(chkShowLoot);
            grpUserInterface.Controls.Add(trkAimLength);
            grpUserInterface.Controls.Add(lblAimline);
            grpUserInterface.Controls.Add(txtTeammateID);
            grpUserInterface.Controls.Add(lblPrimaryTeammate);
            grpUserInterface.Controls.Add(trkZoom);
            grpUserInterface.Controls.Add(lblUIScale);
            grpUserInterface.Controls.Add(lblZoom);
            grpUserInterface.Controls.Add(trkUIScale);
            grpUserInterface.Controls.Add(chkShowAimview);
            grpUserInterface.Controls.Add(chkHideNames);
            grpUserInterface.Location = new Point(5, 165);
            grpUserInterface.Name = "grpUserInterface";
            grpUserInterface.Size = new Size(463, 159);
            grpUserInterface.TabIndex = 26;
            grpUserInterface.TabStop = false;
            grpUserInterface.Text = "UI";
            // 
            // chkShowLoot
            // 
            chkShowLoot.AutoSize = true;
            chkShowLoot.Location = new Point(6, 22);
            chkShowLoot.Name = "chkShowLoot";
            chkShowLoot.Size = new Size(105, 19);
            chkShowLoot.TabIndex = 17;
            chkShowLoot.Text = "Show Loot (F3)";
            chkShowLoot.UseVisualStyleBackColor = true;
            // 
            // trkAimLength
            // 
            trkAimLength.LargeChange = 50;
            trkAimLength.Location = new Point(100, 47);
            trkAimLength.Margin = new Padding(4, 3, 4, 3);
            trkAimLength.Maximum = 1000;
            trkAimLength.Minimum = 10;
            trkAimLength.Name = "trkAimLength";
            trkAimLength.Size = new Size(114, 45);
            trkAimLength.SmallChange = 5;
            trkAimLength.TabIndex = 11;
            trkAimLength.TickStyle = TickStyle.None;
            trkAimLength.Value = 500;
            // 
            // lblAimline
            // 
            lblAimline.AutoSize = true;
            lblAimline.Location = new Point(4, 50);
            lblAimline.Margin = new Padding(4, 0, 4, 0);
            lblAimline.Name = "lblAimline";
            lblAimline.Size = new Size(88, 15);
            lblAimline.TabIndex = 13;
            lblAimline.Text = "Aimline Length";
            lblAimline.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtTeammateID
            // 
            txtTeammateID.Location = new Point(249, 120);
            txtTeammateID.MaxLength = 12;
            txtTeammateID.Name = "txtTeammateID";
            txtTeammateID.Size = new Size(147, 23);
            txtTeammateID.TabIndex = 25;
            // 
            // lblPrimaryTeammate
            // 
            lblPrimaryTeammate.AutoSize = true;
            lblPrimaryTeammate.Location = new Point(249, 98);
            lblPrimaryTeammate.Name = "lblPrimaryTeammate";
            lblPrimaryTeammate.Size = new Size(76, 15);
            lblPrimaryTeammate.TabIndex = 22;
            lblPrimaryTeammate.Text = "Teammate ID";
            // 
            // trkZoom
            // 
            trkZoom.LargeChange = 1;
            trkZoom.Location = new Point(335, 50);
            trkZoom.Maximum = 200;
            trkZoom.Name = "trkZoom";
            trkZoom.Size = new Size(118, 45);
            trkZoom.TabIndex = 15;
            trkZoom.TickStyle = TickStyle.None;
            trkZoom.Value = 100;
            // 
            // lblUIScale
            // 
            lblUIScale.AutoSize = true;
            lblUIScale.Location = new Point(44, 98);
            lblUIScale.Name = "lblUIScale";
            lblUIScale.Size = new Size(48, 15);
            lblUIScale.TabIndex = 28;
            lblUIScale.Text = "UI Scale";
            lblUIScale.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblZoom
            // 
            lblZoom.AutoSize = true;
            lblZoom.Location = new Point(241, 50);
            lblZoom.Margin = new Padding(4, 0, 4, 0);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(87, 15);
            lblZoom.TabIndex = 16;
            lblZoom.Text = "Zoom Distance";
            lblZoom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trkUIScale
            // 
            trkUIScale.LargeChange = 10;
            trkUIScale.Location = new Point(100, 98);
            trkUIScale.Maximum = 200;
            trkUIScale.Minimum = 50;
            trkUIScale.Name = "trkUIScale";
            trkUIScale.Size = new Size(116, 45);
            trkUIScale.TabIndex = 27;
            trkUIScale.TickStyle = TickStyle.None;
            trkUIScale.Value = 100;
            // 
            // chkShowAimview
            // 
            chkShowAimview.AutoSize = true;
            chkShowAimview.Location = new Point(117, 22);
            chkShowAimview.Name = "chkShowAimview";
            chkShowAimview.Size = new Size(127, 19);
            chkShowAimview.TabIndex = 19;
            chkShowAimview.Text = "Show Aimview (F4)";
            chkShowAimview.UseVisualStyleBackColor = true;
            // 
            // chkHideNames
            // 
            chkHideNames.AutoSize = true;
            chkHideNames.Location = new Point(250, 22);
            chkHideNames.Name = "chkHideNames";
            chkHideNames.Size = new Size(114, 19);
            chkHideNames.TabIndex = 26;
            chkHideNames.Text = "Hide Names (F6)";
            chkHideNames.UseVisualStyleBackColor = true;
            // 
            // grpMemoryWriting
            // 
            grpMemoryWriting.Controls.Add(chkChams);
            grpMemoryWriting.Controls.Add(chkNoVisor);
            grpMemoryWriting.Controls.Add(chkOpticThermalVision);
            grpMemoryWriting.Controls.Add(chkNightVision);
            grpMemoryWriting.Controls.Add(chkThermalVision);
            grpMemoryWriting.Location = new Point(6, 330);
            grpMemoryWriting.Name = "grpMemoryWriting";
            grpMemoryWriting.Size = new Size(462, 194);
            grpMemoryWriting.TabIndex = 9;
            grpMemoryWriting.TabStop = false;
            grpMemoryWriting.Text = "Memory Writing [RISKY]";
            // 
            // chkChams
            // 
            chkChams.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkChams.AutoSize = true;
            chkChams.Location = new Point(5, 72);
            chkChams.Name = "chkChams";
            chkChams.Size = new Size(142, 19);
            chkChams.TabIndex = 22;
            chkChams.Text = "Chams (Offline - WIP)";
            chkChams.UseVisualStyleBackColor = true;
            chkChams.CheckedChanged += chkChams_CheckedChanged;
            // 
            // chkNoVisor
            // 
            chkNoVisor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkNoVisor.AutoSize = true;
            chkNoVisor.Location = new Point(165, 47);
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
            chkOpticThermalVision.Location = new Point(165, 22);
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
            chkThermalVision.Location = new Point(5, 47);
            chkThermalVision.Name = "chkThermalVision";
            chkThermalVision.Size = new Size(154, 19);
            chkThermalVision.TabIndex = 19;
            chkThermalVision.Text = "Thermal Vision (Ctrl + T)";
            chkThermalVision.UseVisualStyleBackColor = true;
            chkThermalVision.CheckedChanged += chkThermalVision_CheckedChanged;
            // 
            // grpRadar
            // 
            grpRadar.Controls.Add(btnRestartRadar);
            grpRadar.Controls.Add(chkShowMapSetup);
            grpRadar.Controls.Add(btnToggleMap);
            grpRadar.Location = new Point(5, 22);
            grpRadar.Name = "grpRadar";
            grpRadar.Size = new Size(463, 137);
            grpRadar.TabIndex = 26;
            grpRadar.TabStop = false;
            grpRadar.Text = "Radar";
            // 
            // btnRestartRadar
            // 
            btnRestartRadar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnRestartRadar.Location = new Point(350, 22);
            btnRestartRadar.Name = "btnRestartRadar";
            btnRestartRadar.Size = new Size(107, 27);
            btnRestartRadar.TabIndex = 18;
            btnRestartRadar.Text = "Restart Radar";
            btnRestartRadar.UseVisualStyleBackColor = true;
            btnRestartRadar.Click += btnRestartRadar_Click;
            // 
            // chkShowMapSetup
            // 
            chkShowMapSetup.AutoSize = true;
            chkShowMapSetup.Location = new Point(6, 28);
            chkShowMapSetup.Name = "chkShowMapSetup";
            chkShowMapSetup.Size = new Size(153, 19);
            chkShowMapSetup.TabIndex = 9;
            chkShowMapSetup.Text = "Show Map Setup Helper";
            chkShowMapSetup.UseVisualStyleBackColor = true;
            chkShowMapSetup.CheckedChanged += chkShowMapSetup_CheckedChanged;
            // 
            // btnToggleMap
            // 
            btnToggleMap.Location = new Point(236, 22);
            btnToggleMap.Margin = new Padding(4, 3, 4, 3);
            btnToggleMap.Name = "btnToggleMap";
            btnToggleMap.Size = new Size(107, 27);
            btnToggleMap.TabIndex = 7;
            btnToggleMap.Text = "Toggle Map (F5)";
            btnToggleMap.UseVisualStyleBackColor = true;
            btnToggleMap.Click += btnToggleMap_Click;
            // 
            // tabRadar
            // 
            tabRadar.Controls.Add(chkNoVisorDebug);
            tabRadar.Controls.Add(chkOpticThermalVisionDebug);
            tabRadar.Controls.Add(chkNightVisionDebug);
            tabRadar.Controls.Add(chkThermalVisionDebug);
            tabRadar.Controls.Add(grpMapSetup);
            tabRadar.Location = new Point(4, 24);
            tabRadar.Name = "tabRadar";
            tabRadar.Padding = new Padding(3);
            tabRadar.Size = new Size(1168, 638);
            tabRadar.TabIndex = 0;
            tabRadar.Text = "Radar";
            tabRadar.UseVisualStyleBackColor = true;
            // 
            // chkNoVisorDebug
            // 
            chkNoVisorDebug.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkNoVisorDebug.AutoSize = true;
            chkNoVisorDebug.Location = new Point(1066, 82);
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
            chkOpticThermalVisionDebug.Location = new Point(1066, 56);
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
            chkNightVisionDebug.Location = new Point(1066, 6);
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
            chkThermalVisionDebug.Location = new Point(1066, 31);
            chkThermalVisionDebug.Name = "chkThermalVisionDebug";
            chkThermalVisionDebug.Size = new Size(104, 19);
            chkThermalVisionDebug.TabIndex = 15;
            chkThermalVisionDebug.Text = "Thermal Vision";
            chkThermalVisionDebug.UseVisualStyleBackColor = true;
            chkThermalVisionDebug.CheckedChanged += chkThermalVision_CheckedChanged;
            // 
            // grpMapSetup
            // 
            grpMapSetup.Controls.Add(chkMapFree);
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
            // tabControl
            // 
            tabControl.Controls.Add(tabRadar);
            tabControl.Controls.Add(tabSettings);
            tabControl.Controls.Add(tabPlayerLoadouts);
            tabControl.Controls.Add(tabPlayerHistory);
            tabControl.Controls.Add(tabLootFilter);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1176, 666);
            tabControl.TabIndex = 8;
            // 
            // colDialog
            // 
            colDialog.FullOpen = true;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1176, 666);
            Controls.Add(tabControl);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            Text = "EFT Radar";
            tabLootFilter.ResumeLayout(false);
            tabLootFilter.PerformLayout();
            tabPlayerHistory.ResumeLayout(false);
            tabPlayerLoadouts.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            grpConfig.ResumeLayout(false);
            grpColors.ResumeLayout(false);
            grpColors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picImportantLootColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUSECColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picRegularLootColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picTeamHoverColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picTeammateColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLocalPlayerColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBEARColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBossColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAIRaiderColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPScavColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAIScavColor).EndInit();
            grpLootFilters.ResumeLayout(false);
            grpLootFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLootFilterEditColor).EndInit();
            grpLoot.ResumeLayout(false);
            grpLoot.PerformLayout();
            grpLootValues.ResumeLayout(false);
            grpLootValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkImportantLootValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkRegularLootValue).EndInit();
            grpUserInterface.ResumeLayout(false);
            grpUserInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkAimLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkZoom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkUIScale).EndInit();
            grpMemoryWriting.ResumeLayout(false);
            grpMemoryWriting.PerformLayout();
            grpRadar.ResumeLayout(false);
            grpRadar.PerformLayout();
            tabRadar.ResumeLayout(false);
            tabRadar.PerformLayout();
            grpMapSetup.ResumeLayout(false);
            grpMapSetup.PerformLayout();
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabLootFilter;
        private CheckBox chkLootFilterActive;
        private Label lblActiveFilter;
        private ComboBox cboFilters;
        private Button btnLootFilterRemoveItem;
        private Button btnLootFilterAddItem;
        private TextBox txtItemFilter;
        private ComboBox cboLootItems;
        private ListView lstViewLootFilter;
        private ColumnHeader colHeadBSGId;
        private ColumnHeader colHeadFullName;
        private ColumnHeader colHeadShortName;
        private ColumnHeader colHeadValue;
        private TabPage tabPlayerHistory;
        private ListView lstViewPMCHistory;
        private ColumnHeader columnHeader_Entry;
        private ColumnHeader columnHeader_ID;
        private TabPage tabPlayerLoadouts;
        private RichTextBox rchTxtPlayerInfo;
        private TabPage tabSettings;
        private GroupBox grpConfig;
        private GroupBox grpLoot;
        private TrackBar trkRegularLootValue;
        private Button btnRefreshLoot;
        private CheckBox chkHideLootValue;
        private CheckBox chkImportantLootOnly;
        private Label lblRegularLoot;
        private GroupBox grpUserInterface;
        private CheckBox chkShowLoot;
        private TrackBar trkAimLength;
        private Label lblAimline;
        private TextBox txtTeammateID;
        private Label lblPrimaryTeammate;
        private TrackBar trkZoom;
        private Label lblUIScale;
        private Label lblZoom;
        private TrackBar trkUIScale;
        private CheckBox chkShowAimview;
        private CheckBox chkHideNames;
        private GroupBox grpMemoryWriting;
        private CheckBox chkNoVisor;
        private CheckBox chkOpticThermalVision;
        private CheckBox chkNightVision;
        private CheckBox chkThermalVision;
        private GroupBox grpRadar;
        private Button btnRestartRadar;
        private CheckBox chkShowMapSetup;
        private Button btnToggleMap;
        private TabPage tabRadar;
        private CheckBox chkNoVisorDebug;
        private CheckBox chkOpticThermalVisionDebug;
        private CheckBox chkNightVisionDebug;
        private CheckBox chkThermalVisionDebug;
        private GroupBox grpMapSetup;
        private CheckBox chkMapFree;
        private TextBox txtMapSetupScale;
        private Label lblMapScale;
        private TextBox txtMapSetupY;
        private Label lblMapXY;
        private TextBox txtMapSetupX;
        private Label lblMapCoords;
        private TabControl tabControl;
        private GroupBox grpLootValues;
        private Label lblImportantLootSlider;
        private TrackBar trkImportantLootValue;
        private Label lblRegularLootDisplay;
        private Label lblImportantLootDisplay;
        private GroupBox grpLootFilters;
        private TextBox txtLootFilterEditName;
        private Label lblFilterEditName;
        private Button btnFilterPriorityDown;
        private Button btnFilterPriorityUp;
        private Label lblLootFilterColor;
        private ColorDialog colDialog;
        private PictureBox picLootFilterEditColor;
        private Button btnAddNewFilter;
        private Button btnRemoveFilter;
        private CheckBox chkLootFilterEditActive;
        private Button btnEditSaveFilter;
        private Button btnCancelEditFilter;
        private ListBox lstEditLootFilters;
        private GroupBox grpColors;
        private PictureBox picAIScavColor;
        private Label lblAIScavColor;
        private Label lblBEARColor;
        private Label lblUSECColor;
        private Label lblBossColor;
        private Label lblAIRaiderColor;
        private Label lblPScavColor;
        private PictureBox picUSECColor;
        private PictureBox picBEARColor;
        private PictureBox picBossColor;
        private PictureBox picAIRaiderColor;
        private PictureBox picPScavColor;
        private PictureBox picTeamHoverColor;
        private Label lblTeamHoverColor;
        private PictureBox picTeammateColor;
        private Label lblTeammateColor;
        private PictureBox picLocalPlayerColor;
        private Label lblLocalPlayerColor;
        private PictureBox picRegularLootColor;
        private Label lblRegularLootColor;
        private PictureBox picImportantLootColor;
        private Label lblImportantLootColor;
        private CheckBox chkChams;
    }
}

