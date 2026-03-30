using Microsoft.AspNetCore.Identity;
using BarracredConsultoria.Models;

namespace BarracredConsultoria.Data
{
    public class AppDbSeed
    {
        public static async Task SeedAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] perfis = { "Admin", "Cooperado" };

            foreach (var perfil in perfis)
            {
                if (!await roleManager.RoleExistsAsync(perfil))
                {
                    await roleManager.CreateAsync(new IdentityRole(perfil));
                }
            }

            string adminEmail = "admin@barracred.com.br";
            string adminUser = "admin";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new Usuario
                {
                    Nome = "Administrador BarraCred",
                    UserName = adminUser,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    DataNascimento = new DateTime(1990, 1, 1),
                    DataAnalise = DateTime.Now,
                    Objetivo = "Gestão do Sistema",
                    Foto = "/img/usuarios/default-admin.png" // Caminho padrão
                };

                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}