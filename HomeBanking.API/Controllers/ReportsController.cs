using HomeBanking.API.Application.Models.Queries;
using HomeBanking.API.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeBanking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportQueries _reportQueries;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportQueries reportQueries,
            ILogger<ReportsController> logger)
        {
            _reportQueries = reportQueries;
            _logger = logger;
        }

        /// <summary>
        /// Get family report
        /// </summary>
        /// <param name="request">Report options</param>
        /// <param name="familyid">Family id</param>
        /// <returns></returns>
        [Route("family/{familyid:int}")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ExpenseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExpenseViewModel>>> GetByFamilyAsync(int familyid, ReportRequest request)
        {
            var family = await _reportQueries.ReportByFamilyAsync(familyid, request);

            return Ok(family);
        }

        /// <summary>
        /// Get statistic
        /// </summary>
        /// <param name="request">Statistic options</param>
        /// <param name="familyid">Family id</param>
        /// <returns></returns>
        [Route("family/{familyid:int}/statistic")]
        [HttpPost]
        [ProducesResponseType(typeof(FamilyViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FamilyViewModel>> GetStatisticByFamilyAsync(int familyid, StatisticRequest request)
        {
            var family = await _reportQueries.StatisticByFamilyAsync(familyid, request);

            return Ok(family);
        }

        /// <summary>
        /// Get a report on a person
        /// </summary>
        /// <param name="request">Report options</param>
        /// <param name="userid">User id</param>
        /// <returns></returns>
        [Route("relative/{userid:int}")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ExpenseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExpenseViewModel>>> GetByPersonAsync(int userid, ReportRequest request)
        {
            var person = await _reportQueries.ReportByPersonAsync(userid, request);

            return Ok(person);
        }

        /// <summary>
        /// Get a wallet report
        /// </summary>
        /// <param name="request">Report options</param>
        /// <param name="walletid">Wallet id</param>
        /// <returns></returns>
        [Route("wallet/{walletid:int}")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ExpenseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ExpenseViewModel>>> GetByWalletAsync(int walletid, ReportRequest request)
        {
            var wallet = await _reportQueries.ReportByWalletAsync(walletid, request);

            return Ok(wallet);
        }
    }
}
