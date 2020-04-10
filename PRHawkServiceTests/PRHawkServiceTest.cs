using System;
using PRHawkService.Models;
using Xunit;
using PRHawkService.Implementation;

namespace PRHawkServiceTests
{
    public class PRHawkServiceTest
    {
        [Fact]
        public async System.Threading.Tasks.Task ShouldReturnWhenUserNameNull()
        {
            UserRepositoryRequest request = new UserRepositoryRequest();
            request.PageNum = 1;
            request.Size = 10;

            var test = new UserRepositoryProvider();
            await Assert.ThrowsAsync<ArgumentNullException>(()=>test.ListRepositoriesFromGit(null, request));
        }
    }
}
