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

        public void SaveStory(Story story) {
            if (story.PK == 0) {
                InsertStory(story);
            } else {
                UpdateStory(story);
            }
                
        }

        private void InsertStory(Story story) {
            story.AddDate = DateTime.Now;
            story.LastReadDate = DateTime.Now;

            using (var query = new SQLiteCommand("INSERT INTO Story (Id, Title, ChapterID, Host, AddDate, LastReadDate) VALUES (@Id, @Title, @ChapterID, @Host, @AddDate, @LastReadDate)", conn)) {
                query.Parameters.AddWithValue("@Id", story.Id);
                query.Parameters.AddWithValue("@Title", story.Title);
                query.Parameters.AddWithValue("@ChapterID", story.ChapterID);
                query.Parameters.AddWithValue("@Host", story.Host);
                query.Parameters.AddWithValue("@AddDate", story.AddDate);
                query.Parameters.AddWithValue("@LastReadDate", story.LastReadDate);

                query.ExecuteNonQuery();
            }

            using (var query = new SQLiteCommand("select last_insert_rowid()", conn)) {
                story.PK = (long)query.ExecuteScalar();
            }
        }

        private void UpdateStory(Story story) {
            story.LastReadDate = DateTime.Now;
            using (var query = new SQLiteCommand("UPDATE Story SET LastReadDate = @LastReadDate, ChapterID = @ChapterID WHERE PK = @PK", conn)) {
                query.Parameters.AddWithValue("@LastReadDate", story.LastReadDate);
                query.Parameters.AddWithValue("@ChapterID", story.ChapterID);
                query.Parameters.AddWithValue("@PK", story.PK);

                query.ExecuteNonQuery();
            }
        }

    }
}
