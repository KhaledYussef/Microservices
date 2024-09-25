






namespace Writer.Services
{
    public interface IWriterRepository
    {
        int Delete(int id);
        Models.Writer? Get(int id);
        List<Models.Writer> GetAll();
        Models.Writer Insert(Models.Writer writer);
    }
}