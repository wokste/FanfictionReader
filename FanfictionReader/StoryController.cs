using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FanfictionReader {
    class StoryController {
        private readonly SQLiteConnection _conn;

        public StoryController(SQLiteConnection conn) {
            _conn = conn;
        }

        public IList<Story> GetStoryList() {
            var list = new List<Story>();
            using (var query = new SQLiteCommand("SELECT * FROM Story", _conn)) {
                using (var reader = query.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(GetStory(reader));
                    }
                }
            }

            return list;
        }

        public Story GetStory(long pk) {
            using (var query = new SQLiteCommand("SELECT * FROM Story WHERE PK = @SqlPk", _conn)) {
                query.Parameters.AddWithValue("@SqlPk", pk);
                using (var reader = query.ExecuteReader()) {
                    if (reader.Read()) {
                        return GetStory(reader);
                    }
                }
            }

            return null;
        }

        private Story GetStory(SQLiteDataReader reader) {
            var story = new Story
            {
                SqlPk = reader.GetInt32(reader.GetOrdinal("PK")),
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                ChapterId = reader.GetInt32(reader.GetOrdinal("ChapterID")),
                Host = reader.GetString(reader.GetOrdinal("Host")),
                AddDate = reader.GetDateTime(reader.GetOrdinal("AddDate")),
                LastReadDate = reader.GetDateTime(reader.GetOrdinal("LastReadDate"))
            };

            return story;
        }

        public void SaveStory(Story story) {
            if (story.SqlPk == 0) {
                InsertStory(story);
            } else {
                UpdateStory(story);
            }
                
        }

        private void InsertStory(Story story) {
            story.AddDate = DateTime.Now;
            story.LastReadDate = DateTime.Now;

            using (var query = new SQLiteCommand("INSERT INTO Story (Id, Title, ChapterId, Host, AddDate, LastReadDate) VALUES (@Id, @Title, @ChapterId, @Host, @AddDate, @LastReadDate)", _conn)) {
                query.Parameters.AddWithValue("@Id", story.Id);
                query.Parameters.AddWithValue("@Title", story.Title);
                query.Parameters.AddWithValue("@ChapterId", story.ChapterId);
                query.Parameters.AddWithValue("@Host", story.Host);
                query.Parameters.AddWithValue("@AddDate", story.AddDate);
                query.Parameters.AddWithValue("@LastReadDate", story.LastReadDate);

                query.ExecuteNonQuery();
            }

            using (var query = new SQLiteCommand("select last_insert_rowid()", _conn)) {
                story.SqlPk = (long)query.ExecuteScalar();
            }
        }

        private void UpdateStory(Story story) {
            story.LastReadDate = DateTime.Now;
            using (var query = new SQLiteCommand("UPDATE Story SET LastReadDate = @LastReadDate, ChapterId = @ChapterId WHERE SqlPk = @SqlPk", _conn)) {
                query.Parameters.AddWithValue("@LastReadDate", story.LastReadDate);
                query.Parameters.AddWithValue("@ChapterId", story.ChapterId);
                query.Parameters.AddWithValue("@SqlPk", story.SqlPk);

                query.ExecuteNonQuery();
            }
        }

    }
}
