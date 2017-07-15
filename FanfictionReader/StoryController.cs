using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace FanfictionReader {
    public class StoryController {
        private readonly SQLiteConnection _conn;

        public StoryController(SQLiteConnection conn) {
            _conn = conn;
        }

        public IList<Story> GetStoryList() {
            var list = new List<Story>();
            lock (_conn) {
                using (var query = new SQLiteCommand("SELECT * FROM Story", _conn)) {
                    using (var reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(GetStory(reader));
                        }
                    }
                }
            }

            return list;
        }

        public Story GetStory(long pk) {
            lock (_conn) {
                using (var query = new SQLiteCommand("SELECT * FROM Story WHERE PK = @Pk", _conn)) {
                    query.Parameters.AddWithValue("@Pk", pk);
                    using (var reader = query.ExecuteReader()) {
                        if (reader.Read()) {
                            return GetStory(reader);
                        }
                    }
                }
            }

            return null;
        }

        private Story GetStory(IDataRecord reader) {
            var meta = new StoryMeta {
                Title = reader.GetString(reader.GetOrdinal("Title")),
                AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                ChapterCount = reader.GetInt32(reader.GetOrdinal("ChapterCount")),
                IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete")),
                MinimumAge = reader.GetInt32(reader.GetOrdinal("MinimumAge")),
                Words = reader.GetInt32(reader.GetOrdinal("Words")),
                PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate")),
                UpdateDate = reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                MetaCheckDate = reader.GetDateTime(reader.GetOrdinal("MetaCheckDate"))
            };

            var story = new Story {
                Pk = reader.GetInt32(reader.GetOrdinal("PK")),
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                LastReadChapterId = reader.GetInt32(reader.GetOrdinal("LastReadChapterId")),
                Host = reader.GetString(reader.GetOrdinal("Host")),
                AddDate = reader.GetDateTime(reader.GetOrdinal("AddDate")),
                LastReadDate = reader.GetDateTime(reader.GetOrdinal("LastReadDate")),
                MetaData = meta
            };

            return story;
        }

        public void InsertStory(Story story) {
            story.AddDate = DateTime.Now;

            lock (_conn) {
                using (var query = new SQLiteCommand(@"INSERT INTO Story
                    (Id, Host, AddDate)
                    VALUES (@Id, @Host, @AddDate)"
                    , _conn)) {
                    query.Parameters.AddWithValue("@Id", story.Id);
                    query.Parameters.AddWithValue("@Host", story.Host);
                    query.Parameters.AddWithValue("@AddDate", story.AddDate);

                    query.ExecuteNonQuery();
                }
                using (var query = new SQLiteCommand("select last_insert_rowid()", _conn)) {
                    story.Pk = (long)query.ExecuteScalar();
                }
            }
        }

        public void UpdateStoryMeta(Story story) {

            var meta = story.MetaData;
            if (meta == null)
                return;

            lock (_conn) {
                using (var query = new SQLiteCommand(@"UPDATE Story
                    SET AuthorId = @AuthorId, ChapterCount = @ChapterCount, IsComplete = @IsComplete, MinimumAge = @MinimumAge, Words = @Words, PublishDate = @PublishDate, UpdateDate = @UpdateDate, MetaCheckDate = @MetaCheckDate
                    WHERE Pk = @Pk", _conn)) {
                    query.Parameters.AddWithValue("@Pk", story.Pk);
                    
                    query.Parameters.AddWithValue("@Title", meta.Title);
                    query.Parameters.AddWithValue("@AuthorId", meta.AuthorId);
                    query.Parameters.AddWithValue("@ChapterCount", meta.ChapterCount);
                    query.Parameters.AddWithValue("@IsComplete", meta.IsComplete);
                    query.Parameters.AddWithValue("@MinimumAge", meta.MinimumAge);
                    query.Parameters.AddWithValue("@Words", meta.Words);
                    query.Parameters.AddWithValue("@PublishDate", meta.PublishDate);
                    query.Parameters.AddWithValue("@UpdateDate", meta.UpdateDate);
                    query.Parameters.AddWithValue("@MetaCheckDate", meta.MetaCheckDate);

                    query.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStoryUserData(Story story) {
            var userData = story;
            lock (_conn) {
                using (var query = new SQLiteCommand(@"UPDATE Story
                    SET LastReadDate = @LastReadDate, LastReadChapterId = @LastReadChapterId
                    WHERE Pk = @Pk", _conn)) {
                    query.Parameters.AddWithValue("@Pk", story.Pk);

                    query.Parameters.AddWithValue("@LastReadDate", userData.LastReadDate);
                    query.Parameters.AddWithValue("@LastReadChapterId", userData.LastReadChapterId);

                    query.ExecuteNonQuery();
                }
            }
        }
    }
}
