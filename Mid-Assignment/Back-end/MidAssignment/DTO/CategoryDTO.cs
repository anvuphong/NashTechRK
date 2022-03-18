namespace MidAssignment.DTO
{
    public class CategoryDTO
    {
        public string? CategoryName { get; set; }
    }

    public class CategoryWithIdDTO : CategoryDTO
    {
        public int CategoryId { get; set; }
    }
}