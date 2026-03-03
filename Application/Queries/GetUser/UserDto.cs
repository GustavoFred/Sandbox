namespace Application.Queries.GetUser
{
    public class UserDto
    {
        public string Name { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid UserGuid { get; set; }
    }
}
