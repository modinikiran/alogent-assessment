using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assessment.Web.Models
{
    public interface IPostItRepository
    {
    List<PostIt> GetAllPostIts(int boardId);
    PostIt FindPostIt(int boardId, int postId);
    bool AddPostIt(int boardId, PostIt postIt);
    bool DeletePostIt(int boardId, int postId); 
    }
    
  public class PostItRepository : IPostItRepository
  {
    BoardRepository boards = new BoardRepository();

    // Get all Post Item from the board 
    public List<PostIt> GetAllPostIts(int boardId)
    {
      var board = boards.FindBoard(boardId);
      return board.PostIts;
    }

    // Get single Post Item data based on it's Id
    public PostIt FindPostIt(int boardId, int postId)
    {
      var board = boards.FindBoard(boardId);
      if(board == null) return null;
      return (board.PostIts.FirstOrDefault(pItem => pItem.PostId == postId));
    }

    // Add a new PostItem to the PostItems list
    public bool AddPostIt(int boardId,PostIt postIt)
    {
      var board = boards.FindBoard(boardId);
      var postItem = FindPostIt(boardId, postIt.PostId);
      if (postItem != null) return false;
      board.PostIts.Add(postIt);
      return true;
    }

    // Delete an existing Post Item from the Boards
    public bool DeletePostIt(int boardId, int postId)
    {
      var board = boards.FindBoard(boardId);
      var postItem = FindPostIt(boardId, postId);
      if (postItem == null) return false;
      return board.PostIts.Remove(postItem);
    }
  }
}