using SkiaSharp.Views.Desktop;

namespace eft_dma_radar
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            OpticThermalBox = new CheckBox();
            NightVisionCheckBox = new CheckBox();
            thermalVisionCheckBox = new CheckBox();
            dataGridView1 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            groupBox_Loot = new GroupBox();
            button_RefreshLoot = new Button();
            button_LootApply = new Button();
            label9 = new Label();
            textBox_LootFilterByName = new TextBox();
            label8 = new Label();
            label7 = new Label();
            textBox_LootImpValue = new TextBox();
            textBox_LootRegValue = new TextBox();
            label6 = new Label();
            groupBox_MapSetup = new GroupBox();
            button_Loot = new Button();
            checkBox_MapFree = new CheckBox();
            button_MapSetupApply = new Button();
            textBox_mapScale = new TextBox();
            label5 = new Label();
            textBox_mapY = new TextBox();
            label4 = new Label();
            textBox_mapX = new TextBox();
            label_Pos = new Label();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            checkBox_HidePlayerPanel = new CheckBox();
            label_UIScale = new Label();
            trackBar_UIScale = new TrackBar();
            checkBox_HideNames = new CheckBox();
            textBox_PrimTeamID = new TextBox();
            label3 = new Label();
            checkBox_Aimview = new CheckBox();
            button_Restart = new Button();
            checkBox_MapSetup = new CheckBox();
            checkBox_Loot = new CheckBox();
            label1 = new Label();
            trackBar_Zoom = new TrackBar();
            label2 = new Label();
            trackBar_AimLength = new TrackBar();
            button_Map = new Button();
            tabPage3 = new TabPage();
            richTextBox_PlayersInfo = new RichTextBox();
            tabPage4 = new TabPage();
            listView_PmcHistory = new ListView();
            columnHeader_Entry = new ColumnHeader();
            columnHeader_ID = new ColumnHeader();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            bindingSource1 = new BindingSource(components);
            Column1 = new DataGridViewTextBoxColumn();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox_Loot.SuspendLayout();
            groupBox_MapSetup.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_UIScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_Zoom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_AimLength).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1336, 666);
            tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(OpticThermalBox);
            tabPage1.Controls.Add(NightVisionCheckBox);
            tabPage1.Controls.Add(thermalVisionCheckBox);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Controls.Add(groupBox_Loot);
            tabPage1.Controls.Add(groupBox_MapSetup);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1328, 638);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Radar";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // OpticThermalBox
            // 
            OpticThermalBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpticThermalBox.AutoSize = true;
            OpticThermalBox.Location = new Point(1226, 56);
            OpticThermalBox.Name = "OpticThermalBox";
            OpticThermalBox.Size = new Size(101, 19);
            OpticThermalBox.TabIndex = 16;
            OpticThermalBox.Text = "Optic Thermal";
            OpticThermalBox.UseVisualStyleBackColor = true;
            OpticThermalBox.CheckedChanged += OpticThermalBox_CheckedChanged_1;
            // 
            // NightVisionCheckBox
            // 
            NightVisionCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            NightVisionCheckBox.AutoSize = true;
            NightVisionCheckBox.Location = new Point(1226, 6);
            NightVisionCheckBox.Name = "NightVisionCheckBox";
            NightVisionCheckBox.Size = new Size(94, 19);
            NightVisionCheckBox.TabIndex = 14;
            NightVisionCheckBox.Text = "Night Vision ";
            NightVisionCheckBox.UseVisualStyleBackColor = true;
            NightVisionCheckBox.CheckedChanged += NightVisionCheckBox_CheckedChanged_1;
            // 
            // thermalVisionCheckBox
            // 
            thermalVisionCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            thermalVisionCheckBox.AutoSize = true;
            thermalVisionCheckBox.Location = new Point(1226, 31);
            thermalVisionCheckBox.Name = "thermalVisionCheckBox";
            thermalVisionCheckBox.Size = new Size(104, 19);
            thermalVisionCheckBox.TabIndex = 15;
            thermalVisionCheckBox.Text = "Thermal Vision";
            thermalVisionCheckBox.UseVisualStyleBackColor = true;
            thermalVisionCheckBox.CheckedChanged += thermalVisionCheckBox_CheckedChanged_1;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Black;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.DarkGray;
            dataGridView1.Location = new Point(877, 6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(443, 202);
            dataGridView1.TabIndex = 13;
            dataGridView1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Faction / Name";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Value";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "In Hands";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Dist";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // groupBox_Loot
            // 
            groupBox_Loot.Controls.Add(button_RefreshLoot);
            groupBox_Loot.Controls.Add(button_LootApply);
            groupBox_Loot.Controls.Add(label9);
            groupBox_Loot.Controls.Add(textBox_LootFilterByName);
            groupBox_Loot.Controls.Add(label8);
            groupBox_Loot.Controls.Add(label7);
            groupBox_Loot.Controls.Add(textBox_LootImpValue);
            groupBox_Loot.Controls.Add(textBox_LootRegValue);
            groupBox_Loot.Controls.Add(label6);
            groupBox_Loot.Location = new Point(8, 6);
            groupBox_Loot.Name = "groupBox_Loot";
            groupBox_Loot.Size = new Size(256, 202);
            groupBox_Loot.TabIndex = 12;
            groupBox_Loot.TabStop = false;
            groupBox_Loot.Text = "Loot";
            groupBox_Loot.Visible = false;
            // 
            // button_RefreshLoot
            // 
            button_RefreshLoot.Location = new Point(178, 34);
            button_RefreshLoot.Name = "button_RefreshLoot";
            button_RefreshLoot.Size = new Size(55, 49);
            button_RefreshLoot.TabIndex = 21;
            button_RefreshLoot.Text = "Refresh Loot";
            button_RefreshLoot.UseVisualStyleBackColor = true;
            button_RefreshLoot.Click += button_RefreshLoot_Click;
            // 
            // button_LootApply
            // 
            button_LootApply.Enabled = false;
            button_LootApply.Location = new Point(82, 147);
            button_LootApply.Name = "button_LootApply";
            button_LootApply.Size = new Size(61, 46);
            button_LootApply.TabIndex = 7;
            button_LootApply.Text = "Apply";
            button_LootApply.UseVisualStyleBackColor = true;
            button_LootApply.Click += button_LootApply_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 100);
            label9.Name = "label9";
            label9.Size = new Size(210, 15);
            label9.TabIndex = 6;
            label9.Text = "Find Item(s) by Name (sep by comma)";
            // 
            // textBox_LootFilterByName
            // 
            textBox_LootFilterByName.Location = new Point(6, 118);
            textBox_LootFilterByName.MaxLength = 512;
            textBox_LootFilterByName.Name = "textBox_LootFilterByName";
            textBox_LootFilterByName.Size = new Size(227, 23);
            textBox_LootFilterByName.TabIndex = 5;
            textBox_LootFilterByName.TextChanged += textBox_LootFilterByName_TextChanged;
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
            // textBox_LootImpValue
            // 
            textBox_LootImpValue.Location = new Point(86, 52);
            textBox_LootImpValue.MaxLength = 7;
            textBox_LootImpValue.Name = "textBox_LootImpValue";
            textBox_LootImpValue.Size = new Size(57, 23);
            textBox_LootImpValue.TabIndex = 2;
            textBox_LootImpValue.Text = "300000";
            textBox_LootImpValue.TextChanged += textBox_LootImpValue_TextChanged;
            // 
            // textBox_LootRegValue
            // 
            textBox_LootRegValue.Location = new Point(21, 52);
            textBox_LootRegValue.MaxLength = 6;
            textBox_LootRegValue.Name = "textBox_LootRegValue";
            textBox_LootRegValue.Size = new Size(50, 23);
            textBox_LootRegValue.TabIndex = 1;
            textBox_LootRegValue.Text = "70000";
            textBox_LootRegValue.TextChanged += textBox_LootRegValue_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 19);
            label6.Name = "label6";
            label6.Size = new Size(137, 15);
            label6.TabIndex = 0;
            label6.Text = "Minimum Value to Show";
            // 
            // groupBox_MapSetup
            // 
            groupBox_MapSetup.Controls.Add(button_Loot);
            groupBox_MapSetup.Controls.Add(checkBox_MapFree);
            groupBox_MapSetup.Controls.Add(button_MapSetupApply);
            groupBox_MapSetup.Controls.Add(textBox_mapScale);
            groupBox_MapSetup.Controls.Add(label5);
            groupBox_MapSetup.Controls.Add(textBox_mapY);
            groupBox_MapSetup.Controls.Add(label4);
            groupBox_MapSetup.Controls.Add(textBox_mapX);
            groupBox_MapSetup.Controls.Add(label_Pos);
            groupBox_MapSetup.Location = new Point(8, 6);
            groupBox_MapSetup.Name = "groupBox_MapSetup";
            groupBox_MapSetup.Size = new Size(327, 175);
            groupBox_MapSetup.TabIndex = 11;
            groupBox_MapSetup.TabStop = false;
            groupBox_MapSetup.Text = "Map Setup";
            groupBox_MapSetup.Visible = false;
            // 
            // button_Loot
            // 
            button_Loot.Location = new Point(85, 0);
            button_Loot.Name = "button_Loot";
            button_Loot.Size = new Size(44, 25);
            button_Loot.TabIndex = 12;
            button_Loot.Text = "Loot";
            button_Loot.UseVisualStyleBackColor = true;
            button_Loot.Visible = false;
            button_Loot.Click += button_LootFilter_Click;
            // 
            // checkBox_MapFree
            // 
            checkBox_MapFree.Appearance = Appearance.Button;
            checkBox_MapFree.AutoSize = true;
            checkBox_MapFree.Location = new Point(0, 0);
            checkBox_MapFree.Name = "checkBox_MapFree";
            checkBox_MapFree.Size = new Size(66, 25);
            checkBox_MapFree.TabIndex = 17;
            checkBox_MapFree.Text = "Map Free";
            checkBox_MapFree.UseVisualStyleBackColor = true;
            checkBox_MapFree.CheckedChanged += checkBox_MapFree_CheckedChanged;
            // 
            // button_MapSetupApply
            // 
            button_MapSetupApply.Location = new Point(6, 143);
            button_MapSetupApply.Name = "button_MapSetupApply";
            button_MapSetupApply.Size = new Size(75, 23);
            button_MapSetupApply.TabIndex = 16;
            button_MapSetupApply.Text = "Apply";
            button_MapSetupApply.UseVisualStyleBackColor = true;
            button_MapSetupApply.Click += button_MapSetupApply_Click;
            // 
            // textBox_mapScale
            // 
            textBox_mapScale.Location = new Point(46, 101);
            textBox_mapScale.Name = "textBox_mapScale";
            textBox_mapScale.Size = new Size(50, 23);
            textBox_mapScale.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 104);
            label5.Name = "label5";
            label5.Size = new Size(34, 15);
            label5.TabIndex = 14;
            label5.Text = "Scale";
            // 
            // textBox_mapY
            // 
            textBox_mapY.Location = new Point(102, 67);
            textBox_mapY.Name = "textBox_mapY";
            textBox_mapY.Size = new Size(50, 23);
            textBox_mapY.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 70);
            label4.Name = "label4";
            label4.Size = new Size(24, 15);
            label4.TabIndex = 12;
            label4.Text = "X,Y";
            // 
            // textBox_mapX
            // 
            textBox_mapX.Location = new Point(46, 67);
            textBox_mapX.Name = "textBox_mapX";
            textBox_mapX.Size = new Size(50, 23);
            textBox_mapX.TabIndex = 11;
            // 
            // label_Pos
            // 
            label_Pos.AutoSize = true;
            label_Pos.Location = new Point(7, 19);
            label_Pos.Margin = new Padding(4, 0, 4, 0);
            label_Pos.Name = "label_Pos";
            label_Pos.Size = new Size(43, 15);
            label_Pos.TabIndex = 10;
            label_Pos.Text = "coords";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1328, 638);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Settings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(487, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(335, 629);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Memory Writing";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox_HidePlayerPanel);
            groupBox1.Controls.Add(label_UIScale);
            groupBox1.Controls.Add(trackBar_UIScale);
            groupBox1.Controls.Add(checkBox_HideNames);
            groupBox1.Controls.Add(textBox_PrimTeamID);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(checkBox_Aimview);
            groupBox1.Controls.Add(button_Restart);
            groupBox1.Controls.Add(checkBox_MapSetup);
            groupBox1.Controls.Add(checkBox_Loot);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(trackBar_Zoom);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(trackBar_AimLength);
            groupBox1.Controls.Add(button_Map);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(477, 632);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Radar Config";
            // 
            // checkBox_HidePlayerPanel
            // 
            checkBox_HidePlayerPanel.AutoSize = true;
            checkBox_HidePlayerPanel.Checked = true;
            checkBox_HidePlayerPanel.CheckState = CheckState.Checked;
            checkBox_HidePlayerPanel.Location = new Point(193, 176);
            checkBox_HidePlayerPanel.Name = "checkBox_HidePlayerPanel";
            checkBox_HidePlayerPanel.Size = new Size(110, 19);
            checkBox_HidePlayerPanel.TabIndex = 29;
            checkBox_HidePlayerPanel.Text = "Hide Player Info";
            checkBox_HidePlayerPanel.UseVisualStyleBackColor = true;
            checkBox_HidePlayerPanel.CheckedChanged += checkBox_HidePlayerPanel_CheckedChanged;
            // 
            // label_UIScale
            // 
            label_UIScale.AutoSize = true;
            label_UIScale.Location = new Point(383, 247);
            label_UIScale.Name = "label_UIScale";
            label_UIScale.Size = new Size(66, 15);
            label_UIScale.TabIndex = 28;
            label_UIScale.Text = "UI Scale 1.0";
            label_UIScale.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trackBar_UIScale
            // 
            trackBar_UIScale.LargeChange = 10;
            trackBar_UIScale.Location = new Point(395, 265);
            trackBar_UIScale.Maximum = 200;
            trackBar_UIScale.Minimum = 50;
            trackBar_UIScale.Name = "trackBar_UIScale";
            trackBar_UIScale.Orientation = Orientation.Vertical;
            trackBar_UIScale.Size = new Size(45, 403);
            trackBar_UIScale.TabIndex = 27;
            trackBar_UIScale.Value = 100;
            // 
            // checkBox_HideNames
            // 
            checkBox_HideNames.AutoSize = true;
            checkBox_HideNames.Location = new Point(326, 151);
            checkBox_HideNames.Name = "checkBox_HideNames";
            checkBox_HideNames.Size = new Size(114, 19);
            checkBox_HideNames.TabIndex = 26;
            checkBox_HideNames.Text = "Hide Names (F6)";
            checkBox_HideNames.UseVisualStyleBackColor = true;
            // 
            // textBox_PrimTeamID
            // 
            textBox_PrimTeamID.Location = new Point(44, 97);
            textBox_PrimTeamID.MaxLength = 12;
            textBox_PrimTeamID.Name = "textBox_PrimTeamID";
            textBox_PrimTeamID.Size = new Size(147, 23);
            textBox_PrimTeamID.TabIndex = 25;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 79);
            label3.Name = "label3";
            label3.Size = new Size(147, 15);
            label3.TabIndex = 22;
            label3.Text = "Primary Teammate Acct ID";
            // 
            // checkBox_Aimview
            // 
            checkBox_Aimview.AutoSize = true;
            checkBox_Aimview.Location = new Point(193, 151);
            checkBox_Aimview.Name = "checkBox_Aimview";
            checkBox_Aimview.Size = new Size(127, 19);
            checkBox_Aimview.TabIndex = 19;
            checkBox_Aimview.Text = "Show Aimview (F4)";
            checkBox_Aimview.UseVisualStyleBackColor = true;
            // 
            // button_Restart
            // 
            button_Restart.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            button_Restart.Location = new Point(359, 33);
            button_Restart.Name = "button_Restart";
            button_Restart.Size = new Size(81, 61);
            button_Restart.TabIndex = 18;
            button_Restart.Text = "Restart Game";
            button_Restart.UseVisualStyleBackColor = true;
            button_Restart.Click += button_Restart_Click;
            // 
            // checkBox_MapSetup
            // 
            checkBox_MapSetup.AutoSize = true;
            checkBox_MapSetup.Location = new Point(44, 176);
            checkBox_MapSetup.Name = "checkBox_MapSetup";
            checkBox_MapSetup.Size = new Size(153, 19);
            checkBox_MapSetup.TabIndex = 9;
            checkBox_MapSetup.Text = "Show Map Setup Helper";
            checkBox_MapSetup.UseVisualStyleBackColor = true;
            checkBox_MapSetup.CheckedChanged += checkBox_MapSetup_CheckedChanged;
            // 
            // checkBox_Loot
            // 
            checkBox_Loot.AutoSize = true;
            checkBox_Loot.Location = new Point(44, 151);
            checkBox_Loot.Name = "checkBox_Loot";
            checkBox_Loot.Size = new Size(105, 19);
            checkBox_Loot.TabIndex = 17;
            checkBox_Loot.Text = "Show Loot (F3)";
            checkBox_Loot.UseVisualStyleBackColor = true;
            checkBox_Loot.CheckedChanged += checkBox_Loot_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(193, 217);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(136, 45);
            label1.TabIndex = 16;
            label1.Text = "Zoom\r\nF1/Mouse Whl Up = In\r\nF2/Mouse Whl Dn = Out";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trackBar_Zoom
            // 
            trackBar_Zoom.LargeChange = 1;
            trackBar_Zoom.Location = new Point(237, 265);
            trackBar_Zoom.Maximum = 200;
            trackBar_Zoom.Minimum = 1;
            trackBar_Zoom.Name = "trackBar_Zoom";
            trackBar_Zoom.Orientation = Orientation.Vertical;
            trackBar_Zoom.Size = new Size(45, 403);
            trackBar_Zoom.TabIndex = 15;
            trackBar_Zoom.Value = 100;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(77, 232);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(99, 30);
            label2.TabIndex = 13;
            label2.Text = "Player/Teammate\r\nAimline";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trackBar_AimLength
            // 
            trackBar_AimLength.LargeChange = 50;
            trackBar_AimLength.Location = new Point(104, 265);
            trackBar_AimLength.Margin = new Padding(4, 3, 4, 3);
            trackBar_AimLength.Maximum = 1000;
            trackBar_AimLength.Minimum = 10;
            trackBar_AimLength.Name = "trackBar_AimLength";
            trackBar_AimLength.Orientation = Orientation.Vertical;
            trackBar_AimLength.Size = new Size(45, 403);
            trackBar_AimLength.SmallChange = 5;
            trackBar_AimLength.TabIndex = 11;
            trackBar_AimLength.Value = 500;
            // 
            // button_Map
            // 
            button_Map.Location = new Point(44, 33);
            button_Map.Margin = new Padding(4, 3, 4, 3);
            button_Map.Name = "button_Map";
            button_Map.Size = new Size(107, 27);
            button_Map.TabIndex = 7;
            button_Map.Text = "Toggle Map (F5)";
            button_Map.UseVisualStyleBackColor = true;
            button_Map.Click += button_Map_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(richTextBox_PlayersInfo);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1328, 638);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Player Loadouts";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox_PlayersInfo
            // 
            richTextBox_PlayersInfo.Dock = DockStyle.Fill;
            richTextBox_PlayersInfo.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox_PlayersInfo.Location = new Point(0, 0);
            richTextBox_PlayersInfo.Name = "richTextBox_PlayersInfo";
            richTextBox_PlayersInfo.ReadOnly = true;
            richTextBox_PlayersInfo.Size = new Size(1328, 638);
            richTextBox_PlayersInfo.TabIndex = 0;
            richTextBox_PlayersInfo.Text = "";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(listView_PmcHistory);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1328, 638);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Player History";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // listView_PmcHistory
            // 
            listView_PmcHistory.AutoArrange = false;
            listView_PmcHistory.Columns.AddRange(new ColumnHeader[] { columnHeader_Entry, columnHeader_ID });
            listView_PmcHistory.Dock = DockStyle.Fill;
            listView_PmcHistory.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listView_PmcHistory.FullRowSelect = true;
            listView_PmcHistory.GridLines = true;
            listView_PmcHistory.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView_PmcHistory.Location = new Point(0, 0);
            listView_PmcHistory.MultiSelect = false;
            listView_PmcHistory.Name = "listView_PmcHistory";
            listView_PmcHistory.Size = new Size(1328, 638);
            listView_PmcHistory.TabIndex = 0;
            listView_PmcHistory.UseCompatibleStateImageBehavior = false;
            listView_PmcHistory.View = View.Details;
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
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1336, 666);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "EFT Radar";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox_Loot.ResumeLayout(false);
            groupBox_Loot.PerformLayout();
            groupBox_MapSetup.ResumeLayout(false);
            groupBox_MapSetup.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_UIScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_Zoom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_AimLength).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private Label label2;
        private TrackBar trackBar_AimLength;
        private Button button_Map;
        private Label label_Pos;
        private Label label1;
        private TrackBar trackBar_Zoom;
        private CheckBox checkBox_Loot;
        private CheckBox checkBox_MapSetup;
        private Button button_Restart;
        private GroupBox groupBox_MapSetup;
        private Button button_MapSetupApply;
        private TextBox textBox_mapScale;
        private Label label5;
        private TextBox textBox_mapY;
        private Label label4;
        private TextBox textBox_mapX;
        private BindingSource bindingSource1;
        private CheckBox checkBox_Aimview;
        private CheckBox checkBox_MapFree;
        private TabPage tabPage3;
        private RichTextBox richTextBox_PlayersInfo;
        private TabPage tabPage4;
        private ListView listView_PmcHistory;
        private ColumnHeader columnHeader_Entry;
        private ColumnHeader columnHeader_ID;
        private Label label3;
        private TextBox textBox_PrimTeamID;
        private CheckBox checkBox_HideNames;
        private GroupBox groupBox_Loot;
        private Button button_LootApply;
        private Label label9;
        private TextBox textBox_LootFilterByName;
        private Label label8;
        private Label label7;
        private TextBox textBox_LootImpValue;
        private TextBox textBox_LootRegValue;
        private Label label6;
        private Button button_Loot;
        private Button button_RefreshLoot;
        private Label label_UIScale;
        private TrackBar trackBar_UIScale;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private CheckBox checkBox_HidePlayerPanel;
        private GroupBox groupBox2;
        private CheckBox thermalVisionCheckBox;
        private CheckBox NightVisionCheckBox;
        private CheckBox OpticThermalBox;
    }
}

