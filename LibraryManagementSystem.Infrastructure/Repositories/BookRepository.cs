using Dapper;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BookRepository : IBookRepositery
    {
        private readonly DapperContext _dapperContext;
        public BookRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<Book> AddAsync(Book input)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Title", input.Title);
            parameters.Add("Author", input.Author);
            parameters.Add("ISBN", input.ISBN);
            parameters.Add("Genre", input.Genre);
            parameters.Add("Format", input.Format);
            parameters.Add("Availability", input.Availability);
            parameters.Add("PublishedDate", input.PublishedDate);
            parameters.Add("Description", input.Description);

            using (var connection = _dapperContext.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(
                    "AddBook_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var createdBook = new Book
                {
                    Id = id,
                    Title = input.Title,
                    Author = input.Author,
                    ISBN = input.ISBN,
                    Genre = input.Genre,
                    Format = input.Format,
                    Availability = input.Availability,
                    PublishedDate = input.PublishedDate,
                    Description = input.Description
                };

                return createdBook;
            }

        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "DeleteBook_SP",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );
            }

        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var query = "SELECT * FROM BOOKS";
            using (var connection = _dapperContext.CreateConnection())
            {
                var books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }

        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var book = await connection.QuerySingleOrDefaultAsync<Book>(
                    "GetBookById_SP",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );

                return book;
            }

        }

        public async Task UpdateAsync(Book input)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", input.Id);
            parameters.Add("Title", input.Title);
            parameters.Add("Author", input.Author);
            parameters.Add("ISBN", input.ISBN);
            parameters.Add("Genre", input.Genre);
            parameters.Add("Format", input.Format);
            parameters.Add("Availability", input.Availability);
            parameters.Add("PublishedDate", input.PublishedDate);
            parameters.Add("Description", input.Description);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "UpdateBook_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

        }
    }
}
