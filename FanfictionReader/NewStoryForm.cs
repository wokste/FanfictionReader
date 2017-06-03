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
        private List<Story> stories = new List<Story>();
        private StoryController sc;

        public NewStoryForm(StoryController sc) {
            InitializeComponent();
            
            this.sc = sc;
        }

        private void okButton_Click(object sender, EventArgs e) {
            foreach (var story in stories) {
                sc.SaveStory(story);
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e) {
            var urls = urlTextBox.Text.Split(new char[]{'\r','\n'});
            var parser = new StoryParser();
            var sb = new StringBuilder();

            stories.Clear();

            int line = 0;

            foreach (var url in urls) {
                if (url == "")
                    continue;

                line++;
                try {
                    var story = parser.FromUrl(url);
                    stories.Add(story);
                } catch (Exception) {
                    sb.AppendFormat("Line {0}: {1}\n", line, url);
                }
            }

            errorLabel.Text = sb.ToString();
        }
    }
}
