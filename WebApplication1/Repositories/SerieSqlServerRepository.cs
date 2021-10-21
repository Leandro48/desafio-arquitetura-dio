using ApiCatalogoNetflix.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.Repositories
{
    public class SerieSqlServerRepository : ISerieRepository
    {
        private readonly SqlConnection sqlConnection;

        public SerieSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task<List<Serie>> Obter(int pagina, int quantidade)
        {
            var series = new List<Serie>();

            var comando = $"select * from Series order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                series.Add(new Serie
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Nota = (double)sqlDataReader["Nota"]
                });
            }

            await sqlConnection.CloseAsync();

            return series;
        }

        public async Task<Serie> Obter(Guid id)
        {
            Serie serie = null;

            var comando = $"select * from Series where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                serie = new Serie
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Nota = (double)sqlDataReader["Nota"]
                };
            }

            await sqlConnection.CloseAsync();

            return serie;
        }

        public async Task<List<Serie>> Obter(string nome, string produtora)
        {
            var series = new List<Serie>();

            var comando = $"select * from Series where Nome = '{nome}' and Produtora = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                series.Add(new Serie
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Nota = (double)sqlDataReader["Nota"]
                });
            }

            await sqlConnection.CloseAsync();

            return series;
        }

        public async Task Inserir(Serie serie)
        {
            var comando = $"insert Series (Id, Nome, Produtora, Nota) values ('{serie.Id}', '{serie.Nome}', '{serie.Produtora}', {serie.Nota.ToString()})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Serie serie)
        {
            var comando = $"update Series set Nome = '{serie.Nome}', Produtora = '{serie.Produtora}', Preco = {serie.Nota.ToString()} where Id = '{serie.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Series where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
