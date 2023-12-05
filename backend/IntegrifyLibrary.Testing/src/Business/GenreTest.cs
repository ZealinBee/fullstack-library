using AutoMapper;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Domain;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrifyLibrary.Testing.Business;

// These tests covers not only Genre, but also Author, since the create one method is the same for both Genre and Author

public class GenreTest {

    [Fact]
    public async Task CreateOne_ShouldCreateNewGenre_Successfully() {
        var mockRepo = new Mock<IGenreRepo>();
        var mockMapper = new Mock<IMapper>();
        var service = new GenreService(mockRepo.Object, mockMapper.Object);
        var dto = new CreateGenreDto {
            GenreName = "Fantasy"
        };
        var genre = new Genre {
            GenreName = dto.GenreName
        };
        mockRepo.Setup(repo => repo.CreateOne(It.IsAny<Genre>())).ReturnsAsync(genre);

        var result = await service.CreateOne(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.GenreName, result.GenreName);
    }
    
    [Fact]
    public async Task CreateOne_IfNull_ThrowError() {
        var mockRepo = new Mock<IGenreRepo>();
        var mockMapper = new Mock<IMapper>();
        var service = new GenreService(mockRepo.Object, mockMapper.Object);

        Func<Task> act = async () => await service.CreateOne(null);

        await Assert.ThrowsAsync<ArgumentNullException>(act);
    }

    [Fact]
    public async Task CreateOne_IfExists_ThrowError() {
        var mockRepo = new Mock<IGenreRepo>();
        var mockMapper = new Mock<IMapper>();
        var service = new GenreService(mockRepo.Object, mockMapper.Object);
        var dto = new CreateGenreDto {
            GenreName = "Fantasy"
        };
        mockRepo.Setup(repo => repo.GetOneByGenreName(dto.GenreName)).ReturnsAsync(new Genre());

        Func<Task> act = async () => await service.CreateOne(dto);

        await Assert.ThrowsAsync<Exception>(act);
    }
}