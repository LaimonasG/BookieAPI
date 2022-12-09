namespace Bookie.data.dtos.Comments
{
    public class CommentsSearchParameters
    {
        private const int maxPageSize = 50;
        private int pageSize=2;
        public int pageNumber { get; init; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : value;
        }

}
}
