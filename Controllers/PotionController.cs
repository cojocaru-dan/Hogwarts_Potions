using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _potionService;
        public PotionController(IPotionService potionService)
        {
            _potionService = potionService;
        }

        [HttpGet]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _potionService.GetAllPotions();
        }

        [HttpPost]
        public async Task<IActionResult> AddPotion([FromBody] Potion potion)
        {
            if (potion.ID != 0) return BadRequest("You don't have to specify the ID! Try again!");
            await _potionService.AddPotion(potion);
            return Ok("The potion has been added!");
        }

        [HttpGet("{student_id}")]
        public async Task<List<Potion>> GetStudentPotions(long student_id)
        {
            var studentPotions = await _potionService.GetStudentPotions(student_id);
            return studentPotions;
        }
    }
}