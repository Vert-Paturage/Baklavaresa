namespace Infrastructure.Data.Entities;

internal abstract class DataEntity<TDomainModel>
{
    public DataEntity() { }
    public DataEntity(TDomainModel domainModel)
    {
        // WARNING: C'est charlie ça ?
        FromDomainModel(domainModel);
    }
    public abstract void FromDomainModel(TDomainModel domainModel);
    public abstract TDomainModel ToDomainModel(); 
}