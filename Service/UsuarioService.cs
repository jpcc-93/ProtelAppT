using Microsoft.EntityFrameworkCore;
using ProtelAppT.Data;


namespace ProtelAppT.Service
{
    public class UsuarioService
    {
        private readonly ProtelDbContext _context;

        public UsuarioService(ProtelDbContext context)
        {
            _context = context;
        }

        // Aquí irán los métodos para acceder a datos de la tabla Usuario
        public async Task<int> GetNumeroUsuariosAsync()
        {
            try
            {
                return await _context.USUARIO.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el número de usuarios: {ex.Message}");
                throw;
            }
        }
        public async Task<Usuario> AuthenticateUser(string email, string password)
        {
            var user = await _context.USUARIO.FirstOrDefaultAsync(u => u.Correo == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user; // Credenciales válidas
            }

            return null; // Credenciales inválidas o usuario no encontrado
        }
    }
}