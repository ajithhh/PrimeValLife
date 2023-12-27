namespace PrimeValLife.Core.Services;
using PrimeValLife.Core.IRepository;
using PrimeValLife.Core.IServices;
using PrimeValLife.Core.Models.Audits;

public class AuditService : IAuditService
{
    private readonly IAuditRepository _auditRepository;

    #region Constructor
    public AuditService(IAuditRepository auditRepository)
    {
        _auditRepository = auditRepository;
    }
    #endregion

    #region Save Audit
    public bool SaveAudit(AuditMasterEntity auditMasterEntity)
    {
        return _auditRepository.SaveAudit(auditMasterEntity);
    }
    #endregion
}
