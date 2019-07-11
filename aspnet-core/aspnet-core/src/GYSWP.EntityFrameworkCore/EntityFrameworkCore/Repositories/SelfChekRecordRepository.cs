using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using GYSWP.SelfChekRecords;
using GYSWP.SelfChekRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Abp.Data;

namespace GYSWP.EntityFrameworkCore.Repositories
{
    public class SelfChekRecordRepository : GYSWPRepositoryBase<SelfChekRecord, Guid> , ISelfChekRecordRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;
        public SelfChekRecordRepository(IDbContextProvider<GYSWPDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
          : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }
        private void EnsureConnectionOpen()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
                 {
                    {"ContextType", typeof(GYSWPDbContext) },
                    {"MultiTenancySide", MultiTenancySide }
                 });
        }

        public async Task<List<InspectDto>> GetSearchInspectReports(InspectInputDto input)
        {
            EnsureConnectionOpen();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Year", input.Month.Year),
                new SqlParameter("@Month", input.Month.Month),
                new SqlParameter("@UserName", SqlDbType.NVarChar, 100),
                new SqlParameter("@DeptId", input.DeptId)
            };
            param[2].Value = input.UserName == null ? "" : "%" + input.UserName + "%";

            var sql = @"SELECT A.Id
                        ,A.DeptId
                        ,A.DepartmentName
                        ,A.Name
                        ,A.Position
                        ,A.ECNum
                        ,ISNULL(B.ClickRate, 0) AS ClickRate
                        ,CASE WHEN A.ECNum = 0 THEN 0 ELSE C.SNum/(A.ECNum*1.00) END AS SurfaceRate
                        ,ISNULL(D.ClickNum,0) AS ClickNum 
                        FROM(SELECT o.Id AS DeptId, em.Id, em.Name, em.Position, o.DepartmentName, COUNT(ec.Id) AS ECNum 
	                        FROM [dbo].[Employees] em
	                        INNER JOIN [dbo].[Organizations] o ON CHARINDEX(em.Department, '['+CAST(o.Id AS VARCHAR(50))+']') > 0
	                        LEFT JOIN [dbo].[EmployeeClauses] ec ON em.Id = ec.EmployeeId
	                        GROUP BY o.Id, em.Id, em.Name, em.Position, o.DepartmentName 
                        ) A LEFT JOIN (SELECT EmployeeId, COUNT(1)/20.00 AS ClickRate 
	                        FROM (SELECT EmployeeId FROM(
	                        SELECT EmployeeId, YEAR([CreationTime]) y, MONTH([CreationTime]) m, DAY([CreationTime]) d 
	                        FROM [dbo].[SelfChekRecords] 
	                        WHERE YEAR([CreationTime]) = @Year and MONTH([CreationTime]) = @Month
	                        ) tt GROUP BY EmployeeId, y, m, d
	                        ) tm GROUP BY EmployeeId
                        ) B ON A.Id = B.EmployeeId
	                        LEFT JOIN (SELECT [EmployeeId], COUNT([ClauseId]) AS SNum 
	                        FROM (SELECT [EmployeeId], [ClauseId] 
	                        FROM [dbo].[SelfChekRecords]
	                        WHERE YEAR([CreationTime]) = @Year
	                        GROUP BY [EmployeeId], [ClauseId]
	                        ) djm GROUP BY [EmployeeId]
                        ) C ON A.Id = C.EmployeeId
                        LEFT JOIN (SELECT EmployeeId, COUNT(1) AS ClickNum 
	                        FROM (SELECT EmployeeId FROM(SELECT EmployeeId,[ClauseId], YEAR([CreationTime]) y, MONTH([CreationTime]) m, DAY([CreationTime]) d 
	                        FROM [dbo].[SelfChekRecords] 
	                        WHERE YEAR([CreationTime]) = @Year and MONTH([CreationTime]) = @Month
	                        ) tt GROUP BY EmployeeId,[ClauseId], y, m, d
	                        ) tm GROUP BY EmployeeId
                        ) D ON A.Id = D.EmployeeId
                        WHERE A.DeptId = @DeptId
                        AND (@UserName = '' OR A.Name LIKE @UserName)";

            var command = CreateCommand(sql, CommandType.Text, param);
            using (command)
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<InspectDto>();
                    while (dataReader.Read())
                    {
                        var entity = new InspectDto();
                        entity.EmployeeId = dataReader["Id"].ToString();
                        entity.EmployeeName = dataReader["Name"].ToString();
                        entity.EmployeePosition = dataReader["Position"].ToString();
                        entity.DeptName = dataReader["DepartmentName"].ToString();
                        entity.PostUseNum = (int)dataReader["ECNum"];
                        //entity.SurfaceRate = (decimal)dataReader["SurfaceRate"]; 
                        entity.ClickRate = (decimal)dataReader["ClickRate"];
                        entity.ClickNum = (int)dataReader["ClickNum"];
                        result.Add(entity);
                    }
                    return result;
                }
            }
        }
    }
}
