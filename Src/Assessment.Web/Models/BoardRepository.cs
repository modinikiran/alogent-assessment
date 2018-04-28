using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assessment.Web.Models
{
    public interface IBoardRepository
    {
        IQueryable<Board> GetAll();
        Board Find(int id);
        bool Add(Board board);
        bool Delete(Board board);
    }

    public class BoardRepository : IBoardRepository
    {
        private List<Board> boards;

        public BoardRepository() => boards = GetBoardsFromFile();

        // Return deserialized Boards.json data, of type List
        private List<Board> GetBoardsFromFile()
        {
            var filePath = Application.Configuration["BoardsDataFile"];
            if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var json = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Board>>(json);
        }

        // Get all boards with all data from boards List
        public IQueryable<Board> GetAll()
        {
            return boards.AsQueryable();
        }

        // Get single board data based on it's Id
        public Board Find(int id)
        {
            return boards.FirstOrDefault(x => x.Id == id);
        }

        // Add a new board to the boards list
        public bool Add(Board board)
        {
            if (Find(board.Id) != null) return false;

            boards.Add(board);

            return true;
        }

        // Delete an existing board from the list
        public bool Delete(Board board)
        {
            board = Find(board.Id);
            if (board == null) return false;
            return boards.Remove(board);
        }
    }
}
