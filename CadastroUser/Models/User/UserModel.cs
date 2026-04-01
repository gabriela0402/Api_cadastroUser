namespace CadastroUser.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Senha { get; set; } 
        public int Idade { get; set; }
    }
}