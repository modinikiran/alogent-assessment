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
        return Ok(boards.GetAllBoards());
        }

    // GET: api/boards/2
    [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            if(id <= 0) return BadRequest("Please check the Board Id");

            var board = boards.FindBoard(id);
            if (board == null) return NotFound("Request failed, please check Board Id");
            return Ok(board);
        }

    // Post api/boards/3
    [HttpPost]
        public IActionResult Add(Board value)
        {
            boards.AddBoard(value);
            return Created(new Uri("api/boards", UriKind.Relative), new{BoardId = value.BoardId});
            // return Created(boards.AddBoard(value));
        }

    // DELETE api/boards/4
    [HttpDelete]
        public IActionResult Delete(Board value)
        {
            if (value.BoardId <= 0) return BadRequest();
            return Ok(boards.DeleteBoard(value));
        }   
    }
}
