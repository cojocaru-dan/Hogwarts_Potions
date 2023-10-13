using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task AddPotion(Potion potion);
    Task<List<Potion>> GetAllPotions();
    Task<List<Potion>> GetStudentPotions(long student_id);
    Task AddPotionInPreparing(Potion potionInPreparing);
    Task<List<Potion>> GetAllPotionsIncludingStudents();
    Potion GetPotion(long potion_id);
    void UpdatePotion(long potion_id, Potion dbPotionById);
}