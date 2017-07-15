using System;
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

            Assert.AreEqual(new DateTime(2012, 6, 5), meta.PublishDate);
        }
    }
}