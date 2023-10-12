using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomService.GetAllRooms();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            if (room.ID != 0) return BadRequest("You don't have to specify the ID! Try again!");
            await _roomService.AddRoom(room);
            return Ok("The room has been added!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(long id)
        {
            var room = await _roomService.GetRoom(id);
            if (room == null) return NotFound();
            return room;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
            if (id != updatedRoom.ID) return BadRequest("The id from body must be equal to the id from path!");
            bool successfullyUpdated = await _roomService.UpdateRoom(id, updatedRoom);
            if (successfullyUpdated) return Ok("The room has been updated!");
            return Ok($"The room with id {id} was not found in the database!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomById(long id)
        {
            bool successfullyDeleted = await _roomService.DeleteRoom(id);
            if (successfullyDeleted) return Ok($"The room with id {id} has been deleted!");
            return Ok($"The room with id {id} was not found in the database!");
        }

        [HttpGet("rat-owners")]
        public async Task<ActionResult<List<Room>>> GetRoomsForRatOwners()
        {
            var roomsForRatOwners = await _roomService.GetRoomsForRatOwners();
            return roomsForRatOwners;
        }
    }
}
