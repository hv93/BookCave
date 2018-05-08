using System.Collections.Generic;
using BookCave.Data.EntityModels;
using System.Linq;
using System.Diagnostics;
using BookCave.Data;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {

        private DataContext _db;

        public BookRepo() 
        {
            _db = new DataContext();
        }

        public List<BookViewModel> GetNewestBooks()
        {
            var bookList = (from bo in _db.Books
                            orderby bo.Id descending
                            select new BookViewModel
                            {
                                Id = bo.Id,
                                Title = bo.Title,
                                Author = bo.Author,
                                PublishingYear = bo.PublishingYear,
                                Description = bo.Description,
                                Genre = bo.Genre,
                                Rating = bo.Rating,
                                Price = bo.Price,
                                Formats = bo.Formats,
                                AudioSample = bo.AudioSample,
                                CoverImage = bo.CoverImage
                            }).Take(15).ToList();
            return bookList;
        }

        public List<BookViewModel> GetBookIndex()
        {
            var bookList = (from bo in _db.Books
                            orderby bo.Title
                            select new BookViewModel
                            {
                                Id = bo.Id,
                                Title = bo.Title,
                                Author = bo.Author,
                                PublishingYear = bo.PublishingYear,
                                Description = bo.Description,
                                Genre = bo.Genre,
                                Rating = bo.Rating,
                                Price = bo.Price,
                                Formats = bo.Formats,
                                AudioSample = bo.AudioSample,
                                CoverImage = bo.CoverImage
                            }).Take(112).ToList();
            return bookList;
        }

        public List<BookViewModel> GetBySearchString(string searchString)
        {
            /*
                //genre filter fara í síðar!!
            var filteredbygenre = (from bo in _db.Books
                                where bo.Genre == searchString
                                orderby bo.Title
                                select new BookViewModel
                                {
                                    Id = bo.Id,
                                    Title = bo.Title,
                                    Author = bo.Author,
                                    PublishingYear = bo.PublishingYear,
                                    Description = bo.Description,
                                    Genre = bo.Genre,
                                    Rating = bo.Rating,
                                    Price = bo.Price,
                                    Formats = bo.Formats,
                                    AudioSample = bo.AudioSample,
                                    CoverImage = bo.CoverImage
                                }).ToList();
             
            */
            
            var booksfiltered = (from bo in _db.Books
                        where bo.Title.Contains(searchString)
                        orderby bo.Title
                        select new BookViewModel
                        {
                            Id = bo.Id,
                            Title = bo.Title,
                            Author = bo.Author,
                            PublishingYear = bo.PublishingYear,
                            Description = bo.Description,
                            Genre = bo.Genre,
                            Rating = bo.Rating,
                            Price = bo.Price,
                            Formats = bo.Formats,
                            AudioSample = bo.AudioSample,
                            CoverImage = bo.CoverImage
                        }).ToList();

            return booksfiltered;
            
        }
        public BookDetailsViewModel GetById(int? bookId)
        {
            var book = (from bo in _db.Books
                    where bo.Id == bookId
                    select new BookDetailsViewModel
                    {
                        Id = bo.Id,
                        Title = bo.Title,
                        Author = bo.Author,
                        PublishingYear = bo.PublishingYear,
                        Description = bo.Description,
                        Genre = bo.Genre,
                        Rating = bo.Rating,
                        Price = bo.Price,
                        Formats = bo.Formats,
                        AudioSample = bo.AudioSample,
                        CoverImage = bo.CoverImage
                    }).SingleOrDefault();
                    var reviews = (from rw in _db.Reviews
                   join bks in _db.Books on rw.BookId equals bks.Id
                   select new ReviewViewModel
                   {
                       OwnerId = rw.OwnerId, //current user
                       BookId = rw.BookId, //current bók
                       ActualReview = rw.ActualReview,
                       Rating = rw.Rating

                   }).ToList();
                   if(book == null)
                   {
                       return book;
                   }
            book.Reviews = reviews;

            return book;
        }
        public List<BookViewModel> TopRatedBooks()
        {
            var bookList = (from bo in _db.Books
                            orderby bo.Rating
                            select new BookViewModel
                            {
                                Id = bo.Id,
                                Title = bo.Title,
                                Author = bo.Author,
                                PublishingYear = bo.PublishingYear,
                                Description = bo.Description,
                                Genre = bo.Genre,
                                Rating = bo.Rating,
                                Price = bo.Price,
                                Formats = bo.Formats,
                                AudioSample = bo.AudioSample,
                                CoverImage = bo.CoverImage
                            }).Take(10).ToList();
            return bookList;
        }



        public bool Update(int bookId)
        {
            //útfæra
            return true;
        }
        public bool Delete(int bookId)
        {
            //útfæra
            return false;
        }
        public bool Create(int bookId)
        {
            //útfæra
            return true;
        }

    }
}