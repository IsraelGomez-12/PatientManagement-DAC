using System;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientManagement.API.Controllers;
using PatientManagement.Application.DTOs;
using PatientManagement.Application.Services;
using PatientManagement.Application.Services.PatientServices;
using PatientManagement.Domain.Models;
using PatientManagement.Test.Fixtures;

namespace PatientManagement.Test
{
	public class PatientControllerTest
	{
		private readonly Mock<IPatientService> _mockPatientService;
        private readonly Mock<IAesEncryptionService> _mockAesEncryptionService;
        private readonly PatientController _controller;
        private readonly List<PatientDTO> _patientsMock;

        public PatientControllerTest()
		{
			_mockAesEncryptionService = new Mock<IAesEncryptionService>();
			_mockPatientService = new Mock<IPatientService>();
			_controller = new PatientController(_mockPatientService.Object, _mockAesEncryptionService.Object);
            _patientsMock = PatientFixture.GetPatientMock().ToList();

        }


		[Fact]
		public async Task GetPatients_ReturnsOkResult_WithListOfPatients()
		{
			//Arrange
			_mockPatientService.Setup(x => x.GetPatientsAsync()).ReturnsAsync(_patientsMock);

			//Act
			var result = await _controller.GetPatients();

			//Assert
			result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(_patientsMock);
        }

        [Fact]
        public async Task GetPatient_ReturnsNotFoundResult_WhenPatientDoesNotExist()
        {
            // Arrange
            int patientId = 1;
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(patientId)).ReturnsAsync((PatientDTO)null);

            // Act
            var result = await _controller.GetPatient(patientId);

			// Assert
			result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be($"Patient with ID:{patientId} was not found");
        }

        [Fact]
        public async Task GetPatient_ReturnsOkResult_WhenPatientExist()
        {
            // Arrange
            int patientId = 1;
            var patientFound = _patientsMock[0];

            _mockPatientService.Setup(service => service.GetPatientByIdAsync(patientId)).ReturnsAsync((PatientDTO)patientFound);
            _mockAesEncryptionService
            .Setup(aes => aes.Decrypt(patientFound.SocialSecurityNumber))
            .Returns("787235235NJ");

            var result = await _controller.GetPatient(patientId);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(patientFound);
        }

        [Fact]
        public async Task DeletePatient_ReturnsOkResult_WhenPatientExist()
        {
            // Arrange
            int patientId = 1;
            var patientFound = _patientsMock[0];
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(patientId)).ReturnsAsync((PatientDTO)patientFound);
            _mockPatientService.Setup(service => service.DeletePatientAsync(patientId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePatient(patientId);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be($"Patient successfully deleted");
        }
        [Fact]
        public async Task DeletePatiene_ReturnsNotFoundResult_WhenPatienDoesNotExist()
        {
            // Arrange
            int patientId = 5;
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(patientId)).ReturnsAsync((PatientDTO)null);

            // Act
            var result = await _controller.DeletePatient(patientId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be($"Patient with ID:{patientId} was not found");
        }
        [Fact]
        public async Task PostPatient_ReturnsCreatedAtActionResult_WhenPatientIsValid()
        {
            // Arrange
            var patientDto = _patientsMock[1];
            _mockAesEncryptionService
              .Setup(aes => aes.Encrypt(patientDto.SocialSecurityNumber))
              .Returns("PUQXYKN/SQZwtOWx7XwIrw==");
            _mockPatientService.Setup(service => service.AddPatientAsync(patientDto)).Returns(Task.CompletedTask);
           
            // Act
            var result = await _controller.PostPatient(patientDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>().Which.Value.Should().Be(patientDto);
        }
        [Fact]
        public async Task PostPatient_ReturnsBadRequestActionResult_WhenPatientIsNotValid()
        {
            // Arrange
            var patientDto = _patientsMock[2];
            _controller.ModelState.AddModelError("Email", "Invalid email address.");

            // Act
            var result = await _controller.PostPatient(patientDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PutPatient_ReturnsNotFoundObjectResult_WhenPatientDoesNotExist()
        {
            // Arrange
            var patientDto = _patientsMock[2];
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(3)).ReturnsAsync((PatientDTO)null);

            // Act
            var result = await _controller.PutPatient(3,patientDto);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be($"Patient with ID:{3} was not found");
        }
    

        [Fact]
        public async Task PutPatient_ReturnsOkObjectResult_WhenPatientExist()
        {
            // Arrange
            var patientDto = _patientsMock[0];
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(1)).ReturnsAsync((PatientDTO)patientDto);
            _mockAesEncryptionService
                 .Setup(aes => aes.Encrypt(patientDto.SocialSecurityNumber))
                 .Returns("PUQXYKN/SQZwtOWx7XwIrw==");
            _mockPatientService.Setup(service => service.UpdatePatientAsync(patientDto)).Returns(Task.CompletedTask);
          

            // Act
            var result = await _controller.PutPatient(1, patientDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Patient successfully updated");
        }

        [Fact]
        public async Task PostPatient_ReturnsBadRequestActionResult_WhenPatientAndIdIsNotEqual()
        {
            // Arrange
            var patientDto = _patientsMock[0];

            // Act
            var result = await _controller.PutPatient(5,patientDto);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }


        [Fact]
        public async Task PostPatient_OnException_Returns500()
        {
            // Arrange
            var patientDto = _patientsMock[0];
            _mockPatientService.Setup(service => service.AddPatientAsync(patientDto)).ThrowsAsync(new Exception("Test"));

            // Act
            var result = await _controller.PostPatient(patientDto);

            //Assert
            result.Should().BeOfType<ObjectResult>().
                Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task PutPatient_OnException_Returns500()
        {
            // Arrange
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test"));

            // Act
            var result = await _controller.PutPatient(It.IsAny<int>(),It.IsAny<PatientDTO>());

            //Assert
            result.Should().BeOfType<ObjectResult>().
                Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task DeletePatient_OnException_Returns500()
        {
            // Arrange
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test"));

            // Act
            var result = await _controller.DeletePatient(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>().
                Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task GetPatientById_OnException_Returns500()
        {
            // Arrange
            var patientDto = _patientsMock[0];
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test"));

            // Act
            var result = await _controller.GetPatient(1);

            //Assert
            result.Should().BeOfType<ObjectResult>().
                Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task GetPatients_OnException_Returns500()
        {
            // Arrange
            var patientDto = _patientsMock[0];
            _mockPatientService.Setup(service => service.GetPatientsAsync()).ThrowsAsync(new Exception("Test"));

            // Act
            var result = await _controller.GetPatients();

            //Assert
            result.Should().BeOfType<ObjectResult>().
                Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}

