using DataAccess.Models;
using DataAccess.Models.Tender;
using DataAccess.Models.Constants;
using System.Collections.Generic;

namespace NETCoreWebAPI.BusinessRules.Tender
{
    public static class TenderBusinessRules
    {
        public static TenderDBModel GenerateTenderModel(TenderModel request)
        {
            TenderDBModel tenderDBModel = new TenderDBModel
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                TenderType = request.TenderType,
                StartDateTime = (System.DateTime)request.StartDateTime,
                EndDateTime = (System.DateTime)request.EndDateTime,
                CustomerId = request.CustomerId,
                Status = TenderStatus.Open,
                ProjectType = request.ProjectType,
                Comment = request.Comment,
                CreatedByUsername = request.CreatedByUsername,
                CreatedDateTime = System.DateTime.Now,
                TenderTasks = new List<TenderTasksDBModel>()
            };

            foreach (TenderTasksDBModel item in request.Tasks)
            {
                tenderDBModel.TenderTasks.Add(
                    new TenderTasksDBModel
                    {
                        TaskId = item.TaskId,
                        Task = item.Task,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        ParentTaskId = null,
                        CreatedByUsername = request.CreatedByUsername,
                        CreatedDateTime = System.DateTime.Now
                    });
            }

            return tenderDBModel;
        }
    }
}
