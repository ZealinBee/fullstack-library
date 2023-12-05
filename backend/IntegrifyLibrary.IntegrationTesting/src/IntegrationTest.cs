using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IntegrifyLibrary.Infrastructure;


namespace IntegrifyLibrary.IntegrationTesting;

 public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
   {
       private readonly WebApplicationFactory<Program> _factory;
       public IntegrationTest(WebApplicationFactory<Program> factory)
       {
           _factory = factory;
       }
   }