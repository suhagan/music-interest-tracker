using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APITest
{
  [TestClass]
  public class IDbRepositoryTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      // Arrange
      DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

      ApplicationContext context = new ApplicationContext(options);
      IdbRepository dbRepository = new DbRepository(context);

      // Act
      dbRepository.AddUserToDb(new User { Username = "TestUser" });
      var result = dbRepository.CheckUserExists("TestUser").Result;

      // Assert
      Assert.AreEqual("TestUser", result.Username);
    }
  }
}