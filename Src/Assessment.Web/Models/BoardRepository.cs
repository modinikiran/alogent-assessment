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
        IQueryable<Board> GetAllBoards();
        Board FindBoard(int id);
        bool AddBoard(Board board);
        bool DeleteBoard(int boardId);
    }
    public class BoardRepository : IBoardRepository
    {
        public static List<Board> boards;

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
        public IQueryable<Board> GetAllBoards()
        {
            return boards.AsQueryable();
        }

        // Get single board data based on it's Id
        public Board FindBoard(int id)
        {
            return boards.FirstOrDefault(x => x.BoardId == id);
        }

        // Add a new board to the boards list
        public bool AddBoard(Board board)
        {
            if (FindBoard(board.BoardId) != null) return false;

            boards.Add(board);

            return true;
        }

        // Delete an existing board from the list
        public bool DeleteBoard(int boardId)
        {
           Board board = FindBoard(boardId);

            if (board == null) return false;
            
            return boards.Remove(board);
        }
    }
}
