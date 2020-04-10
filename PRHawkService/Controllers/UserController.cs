using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PRHawkService.Implementation;
using PRHawkService.Models;
using static PRHawkService.Constants.AppConstants;

namespace PRHawkService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        UserRepositoryProvider _repoProvider;


        public UserController(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _repoProvider = new UserRepositoryProvider(config, clientFactory);

        }

        /// <summary>
        /// List of public repositories along with number of pull requests
        /// </summary>
        /// <param name="request">Page Number and No of Records for pagination</param>
        [Route("{githubusername}")]
        [HttpGet]
        public async Task<BaseResponse<List<RepositoryListItem>>> GetGitUserRepositoriesDetails(String githubusername, [FromQuery]UserRepositoryRequest request)
        {
            BaseResponse<List<RepositoryListItem>> response = await _repoProvider.ListRepositoriesFromGit(githubusername, request);
            return response;
        }
    }
}
