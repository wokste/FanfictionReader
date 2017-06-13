using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionReader {
    internal class ChapterCache {
        private readonly SQLiteConnection _conn;

        internal ChapterCache(SQLiteConnection conn) {
            _conn = conn;
        }

        /// <summary>
        /// Looks in the cache whether a chapter exists. If it does, it will return the chapter.
        /// </summary>
        /// <param name="story">The story where the chapter is about.</param>
        /// <param name="chapterId">The chapter id of the story.</param>
        /// <returns>A chapter if it is the cache. Null otherwise.</returns>
        internal Chapter GetChapterIfExists(Story story, int chapterId) {
            lock (_conn) {
                using (var query =
                    new SQLiteCommand("SELECT * FROM Chapter WHERE StoryPk = @StoryPk AND ChapterId = @ChapterId",
                        _conn)) {
                    query.Parameters.AddWithValue("@StoryPk", story.Pk);
                    query.Parameters.AddWithValue("@ChapterId", chapterId);
                    using (var reader = query.ExecuteReader()) {
                        if (reader.Read()) {
                            return GetChapter(story, reader);
                        }
                    }
                }
            }

            return null;
        }

        private Chapter GetChapter(Story story, IDataRecord reader) {
            var chapter = new Chapter();
            chapter.Story = story;
            chapter.ChapterId = reader.GetInt32(reader.GetOrdinal("ChapterId"));
            chapter.Title = reader.GetString(reader.GetOrdinal("Title"));
            chapter.HtmlText = reader.GetString(reader.GetOrdinal("HtmlText"));
            
            return chapter;
        }

        /// <summary>
        /// Save a chapter for later usage
        /// </summary>
        /// <param name="chapter">The chapter to be saved</param>
        internal void SaveChapter(Chapter chapter) {
            if (!chapter.Valid) {
                return;
            }

            if (!UpdateChapter(chapter))
                InsertChapter(chapter);
        }

        private void InsertChapter(Chapter chapter) {
            lock (_conn) {
                using (var query = new SQLiteCommand(@"INSERT INTO Chapter
                    (StoryPk, ChapterId, Title, HtmlText)
                    VALUES (@StoryPk, @ChapterId, @Title, @HtmlText)"
                    , _conn)) {
                    query.Parameters.AddWithValue("@StoryPk", chapter.Story.Pk);
                    query.Parameters.AddWithValue("@ChapterId", chapter.ChapterId);
                    query.Parameters.AddWithValue("@Title", chapter.Title);
                    query.Parameters.AddWithValue("@HtmlText", chapter.HtmlText);

                    query.ExecuteNonQuery();
                }
            }
        }

        private bool UpdateChapter(Chapter chapter) {
            lock (_conn) {
                using (var query = new SQLiteCommand(@"UPDATE Chapter
                    SET Title = @Title, HtmlText = @HtmlText
                    WHERE StoryPk = @StoryPk AND ChapterId = @ChapterId", _conn)) {

                    query.Parameters.AddWithValue("@StoryPk", chapter.Story.Pk);
                    query.Parameters.AddWithValue("@ChapterId", chapter.ChapterId);
                    query.Parameters.AddWithValue("@Title", chapter.Title);
                    query.Parameters.AddWithValue("@HtmlText", chapter.HtmlText);

                    var rows = query.ExecuteNonQuery();
                    return (rows > 0);
                }
            }
        }
    }
}
