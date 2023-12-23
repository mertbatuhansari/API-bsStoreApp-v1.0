using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {

        private readonly IRepositoryManager _manager;

        public BookManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Book CreateOneBook(Book book)
        {
            if(book is null) 
                throw new ArgumentNullException(nameof(book));

            _manager.book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity=_manager.book.GetOneBooksByID(id,trackChanges);
            if (entity is null)
                throw new Exception($"Book with id:{id} could not found");

            _manager.book.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GelAllBooks(bool trackChanges)
        {
            return _manager.book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _manager.book.GetOneBooksByID(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.book.GetOneBooksByID(id,trackChanges);
            if (entity is null)
                throw new Exception($"Book with id:{id} could not found");

            if (book is null)
                throw new ArgumentNullException (nameof(book));

            entity.Title =book.Title;
            entity.Price =book.Price;

            _manager.book.Update(entity);
            _manager.Save();
        }
    }
}
