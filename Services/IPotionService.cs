using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task AddPotion(Potion potion);
    // Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetAllPotions();
    Task<List<Potion>> GetStudentPotions(long student_id);
    // Task<bool> UpdatePotion(long id, Potion potion);
    // Task<bool> DeletePotion(long id);
    // Task<List<Potion>> GetPotionsForRatOwners();
}