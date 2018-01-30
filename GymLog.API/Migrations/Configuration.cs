namespace GymLog.API.Migrations
{
    using GymLog.API.Entities;
    using GymLog.API.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GymLogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(GymLogContext context)
        {
            //AddUsersAndRolesFailured(context);

            AddUsersAndRoles(context);
            AddGymData(context);
        }

        private static void AddUsersAndRoles(GymLogContext context)
        {
            var adminRole = new IdentityRole { Name = "admin", Id = Guid.NewGuid().ToString() };
            var userRole = new IdentityRole { Name = "user", Id = Guid.NewGuid().ToString() };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);

            var hasher = new PasswordHasher();

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@admin.com",
                PasswordHash = hasher.HashPassword("admin"),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });

            context.Users.Add(admin);

            var user = new User
            {
                UserName = "user",
                Email = "user@user.com",
                PasswordHash = hasher.HashPassword("user"),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            context.Users.Add(user);
        }

        private void AddGymData(GymLogContext context)
        {
            var muscles = new List<Muscle>{
                new Muscle() { Name = "Triceps" },
                new Muscle() { Name = "Klatka piersiowa" },
                new Muscle() { Name = "Barki" },
                new Muscle() { Name = "Biceps" },
                new Muscle() { Name = "Brzuch" },
                new Muscle() { Name = "Plecy" },
                new Muscle() { Name = "Przedramiê" },
                new Muscle() { Name = "Uda" },
                new Muscle() { Name = "Poœladki" },
                new Muscle() { Name = "£ydki" },
                new Muscle() { Name = "Cardio" }
                };
            muscles.ForEach(m => context.Muscles.AddOrUpdate(x => x.Name, m));
            context.SaveChanges();



            var equipments = new List<Equipment> {
                new Equipment() { Name = "Hantel" },
                new Equipment() { Name = "£awka prosta" },
                new Equipment() { Name = "Wyci¹g" },
                new Equipment() { Name = "Suwnica" },
                new Equipment() { Name = "Sztanga prosta" },
                new Equipment() { Name = "Sztanga giêta" },
                new Equipment() { Name = "Dr¹¿ek" }
                };
            equipments.ForEach(m => context.Equipments.AddOrUpdate(x => x.Name, m));
            context.SaveChanges();

            //var klata = new List<Muscle>
            //{
            //    muscles.Where(m => m.Name == "Klatka piersiowa").FirstOrDefault(),
            //    muscles.Where(m => m.Name == "Barki").FirstOrDefault()
            //};
            //var przysiady = new List<Muscle>
            //{
            //    muscles.Where(m => m.Name == "Udo").FirstOrDefault(),
            //    muscles.Where(m => m.Name == "Poœladki").FirstOrDefault(),
            //    muscles.Where(m => m.Name == "£ydki").FirstOrDefault()
            //};


            var exercises = new List<Exercise>
            {
                new Exercise() { Name = "Wyciskanie sztangi le¿¹c", Description = "Bla bla bla", Muscle = muscles.Single(m=>m.Name=="Klatka piersiowa") ,Equipment = equipments.Single(m=>m.Name=="£awka prosta")},
                new Exercise() { Name = "Przysiady", Description = "Bla bla bla", Muscle = muscles.Single(m=>m.Name=="Uda"), Equipment = equipments.Single(m=>m.Name=="Suwnica")},
                new Exercise() { Name = "Podci¹ganie", Description = "Bla bla bla", Muscle = muscles.Single(m=>m.Name=="Plecy"), Equipment = equipments.Single(m=>m.Name=="Dr¹¿ek")},
            };
            exercises.ForEach(m => context.Exercises.AddOrUpdate(x => x.Name, m));
            context.SaveChanges();



            var workouts = new List<Workout> {
                new Workout(){ Id = 1, Sets = 5, Reps = 5, Weight = 70, UserId = context.Users.FirstOrDefault().Id, Exercise= exercises.Single(m=>m.Name=="Wyciskanie sztangi le¿¹c") },
                new Workout(){ Id = 2, Sets = 5, Reps = 4, Weight = 85, UserId = context.Users.FirstOrDefault().Id, Exercise= exercises.Single(m=>m.Name=="Przysiady") },
                new Workout(){ Id = 3, Sets = 3, Reps = 14, Weight = 90, UserId = context.Users.FirstOrDefault().Id, Exercise= exercises.Single(m=>m.Name=="Wyciskanie sztangi le¿¹c") },
                new Workout(){ Id = 4, Sets = 5, Reps = 5, Weight = 5, UserId = context.Users.FirstOrDefault().Id, Exercise= exercises.Single(m=>m.Name=="Podci¹ganie") },
                new Workout(){ Id = 5, Sets = 5, Reps = 18, Weight = 90, UserId = context.Users.FirstOrDefault().Id, Exercise= exercises.Single(m=>m.Name=="Wyciskanie sztangi le¿¹c") },
                };
            workouts.ForEach(m => context.Workouts.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();


            var daylogs = new List<Daylog> {
                new Daylog(){Id = 1, Date = DateTime.Today, UserId = context.Users.FirstOrDefault().Id, Workouts = new List<Workout>() },
                new Daylog(){Id = 2, Date = DateTime.Today, UserId = context.Users.FirstOrDefault().Id, Workouts = new List<Workout>() }
            };
            daylogs.ForEach(s => context.Daylogs.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var workoutList = context.Workouts.ToList();
            var daylogList = context.Daylogs.ToList();

            AddOrUpdateDayLog(context, workoutList[0].Id, daylogList[0].Id);
            AddOrUpdateDayLog(context, workoutList[1].Id, daylogList[0].Id);
            AddOrUpdateDayLog(context, workoutList[2].Id, daylogList[0].Id);
            AddOrUpdateDayLog(context, workoutList[3].Id, daylogList[0].Id);
            AddOrUpdateDayLog(context, workoutList[4].Id, daylogList[0].Id);
            AddOrUpdateDayLog(context, workoutList[1].Id, daylogList[1].Id);
            AddOrUpdateDayLog(context, workoutList[2].Id, daylogList[1].Id);
            AddOrUpdateDayLog(context, workoutList[3].Id, daylogList[1].Id);

            context.SaveChanges();
        }

        void AddOrUpdateDayLog(GymLogContext context, int workoutId, int daylogId)
        {
            var crs = context.Workouts.SingleOrDefault(c => c.Id == workoutId);
            var inst = crs.Daylogs.SingleOrDefault(i => i.Id == daylogId);
            if (inst == null)
                crs.Daylogs.Add(context.Daylogs.Single(i => i.Id == daylogId));
        }

        private static void AddUsersAndRolesFailured(GymLogContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var roleAdmin = new IdentityRole { Name = "admin" };
                var roleUser = new IdentityRole { Name = "user" };

                manager.Create(roleAdmin);
                manager.Create(roleUser);
            }

            if (!context.Users.Any())
            {
                var userStore = new UserStore<User>(context);
                var userManager = new ApplicationUserManager(userStore);

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };


                userManager.Create(admin, "admin1");
                userManager.AddToRole(admin.Id, "admin");

                var user = new User
                {
                    UserName = "user",
                    Email = "user@user.com",
                    EmailConfirmed = true
                };

                userManager.Create(user, "Uzytkownik1");
                userManager.AddToRole(user.Id, "user");
            }
        }
    }
}
