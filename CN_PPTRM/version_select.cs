using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ppt_replay_gui
{
    public partial class version_select : Form {

        public enum game_t {
            NONE = -1,
            PPT1 =  0,
            PPT2 =  1
        }

        public enum platform_t {
            NONE = -1,
            PC   =  0
        }

        public game_t game_val;
        public platform_t platform_val;
        public DialogResult bttn_val;

        public version_select() {
            InitializeComponent();

            game_val = game_t.NONE;
            platform_val = platform_t.PC;
            bttn_val = DialogResult.None;
        }

        private void button_cancel_Click(object sender, EventArgs e) {
            bttn_val = DialogResult.Cancel;
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e) {
            bttn_val     = DialogResult.OK;
            game_val     = (game_t    ) comboBox_game    .SelectedIndex;
            platform_val = (platform_t) comboBox_platform.SelectedIndex;

            this.Close();
        }
    }
}
