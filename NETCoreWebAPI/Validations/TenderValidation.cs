using DataAccess.Models.Common;
using DataAccess.Models.Tender;
using System.Collections.Generic;
using System.Reflection;

namespace NETCoreWebAPI.Validations
{
    public static class TenderValidation
    {
        public static List<Error> NewTenderValidation(TenderModel newTender)
        {
            List<Error> validationErrors = new List<Error>();

            if (newTender.CustomerId == null || newTender.CustomerId == 0)
                validationErrors.Add(new Error(nameof(newTender.CustomerId),"Customer Id is required"));

            if (newTender.TenderType == null || newTender.TenderType == 0)
                validationErrors.Add(new Error(nameof(newTender.TenderType), "Tender Type is required"));

            if (newTender.ProjectType == null || newTender.ProjectType == 0)
                validationErrors.Add(new Error(nameof(newTender.ProjectType), "Project Type is required"));

            if (newTender.StartDateTime == null)
                validationErrors.Add(new Error(nameof(newTender.StartDateTime), "Start Date is required"));

            if (newTender.EndDateTime == null)
                validationErrors.Add(new Error(nameof(newTender.EndDateTime), "End Date is required"));

            if (newTender.StartDateTime != null && newTender.EndDateTime != null && newTender.StartDateTime >= newTender.EndDateTime)
                validationErrors.Add(new Error(nameof(newTender.EndDateTime), "End Date cannot be less than Start Date"));

            //if (string.IsNullOrEmpty(newTender.Task1) && string.IsNullOrEmpty(newTender.Task2) && string.IsNullOrEmpty(newTender.Task3))
            //    validationErrors.Add(new Error(nameof(newTender.Task1), "Atleast one task is required"));

            return validationErrors;
        }
    }
}
