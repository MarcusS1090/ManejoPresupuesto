// Ignore Spelling: Crear tipo Cuenta Existe

using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipocuenta);
        Task Crear(TipoCuenta tipoCuenta);
        Task Eliminar(int id);
        Task<bool> Existe(string Nombre, int UsuarioId, int id = 0);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task Obtener();
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tiposCuentaOrdenados);
    }
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {

        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task  Crear(TipoCuenta tipoCuenta)
        { 
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                                     ("TipoCuentas_Insertar",
                                     new
                                     {
                                         usuarioId = tipoCuenta.UsuarioId,
                                         nombre = tipoCuenta.Nombre}, commandType: System.Data.CommandType.StoredProcedure);
            tipoCuenta.Id = id;
        }

            /* validando el campo para que no ingrese el mismo valor varias
             * veces en la base de datos*/
        public async Task<bool> Existe(string Nombre, int UsuarioId, int id = 0)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                                        (@"SELECT 1 FROM TiposCuentas
                                        WHERE Nombre = @Nombre AND
                                        UsuarioId = @UsuarioId AND Id <> @id", new { Nombre, UsuarioId, id });

            return existe ==  1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection (connectionString);
            return await connection.QueryAsync<TipoCuenta>(
                                    @"SELECT Id, Nombre, Orden FROM TiposCuentas
                                    WHERE UsuarioId = @UsuarioId ORDER BY Orden",
                                    new { usuarioId });
        }

        public async Task Actualizar(TipoCuenta tipocuenta)
            
            {
                using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                            @"UPDATE TiposCuentas 
                            SET Nombre = @Nombre
                            WHERE Id = @Id",tipocuenta);
            }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {

            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(
                                    @"SELECT Id,Nombre, Orden FROM TiposCuentas
                                    WHERE Id = @Id AND UsuarioId = @UsuarioId",
                                    new { id, usuarioId });
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas WHERE Id = @Id", new {id});
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tiposCuentaOrdenados)
        {
            var query = "UPDATE  TiposCuentas SET Orden = @Orden WHERE Id = @Id";
            using var connection = new SqlConnection (connectionString);
            await connection.ExecuteAsync(query, tiposCuentaOrdenados);
        }

        public Task Obtener()
        {
            throw new NotImplementedException();
        }
    }

    
}
