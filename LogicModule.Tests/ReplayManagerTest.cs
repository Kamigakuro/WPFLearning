using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicModule.Tests
{

    [TestFixture]
    public class ReplayManagerTest
    {
        private IReplayManager _manager;
        private string _path;

        [SetUp]
        public void Init()
        {
            _path = @"D:\Projects\WPF_MVVM\LogicModule\bin\Debug\test.wotreplay";
            _manager = new ReplayReader();
            if (!File.Exists(_path))
                File.Copy(@"D:\Projects\WPF_MVVM\LogicModule\bin\Debug\test - копия.wotreplay", _path);
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(ReplayReader).Assembly.Location);
            if (dir != null)
            {
                Environment.CurrentDirectory = dir;
                Directory.SetCurrentDirectory(dir);
            }
            else
                throw new Exception("Path.GetDirectoryName(typeof(ReplayReader).Assembly.Location) returned null");
        }

        [Test]
        public void ReadReplay_ReadFile()
        {
            string replay = _manager.ReadReplay(_path);

            Assert.That(replay, Is.Not.Empty);
        }
        [Test]
        public void ReadReplay_NotNull()
        {
            string replay = _manager.ReadReplay(_path);

            Assert.That(replay, Is.Not.Null);
        }
        [Test]
        public void ReadReplay_ContainPlayers()
        {
            string replay = _manager.ReadReplay(_path);
            bool expectedresult = replay.Contains("slyder974");

            Assert.That(expectedresult, Is.True);
        }
        [Test]
        public void ReadReplay_ReadLines()
        {
            string replay = _manager.ReadReplay(_path, 5);

            Assert.That(replay, Is.Not.Empty);
        }
        [Test]
        public void ReplaceReplay_Replace()
        {
            string destpath = @"D:\Projects\WPF_MVVM\LogicModule\bin\Debug\bin\testReplayced.wotreplay";
            _manager.ReplaceReplay(_path, destpath);
            bool result = File.Exists(destpath);

            Assert.That(result, Is.True);
        }
        [Test]
        public void DeleteReplay_Delete()
        {
            _manager.DeleteReplay(_path);
            bool result = File.Exists(_path);

            Assert.That(result, Is.False);
        }

    }
}
