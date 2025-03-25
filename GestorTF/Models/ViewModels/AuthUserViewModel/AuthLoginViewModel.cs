namespace GestorTF.Models.ViewModels.AuthUserViewModel
{
    public class AuthLoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? TokenWhatsapp { get; set; }
    }
}
