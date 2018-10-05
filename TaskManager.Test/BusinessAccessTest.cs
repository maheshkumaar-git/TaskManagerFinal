using System;
using NUnit.Framework;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;

namespace TaskManager.Test
{
    [TestFixture]
    public class BusinessAccessTest
    {
        private BusinessAccess busAccess = new BusinessAccess();
        private FSD_CAPSULEEntities dbContext = new FSD_CAPSULEEntities();

        [Test(), Order(1)]
        public void AddTaskTest()
        {
            Task task = new Task
            {
                Task1 = "Task Test 1",
                Parent_ID = null,
                Start_Date = "2018-08-10",
                End_Date = "2018-08-15",
                StartDate = Convert.ToDateTime("2018-08-10"),
                EndDate = Convert.ToDateTime("2018-08-15"),
                Priority = 5,
                IsTaskEnded = 0,
            };
            ParentTask parentTask = new ParentTask
            {
                Parent_Task = "Task Test 1",
                Parent_ID = task.Task_ID
            };

            var addResp = busAccess.AddTask(task, parentTask);
            Assert.NotNull(addResp);
            Assert.IsTrue(addResp);
        }

        [Test(), Order(2)]
        public void GetTaskListTest()
        {

            var taskList = busAccess.GetTaskList();
            Assert.NotNull(taskList);
            Assert.GreaterOrEqual(taskList.Count, 0);
        }


        [Test(), Order(3)]
        public void GetTaskByIdTest()
        {
            var task = busAccess.GetTaskById(2);

            if (task == null)
                Assert.Null(task);

            Assert.NotNull(task);
            Assert.AreEqual(task.Task_ID, 2);

        }

        [Test(), Order(4)]
        public void UpdateTaskTest()
        {
            ParentTask parentTask = new ParentTask();
            Task task = new Task();
            var taskData = busAccess.GetTaskById(2);
            taskData.Task1 = "Updated Test";
            taskData.Priority = 25;

            task.Task1 = taskData.Task1;
            task.StartDate = taskData.StartDate;
            task.EndDate = taskData.EndDate;
            task.Parent_ID = taskData.Parent_ID;
            task.Priority = taskData.Priority;
            task.Task_ID = taskData.Task_ID;
            task.IsTaskEnded = taskData.IsTaskEnded;

            var updateTask = busAccess.UpdateTask(task, parentTask);
            Assert.NotNull(updateTask);
            Assert.IsTrue(updateTask);
        }

        [Test(), Order(5)]
        public void EndTaskTest()
        {

            var taskEnd = busAccess.EndTask(2);
            Assert.NotNull(taskEnd);
            Assert.IsTrue(taskEnd);
        }

        [Test(), Order(6)]
        public void ParentTaskTest()
        {
            var parentTasks = busAccess.ParentTask();
            Assert.NotNull(parentTasks);
            Assert.GreaterOrEqual(parentTasks.Count, 0);
        }
    }
}
