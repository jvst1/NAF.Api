using NAF.Domain.Enum;

namespace NAF.Domain.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string DocumentoFederal { get; set; }
        public string Apelido { get; set; }
        public TipoPerfil TipoPerfil { get; set; }
    }
}
