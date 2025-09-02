namespace Educational_Victoria.DTOs.SubjectDto
{
    public class UpdateSubjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
    }
}
