using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;

namespace TaskManager.Api.Controllers
{
    public class TaskController : ApiController
    {
        private BusinessAccess busAccess = new BusinessAccess();

        #region  GetTaskList
        /// <summary>
        /// Get all task list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetTaskList()
        {
            try
            {
                var response = Request.CreateResponse<List<TaskModel>>(HttpStatusCode.OK, busAccess.GetTaskList());
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.ToString());
            }
        }
        #endregion


        #region AddTask
        /// <summary>
        /// Add a new task to the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddTask([FromBody]object data)
        {
            try
            {
                Task taskData = new Task();
                ParentTask parentTaskData = new ParentTask();

                taskData = JsonConvert.DeserializeObject<Task>(data.ToString());
                taskData.StartDate = Convert.ToDateTime(taskData.Start_Date);
                taskData.EndDate = Convert.ToDateTime(taskData.End_Date);

                parentTaskData.Parent_ID = taskData.Task_ID;
                parentTaskData.Parent_Task = taskData.Task1;

                bool addTask = busAccess.AddTask(taskData, parentTaskData);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, addTask);
                return response;

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
        }
        #endregion

        #region UpdateTask
        /// <summary>
        /// Update an existing task item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateTask([FromBody]Task data)
        {
            try
            {
                Task taskData = new Task();
                ParentTask parentTaskData = new ParentTask();
                taskData = data;
                taskData.StartDate = Convert.ToDateTime(taskData.Start_Date);
                taskData.EndDate = Convert.ToDateTime(taskData.End_Date);

                bool updateTask = busAccess.UpdateTask(taskData, parentTaskData);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, updateTask);
                return response;

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
        }
        #endregion

        #region EndTask
        /// <summary>
        /// End the task item selected by user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage EndTask([FromUri]int Id)
        {
            try
            {
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, busAccess.EndTask(Id));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
        }
        #endregion

        #region GetTaskById
        /// <summary>
        /// Get Task Data Using Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetTaskById([FromUri]int Id)
        {
            try
            {
                var response = Request.CreateResponse<TaskModel>(HttpStatusCode.OK, busAccess.GetTaskById(Id));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }
        }
        #endregion

        #region ParentTask
        /// <summary>
        /// Get Parent Task List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ParentTask()
        {
            try
            {
                var response = Request.CreateResponse<List<ParentTask>>(HttpStatusCode.OK, busAccess.ParentTask());
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
        }
        #endregion

    }
}
