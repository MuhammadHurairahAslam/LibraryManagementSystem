using LibraryManagementSystem.Application.DTOS.Books;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<List<BookListDto>> GetAllAsync();
        Task<GetBookDto> GetByIdAsync(int id);
        Task<GetBookDto> AddAsync(CreateBookDto input);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateBookDto input);
    }
}
