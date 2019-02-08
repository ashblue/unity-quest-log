using NSubstitute;
using NUnit.Framework;

namespace CleverCrow.QuestLogs.Editors {
    public class QuestCollectionTest {
        private QuestCollection _col;
        private IQuest _quest;
        private IQuest _questCopy;
        
        [SetUp]
        public void BeforeEach () {
            _col = new QuestCollection();
            _quest = Substitute.For<IQuest>();
            _questCopy = Substitute.For<IQuest>();
            _quest.GetCopy().Returns((x) => _questCopy);
        }
        
        public class AddMethod : QuestCollectionTest {
            [Test]
            public void It_should_add_a_quest () {
                _col.Add(_quest);
                
                Assert.AreEqual(1, _col.Quests.Count);
            }
            
            [Test]
            public void It_should_add_a_copy_of_the_quest () {
                _col.Add(_quest);
                
                Assert.AreEqual(_questCopy, _col.Quests[0]);
            }
            
            [Test]
            public void It_should_run_Setup_on_the_quest () {
                _col.Add(_quest);

                _questCopy.Received(1).Setup();
            }
        }

        public class GetMethod : QuestCollectionTest {
            [Test]
            public void It_should_return_the_quest_copy () {
                _col.Add(_quest);
                
                Assert.AreEqual(_questCopy,_col.Get(_quest));
            }
        }
    }
}
