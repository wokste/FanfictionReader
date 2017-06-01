using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FanfictionReader {
    class StoryController {
        SQLiteConnection conn;

        public StoryController(SQLiteConnection conn) {
            this.conn = conn;
        }

        public IList<Story> GetStoryList() {
            var list = new List<Story>();
            using (var query = new SQLiteCommand("SELECT * FROM Story", conn)) {
                using (var reader = query.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(GetStory(reader));
                    }
                }
            }

            return list;
        }

        public Story GetStory(SQLiteDataReader reader) {
            var story = new Story();

            story.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            story.Title = reader.GetString(reader.GetOrdinal("Title"));
            story.ChapterID = reader.GetInt32(reader.GetOrdinal("ChapterID"));

            return story;
        }

        public void AddStory(Story story) {
            using (var query = new SQLiteCommand("INSERT INTO Story (Id, Title, ChapterID) VALUES (?,?,?)", conn)) {
                query.Parameters.Add(story.Id);
                query.Parameters.Add(story.Title);
                query.Parameters.Add(story.ChapterID);

                query.ExecuteNonQuery();
            }
        }

        public void UpdateStory(Story story) {
            using (var query = new SQLiteCommand("UPDATE Story SET Title = ?, ChapterID = ? WHERE Id = ?", conn)) {
                query.Parameters.Add(story.Title);
                query.Parameters.Add(story.ChapterID);
                query.Parameters.Add(story.Id);

                query.ExecuteNonQuery();
            }
        }
    }
}
