/*
 * Puyo Puyo Tetris Classes
 * 
 * Description:
 *     Classes that are used specifically for Puyo Puyo Tetris files.
 * 
 * Author:
 *     iDestyKK
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ppt_replay_gui {
    public class PPT {
        public class data_bin {
            //A few things we need to make clear first...
            public const uint PREP_LOC = 0x2970;
            public const uint DATA_LOC = 0x21F50;
            public const uint PREP_LEN = 0x168;
            public const uint DATA_LEN = 0x16940;

            //Lookup table for where PREP/DATA addresses are
            public static uint[] PREP_ADDR;
            public static uint[] DATA_ADDR;

            public string path;
            public uint replay_count;
            public byte[] bytes;

            public data_bin() {
                path = "";
                replay_count = 0;

                //Configure the address stuff
                PREP_ADDR = new uint[50];
                DATA_ADDR = new uint[50];

                //Setup lookup "table" with addresses.
                for (uint i = 0; i < 50; i++) {
                    PREP_ADDR[i] = PREP_LOC + (PREP_LEN * i);
                    DATA_ADDR[i] = DATA_LOC + (DATA_LEN * i);
                }
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
                return (uint)2000 + BitConverter.ToUInt16(
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
                    get_year(id),
                    get_month(id),
                    get_day(id),
                    get_hour(id),
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

                //Get string length first
                int len;
                for (len = 0; ; len += 2) {
                    if (bytes[offset + len] == 0x00 && bytes[offset + len + 1] == 0x00)
                        break;
                }

                //Extract UTF-16LE from byte array
                return Encoding.Unicode.GetString(bytes, (int)offset, len);
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
                Buffer.BlockCopy(bytes, (int)PREP_ADDR[id], dem_buffer, 0x00, (int)PREP_LEN);
                Buffer.BlockCopy(bytes, (int)DATA_ADDR[id], dem_buffer, (int)PREP_LEN, (int)DATA_LEN);

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
                Buffer.BlockCopy(dem, 0x00, bytes, (int)PREP_ADDR[id], (int)PREP_LEN);
                Buffer.BlockCopy(dem, (int)PREP_LEN, bytes, (int)DATA_ADDR[id], (int)DATA_LEN);
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
                shift = (16 - (offset % 16)) % 16;
                rows = (uint)Math.Ceiling((double)(len + shift) / 16);
                addr = offset - shift;
                target = addr + len + shift;

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
                        if (!Char.IsControl((char)bytes[addr + c]))
                            hexdump += (char)bytes[addr + c];
                        else
                            hexdump += ".";
                    }

                    hexdump += "|" + Environment.NewLine;
                    addr += 0x10;
                }

                return hexdump;
            }

            //Linked-List-like Functionality

            /*
             * data_bin.erase
             * 
             * Erases a replay in the save. This will also move all sequential
             * replays up by 1.
             */

            public void erase(uint id) {
                //Copy blocks of data over
                if (id != 49) {
                    Buffer.BlockCopy(
                        bytes, (int)PREP_ADDR[id + 1],
                        bytes, (int)PREP_ADDR[id],
                        (int)PREP_LEN * (50 - ((int)id + 1))
                    );

                    Buffer.BlockCopy(
                        bytes, (int)DATA_ADDR[id + 1],
                        bytes, (int)DATA_ADDR[id],
                        (int)DATA_LEN * (50 - ((int)id + 1))
                    );
                }

                //Zero out the 50th PREP and DATA spots.
                Array.Clear(bytes, (int)PREP_ADDR[49], (int)PREP_LEN);
                Array.Clear(bytes, (int)DATA_ADDR[49], (int)DATA_LEN);

                replay_count--;
            }
        }
    }
}
