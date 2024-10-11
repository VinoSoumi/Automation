using MailAutomation.Services.Abstration;
using Microsoft.AspNetCore.Mvc;

namespace MailAutomation.Presentation.Controllers
{
    [ApiController]
    [Route("api/cost-center")]
    public class CostCenterController : ControllerBase
    {
        private readonly IMailService mailService;
        private readonly IServiceManager serviceMgr;
        public CostCenterController(IMailService mailService, IServiceManager serviceMgr)
        {
            this.mailService = mailService;
            this.serviceMgr = serviceMgr;
        }

        [HttpGet("do-mail-automation")]
        public async Task<IActionResult> ExtractExpensesFromInbox()
        {
            string email = "xxxxxxxxxxxxx@gmail.com";
            string password = "xxxxx";
            var emails = await mailService.DoMailAutomationAsync(email, password);
            return Ok(emails);
        }


        [HttpGet("list-expenses")]
        public async Task<IActionResult> ListExpense()
        {
            var expenses = await serviceMgr.ExpenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("list-expenses-with-mailinfo")]
        public async Task<IActionResult> ListExpenseWithMailInfo()
        {
            var expenses = await serviceMgr.ExpenseService.GetAllWithMailInfoAsync();
            return Ok(expenses);
        }
    }
}
