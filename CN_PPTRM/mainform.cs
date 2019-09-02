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
        //A few things we need to make clear first...
        const uint PREP_LOC = 0x2970;
        const uint DATA_LOC = 0x21F50;
        const uint PREP_LEN = 0x168;
        const uint DATA_LEN = 0x16940;

        //Lookup "table" for addresses where the data will be stored at.
        static uint[] PREP_ADDR;
        static uint[] DATA_ADDR;

        //Data.bin class
        public class data_bin {
            public string path;
            public uint   replay_count;
            public byte[] bytes;
            public Form form;

            public data_bin() {
                path = "";
                replay_count = 0;
            }

            public void open(string fp) {
                path = fp;
                open();
            }

            public void open() {
                //Read in all bytes into "bytes"
                bytes = File.ReadAllBytes(path);

                //Figure out the number of replays in the save
                int v;

                for (replay_count = 0; replay_count < 50; replay_count++) {
                    v = BitConverter.ToInt32(
                        bytes,
                        (int)PREP_ADDR[replay_count]
                    );

                    if (v != 0x50455250)
                        break;
                }
            }

            /*
             * Replay Get Information Functions
             * 
             * Simple utility functions for getting specific data about a
             * replay. This is mainly for code readability.
             */

            //Time
            public uint get_year(uint id) {
                return (uint) 2000 + BitConverter.ToUInt16(
                    bytes,
                    (int)PREP_ADDR[id] + 0x28
                );
            }

            public uint get_month(uint id) {
                return bytes[PREP_ADDR[id] + 0x2A];
            }

            public uint get_day(uint id) {
                return bytes[PREP_ADDR[id] + 0x2B];
            }

            public uint get_hour(uint id) {
                return bytes[PREP_ADDR[id] + 0x2C];
            }

            public uint get_minute(uint id) {
                return bytes[PREP_ADDR[id] + 0x2D];
            }

            //Time Format String
            public string time_format(uint id) {
                return string.Format(
                    "{0:D4}{1:D2}{2:D2}_{3:D2}{4:D2}",
                    get_year  (id),
                    get_month (id),
                    get_day   (id),
                    get_hour  (id),
                    get_minute(id)
                );
            }

            //Player Data
            public uint get_player_count(uint id) {
                return bytes[PREP_ADDR[id] + 0x07];
            }

            public string get_player_name(uint id, uint player) {
                uint offset = PREP_ADDR[id] + 0x30 + (0x3C * player);
                string ret = "";

                for (uint i = 0;; i += 2) {
                    if (bytes[offset + i] == 0x00)
                        break;

                    ret += (char) bytes[offset + i];
                }

                return ret;
            }

            //Duration Data
            public ushort get_length_as_seconds(uint id) {
                return (ushort)BitConverter.ToUInt16(
                    bytes,
                    (int)PREP_ADDR[id] + 0x18
                );
            }

            public string get_length_as_string(uint id) {
                ushort duration = get_length_as_seconds(id);
                int min, sec;

                min = duration / 60;
                sec = duration % 60;

                return min.ToString() + ":" + sec.ToString("00");
            }

            /*
             * data_bin.fill_tree
             * 
             * Fills in a TreeView with the replays available in this object.
             * Clicking those nodes will let you view data about each replay,
             * and even export them as a file, or replace them with another
             * replay, if possible.
             */

            public void fill_tree(TreeView tv) {
                //Clear out TreeView...
                tv.Nodes.Clear();

                //Recreate and fill in with actual data.
                TreeNode root = tv.Nodes.Add("Save Data");
                root.Name = "treeNode_saveData";

                for (uint i = 0; i < replay_count; i++) {
                    TreeNode n = new TreeNode(i.ToString("00") + ": " + time_format(i) + ".dem");
                    n.Name = i.ToString("00");
                    root.Nodes.Add(n);
                }
            }

            /*
             * data_bin.generate_dem
             * 
             * Generates an array of bytes that can be extracted as a valid DEM
             * file. This is essentially concatenating the PREP and DATA
             * sections together.
             */

            public byte[] generate_dem(uint id) {
                byte[] dem_buffer = new byte[PREP_LEN + DATA_LEN];
                Buffer.BlockCopy(bytes, (int) PREP_ADDR[id], dem_buffer, 0x00          , (int) PREP_LEN);
                Buffer.BlockCopy(bytes, (int) DATA_ADDR[id], dem_buffer, (int) PREP_LEN, (int) DATA_LEN);

                return dem_buffer;
            }

            /*
             * data_bin.import_dem
             * 
             * Imports a DEM file's bytes from "dem" and injects it into the
             * save, replacing a replay at position "id". This is essentially
             * the inverse of "generate_dem", as we are importing data rather
             * than exporting it.
             */

            public void import_dem(byte[] dem, uint id) {
                Buffer.BlockCopy(dem, 0x00         , bytes, (int) PREP_ADDR[id], (int) PREP_LEN);
                Buffer.BlockCopy(dem, (int)PREP_LEN, bytes, (int) DATA_ADDR[id], (int) DATA_LEN);
            }

            /*
             * data_bin.generate_hexdump
             * 
             * Generates a string with a hexdump mimicing the POSIX command
             * "hexdump -Cv FILE". This can be used to aid in figuring out some
             * other values in the save data... Or, you know, just to dump more
             * information for the end user to see.
             */

            public string generate_hexdump(uint offset, uint len) {
                string hexdump;
                uint shift, rows, addr, target;

                //Print out bytes... 16 values per row, starting where the
                //section should begin at...
                hexdump = "";
                shift   = (16 - (offset % 16)) % 16;
                rows    = (uint) Math.Ceiling((double)(len + shift) / 16);
                addr    = offset - shift;
                target  = addr + len + shift;

                hexdump += "           00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F" + Environment.NewLine;
                hexdump += "          _________________________________________________" + Environment.NewLine;

                for (int r = 0; r < rows; r++) {
                    hexdump += string.Format("{0:X8} | ", addr);

                    //Hex Values
                    for (int c = 0; c < 16; c++) {
                        if (addr + c < offset || addr + c >= offset + len)
                            hexdump += "   ";
                        else
                            hexdump += string.Format("{0:X2} ", bytes[addr + c]);
                    }

                    //Plain Text
                    hexdump += "|";

                    for (int c = 0; c < 16; c++) {
                        if (addr + c < offset || addr + c >= offset + len)
                            hexdump += " ";
                        else
                        if (!Char.IsControl((char) bytes[addr + c]))
                            hexdump += (char) bytes[addr + c];
                        else
                            hexdump += ".";
                    }

                    hexdump += "|" + Environment.NewLine;
                    addr += 0x10;
                }

                return hexdump;
            }
        }

        //Okay, and some utility functions to make my life easier
        public byte[] gz_compress(byte[] bytes) {
            using (var ms = new MemoryStream())
            using (var gzs = new GZipStream(ms, CompressionLevel.Optimal)) {
                gzs.Write(bytes, 0, bytes.Length);
                gzs.Close();
                return ms.ToArray();
            }
        }

        public byte[] gz_decompress(byte[] bytes) {
            try {
                using (var ms = new MemoryStream(bytes))
                using (var res = new MemoryStream())
                using (var gzs = new GZipStream(ms, CompressionMode.Decompress)) {
                    gzs.CopyTo(res);
                    return res.ToArray();
                }
            }
            catch (System.IO.InvalidDataException) {
                //That was not a properly encoded GZip file. So just assume it's RAW.
                return bytes;
            }
        }

        //Oh and let's just make it global. Yeah?
        public data_bin DATA;

        private string wintitle = "CN_PPTRM - Puyo Puyo Tetris Replay Manager";

        public mainform() {
            InitializeComponent();
            DATA = new data_bin();
            DATA.form = this;

            //Configure the address stuff
            PREP_ADDR = new uint[50];
            DATA_ADDR = new uint[50];

            //Setup lookup "table" with addresses.
            for (uint i = 0; i < 50; i++) {
                PREP_ADDR[i] = PREP_LOC + (PREP_LEN * i);
                DATA_ADDR[i] = DATA_LOC + (DATA_LEN * i);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) {
            about win = new about();
            win.ShowDialog();
        }

        private void ImportSaveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog_data.ShowDialog() == DialogResult.OK) {
                DATA.path = openFileDialog_data.FileName;

                //Adjust the form's name
                this.Text = wintitle + " (" + DATA.path + ")";

                //Process the save data
                DATA.open();
                DATA.fill_tree(treeView_replays);

                tab_replay_control.Visible = true;
            }
        }

        private void TreeView_replays_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Parent != null) {
                if (e.Node.Parent.Name == "treeNode_saveData") {
                    //Welp, we got a node from "data.bin". Fill out the data accordingly.

                    //Get the replay number
                    string label = e.Node.Text.Substring(0, 2);
                    uint   id    = Convert.ToUInt32(label);

                    //Okay, now let's fill out some stuff.
                    label_id.Text = id.ToString();
                    label_recorded.Text = string.Format(
                        "{0:D4}/{1:D2}/{2:D2} @ {3:D2}:{4:D2}",
                        DATA.get_year  (id),
                        DATA.get_month (id),
                        DATA.get_day   (id),
                        DATA.get_hour  (id),
                        DATA.get_minute(id)
                    );

                    //Player information
                    label_player_count.Text = DATA.get_player_count(id).ToString();
                    label_player1.Text = DATA.get_player_name(id, 0);
                    label_player2.Text = DATA.get_player_name(id, 1);
                    label_player3.Text = DATA.get_player_name(id, 2);
                    label_player4.Text = DATA.get_player_name(id, 3);

                    //Length of the match
                    label_length.Text = DATA.get_length_as_string(id);

                    //Debug information
                    label_prep_location.Text = string.Format("0x{0:X8}", PREP_ADDR[id]);
                    label_data_location.Text = string.Format("0x{0:X8}", DATA_ADDR[id]);

                    //Generate and dump hex values
                    textBox_prepDump.Text = DATA.generate_hexdump(
                        PREP_ADDR[id],
                        PREP_LEN
                    );
                }
            }
        }

        private void Button_extract_replay_Click(object sender, EventArgs e) {
            if (saveFileDialog_dem.ShowDialog() == DialogResult.OK) {
                string path = saveFileDialog_dem.FileName;

                //Read the data and extract it accordingly.
                byte[] buffer = gz_compress(
                    DATA.generate_dem(Convert.ToUInt32(label_id.Text))
                );

                //File.WriteAllBytes(path, buffer);
                File.WriteAllBytes(path, buffer);

                MessageBox.Show("Replay Saved Successfully.");
            }
        }

        private void Button_import_replay_Click(object sender, EventArgs e) {
            if (openFileDialog_dem.ShowDialog() == DialogResult.OK) {

                byte[] buffer = gz_decompress(
                    File.ReadAllBytes(openFileDialog_dem.FileName)
                );
                DATA.import_dem(buffer, Convert.ToUInt32(label_id.Text));
                DATA.fill_tree(treeView_replays);

                //Select the one that got added
                //TODO: Oh my god this needs to be cleaned up
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
                    byte[] buffer = gz_compress(
                        DATA.generate_dem(i)
                    );

                    File.WriteAllBytes(
                        folderBrowserDialog_exportAll.SelectedPath + "\\" + DATA.time_format(i) + ".dem",
                        buffer
                    );
                }
            }
        }

        private void InsertReplayToolStripMenuItem_Click(object sender, EventArgs e) {
            //Whine if the person's save already has 50 (or more... somehow?)
            if (DATA.replay_count >= 50) {
                MessageBox.Show(
                    "This save hit the limit of 50 replays! " +
                    "You can't insert anymore until you delete some replays."
                );
                return;
            }

            //Okay... let's just assume we can import then (probably a really bad idea)
            if (openFileDialog_dem.ShowDialog() == DialogResult.OK) {

                byte[] buffer = gz_decompress(
                    File.ReadAllBytes(openFileDialog_dem.FileName)
                );
                DATA.import_dem(buffer, DATA.replay_count);

                DATA.replay_count++;

                DATA.fill_tree(treeView_replays);

                //Select the one that got added
                treeView_replays.SelectedNode = treeView_replays.Nodes.Find(
                    (DATA.replay_count - 1).ToString("00"), true
                )[0];
            }
        }

        private void ClearAllReplaysToolStripMenuItem_Click(object sender, EventArgs e) {
            //For now, let's just not give a confirmation.
            Array.Clear(DATA.bytes, (int) PREP_LOC, (int) PREP_LEN * 50);
            Array.Clear(DATA.bytes, (int) DATA_LOC, (int) DATA_LEN * 50);

            DATA.replay_count = 0;

            DATA.fill_tree(treeView_replays);
        }

        private void ExportSaveToolStripMenuItem_Click(object sender, EventArgs e) {
            //For now, let's not make a backup.
            //TODO: Make a backup mechanism.

            if (saveFileDialog_data.ShowDialog() == DialogResult.OK) {
                File.WriteAllBytes(saveFileDialog_data.FileName, DATA.bytes);
            }
        }

        private void Button_delete_replay_Click(object sender, EventArgs e) {
            //For now, let's just assume that the user doesn't need to confirm
            //the total annihilation of their replay. So be it.
            int id = Convert.ToInt32(label_id.Text);

            //Copy blocks of data over
            if (id != 49) {
                Buffer.BlockCopy(
                    DATA.bytes, (int)PREP_ADDR[id + 1],
                    DATA.bytes, (int)PREP_ADDR[id    ],
                    (int) PREP_LEN * (50 - (id + 1))
                );

                Buffer.BlockCopy(
                    DATA.bytes, (int)DATA_ADDR[id + 1],
                    DATA.bytes, (int)DATA_ADDR[id    ],
                    (int) DATA_LEN * (50 - (id + 1))
                );
            }

            //Zero out the 50th PREP and DATA spots.
            Array.Clear(DATA.bytes, (int) PREP_ADDR[49], (int) PREP_LEN);
            Array.Clear(DATA.bytes, (int) DATA_ADDR[49], (int) DATA_LEN);

            DATA.replay_count--;

            DATA.fill_tree(treeView_replays);

            //Select the one that got added
            if (id >= DATA.replay_count)
                id = (int) DATA.replay_count - 1;

            if (id != -1) {
                treeView_replays.SelectedNode = treeView_replays.Nodes.Find(
                    (id).ToString("00"), true
                )[0];
            }
        }
    }
}
