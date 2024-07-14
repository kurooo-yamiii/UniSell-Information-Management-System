using WebUniform.Models;

namespace WebUniform.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Database>();

                context.Database.EnsureCreated();

                // Check if users already exist
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>
                    {
                        new User
                        {
                            Name = "Rhina E. Corpuz",
                            Username = "rhinacorpuz20@gmail.com",
                            Password = "12345",
                            Contact = "09945446526",
                            Department = "College of Education",
                            Status = "Active"
                        },
                         new User
                        {
                            Name = "Admin",
                            Username = "Admin",
                            Password = "12345",
                            Contact = "N/A",
                            Department = "N/A",
                            Status = "Admin"
                        },
                        new User
                        {
                            Name = "Kenneth L. Cobarrubias",
                            Username = "kennethcobarubias12@gmail.com",
                            Password = "12345",
                            Contact = "09488239866",
                            Department = "College of Education",
                            Status = "Active"
                        }
                    });

                    context.SaveChanges();
                }

                // Retrieve the users from the database
                var rhina = context.Users.FirstOrDefault(u => u.Username == "rhinacorpuz20@gmail.com");
                var kenneth = context.Users.FirstOrDefault(u => u.Username == "kennethcobarubias12@gmail.com");

                if (!context.Slacks.Any())
                {
                    context.Slacks.AddRange(new List<Slack>
                    {
                        new Slack
                        {
                            Waist = "32.5 CM",
                            Length = "120 CM",
                            Image = "https://www.lowes.com.au/media/catalog/product/4/6/46939_f.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=550&width=413&canvas=413:550",
                            Address = new Address
                            {
                                Street = "H. Alarcon",
                                City = "Antipolo City",
                                State = "Rizal"
                            },
                            UserId = rhina.Id
                        },
                        new Slack
                        {
                            Waist = "42.5 CM",
                            Length = "150 CM",
                            Image = "https://www.lowes.com.au/media/catalog/product/4/6/46939_f.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=550&width=413&canvas=413:550",
                            Address = new Address
                            {
                                Street = "H. Alarcon",
                                City = "Antipolo City",
                                State = "Rizal"
                            },
                            UserId = kenneth.Id
                        },
                        new Slack
                        {
                            Waist = "51.5 CM",
                            Length = "110 CM",
                            Image = "https://www.lowes.com.au/media/catalog/product/4/6/46939_f.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=550&width=413&canvas=413:550",
                            Address = new Address
                            {
                                Street = "H. Alarcon",
                                City = "Antipolo City",
                                State = "Rizal"
                            },
                            UserId = kenneth.Id
                        }
                    });

                    context.SaveChanges();
                }

                if (!context.Uniforms.Any())
                {
                    context.Uniforms.AddRange(new List<Uniform>
                    {
                        new Uniform
                        {
                            Shoulder = "32.5 CM",
                            Sleeve = "40.50 CM",
                            Length = "120 CM",
                            Image = "https://i.ebayimg.com/images/g/W1QAAOSwfXlgMG1I/s-l1200.webp",
                            Address = new Address
                            {
                                Street = "H. Alarcon",
                                City = "Antipolo City",
                                State = "Rizal"
                            },
                            UserId = rhina.Id
                        },
                        new Uniform
                        {
                            Shoulder = "51.5 CM",
                            Sleeve = "40.50 CM",
                            Length = "120 CM",
                            Image = "https://i.ebayimg.com/images/g/W1QAAOSwfXlgMG1I/s-l1200.webp",
                            Address = new Address
                            {
                                Street = "H. Alarcon",
                                City = "Antipolo City",
                                State = "Rizal"
                            },
                            UserId = kenneth.Id
                        }
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
