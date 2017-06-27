using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FanfictionReader {
    internal partial class NewStoryForm : Form {
        private readonly List<Story> _stories = new List<Story>();
        private readonly Reader _reader;

        internal NewStoryForm(Reader reader) {
            InitializeComponent();

            _reader = reader;
        }

        private void okButton_Click(object sender, EventArgs e) {
            foreach (var story in _stories) {
                _reader.SaveStoryAsync(story);
            }

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e) {
            var urls = urlTextBox.Text.Split('\r', '\n');
            var parser = new UrlParser();
            var sb = new StringBuilder();

            _stories.Clear();

            var line = 0;

            foreach (var url in urls) {
                if (url == "")
                    continue;

                line++;
                try {
                    var story = parser.UrlToStory(url);
                    _stories.Add(story);
                } catch (Exception) {
                    sb.AppendFormat("Line {0}: {1}\n", line, url);
                }
            }

            errorLabel.Text = sb.ToString();
        }
    }
}
