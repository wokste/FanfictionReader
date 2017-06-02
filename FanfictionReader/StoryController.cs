using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text.RegularExpressions;

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
            
            story.PK = reader.GetInt32(reader.GetOrdinal("PK"));
            story.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            story.Title = reader.GetString(reader.GetOrdinal("Title"));
            story.ChapterID = reader.GetInt32(reader.GetOrdinal("ChapterID"));
            story.Host = reader.GetString(reader.GetOrdinal("Host"));
            story.AddDate = reader.GetDateTime(reader.GetOrdinal("AddDate"));
            story.LastReadDate = reader.GetDateTime(reader.GetOrdinal("LastReadDate"));

            return story;
        }

        public void AddStory(Story story) {
            story.AddDate = DateTime.Now;
            story.LastReadDate = DateTime.Now;

            using (var query = new SQLiteCommand("INSERT INTO Story (Id, Title, ChapterID, Host, AddDate, LastReadDate) VALUES (?,?,?,?,?,?)", conn)) {
                query.Parameters.Add(story.Id);
                query.Parameters.Add(story.Title);
                query.Parameters.Add(story.ChapterID);
                query.Parameters.Add(story.Host);
                query.Parameters.Add(story.AddDate);
                query.Parameters.Add(story.LastReadDate);

                query.ExecuteNonQuery();
            }
        }

        public void UpdateStory(Story story) {
            story.LastReadDate = DateTime.Now;
            using (var query = new SQLiteCommand("UPDATE Story SET LastReadDate = ?, ChapterID = ? WHERE PK = ?", conn)) {
                query.Parameters.Add(story.LastReadDate);
                query.Parameters.Add(story.ChapterID);
                query.Parameters.Add(story.PK);

                query.ExecuteNonQuery();
            }
        }
    }
}
