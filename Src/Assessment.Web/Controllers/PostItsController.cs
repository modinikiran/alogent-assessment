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
  [Route("api/boards")]
  public class PostItsController : Controller
  {
    private IPostItRepository postIts;

    public IPostItRepository PostIts { get => postIts; set => postIts = value; }

    public PostItsController(IPostItRepository postIts) => this.PostIts = postIts ?? throw new ArgumentNullException(nameof(postIts));

    // GET: api/boards/{id}/postIts/
    [HttpGet("{boardId}/PostIts")]
    public IActionResult GetAllPostIts(int boardId)
    {
       var postItems = PostIts.GetAllPostIts(boardId);

       if(postItems == null) return NotFound();
       
       return Ok(postItems);
    } 

    // GET: api/boards/{id}/postIts/2
    [HttpGet("{boardId}/PostIts/{postId:int}")]
    public IActionResult FindPostIt(int boardId, int postId)
    {
      if (postId <= 0 || boardId <= 0) return BadRequest(new { PostItemId = postId, Status = "Please provide valid Post Item Id" });

      var postItem = PostIts.FindPostIt(boardId, postId);

      if(postItem == null) return NotFound(new { PostItemId = postId, Status = "Requested PostIt Item not found" });
      return Ok(new { PostItem = postItem });
    }

// POST /boards/[board-id]/post-its/
    [HttpPost("{boardId}/PostIts")]
    public IActionResult AddPostIt(int boardId, PostIt value)
    {
      if (boardId <= 0 || value.PostId <= 0) return BadRequest(new { BoardId = boardId, PostItemId = value.PostId,Status = "Please provide valid Post Item Id" });

      PostIts.AddPostIt(boardId, value);

      return Created(new Uri("{boardId}/PostIts", UriKind.Relative), new { PostItem = value.PostId });
    }

    // DELETE api/boards/{id}/postIts/4
    [HttpDelete("{boardId:int}/PostIts/{postId}")]
    public IActionResult DeletePostIt(int boardId, int postId)
    {
      if (postId <= 0) return BadRequest(new { PostItemId = postId, Status = "Please provide valid Post Item Id" });

      bool postDeleted = PostIts.DeletePostIt(boardId, postId);

      if (postDeleted) return Ok(new { PostItemId = postId, Status = "PostIt Item deleted" });
      else return BadRequest(new { PostItemId = postId, status = "Board Item does not exist" });
    }
  }
}
