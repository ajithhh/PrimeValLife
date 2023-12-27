namespace PrimeValLife.Core.IServices; 
using PrimeValLife.Core.Models.Audits;

public interface IAuditService
{
    bool SaveAudit(AuditMasterEntity auditMasterEntity);
}
