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
        public enum game_t {
            NONE = -1,
            PPT1 = 0,
            PPT2 = 1
        }

        public enum platform_t {
            NONE = -1,
            PC = 0
        }

        /*
         * generate_dem_header
         * 
         * Generate a header for binary files, or for comparison purposes. This
         * serves as the first 16 bytes in a new DEM file. Older DEM files will
         * start with 0x1F 0x8B, being GZip's magic number.
         */

        public byte[] generate_dem_header(game_t game, platform_t platform, uint version = 0) {
            byte[] data = new byte[16];
            byte[] bytes_game, bytes_platform, bytes_version;
            int i, j;
            const string header_start = "DEM1";

            // Write the string
            for (i = 0; i < 4; i++)
                data[i] = (byte) header_start[i];

            // Convert the three integers to bytes and write them
            bytes_game     = BitConverter.GetBytes(( int) game    );
            bytes_platform = BitConverter.GetBytes(( int) platform);
            bytes_version  = BitConverter.GetBytes((uint) version );

            // This could probably be written better...
            for (j = 0; j < 4; i++, j++) data[i] = bytes_game    [j];
            for (j = 0; j < 4; i++, j++) data[i] = bytes_platform[j];
            for (j = 0; j < 4; i++, j++) data[i] = bytes_version [j];

            return data;
        }

        public string generate_game_name(game_t game, platform_t platform) {
            string name = "";

            switch (game) {
                case game_t.PPT1:
                    name += "Puyo Puyo Tetris";
                    break;
                case game_t.PPT2:
                    name += "Puyo Puyo Tetris 2";
                    break;
                default:
                    name += "Unknown Game";
                    break;
            }

            switch (platform) {
                case platform_t.PC:
                    name += " (PC)";
                    break;
                default:
                    name += " (?)";
                    break;
            }

            return name;
        }

        public abstract class data_bin {
            // A few things we need to make clear first...
            public uint PREP_LOC;
            public uint DATA_LOC;
            public uint PREP_LEN;
            public uint DATA_LEN;

            // Lookup table for where PREP/DATA addresses are
            public static uint[] PREP_ADDR;
            public static uint[] DATA_ADDR;

            public string path;
            public uint replay_count;
            public byte[] bytes;

            // Defined game and platform variables for checking
            public game_t game_val;
            public platform_t platform_val;

            // IO
            public abstract void open(string fp);
            public abstract void open();

            // Time
            public abstract uint get_year(uint id);
            public abstract uint get_month(uint id);
            public abstract uint get_day(uint id);
            public abstract uint get_hour(uint id);
            public abstract uint get_minute(uint id);
            public abstract string time_format(uint id);

            // Player Data
            public abstract uint get_player_count(uint id);
            public abstract string get_player_name(uint id, uint player);

            // Duration Data
            public abstract ushort get_length_as_seconds(uint id);
            public abstract string get_length_as_string(uint id);

            // Other things

            /*
             * data_bin.fill_tree
             * 
             * Fills in a TreeView with the replays available in this object.
             * Clicking those nodes will let you view data about each replay,
             * and even export them as a file, or replace them with another
             * replay, if possible.
             */

            public void fill_tree(TreeView tv) {
                // Clear out TreeView...
                tv.Nodes.Clear();

                // Recreate and fill in with actual data.
                TreeNode root = tv.Nodes.Add("Save Data");
                root.Name = "treeNode_saveData";

                for (uint i = 0; i < replay_count; i++)
                {
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

                // Print out bytes... 16 values per row, starting where the
                // section should begin at...
                hexdump = "";
                shift = (16 - (offset % 16)) % 16;
                rows = (uint)Math.Ceiling((double)(len + shift) / 16);
                addr = offset - shift;
                target = addr + len + shift;

                hexdump += "           00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F" + Environment.NewLine;
                hexdump += "          _________________________________________________" + Environment.NewLine;

                for (int r = 0; r < rows; r++)
                {
                    hexdump += string.Format("{0:X8} | ", addr);

                    // Hex Values
                    for (int c = 0; c < 16; c++)
                    {
                        if (addr + c < offset || addr + c >= offset + len)
                            hexdump += "   ";
                        else
                            hexdump += string.Format("{0:X2} ", bytes[addr + c]);
                    }

                    // Plain Text
                    hexdump += "|";

                    for (int c = 0; c < 16; c++)
                    {
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
                // Copy blocks of data over
                if (id != 49)
                {
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

                // Zero out the 50th PREP and DATA spots.
                Array.Clear(bytes, (int)PREP_ADDR[49], (int)PREP_LEN);
                Array.Clear(bytes, (int)DATA_ADDR[49], (int)DATA_LEN);

                replay_count--;
            }
        }

        public class data_bin_ppt1_pc : data_bin {

            public data_bin_ppt1_pc() {
                // This class is strictly for PPT1 (PC Version)
                game_val     = PPT.game_t.PPT1;
                platform_val = PPT.platform_t.PC;

                // Addresses specific to Puyo Puyo Tetris 1 PC version "data.bin"
                PREP_LOC = 0x2970;
                DATA_LOC = 0x21F50;
                PREP_LEN = 0x168;
                DATA_LEN = 0x16940;

                path = "";
                replay_count = 0;

                // Configure the address stuff
                PREP_ADDR = new uint[50];
                DATA_ADDR = new uint[50];

                // Setup lookup "table" with addresses.
                for (uint i = 0; i < 50; i++) {
                    PREP_ADDR[i] = PREP_LOC + (PREP_LEN * i);
                    DATA_ADDR[i] = DATA_LOC + (DATA_LEN * i);
                }
            }

            public override void open(string fp) {
                path = fp;
                open();
            }

            public override void open() {
                // Read in all bytes into "bytes"
                bytes = File.ReadAllBytes(path);

                // Figure out the number of replays in the save
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

            // Time
            public override uint get_year(uint id) {
                return (uint)2000 + BitConverter.ToUInt16(
                    bytes,
                    (int)PREP_ADDR[id] + 0x28
                );
            }

            public override uint get_month(uint id) {
                return bytes[PREP_ADDR[id] + 0x2A];
            }

            public override uint get_day(uint id) {
                return bytes[PREP_ADDR[id] + 0x2B];
            }

            public override uint get_hour(uint id) {
                return bytes[PREP_ADDR[id] + 0x2C];
            }

            public override uint get_minute(uint id) {
                return bytes[PREP_ADDR[id] + 0x2D];
            }

            // Time Format String
            public override string time_format(uint id) {
                return string.Format(
                    "{0:D4}{1:D2}{2:D2}_{3:D2}{4:D2}",
                    get_year(id),
                    get_month(id),
                    get_day(id),
                    get_hour(id),
                    get_minute(id)
                );
            }

            // Player Data
            public override uint get_player_count(uint id) {
                return bytes[PREP_ADDR[id] + 0x07];
            }

            public override string get_player_name(uint id, uint player) {
                uint offset = PREP_ADDR[id] + 0x30 + (0x3C * player);
                string ret = "";

                // Get string length first
                int len;
                for (len = 0; ; len += 2) {
                    if (bytes[offset + len] == 0x00 && bytes[offset + len + 1] == 0x00)
                        break;
                }

                // Extract UTF-16LE from byte array
                return Encoding.Unicode.GetString(bytes, (int)offset, len);
            }

            // Duration Data
            public override ushort get_length_as_seconds(uint id) {
                return (ushort)BitConverter.ToUInt16(
                    bytes,
                    (int)PREP_ADDR[id] + 0x18
                );
            }

            public override string get_length_as_string(uint id) {
                ushort duration = get_length_as_seconds(id);
                int min, sec;

                min = duration / 60;
                sec = duration % 60;

                return min.ToString() + ":" + sec.ToString("00");
            }
        }

        public class data_bin_ppt2_pc : data_bin {

            public data_bin_ppt2_pc() {
                // This class is strictly for PPT2 (PC Version)
                game_val     = PPT.game_t.PPT2;
                platform_val = PPT.platform_t.PC;

                // Addresses specific to Puyo Puyo Tetris 2 PC version "data.bin"
                PREP_LOC = 0x6E1BC;
                DATA_LOC = 0x9C08C;
                PREP_LEN = 0x1B4;
                DATA_LEN = 0x23480;

                path = "";
                replay_count = 0;

                // Configure the address stuff
                PREP_ADDR = new uint[50];
                DATA_ADDR = new uint[50];

                // Setup lookup "table" with addresses.
                for (uint i = 0; i < 50; i++) {
                    PREP_ADDR[i] = PREP_LOC + (PREP_LEN * i);
                    DATA_ADDR[i] = DATA_LOC + (DATA_LEN * i);
                }
            }

            public override void open(string fp) {
                path = fp;
                open();
            }

            public override void open() {
                // Read in all bytes into "bytes"
                bytes = File.ReadAllBytes(path);

                // Figure out the number of replays in the save
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

            // Time
            public override uint get_year(uint id) {
                return (uint)2000 + BitConverter.ToUInt16(
                    bytes,
                    (int)PREP_ADDR[id] + 0x20
                );
            }

            public override uint get_month(uint id) {
                return bytes[PREP_ADDR[id] + 0x22];
            }

            public override uint get_day(uint id) {
                return bytes[PREP_ADDR[id] + 0x23];
            }

            public override uint get_hour(uint id) {
                return bytes[PREP_ADDR[id] + 0x24];
            }

            public override uint get_minute(uint id) {
                return bytes[PREP_ADDR[id] + 0x25];
            }

            // Time Format String
            public override string time_format(uint id) {
                return string.Format(
                    "{0:D4}{1:D2}{2:D2}_{3:D2}{4:D2}",
                    get_year(id),
                    get_month(id),
                    get_day(id),
                    get_hour(id),
                    get_minute(id)
                );
            }

            // Player Data
            public override uint get_player_count(uint id) {
                return bytes[PREP_ADDR[id] + 0x07];
            }

            public override string get_player_name(uint id, uint player) {
                uint offset = PREP_ADDR[id] + 0x28 + (0x40 * player);
                string ret = "";

                // Get string length first
                int len;
                for (len = 0; ; len += 2) {
                    if (bytes[offset + len] == 0x00 && bytes[offset + len + 1] == 0x00)
                        break;
                }

                // Extract UTF-16LE from byte array
                return Encoding.Unicode.GetString(bytes, (int)offset, len);
            }

            // Duration Data
            public override ushort get_length_as_seconds(uint id) {
                return (ushort)BitConverter.ToUInt16(
                    bytes,
                    (int)PREP_ADDR[id] + 0x16
                );
            }

            public override string get_length_as_string(uint id) {
                ushort duration = get_length_as_seconds(id);
                int min, sec;

                min = duration / 60;
                sec = duration % 60;

                return min.ToString() + ":" + sec.ToString("00");
            }
        }
    }
}
