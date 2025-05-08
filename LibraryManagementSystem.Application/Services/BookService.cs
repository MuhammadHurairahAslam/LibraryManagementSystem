using AutoMapper;
using LibraryManagementSystem.Application.DTOS.Books;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepositery _bookRepositery;
        private readonly IMapper _mapper;
        public BookService(IBookRepositery bookRepositery, IMapper mapper)
        {
            _bookRepositery = bookRepositery;
            _mapper = mapper;
        }

        public async Task<GetBookDto> AddAsync(CreateBookDto input)
        {
            var book = _mapper.Map<Book>(input);
            var createdBook=await _bookRepositery.AddAsync(book);
            return _mapper.Map<GetBookDto>(createdBook);
        }

        public async Task DeleteAsync(int id)
        {
            await _bookRepositery.DeleteAsync(id);
        }

        public async Task<List<BookListDto>> GetAllAsync()
        {
            var _books = await _bookRepositery.GetAllAsync();
            return _mapper.Map<List<BookListDto>>(_books);
        }

        public async Task<GetBookDto> GetByIdAsync(int id)
        {
            var _book = await _bookRepositery.GetByIdAsync(id);
            return _mapper.Map<GetBookDto>(_book);
        }

        public async Task UpdateAsync(UpdateBookDto input)
        {
            var book = _mapper.Map<Book>(input);
            await _bookRepositery.UpdateAsync(book);
        }
    }
}
