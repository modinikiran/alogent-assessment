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
      if (postId <= 0) return BadRequest("Please check the Post Item Id");
      var postItem = PostIts.FindPostIt(boardId, postId);
      if(postItem == null) return NotFound("Please check the Post Item Id");
      return Ok(postItem);
    }

// POST /boards/[board-id]/post-its/
    [HttpPost("{boardId}/PostIts")]
    public IActionResult AddPostIt(int boardId, PostIt value)
    {
      if (boardId <= 0 || value.PostId <= 0) return BadRequest("Please check the requested Id");
      PostIts.AddPostIt(boardId, value);
      return Created(new Uri("{boardId}/PostIts", UriKind.Relative), new { PostItem = value.PostId });
    }

    // DELETE api/boards/{id}/postIts/4
    [HttpDelete("{boardId:int}/PostIts/{postId}")]
    public IActionResult DeletePostIt(int boardId, int postId)
    {
      if (postId <= 0) return BadRequest();
      return Ok(PostIts.DeletePostIt(boardId,postId));
    }
  }
}
