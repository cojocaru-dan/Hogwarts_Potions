using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Data;
public static class DbInitializer
{
    public static void Initialize(HogwartsContext context)
    {
        context.Database.EnsureCreated();
        // context.Database.ExecuteSqlRaw("TRUNCATE TABLE Students");
        // context.Database.ExecuteSqlRaw("DELETE FROM Rooms");
        // Look for any students.
        /* if (context.Students.Any())
        {
            return;   // DB has been seeded
        }

        var students = new Student[]
            {
            new Student{Name="Alexander",HouseType=HouseType.Gryffindor, PetType=PetType.Rat, Room=new Room{Capacity=2, Residents=new HashSet<Student>()}},
            new Student{Name="Lilian",HouseType=HouseType.Gryffindor, PetType=PetType.Rat, Room=new Room{Capacity=2, Residents=new HashSet<Student>()}},
            };
        foreach (Student s in students)
        {
            context.Students.Add(s);
        }
        context.SaveChanges(); */

        /* var rooms = new Room[]
        {
        new Room{Capacity=2, Residents=new HashSet<Student>()},
        new Room{Capacity=2, Residents=new HashSet<Student>()},
        };
        foreach (Room r in rooms)
        {
            context.Rooms.Add(r);
        }
        context.SaveChanges(); */
    }
}