using LibraryManagementSystem.Application.DTOS.Books;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces.Repositories
{
    public interface IBookRepositery
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book input);
        Task DeleteAsync(int id);
        Task UpdateAsync(Book input);
    }
}
