using FanfictionReader;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            Assert.IsTrue(meta.Reviews > 26000);
            Assert.IsTrue(meta.Favs > 17000);
            Assert.IsTrue(meta.Follows > 13000);
            Assert.IsTrue(meta.Words > 700000);

            Assert.AreEqual(new DateTime(2012, 6, 5), meta.PublishDate);
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