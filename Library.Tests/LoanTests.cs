using Xunit;
using Library.Domain;
using System;

namespace Library.Tests
{
    public class LoanTests
    {
        [Fact]
        public void Loan_Creation_Should_Set_Dates()
        {
            var loan = new Loan
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            Assert.True(loan.DueDate > loan.LoanDate);
        }

        [Fact]
        public void Returning_Book_Should_Set_ReturnDate()
        {
            var loan = new Loan
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            loan.ReturnDate = DateTime.Now;

            Assert.NotNull(loan.ReturnDate);
        }

        [Fact]
        public void Loan_Should_Have_BookId()
        {
            var loan = new Loan
            {
                BookId = 1
            };

            Assert.Equal(1, loan.BookId);
        }

        [Fact]
        public void Loan_Should_Have_MemberId()
        {
            var loan = new Loan
            {
                MemberId = 2
            };

            Assert.Equal(2, loan.MemberId);
        }

        [Fact]
        public void Loan_Should_Be_Overdue()
        {
            var loan = new Loan
            {
                LoanDate = DateTime.Now.AddDays(-20),
                DueDate = DateTime.Now.AddDays(-5)
            };

            Assert.True(loan.DueDate < DateTime.Now);
        }
    }
}