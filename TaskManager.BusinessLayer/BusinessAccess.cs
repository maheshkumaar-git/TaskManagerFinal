using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataLayer;

namespace TaskManager.BusinessLayer
{
    public class BusinessAccess
    {
        private DataAccess dataAccess = new DataAccess();

        #region
        /// <summary>
        /// GetTaskList
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetTaskList()
        {
              var taskList = dataAccess.GetTaskList();

            List<TaskModel> taskListData = new List<TaskModel>();
            foreach (var item in taskList)
            {
                TaskModel taskDto = new TaskModel();
                taskDto.Task_ID = item.Task_ID;
                taskDto.Parent_ID = item.Parent_ID;
                taskDto.Task1 = item.Task1;
                taskDto.StartDate = item.StartDate;
                taskDto.EndDate = item.EndDate;
                taskDto.Priority = item.Priority;
                taskDto.IsTaskEnded = item.IsTaskEnded == null ? 0 : item.IsTaskEnded;
                taskDto.Start_Date = item.Start_Date;
                taskDto.End_Date = item.End_Date;
                taskDto.ParentTask = item.ParentTask;
                taskListData.Add(taskDto);
            }
            return taskListData;
        }
        #endregion

        #region
        /// <summary>
        /// AddTask
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parentTask"></param>
        /// <returns></returns>
        public bool AddTask(Task task, ParentTask parentTask)
        {
            bool IsTaskAdded;
            IsTaskAdded = dataAccess.AddTask(task, parentTask);
            return IsTaskAdded;
        }
        #endregion

        #region
        /// <summary>
        /// UpdateTask
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parentTask"></param>
        /// <returns></returns>
        public bool UpdateTask(Task task, ParentTask parentTask)
        {
            bool IsTaskUpdated;
            IsTaskUpdated = dataAccess.UpdateTask(task, parentTask);
            return IsTaskUpdated;
        }
        #endregion

        #region
        /// <summary>
        /// EndTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool EndTask(int taskId)
        {
            bool IsTaskEnded;
            IsTaskEnded = dataAccess.EndTask(taskId);
            return IsTaskEnded;

        }
        #endregion

        #region
        /// <summary>
        /// GetTaskById
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskModel GetTaskById(int taskId)
        {
            TaskModel taskData = new TaskModel();
            Task task = (dataAccess.GetTaskById(taskId));
            taskData.Task1 = task.Task1;
            taskData.StartDate = task.StartDate;
            taskData.EndDate = task.EndDate;
            taskData.Parent_ID = task.Parent_ID;
            taskData.Priority = task.Priority;
            taskData.Task_ID = task.Task_ID;
            taskData.IsTaskEnded = task.IsTaskEnded;
            taskData.ParentTask = task.ParentTask;
            return taskData;
        }
        #endregion

        #region 
        /// <summary>
        /// ParentTask
        /// </summary>
        /// <returns></returns>
        public List<ParentTask> ParentTask()
        {
            List<ParentTask> ParentTaskList;
            ParentTaskList = dataAccess.ParentTask();
            return ParentTaskList;
        }
        #endregion
    }
}
