namespace WebApi.Models
{
    //Entities
    //'ll interact with DataBase.
    public class User
    {
        public int Id { get; set; }

        public string name { get; set; } = "Symbol";

        public int age { get; set; } = 21;

        public Type type { get; set; } = Type.Student;

        public DateTime createdAt { get; set; } = DateTime.Now;

        public User()
        {

        }

        //copyConstructor
        public User(User newUser)
        {
            this.name = newUser.name;
            this.age = newUser.age;
            this.type = newUser.type;
        }
    }
}