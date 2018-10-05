using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataLayer
{
    public class DataAccess
    {
        private readonly FSD_CAPSULEEntities dbContext = new FSD_CAPSULEEntities();

        #region GetTaskList

       
        public List<TaskDto> GetTaskList()
        {
            List<TaskDto> taskList = new List<TaskDto>();
            
                taskList = (from task in dbContext.Tasks
                            from Ptask in dbContext.ParentTasks.Where(x => x.Parent_ID == task.Parent_ID).DefaultIfEmpty()
                            select new TaskDto
                            {
                                Task_ID = task.Task_ID,
                                Parent_ID = task.Parent_ID,
                                Task1 = task.Task1,
                                StartDate = task.StartDate,
                                EndDate = task.EndDate,
                                Priority = task.Priority,
                                IsTaskEnded = task.IsTaskEnded,
                                ParentTask = Ptask.Parent_Task,
                            }).Distinct().ToList();
            
           
            return taskList;
        }
        #endregion


        #region AddTask
        public bool AddTask(Task task,ParentTask parentTask)
        {
            try
            {

                var addTask = dbContext.Tasks.Add(task);
                var addParentTask = dbContext.ParentTasks.Add(parentTask);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region UpdateTask
        public bool UpdateTask(Task task, ParentTask parentTask)
        {
            try
            {
                Task existTask = dbContext.Tasks.Find(task.Task_ID);
                ((IObjectContextAdapter)dbContext).ObjectContext.Detach(existTask);

                dbContext.Entry(task).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region EndTask
        public bool EndTask(int taskId)
        {
            try
            {
                Task taskData = dbContext.Tasks.Find(taskId);

                taskData.IsTaskEnded = 1;
                dbContext.Entry(taskData).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetTaskById
        public Task GetTaskById(int taskId)
        {
            Task taskData = new Task();
            try
            {
                taskData = dbContext.Tasks.Find(taskId);
                return taskData;
            }
            catch (Exception ex)
            {
                return taskData;
            }
        }
        #endregion

        #region ParentTask
        public List<ParentTask> ParentTask()
        {
            List<ParentTask> parentTaskList = new List<DataLayer.ParentTask>();

            try
            {

               var PTask = (from task in dbContext.Tasks select new { Id = task.Task_ID, Parent_Task = task.Task1 }).ToList();
               foreach (var data in PTask) {
                    ParentTask ptask = new ParentTask();
                    ptask.Id = data.Id;
                    ptask.Parent_Task = data.Parent_Task;
                    parentTaskList.Add(ptask);
                }
                return parentTaskList;
            }
            catch (Exception ex)
            {
                return parentTaskList;
            }
        }
        #endregion

    }
}
