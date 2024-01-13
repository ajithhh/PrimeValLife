namespace PrimeValLife.Core.Repository;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PrimeValLife.Core.IRepository;
using PrimeValLife.Core.Models.Audits;
using PrimeValLife.Core.Utilities;
using System.Data;

public class AuditRepository : IAuditRepository
{
    #region Declarations
    private readonly IConfiguration _config;
    public IDbConnection MIYDapperDbConnection => new SqlConnection(_config.GetConnectionString(""));
    #endregion

    #region Constructor
    public AuditRepository(IConfiguration config) => _config = config;
    #endregion

    #region Save Audit
    public bool SaveAudit(AuditMasterEntity auditMasterEntity)
    {
        bool isSaved = false;
        long _auditId = 0;
        using (var dbconnection = MIYDapperDbConnection)
        {
            //_auditId = dbconnection.Query<long>(SQLQueryConstants.SaveAudit, new { @Type = auditMasterEntity.Type, @Action = auditMasterEntity.Action, @ActionId = auditMasterEntity.ActionId, @ActionValue = auditMasterEntity.ActionValue, @ActionBy = auditMasterEntity.ActionBy, @Status = auditMasterEntity.Status, @Timestamp = auditMasterEntity.Timestamp }).SingleOrDefault();
        }
        if (_auditId > 0)
        {
            isSaved = true;
        }
        return isSaved;
    }
    #endregion
}
