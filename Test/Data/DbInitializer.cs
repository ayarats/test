namespace Data
{
    public static class DbInitializer
    {
        public static void Seed(RepositoryContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
