﻿// <auto-generated />
using System;
using IntegrifyLibrary.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IntegrifyLibrary.Domain.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("author_name");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("book_name");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uuid")
                        .HasColumnName("genre_id");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("isbn");

                    b.Property<DateOnly>("ModifiedAt")
                        .HasColumnType("date")
                        .HasColumnName("modified_at");

                    b.Property<int>("PageCount")
                        .HasColumnType("integer")
                        .HasColumnName("page_count");

                    b.Property<DateOnly>("PublishedDate")
                        .HasColumnType("date")
                        .HasColumnName("published_date");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Loan", b =>
                {
                    b.Property<Guid>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("loan_id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("DueDate")
                        .HasColumnType("date")
                        .HasColumnName("due_date");

                    b.Property<DateOnly>("LoanDate")
                        .HasColumnType("date")
                        .HasColumnName("loan_date");

                    b.Property<DateOnly>("ModifiedAt")
                        .HasColumnType("date")
                        .HasColumnName("modified_at");

                    b.Property<DateOnly>("ReturnedDate")
                        .HasColumnType("date")
                        .HasColumnName("returned_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("LoanId")
                        .HasName("pk_loans");

                    b.ToTable("loans", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.Property<DateOnly>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
