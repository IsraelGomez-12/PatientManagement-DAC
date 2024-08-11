using System;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientManagement.API.Controllers;
using PatientManagement.Application.DTOs;
using PatientManagement.Application.Services;
using PatientManagement.Application.Services.AuthenticateServices;
using PatientManagement.Application.Services.PatientServices;
using PatientManagement.Test.Fixtures;

namespace PatientManagement.Test
{
    public class AuthenticateControllerTest
    {
        private readonly Mock<IAuthenticateService> _mockAuthService;
        private readonly AuthenticationController _controller;
        private readonly LoginDTO _loginMock;

        public AuthenticateControllerTest()
        {
            _mockAuthService = new Mock<IAuthenticateService>();
            _controller = new AuthenticationController(_mockAuthService.Object);
            _loginMock = LoginFixture.GetLoginMock();

        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenTokenIsNull()
        {
            // Arrange
            _mockAuthService.Setup(service => service.Login(_loginMock)).ReturnsAsync(new TokenDTO { Token = null });

            // Act
            var result = await _controller.Login(_loginMock);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Incorrect Credentials");
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenTokenIsValid()
        {
            // Arrange
            
            var tokenDto = new TokenDTO { Token = "some-token" };
            _mockAuthService.Setup(service => service.Login(_loginMock)).ReturnsAsync(tokenDto);

            // Act
            var result = await _controller.Login(_loginMock);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(tokenDto);
        }

        [Fact]
        public async Task Login_OnException_Returns500()
        {
            // Arrange

            _mockAuthService.Setup(service => service.Login(_loginMock)).ThrowsAsync(new Exception("Test"));

            // Act
            var result = await _controller.Login(_loginMock);

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

    }
}

