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
    public class RoomService : IRoomService
    {
        public const int MaxIngredientsForPotions = 5;
        private readonly HogwartsContext _context;

        public RoomService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task AddRoom(Room room)
        {
            var newRoom = new Room{Capacity=room.Capacity, Residents=room.Residents};
            await _context.Rooms.AddAsync(newRoom);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(long roomId)
        {
            var foundRoom = await _context.Rooms.FindAsync(roomId);
            return foundRoom;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var allRooms = await _context.Rooms.ToListAsync();
            return allRooms;
        }

        public async Task<bool> UpdateRoom(long id, Room updatedRoom)
        {
            var foundRoom = await _context.Rooms.FindAsync(id);
            if (foundRoom != null)
            {
                foundRoom.ID = updatedRoom.ID;
                foundRoom.Capacity = updatedRoom.Capacity;
                foundRoom.Residents = updatedRoom.Residents;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoom(long id)
        {
            var roomStudents = _context.Students.Where(s => s.Room.ID == id);
            foreach (var roomStud in roomStudents)
            {
                _context.Students.Remove(roomStud);
            }

            var roomToBeDeleted = await _context.Rooms.FindAsync(id);
            if (roomToBeDeleted != null)
            {
                _context.Rooms.Remove(roomToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            var roomsForRatOwners = new List<Room>();
            var allStudents = await _context.Students.Include(s => s.Room).ToListAsync();
            var unsafeIds = new HashSet<long>();
            foreach (var student in allStudents)
            {
                if (student.PetType == PetType.Cat || student.PetType == PetType.Owl)
                {
                    unsafeIds.Add(student.Room.ID);
                }
            }

            var allRooms = await _context.Rooms.ToListAsync();
            foreach (var room in allRooms)
            {
                if (!unsafeIds.Contains(room.ID))
                {
                    foreach (var student in room.Residents)
                    {
                        student.Room = null;
                    }
                    roomsForRatOwners.Add(room);
                }
            }
            return roomsForRatOwners;
        }
    }
}
