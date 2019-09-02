namespace ppt_replay_gui
{
    partial class mainform
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAllReplaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertReplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllReplaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView_replays = new System.Windows.Forms.TreeView();
            this.tab_replay_control = new System.Windows.Forms.TabControl();
            this.tabReplayInfo = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_delete_replay = new System.Windows.Forms.Button();
            this.button_import_replay = new System.Windows.Forms.Button();
            this.button_extract_replay = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_player4 = new System.Windows.Forms.Label();
            this.label_player3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_player2 = new System.Windows.Forms.Label();
            this.label_player1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_length = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_player_count = new System.Windows.Forms.Label();
            this.label_recorded = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_id = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPagePrep = new System.Windows.Forms.TabPage();
            this.textBox_prepDump = new System.Windows.Forms.TextBox();
            this.tabReplayDebug = new System.Windows.Forms.TabPage();
            this.label_data_location = new System.Windows.Forms.Label();
            this.label_prep_location = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog_data = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.saveFileDialog_dem = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog_dem = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog_exportAll = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.saveFileDialog_data = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.tab_replay_control.SuspendLayout();
            this.tabReplayInfo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPagePrep.SuspendLayout();
            this.tabReplayDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(704, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSaveToolStripMenuItem,
            this.exportSaveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importSaveToolStripMenuItem
            // 
            this.importSaveToolStripMenuItem.Name = "importSaveToolStripMenuItem";
            this.importSaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importSaveToolStripMenuItem.Text = "Import Save";
            this.importSaveToolStripMenuItem.Click += new System.EventHandler(this.ImportSaveToolStripMenuItem_Click);
            // 
            // exportSaveToolStripMenuItem
            // 
            this.exportSaveToolStripMenuItem.Name = "exportSaveToolStripMenuItem";
            this.exportSaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportSaveToolStripMenuItem.Text = "Export Save";
            this.exportSaveToolStripMenuItem.Click += new System.EventHandler(this.ExportSaveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractAllReplaysToolStripMenuItem,
            this.insertReplayToolStripMenuItem,
            this.clearAllReplaysToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // extractAllReplaysToolStripMenuItem
            // 
            this.extractAllReplaysToolStripMenuItem.Name = "extractAllReplaysToolStripMenuItem";
            this.extractAllReplaysToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.extractAllReplaysToolStripMenuItem.Text = "&Extract all replays";
            this.extractAllReplaysToolStripMenuItem.Click += new System.EventHandler(this.ExtractAllReplaysToolStripMenuItem_Click);
            // 
            // insertReplayToolStripMenuItem
            // 
            this.insertReplayToolStripMenuItem.Name = "insertReplayToolStripMenuItem";
            this.insertReplayToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.insertReplayToolStripMenuItem.Text = "&Insert replay";
            this.insertReplayToolStripMenuItem.Click += new System.EventHandler(this.InsertReplayToolStripMenuItem_Click);
            // 
            // clearAllReplaysToolStripMenuItem
            // 
            this.clearAllReplaysToolStripMenuItem.Name = "clearAllReplaysToolStripMenuItem";
            this.clearAllReplaysToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.clearAllReplaysToolStripMenuItem.Text = "&Clear all replays";
            this.clearAllReplaysToolStripMenuItem.Click += new System.EventHandler(this.ClearAllReplaysToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // treeView_replays
            // 
            this.treeView_replays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView_replays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView_replays.Location = new System.Drawing.Point(0, 0);
            this.treeView_replays.Name = "treeView_replays";
            this.treeView_replays.Size = new System.Drawing.Size(218, 292);
            this.treeView_replays.TabIndex = 1;
            this.treeView_replays.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_replays_AfterSelect);
            // 
            // tab_replay_control
            // 
            this.tab_replay_control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_replay_control.Controls.Add(this.tabReplayInfo);
            this.tab_replay_control.Controls.Add(this.tabPagePrep);
            this.tab_replay_control.Controls.Add(this.tabReplayDebug);
            this.tab_replay_control.Location = new System.Drawing.Point(0, 0);
            this.tab_replay_control.Name = "tab_replay_control";
            this.tab_replay_control.SelectedIndex = 0;
            this.tab_replay_control.Size = new System.Drawing.Size(482, 292);
            this.tab_replay_control.TabIndex = 2;
            this.tab_replay_control.Visible = false;
            // 
            // tabReplayInfo
            // 
            this.tabReplayInfo.Controls.Add(this.groupBox3);
            this.tabReplayInfo.Controls.Add(this.groupBox2);
            this.tabReplayInfo.Controls.Add(this.groupBox1);
            this.tabReplayInfo.Location = new System.Drawing.Point(4, 22);
            this.tabReplayInfo.Name = "tabReplayInfo";
            this.tabReplayInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabReplayInfo.Size = new System.Drawing.Size(474, 266);
            this.tabReplayInfo.TabIndex = 0;
            this.tabReplayInfo.Text = "Replay Information";
            this.tabReplayInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button_delete_replay);
            this.groupBox3.Controls.Add(this.button_import_replay);
            this.groupBox3.Controls.Add(this.button_extract_replay);
            this.groupBox3.Location = new System.Drawing.Point(240, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 254);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // button_delete_replay
            // 
            this.button_delete_replay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_delete_replay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_delete_replay.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.button_delete_replay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_delete_replay.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button_delete_replay.Location = new System.Drawing.Point(7, 225);
            this.button_delete_replay.Name = "button_delete_replay";
            this.button_delete_replay.Size = new System.Drawing.Size(215, 23);
            this.button_delete_replay.TabIndex = 2;
            this.button_delete_replay.Text = "Delete Replay";
            this.button_delete_replay.UseVisualStyleBackColor = false;
            this.button_delete_replay.Click += new System.EventHandler(this.Button_delete_replay_Click);
            // 
            // button_import_replay
            // 
            this.button_import_replay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_import_replay.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_import_replay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_import_replay.Location = new System.Drawing.Point(6, 19);
            this.button_import_replay.Name = "button_import_replay";
            this.button_import_replay.Size = new System.Drawing.Size(215, 23);
            this.button_import_replay.TabIndex = 1;
            this.button_import_replay.Text = "Replace Replay";
            this.button_import_replay.UseVisualStyleBackColor = true;
            this.button_import_replay.Click += new System.EventHandler(this.Button_import_replay_Click);
            // 
            // button_extract_replay
            // 
            this.button_extract_replay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_extract_replay.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_extract_replay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_extract_replay.Location = new System.Drawing.Point(6, 48);
            this.button_extract_replay.Name = "button_extract_replay";
            this.button_extract_replay.Size = new System.Drawing.Size(215, 23);
            this.button_extract_replay.TabIndex = 0;
            this.button_extract_replay.Text = "Extract Replay";
            this.button_extract_replay.UseVisualStyleBackColor = true;
            this.button_extract_replay.Click += new System.EventHandler(this.Button_extract_replay_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label_player4);
            this.groupBox2.Controls.Add(this.label_player3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label_player2);
            this.groupBox2.Controls.Add(this.label_player1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(6, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 148);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Players";
            // 
            // label_player4
            // 
            this.label_player4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player4.Location = new System.Drawing.Point(24, 73);
            this.label_player4.Name = "label_player4";
            this.label_player4.Size = new System.Drawing.Size(198, 19);
            this.label_player4.TabIndex = 10;
            this.label_player4.Text = "??";
            this.label_player4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_player3
            // 
            this.label_player3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player3.Location = new System.Drawing.Point(24, 54);
            this.label_player3.Name = "label_player3";
            this.label_player3.Size = new System.Drawing.Size(198, 19);
            this.label_player3.TabIndex = 11;
            this.label_player3.Text = "??";
            this.label_player3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 19);
            this.label10.TabIndex = 8;
            this.label10.Text = "4:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 19);
            this.label11.TabIndex = 9;
            this.label11.Text = "3:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_player2
            // 
            this.label_player2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player2.Location = new System.Drawing.Point(24, 35);
            this.label_player2.Name = "label_player2";
            this.label_player2.Size = new System.Drawing.Size(198, 19);
            this.label_player2.TabIndex = 7;
            this.label_player2.Text = "??";
            this.label_player2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_player1
            // 
            this.label_player1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player1.Location = new System.Drawing.Point(24, 16);
            this.label_player1.Name = "label_player1";
            this.label_player1.Size = new System.Drawing.Size(198, 19);
            this.label_player1.TabIndex = 7;
            this.label_player1.Text = "??";
            this.label_player1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "2:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "1:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_length);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label_player_count);
            this.groupBox1.Controls.Add(this.label_recorded);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label_id);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Match Information";
            // 
            // label_length
            // 
            this.label_length.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_length.Location = new System.Drawing.Point(62, 73);
            this.label_length.Name = "label_length";
            this.label_length.Size = new System.Drawing.Size(160, 19);
            this.label_length.TabIndex = 7;
            this.label_length.Text = "??";
            this.label_length.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Length:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_player_count
            // 
            this.label_player_count.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player_count.Location = new System.Drawing.Point(62, 54);
            this.label_player_count.Name = "label_player_count";
            this.label_player_count.Size = new System.Drawing.Size(160, 19);
            this.label_player_count.TabIndex = 5;
            this.label_player_count.Text = "??";
            this.label_player_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_recorded
            // 
            this.label_recorded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_recorded.Location = new System.Drawing.Point(62, 35);
            this.label_recorded.Name = "label_recorded";
            this.label_recorded.Size = new System.Drawing.Size(160, 19);
            this.label_recorded.TabIndex = 3;
            this.label_recorded.Text = "??";
            this.label_recorded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Players:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_id
            // 
            this.label_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_id.Location = new System.Drawing.Point(62, 16);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(160, 19);
            this.label_id.TabIndex = 2;
            this.label_id.Text = "??";
            this.label_id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Recorded:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPagePrep
            // 
            this.tabPagePrep.Controls.Add(this.textBox_prepDump);
            this.tabPagePrep.Location = new System.Drawing.Point(4, 22);
            this.tabPagePrep.Name = "tabPagePrep";
            this.tabPagePrep.Size = new System.Drawing.Size(474, 266);
            this.tabPagePrep.TabIndex = 2;
            this.tabPagePrep.Text = "PREP Hex dump";
            this.tabPagePrep.UseVisualStyleBackColor = true;
            // 
            // textBox_prepDump
            // 
            this.textBox_prepDump.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_prepDump.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_prepDump.Location = new System.Drawing.Point(0, 0);
            this.textBox_prepDump.Multiline = true;
            this.textBox_prepDump.Name = "textBox_prepDump";
            this.textBox_prepDump.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_prepDump.Size = new System.Drawing.Size(474, 266);
            this.textBox_prepDump.TabIndex = 0;
            this.textBox_prepDump.WordWrap = false;
            // 
            // tabReplayDebug
            // 
            this.tabReplayDebug.Controls.Add(this.label_data_location);
            this.tabReplayDebug.Controls.Add(this.label_prep_location);
            this.tabReplayDebug.Controls.Add(this.label9);
            this.tabReplayDebug.Controls.Add(this.label12);
            this.tabReplayDebug.Location = new System.Drawing.Point(4, 22);
            this.tabReplayDebug.Name = "tabReplayDebug";
            this.tabReplayDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabReplayDebug.Size = new System.Drawing.Size(474, 266);
            this.tabReplayDebug.TabIndex = 1;
            this.tabReplayDebug.Text = "Debug";
            this.tabReplayDebug.UseVisualStyleBackColor = true;
            // 
            // label_data_location
            // 
            this.label_data_location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_data_location.Location = new System.Drawing.Point(96, 22);
            this.label_data_location.Name = "label_data_location";
            this.label_data_location.Size = new System.Drawing.Size(165, 19);
            this.label_data_location.TabIndex = 7;
            this.label_data_location.Text = "??";
            this.label_data_location.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_prep_location
            // 
            this.label_prep_location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_prep_location.Location = new System.Drawing.Point(96, 3);
            this.label_prep_location.Name = "label_prep_location";
            this.label_prep_location.Size = new System.Drawing.Size(165, 19);
            this.label_prep_location.TabIndex = 6;
            this.label_prep_location.Text = "??";
            this.label_prep_location.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 19);
            this.label9.TabIndex = 5;
            this.label9.Text = "DATA Location:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 19);
            this.label12.TabIndex = 4;
            this.label12.Text = "PREP Location:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(482, 292);
            this.label3.TabIndex = 3;
            this.label3.Text = "Open a replay and select it from the list to the left.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog_data
            // 
            this.openFileDialog_data.FileName = "openFileDialog1";
            this.openFileDialog_data.Filter = "Puyo Puyo Tetris Save|data.bin|Bin files|*.bin|All files|*.*";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_replays);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tab_replay_control);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(704, 292);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 4;
            // 
            // saveFileDialog_dem
            // 
            this.saveFileDialog_dem.Filter = "Replay Files|*.dem|All Files|*.*";
            // 
            // openFileDialog_dem
            // 
            this.openFileDialog_dem.Filter = "Replay Files|*.dem|All Files|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Location = new System.Drawing.Point(0, 319);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(704, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // saveFileDialog_data
            // 
            this.saveFileDialog_data.Filter = "Puyo Puyo Tetris Save|data.bin|Bin files|*.bin|All files|*.*";
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 341);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainform";
            this.Text = "CN_PPTRM - Puyo Puyo Tetris Replay Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tab_replay_control.ResumeLayout(false);
            this.tabReplayInfo.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPagePrep.ResumeLayout(false);
            this.tabPagePrep.PerformLayout();
            this.tabReplayDebug.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public System.Windows.Forms.TreeView treeView_replays;
        private System.Windows.Forms.TabControl tab_replay_control;
        private System.Windows.Forms.TabPage tabReplayInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_extract_replay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_player4;
        private System.Windows.Forms.Label label_player3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_player2;
        private System.Windows.Forms.Label label_player1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_player_count;
        private System.Windows.Forms.Label label_recorded;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabReplayDebug;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog_data;
        private System.Windows.Forms.Label label_data_location;
        private System.Windows.Forms.Label label_prep_location;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_import_replay;
        private System.Windows.Forms.Label label_length;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_dem;
        private System.Windows.Forms.OpenFileDialog openFileDialog_dem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractAllReplaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllReplaysToolStripMenuItem;
        private System.Windows.Forms.Button button_delete_replay;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_exportAll;
        private System.Windows.Forms.ToolStripMenuItem insertReplayToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage tabPagePrep;
        private System.Windows.Forms.TextBox textBox_prepDump;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_data;
    }
}

