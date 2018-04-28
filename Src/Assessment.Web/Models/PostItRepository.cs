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
    IQueryable<PostIt> GetAllPostIts();
    PostIt FindPostIt(int id);
    bool AddPostIt(PostIt postIt);
    bool DeletePostIt(PostIt postIt); 
    }

  public class PostItRepository : IPostItRepository
  {
    private List<PostIt> postIts;

    public PostItRepository() => postIts = GetPostItsFromFile();

    // Return deserialized Boards.json data, of type List
    private List<PostIt> GetPostItsFromFile()
    {
      var filePath = Application.Configuration["PostItDataFile"];
      if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

      var json = System.IO.File.ReadAllText(filePath);

      return JsonConvert.DeserializeObject<List<PostIt>>(json);
    }

    // Get all boards with all data from boards List
    public IQueryable<PostIt> GetAllPostIts()
    {
      return postIts.AsQueryable();
    }

    // Get single board data based on it's Id
    public PostIt FindPostIt(int id)
    {
      return postIts.FirstOrDefault(x => x.Id == id);
    }

    // Add a new board to the boards list
    public bool AddPostIt(PostIt postIt)
    {
      if (FindPostIt(postIt.Id) != null) return false;

      postIts.Add(postIt);

      return true;
    }

    // Delete an existing board from the list
    public bool DeletePostIt(PostIt postIt)
    {
      postIt = FindPostIt(postIt.Id);
      if (postIt == null) return false;
      return postIts.Remove(postIt);
    }
  }
}