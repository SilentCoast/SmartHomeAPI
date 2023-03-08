using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAPI.Models;

namespace SmartHomeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly SmartHomeContext db;
        public RoomsController(SmartHomeContext db)
        {
            this.db = db;
        }
        [HttpPost]
        public int? AddRoom([FromHeader] string Name, [FromHeader] string Type, [FromHeader] string token)
        {
            int userId = Convert.ToInt32(token);
            Room  room = new Room()
            {
                Name = Name,
                Type = (from p in db.RoomTypes where p.Name==Type select p).First(),
                UserId= userId,
                User = (from p in db.Users where p.Id==userId select p).First()
            };
            if(room.Type!=null)
            {
                room.TypeId = room.Type.Id;
            }
            db.Rooms.Add(room);
            db.SaveChanges();

           
            return room.Id;
        }
        [HttpGet("{token}")]
        public List<RoomDTO> GetRooms([FromRoute] string token)
        {
            List<RoomDTO> roomDTOs= new List<RoomDTO>();
            var rooms = (from p in db.Rooms where p.UserId==Convert.ToInt32(token) select p).ToList();
            foreach(var room in rooms)
            {
                string Type = (from p in db.RoomTypes where p.Id==room.TypeId select p.Name).First();
                RoomDTO roomDTO = new RoomDTO();
                roomDTO.Id = room.Id;
                roomDTO.Name = room.Name;
                roomDTO.Type = Type;
                
                roomDTOs.Add(roomDTO);
            }
            return roomDTOs;
        }
    }
}
