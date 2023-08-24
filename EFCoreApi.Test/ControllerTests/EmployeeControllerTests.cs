using AutoMapper;
using EFCoreApi.Controllers;
using EFCoreApi.Data.Configuration;
using EFCoreApi.Models;
using EFCoreApi.Models.Dto;
using EFCoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreApi.Test.ControllerTests
{
    public class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        private Mock<IEmployeeService> _mockEmployeeService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object);
            _mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingConfig())).CreateMapper();
        }

        [Test]
        public async Task GetEmployees_Sucess()
        {
            // Arrange
            var fakeEmployees = new List<EmployeeDto>
            {
                new EmployeeDto()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jhon",
                    LastName = "doe",
                    EmailAddress = "JhonDoe@gmail.com",
                    Age = 25
                },
                new EmployeeDto()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jacob",
                    LastName = "Hilderth",
                    EmailAddress = "JacobHilderth@gmail.com",
                    Age = 30
                }
            };

            _mockEmployeeService.Setup(service => service.GetEmployees()).ReturnsAsync(fakeEmployees);

            // Act
            var result = await _employeeController.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.That(okResult.Value, Is.EqualTo(fakeEmployees));
        }

        [Test]
        public async Task GetById_ValidId_ReturnsOk()
        {
            // Arrange
            Guid validId = Guid.NewGuid();
            var employeeDto = new EmployeeDto { Id = validId, FirstName = "John Doe" };
            _mockEmployeeService.Setup(service => service.GetEmployeeById(validId))
                                .ReturnsAsync(employeeDto);

            // Act
            var result = await _employeeController.GetById(validId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.AreEqual(employeeDto, okResult.Value);
        }

        [Test]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            Guid invalidId = Guid.NewGuid();
            _mockEmployeeService.Setup(service => service.GetEmployeeById(invalidId))
                                .ReturnsAsync((EmployeeDto)null);

            // Act
            var result = await _employeeController.GetById(invalidId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = (NotFoundResult)result;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Post_ValidDto_ReturnsOk()
        {
            // Arrange
            var validDto = new EmployeeDto { FirstName = "John Doe" };
            _mockEmployeeService.Setup(service => service.CreateEmployee(validDto))
                                .ReturnsAsync(validDto); // Assuming the service returns the same DTO

            // Act
            var result = await _employeeController.Post(validDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Post_InvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new EmployeeDto(); // Assuming an invalid DTO without required fields
            _mockEmployeeService.Setup(service => service.CreateEmployee(invalidDto))
                                .ReturnsAsync((EmployeeDto)null);

            // Act
            var result = await _employeeController.Post(invalidDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            var badRequestResult = (BadRequestResult)result;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task Delete_ExistingId_ReturnsOk()
        {
            // Arrange
            Guid existingId = Guid.NewGuid();
            _mockEmployeeService.Setup(service => service.DeleteEmployee(existingId))
                                .ReturnsAsync(true);

            // Act
            var result = await _employeeController.Delete(existingId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task Delete_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            Guid nonExistingId = Guid.NewGuid();
            _mockEmployeeService.Setup(service => service.DeleteEmployee(nonExistingId))
                                .ReturnsAsync(false);

            // Act
            var result = await _employeeController.Delete(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = (NotFoundResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task Put_ValidDto_ReturnsOk()
        {
            // Arrange
            var validDto = new EmployeeDto { FirstName = "Updated John Doe" };
            _employeeController.ModelState.Clear(); 
            _mockEmployeeService.Setup(service => service.UpdateEmployee(validDto))
                                .ReturnsAsync(validDto); 

            // Act
            var result = await _employeeController.Put(validDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task Put_InvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new EmployeeDto(); // Assuming an invalid DTO without required fields
            _mockEmployeeService.Setup(service => service.UpdateEmployee(invalidDto))
                                .ReturnsAsync((EmployeeDto)null);

            // Act
            var result = await _employeeController.Put(invalidDto);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            var badRequestResult = (BadRequestResult)result;
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }


    }
}