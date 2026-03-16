using Bogus;
using Library.Domain;
using Library.MVC.Data;

namespace Library.MVC.Seed;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Books.Any())
        {
            return;
        }

        var bookFaker = new Faker<Book>()
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Author, f => f.Name.FullName())
            .RuleFor(b => b.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(b => b.Isbn, f => f.Random.Replace("###-##########"))
            .RuleFor(b => b.IsAvailable, true);

        var books = bookFaker.Generate(20);

        context.Books.AddRange(books);

        var memberFaker = new Faker<Member>()
            .RuleFor(m => m.Name, f => f.Name.FullName())
            .RuleFor(m => m.Email, f => f.Internet.Email())
            .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber());

        var members = memberFaker.Generate(10);

        context.Members.AddRange(members);

        context.SaveChanges();

        var random = new Random();

        var loans = new List<Loan>();

        for (int i = 0; i < 15; i++)
        {
            var book = books[random.Next(books.Count)];
            var member = members[random.Next(members.Count)];

            var loanDate = DateTime.Now.AddDays(-random.Next(1, 30));

            var returned = random.Next(0, 2) == 1;

            DateTime? returnDate = returned ? loanDate.AddDays(random.Next(1, 10)) : null;

            if (!returned)
            {
                book.IsAvailable = false;
            }

            loans.Add(new Loan
            {
                BookId = book.Id,
                MemberId = member.Id,
                LoanDate = loanDate,
                DueDate = loanDate.AddDays(14),
                ReturnDate = returnDate
            });
        }

        context.Loans.AddRange(loans);

        context.SaveChanges();
    }
}