using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.IntegrationTesting.Controller;
public class BookControllerTest {
    [Fact]
    public void GetBooks_ListAllBooksFromDb_Successfully() {
        DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
        optionsBuilder.UseInMemoryDatabase("GetBooks_ListAllBooksFromDb_Successfully");
    }
}

