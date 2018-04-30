using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Models
{
  public class PostIt
  {
    private string _postName;
    private DateTime _createdAt;
    private int _postId;
    private string _postInfo;

    public int PostId { get => _postId; set => _postId = value; }
    public string PostName { get => _postName; set => _postName = value; }
    public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
    public string PostInfo { get => _postInfo; set => _postInfo = value; }
  }
}
