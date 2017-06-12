namespace datos.OPTRACKER
{
    using System.Data.Entity;

    public partial class ModelOPT : DbContext
    {
        public ModelOPT()
            : base("name=ModelOPT")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}