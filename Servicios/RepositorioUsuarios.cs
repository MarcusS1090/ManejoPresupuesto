﻿using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task Actualizar(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }
    public class RepositorioUsuarios: IRepositorioUsuarios
    {
        private readonly string connectiongString;
        
        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectiongString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(connectiongString);
            var usuarioid = await connection.QuerySingleAsync<int>(@"
                    INSERT INTO Usuarios (Email,EmailNormalizado,PasswordHash) 
                    VALUES (@Email,@EmailNormalizado,@PasswordHash);
                    SELECT SCOPE_IDENTITY();
                    ", usuario);

            await connection.ExecuteAsync("CrearDatosUsuarioNuevo", new { usuarioid },
                    commandType: System.Data.CommandType.StoredProcedure);

            return usuarioid;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectiongString);

            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuarios WHERE EmailNormalizado = @emailNormalizado",
                    new { emailNormalizado });
        }

        public async Task Actualizar(Usuario usuario) 
        {
            using var connection = new SqlConnection(connectiongString);
            await connection.ExecuteAsync(@"
                UPDATE Usuarios 
                SET PasswordHash = @PasswordHash
                WHERE Id = @Id", usuario);
        }
    }
}
