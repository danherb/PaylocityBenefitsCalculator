using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests : IntegrationTest
{
    public DependentIntegrationTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        // Arrange
        var dependentsDtos = new List<GetDependentDto>
        {
            new()
            {
                Id = 100,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3)
            },
            new()
            {
                Id = 101,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23)
            },
            new()
            {
                Id = 102,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18)
            },
            new()
            {
                Id = 103,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            }
        };

        var dependents = dependentsDtos.Select(x => _mapper.Map<Dependent>(x)).ToList();

        var employee1 = new Employee
        {
            Id = 10,
            FirstName = "Adam",
            LastName = "Doe",
            Salary = 60000,
            DateOfBirth = new DateTime(1985, 5, 15),
            Dependents = dependents.Take(3).ToList(), //ugly, but I didnt want to modify initial dependentsDtos list
        };

        var employee2 = new Employee
        {
            Id = 20,
            FirstName = "Daniel",
            LastName = "Blah",
            Salary = 50000,
            DateOfBirth = new DateTime(1955, 5, 15),
            Dependents = dependents.Skip(3).Take(1).ToList(), //ugly, but I didnt want to modify initial dependentsDtos list
        };

        _dbContext.Employees.Add(employee1);
        _dbContext.Employees.Add(employee2);
        _dbContext.SaveChanges();

        // Act
        var response = await HttpClient.GetAsync("/api/v1/dependents");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, dependentsDtos);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        // Arrange
        var dependent = new GetDependentDto
        {
            Id = 1,
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3)
        };

		var dependents = new List<Dependent>
		{
			_mapper.Map<Dependent>(dependent)
		};

		var employee = new Employee
        {
            Id = 10,
            FirstName = "Adam",
            LastName = "Doe",
            Salary = 60000,
            DateOfBirth = new DateTime(1985, 5, 15),
            Dependents = dependents
        };

		_dbContext.Employees.Add(employee);
		_dbContext.SaveChanges();

		// Act
		var response = await HttpClient.GetAsync("/api/v1/dependents/1");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/dependents/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

