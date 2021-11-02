using Issuing.Application;
using Issuing.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public ActionResult<Card> Create(Card cardCreateRequest)
        {
            _cardService.Create(cardCreateRequest);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Card>> ListAll()
        {
            return _cardService.GetAll().ToList();
        }
    }
}
