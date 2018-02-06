using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                // Look for any movies.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                     new Book
                     {
                         Title = "Microsoft Visual C# Step by Step (8th Edition)",
                         Author = "John Sharp",
                         ISBN = "9781509301041",
                         PublicationYear = 2015,
                         Publisher = "Microsoft Press",
                         Country = "Canada",
                     },

                     new Book
                     {
                         Title = "C# 7.0 in a Nutshell: The Definitive Reference",
                         Author = "Joseph Albahari",
                         ISBN = "1491987650",
                         PublicationYear = 2017,
                         Publisher = "O'Reilly Media",
                         Country = "UK",
                     },

                     new Book
                     {
                         Title = "C#: Learn C# in One Day and Learn It Well. C# for Beginners with Hands-on Project. (Learn Coding Fast with Hands-On Project Book 3)",
                         Author = "Jamie Chan",
                         ISBN = "1518800270",
                         PublicationYear = 2015,
                         Publisher = "Learn Coding Fast",
                         Country = "USA",
                     },

                     new Book
                     {
                         Title = "The C# Player's Guide (3rd Edition)",
                         Author = "RB Whitaker",
                         ISBN = "0985580135",
                         PublicationYear = 2016,
                         Publisher = "Starbound Software",
                         Country = "USA",
                     },

                     new Book
                     {
                         Title = "Pro C# 7: With .NET and .NET Core",
                         Author = "Andrew Troelsen",
                         ISBN = "1484230175",
                         PublicationYear = 2017,
                         Publisher = "Apress",
                         Country = "India",
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
