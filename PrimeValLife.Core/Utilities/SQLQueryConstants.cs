namespace PrimeValLife.Core.Utilities;

public class SQLQueryConstants
{
    #region Audit Queries
    public const string SaveAudit = " INSERT INTO dwi.Audit_MasterTable(Type,Action,ActionId,ActionValue,ActionBy,Status,Timestamp) VALUES (@Type,@Action,@ActionId,@ActionValue,@ActionBy,@Status,@Timestamp) Select cast(SCOPE_IDENTITY() as bigint)";
    #endregion
}