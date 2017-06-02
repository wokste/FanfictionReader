using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanfictionReader {
    partial class NewStoryForm : Form {
        private Story story;
        private StoryController sc;

        public NewStoryForm(StoryController sc, Story story) {
            InitializeComponent();

            this.story = story;
            this.sc = sc;
        }

        private void okButton_Click(object sender, EventArgs e) {
            if (story != null) {
                sc.SaveStory(story);
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e) {
            var url = urlTextBox.Text;
            var sp = new StoryParser();

            try {
                story = sp.FromUrl(url);
            } catch (Exception ex) {
                story = null;
                errorLabel.Text = ex.Message;
            }
        }
    }
}
