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
        public IEnumerable<Board> GetAll() => boards.GetAll();

    // GET: api/boards/2
    [HttpGet("{id}")]
        public Board Find(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Board ID must be greater than zero.");

            return boards.Find(id);
        }

    // PUT api/boards/3
    [HttpPost]
        public bool Add(Board value)
        {
            // add null check
            return boards.Add(value);
        }

    // DELETE api/boards/4
    [HttpDelete]
        public bool Delete(Board value)
        {
            return boards.Delete(value);
        }   
    }
}
