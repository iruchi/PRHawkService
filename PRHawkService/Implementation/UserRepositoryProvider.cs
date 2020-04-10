using System;
using Newtonsoft.Json;
using PRHawkService.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using static PRHawkService.Constants.AppConstants;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace PRHawkService.Implementation
{
    public class UserRepositoryProvider
    {
        private readonly IConfiguration Configuration;
        private IHttpClientFactory _httpClientFactory;

        public UserRepositoryProvider(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            Configuration = config;
            _httpClientFactory = httpClientFactory;
        }

        public UserRepositoryProvider()
        {
        }

        /// <summary>
        /// Get List of repositories and number of pull request for user in git
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of Files and Folder</returns>

        public async Task<BaseResponse<List<RepositoryListItem>>> ListRepositoriesFromGit(String userName, UserRepositoryRequest request)
        {
            BaseResponse<List<RepositoryListItem>> response = new BaseResponse<List<RepositoryListItem>>();
            int noOfPull = 0;
            if (!String.IsNullOrEmpty(userName))

            {
                // Api to get all repositories
                string apiRepoListUrl = Configuration["GitRepoUrl"] + userName.Trim() + "/repos";

                var repoListResponse = await MakeGitRepoRequestAsync<List<RepositoryListResponseItem>>(apiRepoListUrl);
                List<RepositoryListItem> repoList = new List<RepositoryListItem>();

                if (repoListResponse != null)
                {
                    List<RepositoryListResponseItem> result = repoListResponse;
                    foreach (RepositoryListResponseItem repo in result)
                    {
                        if (!repo.full_name.Contains(".github"))
                        {
                            // Api to get number of open pull request for each repository
                            String apiRepoPullReqUrl = Configuration["GitPullReq"] + repo.full_name + "%20is:pr%20is:" + StateOfPull.open + "&per_page=1";

                            await MakeGitPullRequestAsync<PullRequestResponse>(apiRepoPullReqUrl).ContinueWith(t =>
                             {
                                 PullRequestResponse repoPullRequest = t.Result;
                                 if (repoPullRequest != null)
                                 {
                                     noOfPull = Convert.ToInt32(repoPullRequest.total_count);
                                     repoList.Add(new RepositoryListItem() { RepoName = repo.full_name, RepoLink = repo.html_url, PullRequestNum = noOfPull });
                                 }

                             });
                        }
                    }

                    // Logic to sort the repository list by number of pull request
                    repoList = repoList.OrderByDescending(x => x.PullRequestNum).ToList();

                    // Logic to implement pagination
                    int size = request.Size;

                    // Since the pageNumber provided will no be zero-indexed
                    int pageNum = request.PageNum - 1;

                    int totalCount = repoList.Count;
                    if (size < totalCount && pageNum >= 0 && pageNum < totalCount)
                    {
                        List<RepositoryListItem> tempList = new List<RepositoryListItem>();
                        int skipTo = pageNum * size;
                        tempList = repoList.Skip(skipTo).Take(size).ToList();
                        repoList = new List<RepositoryListItem>(tempList.AsEnumerable());
                    }
                   
                    response.Data= repoList;
                    response.Code = StatusCode.OK;
                    response.Message = StatusMessage.OK;
                }
                else
                {
                    response.Data = null;
                    response.Code = StatusCode.OK;
                    response.Message = StatusMessage.NoRecordsFound;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
            return response;
        }

        public async Task<List<RepositoryListResponseItem>> MakeGitRepoRequestAsync<T>(string url)
        {
            List<RepositoryListResponseItem> result = new List<RepositoryListResponseItem>();
            String webResponse = await CreateResponseAsync(url);
            if (webResponse != null)
            {
                result = JsonConvert.DeserializeObject<List<RepositoryListResponseItem>>(webResponse);
            }
            return result;

        }

        public async Task<PullRequestResponse> MakeGitPullRequestAsync<T>(string url)
        {
            PullRequestResponse result = new PullRequestResponse();
            String webResponse = await CreateResponseAsync(url);
            if (webResponse != null)
            {
                result = JsonConvert.DeserializeObject<PullRequestResponse>(webResponse);
            }
            return result;
        }

        public async Task<string> CreateResponseAsync(string url)
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpFactoryTesting");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string apiResponse = null;

            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                apiResponse = await response.Content.ReadAsStringAsync();
            }

            return apiResponse;
        }
    }
}
