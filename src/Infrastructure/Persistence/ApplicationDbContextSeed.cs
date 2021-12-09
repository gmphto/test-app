using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context, IExcelReader reader)
    {

        string resourceName = "Infrastructure.SeedData.Test_Accounts.xlsx";
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream != null)
        {
            DataTable data = reader.ReadExcelDocument(stream);

            if (!context.AccountItems.Any()) // Seed accounts, if needed
            {

                var list = 
                data.AsEnumerable()
                    .Select(row => new AccountItem
                 {
                     AccountId = int.Parse(row.Field<string>("AccountId")),
                     FirstName = row.Field<string>("FirstName"),
                     LastName = row.Field<string>("FirstName")
                 }).ToList();

                context.AccountItems.AddRange(list);

                await context.SaveChangesAsync();
            }

        }

    }
}