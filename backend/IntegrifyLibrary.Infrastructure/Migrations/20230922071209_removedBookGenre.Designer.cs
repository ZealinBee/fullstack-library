﻿// <auto-generated />
using System;
using IntegrifyLibrary.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230922071209_removedBookGenre")]
    partial class removedBookGenre
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "role", new[] { "user", "librarian" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IntegrifyLibrary.Domain.Author", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("AuthorImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("author_image");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("author_name");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("ModifiedAt")
                        .HasColumnType("date")
                        .HasColumnName("modified_at");

                    b.HasKey("AuthorId")
                        .HasName("pk_authors");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Book", b =>
                {
                    b.Property<Guid>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("author_name");

                    b.Property<string>("BookImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("book_image");

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

                    b.HasKey("BookId")
                        .HasName("pk_books");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_books_author_id");

                    b.HasIndex("GenreId")
                        .HasDatabaseName("ix_books_genre_id");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Genre", b =>
                {
                    b.Property<Guid>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("genre_id");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("GenreImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("genre_image");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("genre_name");

                    b.Property<DateOnly>("ModifiedAt")
                        .HasColumnType("date")
                        .HasColumnName("modified_at");

                    b.HasKey("GenreId")
                        .HasName("pk_genres");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_genres_book_id");

                    b.ToTable("genres", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Loan", b =>
                {
                    b.Property<Guid>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("loan_id");

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

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_loans_user_id");

                    b.ToTable("loans", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.LoanDetails", b =>
                {
                    b.Property<Guid>("LoanDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("loan_details_id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<Guid>("LoanId")
                        .HasColumnType("uuid")
                        .HasColumnName("loan_id");

                    b.HasKey("LoanDetailsId")
                        .HasName("pk_loan_details");

                    b.HasIndex("LoanId")
                        .HasDatabaseName("ix_loan_details_loan_id");

                    b.ToTable("loan_details", (string)null);
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

                    b.Property<string>("UserImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_image");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Book", b =>
                {
                    b.HasOne("IntegrifyLibrary.Domain.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_authors_author_id");

                    b.HasOne("IntegrifyLibrary.Domain.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_genres_genre_id");

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Genre", b =>
                {
                    b.HasOne("IntegrifyLibrary.Domain.Book", null)
                        .WithMany("Genres")
                        .HasForeignKey("BookId")
                        .HasConstraintName("fk_genres_books_book_id");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Loan", b =>
                {
                    b.HasOne("IntegrifyLibrary.Domain.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_loans_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.LoanDetails", b =>
                {
                    b.HasOne("IntegrifyLibrary.Domain.Loan", "Loan")
                        .WithMany("LoanDetails")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_loan_details_loans_loan_id");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Book", b =>
                {
                    b.Navigation("Genres");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.Loan", b =>
                {
                    b.Navigation("LoanDetails");
                });

            modelBuilder.Entity("IntegrifyLibrary.Domain.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
