using System;
using System.Collections.Generic;
using Core.TEF.Commands;
using Core.TEF.Service;
using Core.TEF.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlgarCliSiTef.Controllers
{
    [Route("api/algarTelecom/tef")]
    public class ValuesController : Controller
    {
        private readonly TEFService _tefService = new TEFService();
            
        // GET api/tef
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/tef
        [HttpPost]
        [Route("transacao")]
        public IActionResult Post([FromBody] RechargeCommand rechargeCommands)
        {
            RechargeViewModel rechargeViewModel =  _tefService.RealizaTransacao(rechargeCommands);

            return Ok(rechargeViewModel);
        }

        [HttpPost]
        [Route("finaliza")]
        public IActionResult Finaliza([FromBody] EndPaymentCommand endPaymentCommand)
        {
            _tefService.FinalizaPagamento(endPaymentCommand);

            return Ok();
        }

    }
}
