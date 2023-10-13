using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class PotionService : IPotionService
    {
        private readonly HogwartsContext _context;

        public PotionService(HogwartsContext context)
        {
            _context = context;
        }
        public async Task<List<Potion>> GetAllPotions()
        {
            var allPotions = await _context.Potions.ToListAsync();
            return allPotions;
        }
        public async Task<List<Potion>> GetAllPotionsIncludingStudents()
        {
            var allPotionsIncludingStudents = await _context.Potions.Include(p => p.Student).ToListAsync();
            return allPotionsIncludingStudents;
        }

        public void UpdatePotion(long potion_id, Potion dbPotionById)
        {
            var dbPotionToUpdate = _context.Potions.Where(p => p.ID == potion_id).FirstOrDefault();
            dbPotionToUpdate.Ingredients = dbPotionById.Ingredients;
            _context.SaveChanges();
        }

        public async Task AddPotion(Potion potion)
        {
            var allPotions = await _context.Potions.Include(p => p.Recipe).ThenInclude(r => r.Ingredients).ToListAsync();
            var brewingStatus = "brew";
            if (potion.Ingredients.Count == 5)
            {
                foreach (var dbPotion in allPotions)
                {
                    int commonIngredients = 0;
                    foreach (var newIngredient in potion.Recipe.Ingredients)
                    {
                        var commonIngredient = dbPotion.Recipe.Ingredients.FirstOrDefault(ingr => ingr.Name == newIngredient.Name);
                        if (commonIngredient != null) commonIngredients += 1;
                    }
                    if (commonIngredients == 5) brewingStatus = "replica";
                }
                if (brewingStatus != "replica") brewingStatus = "discovery";
                var newPotion = new Potion 
                { 
                    Name = potion.Name, 
                    Student = potion.Student,
                    Ingredients = potion.Ingredients,
                    BrewingStatus = brewingStatus,
                    Recipe = potion.Recipe
                };
                await _context.Potions.AddAsync(newPotion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Potion>> GetStudentPotions(long student_id)
        {
            var studentPotions = new List<Potion>();
            var allPotions = await _context.Potions.Include(p => p.Student).ToListAsync();
            foreach (var dbPotion in allPotions)
            {
                if (dbPotion.Student.ID == student_id) studentPotions.Add(dbPotion);
            }
            return studentPotions;
        }

        public async Task AddPotionInPreparing(Potion potionInPreparing)
        {
            await _context.Potions.AddAsync(potionInPreparing);
            await _context.SaveChangesAsync();
        }

        public Potion GetPotion(long potion_id)
        {
            var potionById = _context.Potions
                                .Include(p => p.Ingredients)
                                .ToList()
                                .FirstOrDefault(potion => potion.ID == potion_id);
            return potionById;
        }
    }
}