using ProtelAppT.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProtelAppT.Service
{
    public class ClienteService
    {
        private readonly ProtelDbContext _dbContext;
        public ClienteService(ProtelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _dbContext.CLIENTE.Include(c => c.EstadoCliente).ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _dbContext.CLIENTE.FindAsync(id);
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            _dbContext.CLIENTE.Add(cliente);
            await _dbContext.SaveChangesAsync();
            return cliente;
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _dbContext.CLIENTE.Update(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _dbContext.CLIENTE.FindAsync(id);
            if (cliente != null)
            {
                _dbContext.CLIENTE.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteClientesAsync(List<Cliente> clientes)
        {
            _dbContext.CLIENTE.RemoveRange(clientes);
            await _dbContext.SaveChangesAsync();
        }
    }
}
