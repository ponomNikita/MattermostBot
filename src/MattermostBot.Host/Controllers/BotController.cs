using System.Threading.Tasks;
using MattermostBot.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace MattermostBot.Host.Controllers
{
    [Route("[controller]/[action]")]
    public class BotController : Controller
    {
        [HttpPost]
        public async Task<HookResponse> Hook(HookRequest request)
        {
            return new HookResponse
            {
                Text = @"
| Component  | Tests Run   | Tests Failed                                   |
|:-----------|:------------|:-----------------------------------------------|
| Server     | 948         | :white_check_mark: 0                           |
| Web Client | 123         | :warning: [2 (see details)](http://linktologs) |
| iOS Client | 78          | :warning: [3 (see details)](http://linktologs) |
"
            };
        }
    }
}