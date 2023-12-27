namespace PrimeValLife.Core.IRepository;
using PrimeValLife.Core.Models.Audits;

public interface IAuditRepository
{
    bool SaveAudit(AuditMasterEntity auditMasterEntity);
}