namespace Bookie.data.Auth.Model
{
    public static class BookieRoles
    {
        public const string Admin = nameof(Admin);
        public const string BookieUser = nameof(BookieUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, BookieUser };
    }
}
