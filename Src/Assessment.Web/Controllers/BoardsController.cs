using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assessment.Web.Models;
using Newtonsoft.Json;

namespace Assessment.Web.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        public IBoardRepository boards;

        public BoardsController(IBoardRepository boards) => this.boards = boards;

    // GET: api/boards
    [HttpGet]
        public IActionResult GetAllBoards()
        {
            return Ok(value: boards.GetAllBoards());        
        }

    // GET: api/boards/2
    [HttpGet("{boardId}")]
        public IActionResult FindBoard(int boardId)
        {
            if(boardId <= 0) return BadRequest(new {BoardId = boardId, Status = "Please provide valid Board Id" });

            var board = boards.FindBoard(boardId);

            if (board == null) return NotFound(new {BoardId = boardId, Status = "Requested Board Item not found" });
            return Ok(new {Board = board});
        }

    // Post api/boards/
    [HttpPost]
        public IActionResult AddBoard(Board value)
        {
            if (value.BoardId <= 0) return BadRequest(new { BoardId = value.BoardId, Status = "Please provide valid Board Id" });

            bool created = boards.AddBoard(board: value);

            if(created) return Created(uri: new Uri(uriString: "api/boards", uriKind: UriKind.Relative), value: new { value.BoardId });
            else return BadRequest(new { BoardId = value.BoardId, Status = "Board Item already exists" });
        }

    // DELETE api/boards/4
    [HttpDelete("{boardId}")]
        public IActionResult DeleteBoard(int boardId)
        {
            if (boardId <= 0) return BadRequest(new { BoardId = boardId, Status = "Please provide valid Board Id" });

            bool boardDeleted = boards.DeleteBoard(boardId);
            
            if(boardDeleted) return Ok(new {BoardId = boardId, Status = "Board Item deleted" });
            else return BadRequest(new {BoardId = boardId, status = "Board Item does not exist" });
        }   
    }
}
