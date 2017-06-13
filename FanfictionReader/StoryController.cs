using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace FanfictionReader {
    internal class StoryController {
        private readonly SQLiteConnection _conn;

        internal StoryController(SQLiteConnection conn) {
            _conn = conn;
        }

        internal IList<Story> GetStoryList() {
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

        internal Story GetStory(long pk) {
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
            var story = new Story();
            story.Pk = reader.GetInt32(reader.GetOrdinal("PK"));
            story.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            story.Title = reader.GetString(reader.GetOrdinal("Title"));
            story.LastReadChapterId = reader.GetInt32(reader.GetOrdinal("LastReadChapterId"));
            story.Host = reader.GetString(reader.GetOrdinal("Host"));
            story.AddDate = reader.GetDateTime(reader.GetOrdinal("AddDate"));
            story.LastReadDate = reader.GetDateTime(reader.GetOrdinal("LastReadDate"));
            story.AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId"));
            story.ChapterCount = reader.GetInt32(reader.GetOrdinal("ChapterCount"));
            story.IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete"));
            story.MinimumAge = reader.GetInt32(reader.GetOrdinal("MinimumAge"));
            story.Words = reader.GetInt32(reader.GetOrdinal("Words"));
            story.PublishDate = reader.GetDateTime(reader.GetOrdinal("PublishDate"));
            story.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UpdateDate"));
            story.MetaCheckDate = reader.GetDateTime(reader.GetOrdinal("MetaCheckDate"));

            return story;
        }

        internal void SaveStory(Story story) {
            if (story.Pk == 0) {
                InsertStory(story);
            } else {
                UpdateStory(story);
            }
                
        }

        private void InsertStory(Story story) {
            story.AddDate = DateTime.Now;

            lock (_conn) {
                using (var query = new SQLiteCommand(@"INSERT INTO Story
                    (Id, Title, LastReadChapterId, Host, AddDate, LastReadDate, AuthorId, ChapterCount, IsComplete, MinimumAge, Words, PublishDate, UpdateDate, MetaCheckDate)
                    VALUES (@Id, @Title, @LastReadChapterId, @Host, @AddDate, @LastReadDate, @AuthorId, @ChapterCount, @IsComplete, @MinimumAge, @Words, @PublishDate, @UpdateDate, @MetaCheckDate)"
                    , _conn)) {
                    query.Parameters.AddWithValue("@Id", story.Id);
                    query.Parameters.AddWithValue("@Title", story.Title);
                    query.Parameters.AddWithValue("@Host", story.Host);
                    query.Parameters.AddWithValue("@AddDate", story.AddDate);
                    query.Parameters.AddWithValue("@LastReadChapterId", story.LastReadChapterId);
                    query.Parameters.AddWithValue("@LastReadDate", story.LastReadDate);
                    query.Parameters.AddWithValue("@AuthorId", story.AuthorId);
                    query.Parameters.AddWithValue("@ChapterCount", story.ChapterCount);
                    query.Parameters.AddWithValue("@IsComplete", story.IsComplete);
                    query.Parameters.AddWithValue("@MinimumAge", story.MinimumAge);
                    query.Parameters.AddWithValue("@Words", story.Words);
                    query.Parameters.AddWithValue("@PublishDate", story.PublishDate);
                    query.Parameters.AddWithValue("@UpdateDate", story.UpdateDate);
                    query.Parameters.AddWithValue("@MetaCheckDate", story.MetaCheckDate);

                    query.ExecuteNonQuery();
                }
                using (var query = new SQLiteCommand("select last_insert_rowid()", _conn)) {
                    story.Pk = (long)query.ExecuteScalar();
                }
            }
        }

        private void UpdateStory(Story story) {
            lock (_conn) {
                using (var query = new SQLiteCommand(@"UPDATE Story
                    SET LastReadDate = @LastReadDate, LastReadChapterId = @LastReadChapterId, AuthorId = @AuthorId, ChapterCount = @ChapterCount, IsComplete = @IsComplete, MinimumAge = @MinimumAge, Words = @Words, PublishDate = @PublishDate, UpdateDate = @UpdateDate, MetaCheckDate = @MetaCheckDate
                    WHERE Pk = @Pk", _conn)) {
                    query.Parameters.AddWithValue("@Pk", story.Pk);

                    query.Parameters.AddWithValue("@LastReadDate", story.LastReadDate);
                    query.Parameters.AddWithValue("@LastReadChapterId", story.LastReadChapterId);
                    query.Parameters.AddWithValue("@Title", story.Title);
                    query.Parameters.AddWithValue("@AuthorId", story.AuthorId);
                    query.Parameters.AddWithValue("@ChapterCount", story.ChapterCount);
                    query.Parameters.AddWithValue("@IsComplete", story.IsComplete);
                    query.Parameters.AddWithValue("@MinimumAge", story.MinimumAge);
                    query.Parameters.AddWithValue("@Words", story.Words);
                    query.Parameters.AddWithValue("@PublishDate", story.PublishDate);
                    query.Parameters.AddWithValue("@UpdateDate", story.UpdateDate);
                    query.Parameters.AddWithValue("@MetaCheckDate", story.MetaCheckDate);

                    query.ExecuteNonQuery();
                }
            }
        }
    }
}
