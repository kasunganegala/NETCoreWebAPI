using DataAccess.Models;
using DataAccess.Models.Tender;
using DataAccess.Models.Constants;
using System.Collections.Generic;
using DataAccess.Models.Bid;

namespace NETCoreWebAPI.BusinessRules.Bid
{
    public static class BidBusinessRules
    {
        public static BidDBModel GenerateBidModel(BidModel request)
        {
            BidDBModel tenderDBModel = new BidDBModel
            {
                Id = request.Id,
                TenderId = request.TenderId,
                StartDateTime = (System.DateTime)request.StartDateTime,
                EndDateTime = (System.DateTime)request.EndDateTime,
                IsSubmitted = request.IsSubmitted,
                Status = BidStatus.Submitted,
                Comment = request.Comment,
                CreatedByUsername = request.CreatedByUsername,
                CreatedDateTime = System.DateTime.Now,
                BidTasks = new List<BidTasksDBModel>()
            };

            foreach (BidTasksDBModel item in request.BidTasks)
            {
                tenderDBModel.BidTasks.Add(
                    new BidTasksDBModel
                    {
                        BidId = item.BidId,
                        TaskId = item.TaskId,
                        ParentTaskId = item.ParentTaskId,
                        Task = item.Task,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        CreatedByUsername = request.CreatedByUsername,
                        CreatedDateTime = System.DateTime.Now
                    });
            }

            return tenderDBModel;
        }
    }
}
