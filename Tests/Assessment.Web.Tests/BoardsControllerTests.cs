using System;
using Assessment.Web.Controllers;
using Assessment.Web.Models;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Web.Tests
{
    [TestFixture]
    class BoardsControllerTests
    {

        [Test]
        public void Constructor_CreatesController()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.NotNull(controller);
        }

        // Test case to check if all Boards are retrieved.
        [Test]
        public void GetAllBoards_WithDataInBoardsRepo_ReturnOk()
        {
            var boardRepo = new Mock<IBoardRepository>();
            var controller = new BoardsController(boardRepo.Object);

            var result = controller.GetAllBoards();
            var okResult = result as OkObjectResult;

            boardRepo.Verify(x => x.GetAllBoards(), Times.Once);

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        // Test case to check Not Found (404) status when requested for a Board Item
        [TestCase(99, 404)]
        public void FindBoard_NonExistentId_ReturnNotFound(int input, int expected)
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);

            var result = controller.FindBoard(input);
            var notFound = result as NotFoundObjectResult;

            Assert.IsNotNull(notFound);
            Assert.AreEqual(expected, notFound.StatusCode);
        }

        // Test case to check Bad request(400) status when requested for a board
        [TestCase(0, 400)]
        [TestCase(-1, 400)]
        public void FindBoard_IdLessThanOne_ReturnBadRequest(int input, int expected)
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);

            var result = controller.FindBoard(input);
            var badRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);
            Assert.AreEqual(expected, badRequest.StatusCode);
        }

        // Test case to check success statusif Board item exists
        [TestCase(3, 200)]
        public void FindBoard_ValidId_ReturnOk(int input, int expected)
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.FindBoard(It.IsAny<int>())).Returns(new Board());

            var controller = new BoardsController(boardRepo.Object);

            var result = controller.FindBoard(input);
            var okResult = result as OkObjectResult;

            boardRepo.Verify(x => x.FindBoard(input), Times.Once);

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expected, okResult.StatusCode);
        }

        // Test case to check Success status if Board item is deleted
        [TestCase(3, 200)]
        public void DeleteBoard_ValidId_ReturnOk(int input, int expected)
        {
            var boardRepo = new Mock<IBoardRepository>();
            var controller = new BoardsController(boardRepo.Object);
            boardRepo.Setup(x => x.DeleteBoard(It.IsAny<int>())).Returns(true);

            var result = controller.DeleteBoard(input);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expected, okResult.StatusCode);
        }

        // Test case to get Bad request if trying to delete non-existing board Item
        [TestCase(0, 400)]
        public void DeleteBoard_InvalidId_BadRequest(int input, int expected)
        {
            var boardRepo = new Mock<IBoardRepository>();
            var controller = new BoardsController(boardRepo.Object);

            boardRepo.Setup(x => x.DeleteBoard(It.IsAny<int>())).Returns(true);

            var result = controller.DeleteBoard(input);
            var badRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);
            Assert.AreEqual(expected, badRequest.StatusCode);
        }
    }
}
