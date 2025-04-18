/*
 * Puyo Puyo Tetris Replay Manager (CN_PPTRM) 
 * 
 * Description:
 *     Tool for managing replays in Puyo Puyo Tetris. It must suck for players
 *     to have to keep deleting replays because of the 50 replay limit. This
 *     tool aims to remove the 50 replay limit by letting you export the replay
 *     data directly from saves. It also lets you import saves if you wish.
 * 
 * Author:
 *     iDestyKK
 */

using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ppt_replay_gui {
    public partial class mainform : Form {
        // Utility Class
        private UTIL util = new UTIL();

        // Data.bin class
        private PPT.data_bin DATA = new PPT.data_bin_ppt1_pc();

        // Also a PPT class, think of it as util
        private PPT ppt = new PPT();

        // Oh and let's just make it global. Yeah?
        public int selected_id;

        private string wintitle = "CN_PPTRM - Puyo Puyo Tetris Replay Manager";

        public mainform() {
            InitializeComponent();

            selected_id = -1;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) {
            about win = new about();
            win.ShowDialog();
        }

        private void ImportSaveToolStripMenuItem_Click(object sender, EventArgs e) {
            // First, get the save file
            if (openFileDialog_data.ShowDialog() == DialogResult.OK) {
                // Then let's see what save it is (User Input)
                version_select vsel = new version_select();
                vsel.ShowDialog();

                if (vsel.bttn_val == DialogResult.OK) {
                    // Recreate "DATA" based on game
                    switch (vsel.game_val) {
                        case PPT.game_t.PPT1:
                            DATA = new PPT.data_bin_ppt1_pc();
                            break;
                        case PPT.game_t.PPT2:
                            DATA = new PPT.data_bin_ppt2_pc();
                            break;
                        case PPT.game_t.PPES:
                            DATA = new PPT.data_bin_ppes_pc();
                            break;
                        default:
                            MessageBox.Show(
                                "Invalid Game/Platform selection. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            break;
                    }

                    DATA.path = openFileDialog_data.FileName;

                    // Adjust the form's name
                    this.Text = wintitle + " (" + DATA.path + ")";

                    // Process the save data
                    DATA.open();
                    DATA.fill_tree(treeView_replays);

                    // tab_replay_control.Visible = true;
                }
            }
        }

        private void TreeView_replays_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Parent != null) {
                if (e.Node.Parent.Name == "treeNode_saveData") {
                    // Welp, we got a node from "data.bin". Fill out the data accordingly.

                    // Get the replay number
                    string label = e.Node.Text.Substring(0, 2);
                    uint   id    = Convert.ToUInt32(label);

                    selected_id = (int) id;

                    // Okay, now let's fill out some stuff.
                    label_id.Text = id.ToString();
                    label_recorded.Text = string.Format(
                        "{0:D4}/{1:D2}/{2:D2} @ {3:D2}:{4:D2}",
                        DATA.get_year  (id),
                        DATA.get_month (id),
                        DATA.get_day   (id),
                        DATA.get_hour  (id),
                        DATA.get_minute(id)
                    );

                    // Player information
                    label_player_count.Text = DATA.get_player_count(id).ToString();
                    label_player1.Text = DATA.get_player_name(id, 0);
                    label_player2.Text = DATA.get_player_name(id, 1);
                    label_player3.Text = DATA.get_player_name(id, 2);
                    label_player4.Text = DATA.get_player_name(id, 3);

                    // Length of the match
                    label_length.Text = DATA.get_length_as_string(id);

                    // Debug information
                    label_prep_location.Text = string.Format("0x{0:X8}", PPT.data_bin.PREP_ADDR[id]);
                    label_data_location.Text = string.Format("0x{0:X8}", PPT.data_bin.DATA_ADDR[id]);

                    // Generate and dump hex values
                    textBox_prepDump.Text = DATA.generate_hexdump(
                        PPT.data_bin.PREP_ADDR[id],
                        DATA.PREP_LEN
                    );
                }
                else {
                    selected_id = -1;
                }
            }
            else {
                selected_id = -1;
            }

            tab_replay_control.Visible = (selected_id != -1);
        }

        private void Button_extract_replay_Click(object sender, EventArgs e) {
            if (saveFileDialog_dem.ShowDialog() == DialogResult.OK) {
                string path = saveFileDialog_dem.FileName;

                // Generate both file segments
                byte[] header = ppt.generate_dem_header(
                    DATA.game_val, DATA.platform_val
                );

                byte[] data = util.gz_compress(
                    DATA.generate_dem(Convert.ToUInt32(label_id.Text))
                );

                // Create final file based on both segments
                byte[] buffer = new byte[header.Length + data.Length];
                header.CopyTo(buffer, 0);
                data.CopyTo(buffer, header.Length);

                // File.WriteAllBytes(path, buffer);
                File.WriteAllBytes(path, buffer);

                MessageBox.Show("Replay Saved Successfully.");
            }
        }

        public byte[] open_dem_file(string fpath) {
            byte[] buffer;
            PPT.game_t game_val;
            PPT.platform_t platform_val;
            uint version;

            // Check header. This is 16 bytes
            byte[] header_test = new byte[16];

            using (BinaryReader br = new BinaryReader(new FileStream(fpath, FileMode.Open))) {
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                br.Read(header_test, 0, 16);

                // Check if legacy. If so, DATA's PPT type MUST be PPT1/PC
                if (BitConverter.ToUInt16(header_test, 0) == 0x8B1F) {
                    // Header tells it's a legacy file w/ 0x1F 0x8B.
                    if (
                        DATA.game_val == PPT.game_t.PPT1 &&
                        DATA.platform_val == PPT.platform_t.PC
                    ) {
                        // It's PPT1/PC. Perform the read of entire file
                        br.BaseStream.Seek(0, SeekOrigin.Begin);

                        buffer = new byte[br.BaseStream.Length];
                        br.Read(buffer, 0, (int)br.BaseStream.Length);

                        // Decompress via GZip
                        return util.gz_decompress(buffer);
                    }

                    // It's not PPT1/PC. Report an error and back out.
                    MessageBox.Show(
                        "You have selected a legacy replay from an " +
                        "older version of CN_PPTRM. It can only be " +
                        "imported into the PC version of Puyo Puyo " +
                        "Tetris.",

                        "Failed to load replay - " +
                        "Legacy PPT Replay Error",

                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return null;
                }
                else
                // Check if legacy uncompressed. If so, same applies
                if (BitConverter.ToUInt32(header_test, 0) == 0x50455250) {
                    // Header tells it's a legacy file w/ PREP.
                    if (
                        DATA.game_val == PPT.game_t.PPT1 &&
                        DATA.platform_val == PPT.platform_t.PC
                    ) {
                        // It's PPT1/PC. Perform the read of entire file
                        br.BaseStream.Seek(0, SeekOrigin.Begin);

                        buffer = new byte[br.BaseStream.Length];
                        br.Read(buffer, 0, (int)br.BaseStream.Length);

                        // Decompress via GZip
                        return buffer;
                    }

                    // It's not PPT1/PC. Report an error and back out.
                    MessageBox.Show(
                        "You have selected a legacy replay from an " +
                        "older version of CN_PPTRM. It can only be " +
                        "imported into the PC version of Puyo Puyo " +
                        "Tetris.",

                        "Failed to load replay - " +
                        "Legacy PPT Replay Error",

                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return null;
                }
                else
                // Check if new format. If so, we'll know what to do.
                if (BitConverter.ToUInt32(header_test, 0) == 0x314D4544) {
                    // Extract game, platform, and version
                    game_val = (PPT.game_t)
                        BitConverter.ToInt32(header_test, 4);

                    platform_val = (PPT.platform_t)
                        BitConverter.ToInt32(header_test, 8);

                    version = (uint)
                        BitConverter.ToUInt32(header_test, 12);

                    // Compare with DATA (let's ignore "version" for now)
                    if (
                        game_val != DATA.game_val ||
                        platform_val != DATA.platform_val
                    ) {
                        // Check failed. Print out a detailed error message.
                        MessageBox.Show(
                            "You have attempted to read in a replay from " +
                            ppt.generate_game_name(game_val, platform_val) +
                            ". Please load in a replay file for " +
                            ppt.generate_game_name(
                                DATA.game_val, DATA.platform_val
                            ) +
                            " to insert into the save.",
                            "Failed to load replay - Wrong Game",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return null;
                    }

                    // Assume it's a valid save. Skip the first 16 bytes and
                    // read in the rest of the file. Then decompress it.
                    br.BaseStream.Seek(16, SeekOrigin.Begin);

                    buffer = new byte[br.BaseStream.Length - 16];
                    br.Read(buffer, 0, (int)(br.BaseStream.Length - 16));

                    // Decompress via GZip
                    return util.gz_decompress(buffer);
                }
                else {
                    // Invalid Replay Format
                    MessageBox.Show(
                        "Invalid Replay File. Header Mismatch.",
                        "Failed to load replay - Invalid",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return null;
                }
            }

            return null;
        }

        private void Button_import_replay_Click(object sender, EventArgs e) {
            if (openFileDialog_dem.ShowDialog() == DialogResult.OK) {
                // TODO: Make replay comparison box. For now, we'll just ask if
                // the user is sure they want to carry on with replacing it.
                // Are you sure you want to do this?

                // Perform replay validity check before doing something stupid
                byte[] buffer = open_dem_file(openFileDialog_dem.FileName);

                // If "open_dem_file" failed, don't do anything
                if (buffer == null)
                    return;

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to overwrite this replay?",
                    "Confirm Replacement",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                );

                if (confirm != DialogResult.Yes)
                    return;

                DATA.import_dem(buffer, Convert.ToUInt32(label_id.Text));
                DATA.fill_tree(treeView_replays);

                // Select the one that got added
                // TODO: Oh my god this needs to be cleaned up
                treeView_replays.SelectedNode = treeView_replays.Nodes.Find(
                    (Convert.ToInt32(label_id.Text)).ToString("00"), true
                )[0];

                MessageBox.Show("Replay Replaced Successfully.");
            }
        }

        private void ExtractAllReplaysToolStripMenuItem_Click(object sender, EventArgs e) {
            if (DATA.replay_count == 0) {
                MessageBox.Show(
                    "This save has no replays to export..."
                );
                return;
            }

            if (folderBrowserDialog_exportAll.ShowDialog() == DialogResult.OK) {
                for (uint i = 0; i < DATA.replay_count; i++) {
                    // Generate both file segments
                    byte[] header = ppt.generate_dem_header(
                        DATA.game_val, DATA.platform_val
                    );

                    byte[] data = util.gz_compress(
                        DATA.generate_dem(i)
                    );

                    // Create final file based on both segments
                    byte[] buffer = new byte[header.Length + data.Length];
                    header.CopyTo(buffer, 0);
                    data.CopyTo(buffer, header.Length);

                    File.WriteAllBytes(
                        folderBrowserDialog_exportAll.SelectedPath + "\\" +
                        DATA.time_format(i) + ".dem",
                        buffer
                    );
                }
            }
        }

        private void InsertReplayToolStripMenuItem_Click(object sender, EventArgs e) {
            // Whine if the person's save already has 50 (or more... somehow?)
            if (DATA.replay_count >= 50) {
                MessageBox.Show(
                    "This save hit the limit of 50 replays! " +
                    "You can't insert anymore until you delete some replays."
                );
                return;
            }

            // Okay... let's just assume we can import then (probably a really bad idea)
            if (openFileDialog_dem.ShowDialog() == DialogResult.OK) {
                byte[] buffer = open_dem_file(openFileDialog_dem.FileName);

                // If "open_dem_file" failed, don't do anything
                if (buffer == null)
                    return;

                //byte[] buffer = util.gz_decompress(fbytes);
                DATA.import_dem(buffer, DATA.replay_count);

                DATA.replay_count++;

                DATA.fill_tree(treeView_replays);

                // Select the one that got added
                treeView_replays.SelectedNode = treeView_replays.Nodes.Find(
                    (DATA.replay_count - 1).ToString("00"), true
                )[0];
            }
        }

        private void ClearAllReplaysToolStripMenuItem_Click(object sender, EventArgs e) {
            // Are you sure you want to do this?
            DialogResult confirm = MessageBox.Show(
                "You are about to delete all replays in this save. Are you sure?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (confirm != DialogResult.Yes)
                return;

            Array.Clear(
                DATA.bytes,
                (int) DATA.PREP_LOC,
                (int) DATA.PREP_LEN * 50
            );

            Array.Clear(
                DATA.bytes,
                (int) DATA.DATA_LOC,
                (int) DATA.DATA_LEN * 50
            );

            DATA.replay_count = 0;

            DATA.fill_tree(treeView_replays);

            selected_id = -1;

            tab_replay_control.Visible = false;
        }

        private void ExportSaveToolStripMenuItem_Click(object sender, EventArgs e) {
            // For now, let's not make a backup.
            // TODO: Make a backup mechanism.

            if (saveFileDialog_data.ShowDialog() == DialogResult.OK) {
                File.WriteAllBytes(saveFileDialog_data.FileName, DATA.bytes);
            }
        }

        private void Button_delete_replay_Click(object sender, EventArgs e) {
            // For now, let's just assume that the user doesn't need to confirm
            // the total annihilation of their replay. So be it.

            // Do you really want to do this?
            DialogResult confirm = MessageBox.Show(
                "You are about to delete this replay. Are you sure?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (confirm != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(label_id.Text);

            // Remove it from the save and update the GUI.
            DATA.erase((uint) id);
            DATA.fill_tree(treeView_replays);

            // Select the one that got added
            if (id >= DATA.replay_count)
                id = (int) DATA.replay_count - 1;

            if (id != -1) {
                treeView_replays.SelectedNode = treeView_replays.Nodes.Find(
                    (id).ToString("00"), true
                )[0];
            }

            selected_id = (int) id;

            tab_replay_control.Visible = (selected_id != -1);
        }
    }
}
