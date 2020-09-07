﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server;

namespace Server.Data.Migrations.UserConfigurationDb
{
    [DbContext(typeof(UserConfigurationDbContext))]
    partial class UserConfigurationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Server.Entities.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Server.Entities.UserEntity", b =>
                {
                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSaltMd5")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Account");

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("Server.Entities.UserRole", b =>
                {
                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserAccount")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoleName", "UserAccount");

                    b.HasIndex("UserAccount");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Server.Entities.UserRole", b =>
                {
                    b.HasOne("Server.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Entities.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
