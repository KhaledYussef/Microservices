namespace Writer.Services
{
    public class WriterRepository : List<Models.Writer>, IWriterRepository
    {
        private readonly static List<Models.Writer> _articles = Populate();

        private static List<Models.Writer> Populate()
        {
            var result = new List<Models.Writer>()
            {
            new() {
                Id = 1,
                Name = "First Writer",
            },
            new() {
                Id = 2,
                Name = "Second Writer",
            },
            new() {
                Id = 3,
                Name = "Third Writer",
            }
        };

            return result;
        }

        public List<Models.Writer> GetAll()
        {
            return _articles;
        }

        public Models.Writer? Get(int id)
        {
            return _articles.FirstOrDefault(x => x.Id == id);
        }

        public int Delete(int id)
        {
            var removed = _articles.SingleOrDefault(x => x.Id == id);
            if (removed != null)
                _articles.Remove(removed);

            return removed?.Id ?? 0;
        }

        public Models.Writer Insert(Models.Writer writer)
        {
            writer.Id = _articles.Max(x => x.Id) + 1;
            _articles.Add(writer);

            return writer;
        }
    }
}
