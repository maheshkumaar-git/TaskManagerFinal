using NBench;
using System;
using System.Linq;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;

namespace TaskManager.NBench.Test
{

    public class TestCase : PerformanceTestSetup<TestCase>
    {
        


        BusinessAccess busAccess = null;
         FSD_CAPSULEEntities dbContext = null;
        private const int AcceptableMinAddThroughput = 500;
        
        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            busAccess = new BusinessAccess();
            dbContext = new FSD_CAPSULEEntities();

        }

        #region AddTaskPerfTest
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, SkipWarmups = true, TestMode = TestMode.Test)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 900000)]
        public void AddTaskPerfTest()
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
        }
        #endregion

        #region ParentTaskPerfTest
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, SkipWarmups = true,TestMode = TestMode.Test )]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 900000)]
        public void ParentTaskPerfTest()
        {
            var parentTasks = busAccess.ParentTask();          
            var count = parentTasks.Count();

        }
        #endregion

        #region GetTaskListPerfTest

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, SkipWarmups = true,TestMode = TestMode.Test )]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 900000)]
        public void GetTaskListPerfTest()
        {
            var taskList = busAccess.GetTaskList();
            var count = taskList.Count();
        }
        #endregion

        #region EndTaskPerfTest
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, SkipWarmups = true,TestMode = TestMode.Test )]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 900000)]
        public void EndTaskPerfTest()
        {
            var taskEnd = busAccess.EndTask(2);
        }
        #endregion 

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
            busAccess = null;
            dbContext = null;
        }
    }
}
