using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Models
{
  public class Board
  {
    private int _boardId;
    private DateTime _createdAt;
    private string _name;
    private List<PostIt> _postIts;

    public int BoardId { get => _boardId; set => _boardId = value; }
    public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
    public string Name { get => _name; set => _name = value; }
    public List<PostIt> PostIts { get => _postIts; set => _postIts = value; }

  }
}
