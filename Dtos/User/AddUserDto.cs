namespace WebApi.Dtos
{
    public class AddUserDto
    {
        public string name { get; set; } = "Symbol";

        public int age { get; set; } = 21;

        public Models.Type type { get; set; } = Models.Type.Student;
    }
}