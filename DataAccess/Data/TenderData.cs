using Dapper;
using DataAccess.DBAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class TenderData : ITenderData
    {
        private readonly ISqlDataAccess _db;

        public TenderData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task InsertNewTender(UserModel user) =>
            _db.SaveData("dbo.spTender_Insert", new { user.FirstName, user.LastName });

    }
}
