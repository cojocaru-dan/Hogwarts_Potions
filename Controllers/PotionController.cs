using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
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

        [HttpPost("brew")]
        public async Task<IActionResult> UpdatePotionInPreparingWithStudentAndStatus([FromBody] Potion potion)
        {
            Potion potionInPreparing = new()
            {
                Student = potion.Student,
                BrewingStatus = potion.BrewingStatus
            };
            await _potionService.AddPotionInPreparing(potionInPreparing);
            var allPotions = await _potionService.GetAllPotionsIncludingStudents();
            Potion lastAddedPotion = allPotions[^1];
            System.Console.WriteLine(lastAddedPotion.ID);
            System.Console.WriteLine(lastAddedPotion.Student.ID);
            return Ok(lastAddedPotion);
        }

        [HttpPut("{potion_id}/add")]
        public IActionResult UpdatePotionInPreparingWithOneIngredient([FromBody] Ingredient ingredientToBeAdded, int potion_id)
        {
            var dbPotionById = _potionService.GetPotion(potion_id);

            if (dbPotionById == null) return NotFound();
            if (dbPotionById.Ingredients.Contains(ingredientToBeAdded)) return Ok("This ingredient is already present!");
            if (dbPotionById.Ingredients.Count >= 5) return Ok("This potion has already 5 ingredients!");

            dbPotionById.Ingredients.Add(ingredientToBeAdded);
            _potionService.UpdatePotion(potion_id, dbPotionById);
            return Ok(dbPotionById);
        }
    }
}