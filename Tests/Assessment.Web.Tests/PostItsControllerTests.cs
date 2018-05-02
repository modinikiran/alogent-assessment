using System;
using Assessment.Web.Controllers;
using Assessment.Web.Models;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Web.Tests
{
    [TestFixture]
    class PostItsControllerTests
    {

        [Test]
        public void Constructor_CreatesController()
        {
            var postItRepo = Mock.Of<IPostItRepository>();
            var controller = new PostItsController(postItRepo);
            Assert.NotNull(controller);
        }

        // Test case to check if all Post Items are retrieved under a Board Item
        [TestCase(2,200)]
        public void GetAllPostIts_WithDataInPostItsRepo_ReturnOk(int boardId, int expected)
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.FindBoard(It.IsAny<int>())).Returns(new Board());

            var postItRepo = new Mock<IPostItRepository>();
            var controller = new PostItsController(postItRepo.Object);

            postItRepo.Setup(x => x.FindPostIt(It.IsAny<int>(), It.IsAny<int>())).Returns(new PostIt());

            var result = controller.GetAllPostIts(boardId);
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult); 
            Assert.AreEqual(expected, okResult.StatusCode);
        }

        // Test case to check Not Found (404) status when requested for a Post Item
        [TestCase(2,10, 404)]
        public void FindPostIt_NonExistentId_ReturnNotFound(int boardId, int postId, int expected)
        {
            var postItRepo = Mock.Of<IPostItRepository>();
            var controller = new PostItsController(postItRepo);

            var result = controller.FindPostIt(boardId, postId);
            var notFound = result as NotFoundObjectResult;

            Assert.IsNotNull(notFound);
            Assert.AreEqual(expected, notFound.StatusCode);
        }

        // Test case to check Bad request(400) status if there's a client error
        [TestCase(0, 2, 400)]
        [TestCase(1, -2, 400)]
        public void FindPostIt_IdLessThanOne_ReturnBadRequest(int boardId, int postId, int expected)
        {
            var postItRepo = Mock.Of<IPostItRepository>();
            var controller = new PostItsController(postItRepo);

            var result = controller.FindPostIt(boardId,postId);
            var badRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);
            Assert.AreEqual(expected, badRequest.StatusCode);
        }

        // Test case to check success status if PostIt item exists in a Board
        [TestCase(1,2,200)]
        public void FindPostIt_ValidId_ReturnOk(int boardId, int postId, int expected)
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.FindBoard(It.IsAny<int>())).Returns(new Board());

            var postItRepo = new Mock<IPostItRepository>();
            var controller = new PostItsController(postItRepo.Object);

            postItRepo.Setup(x => x.FindPostIt(It.IsAny<int>(), It.IsAny<int>())).Returns(new PostIt());

            var result = controller.FindPostIt(boardId, postId);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expected, okResult.StatusCode);     
        }

        // Test case to check Success status if PostIt item is deleted
        [TestCase(1,2, 200)]
        public void DeletePostIt_ValidId_ReturnOk(int boardId, int postId, int expected)
        {
        var postItRepo = new Mock<IPostItRepository>();
        var controller = new PostItsController(postItRepo.Object);
        postItRepo.Setup(x => x.DeletePostIt(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var result = controller.DeletePostIt(boardId, postId);
        var okResult = result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(expected, okResult.StatusCode);
        }

        // Test case to get Bad request if trying to delete non-existing PostIt Item
        [TestCase(3,10, 400)]
        public void DeleteBoard_InvalidId_BadRequest(int boardId, int postId, int expected)
        {
          var postItRepo = new Mock<IPostItRepository>();
          var controller = new PostItsController(postItRepo.Object);

            postItRepo.Setup(x => x.DeletePostIt(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

          var result = controller.DeletePostIt(boardId, postId);
          var badRequest = result as BadRequestObjectResult;

          Assert.IsNotNull(badRequest);
          Assert.AreEqual(expected, badRequest.StatusCode);
        }
    }
}
