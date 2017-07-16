using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FanfictionReader.Tests {
    [TestClass()]
    public class FictionpressStoryParserTests {
        [TestMethod()]
        public void GetMetaTest() {
            var parser = new FictionpressStoryParser();
            var story = new Story {
                Host = "fanfiction.net",
                Id = 8186071
            };

            var meta = parser.GetMeta(story);

            Assert.AreEqual("Harry Crow", meta.Title);
            Assert.AreEqual(106, meta.ChapterCount);
            Assert.AreEqual(true, meta.IsComplete);
            Assert.IsTrue(meta.Reviews > 26000, "Review count");
            Assert.IsTrue(meta.Favs > 17000, "Favs count");
            Assert.IsTrue(meta.Follows > 13000, "Follower count");
            Assert.IsTrue(meta.Words > 700000,"Word count");
            Assert.IsTrue(meta.Description.Length > 50, "Description exists");

            Assert.AreEqual(new DateTime(2012, 6, 5), meta.PublishDate);
            Assert.IsTrue(meta.UpdateDate >= new DateTime(2014,6,7), "Update Time");
        }

        [TestMethod()]
        public void GetMetaOneShotTest() {
            var parser = new FictionpressStoryParser();
            var story = new Story {
                Host = "fanfiction.net",
                Id = 5777316
            };
            
            var meta = parser.GetMeta(story);

            Assert.AreEqual("Hedwig and the Goblet of Fire", meta.Title);
            Assert.IsTrue(meta.Reviews > 400, "Review count");
            Assert.IsTrue(meta.Favs > 3000, "Favs count");
            Assert.IsTrue(meta.Follows > 700, "Follower count");
            Assert.IsTrue(meta.Words > 3000, "Word count");
            Assert.AreEqual("Harry uses Hedwig to test the restrictions on the Goblet of Fire. Obviously, they're not good enough to stop the smartest owl in Britain!", meta.Description);
            
            Assert.AreEqual(1, meta.ChapterCount);
            Assert.AreEqual(true, meta.IsComplete);
            
            Assert.AreEqual(meta.PublishDate, meta.UpdateDate);
        }

        [TestMethod()]
        public void TokenToDateTest() {
            var parser = new FictionpressStoryParser();
            var checkTime = new DateTime(2010, 4, 17);

            var formats = new Dictionary<string, DateTime>{
                { "8/25/2015", new DateTime(2015,8,25) },
                { "8/25", new DateTime(2010,8,25)},
                { "1h", checkTime - TimeSpan.FromHours(1)},
                { "10m", checkTime - TimeSpan.FromMinutes(10)}
            };

            foreach (var pair in formats) {
                DateTime time = parser.TokenToDate(pair.Key, checkTime);
                Assert.AreEqual(pair.Value, time);
            }
        }

        [TestMethod()]
        public void TokenToIntTest() {
            var parser = new FictionpressStoryParser();
            Assert.AreEqual(1234567, parser.TokenToInt("1,234,567"));
        }
    }
}