using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace CleverCrow.QuestLogs.Editors {
    public class QuestDefinitionTest {
        private QuestDefinition _quest;
        private IQuestTask _task;
        
        [SetUp]
        public void BeforeEach () {
            _quest = ScriptableObject.CreateInstance<QuestDefinition>();
            _task = Substitute.For<IQuestTask>();
        }
        
        public class GetCopyMethod : QuestDefinitionTest {
            [Test]
            public void It_should_return_a_copy_of_the_current_quest () {
                Assert.AreNotEqual(_quest.GetCopy(), _quest);
            }
        }

        public class ActiveTaskProperty : QuestDefinitionTest {
            [Test]
            public void It_should_default_to_the_first_task () {
                _quest.Tasks.Add(_task);
                
                Assert.AreEqual(_task, _quest.ActiveTask);
            }

            [Test]
            public void Points_to_last_task_if_Next_has_been_called_in_excess_of_tasks () {
                _quest.Tasks.Add(Substitute.For<IQuestTask>());
                _quest.Tasks.Add(_task);

                _quest.NextTask();
                _quest.NextTask();

                Assert.AreEqual(_task, _quest.ActiveTask);
            }
        }

        public class NextTaskMethod : QuestDefinitionTest {
            private IQuestTask _taskLast;
            
            [SetUp]
            public void NextTaskMethodBeforeEach () {
                _taskLast = Substitute.For<IQuestTask>();
                
                _quest.Tasks.Add(_task);
                _quest.Tasks.Add(_taskLast);
                _quest.NextTask();
            }
            
            [Test]
            public void It_should_advance_the_ActiveTask_to_the_next_task () {
                Assert.AreEqual(_taskLast, _quest.ActiveTask);
            }

            [Test]
            public void It_should_mark_the_next_task_as_ongoing () {
                Assert.AreEqual(QuestStatus.Ongoing, _taskLast.Status);
            }

            [Test]
            public void It_should_mark_the_previous_task_as_complete () {
                Assert.AreEqual(QuestStatus.Success, _task.Status);
            }
            
            [Test]
            public void It_should_not_mark_the_quest_complete_early () {
                Assert.AreNotEqual(QuestStatus.Success, _quest.Status);
            }

            [Test]
            public void If_Next_exceeds_the_last_task_mark_the_quest_complete () {
                _quest.NextTask();

                Assert.AreEqual(QuestStatus.Success, _quest.Status);
            }
            
            [Test]
            public void If_Next_exceeds_the_last_task_mark_the_last_task_complete () {
                _quest.NextTask();

                Assert.AreEqual(QuestStatus.Success, _taskLast.Status);
            }
        }
    }
}
