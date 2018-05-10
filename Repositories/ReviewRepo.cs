using System.Collections.Generic;
using BookCave.Data.EntityModels;
using System.Linq;
using System.Diagnostics;
using BookCave.Data;
using BookCave.Models.ViewModels;
using BookCave.Models.InputModels;
using Microsoft.AspNetCore.Identity;
using BookCave.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Repositories
{
    public class ReviewRepo
    {
        private DataContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public ReviewRepo(SignInManager<ApplicationUser> signinManager, UserManager<ApplicationUser> userManager)
        {
            _signinManager = signinManager;
            _userManager = userManager;
        }
        public ReviewRepo() 
        {
            _db = new DataContext();
        }
        public List<ReviewViewModel> GetByBookId(int bookId) //Nær í review tengd þessarri bók
        {
            var bookReviews = (from rv in _db.Reviews
                         where rv.BookId == bookId
                         select new ReviewViewModel
                        {
                            UserId = rv.UserId,
                            BookId = rv.BookId,
                            ActualReview = rv.ActualReview,
                        }).ToList();
            return bookReviews;
        }

        public List<ReviewViewModel> GetByOwnerId(string ownerId) //Nær í review tengd þessum notanda
        {
            var ownerReviews = (from rv in _db.Reviews
                         where rv.UserId == ownerId
                         select new ReviewViewModel
                        {

                            ActualReview = rv.ActualReview,
                            Rating = rv.Rating
                        }).ToList();
            
            return ownerReviews;
        }
        public ReviewViewModel GetByReviewId(int reviewId) //Nær í ákveðið review
        {
            var review = (from rv in _db.Reviews
                          where rv.Id == reviewId
                          select new ReviewViewModel
                          {
                            Id = rv.Id,
                            UserId = rv.UserId,
                            BookId = rv.BookId,
                            ActualReview = rv.ActualReview,
                            Rating = rv.Rating
                          }).SingleOrDefault();
            
            return review;
        }
        [HttpPost]
        public bool Create(ReviewInputModel rev)
        {
            
            var reviewToAdd = (from rv in _db.Reviews
                                where rev.UserId == rv.UserId && rv.BookId == rev.BookId && rv.Id == rev.Id
                                select new Review
                                {
                                    Id = rv.Id,
                                    UserId= rv.UserId,
                                    BookId = rv.BookId,
                                    ActualReview = rv.ActualReview,
                                    Rating = rv.Rating
                                }).FirstOrDefault();
            if (reviewToAdd == null)
            {
                reviewToAdd = new Review
                {
                    UserId= rev.UserId,
                    BookId = rev.BookId,
                    ActualReview = rev.ActualReview,
                    Rating = rev.Rating
                };
                if(reviewToAdd == null)
                {
                    return false;
                }
            }
            
   //         _db.Add(reviewToAdd);
    //        _db.SaveChanges();

            return true;
        }
    }
}