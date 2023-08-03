using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.BenefitCosts;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ApiTests.IntegrationTests;

public class BenefitCostsIntegrationTests : IntegrationTest
{
	public BenefitCostsIntegrationTests(WebApplicationFactory<Program> factory) : base(factory)
	{
	}

	[Fact]
	public async Task WhenAskedForABenefitCosts_ShouldReturnCorrectBenefitCosts()
	{
		// Arrange

		// Before deductions: 104k / 26 = 4k
		// Basic cost: 1000
		// High salary: 4k * 0.02 = 80
		// 3 dependents: 3 * 600 = 1800
		// result: 4000 - 1000 - 1800 - 80 = 1120
		const int expectedPaycheck = 1120; 

		var employee = new Employee
		{
			Id = 1,
			FirstName = "Adam",
			LastName = "Morant",
			Salary = 104000,
			DateOfBirth = new DateTime(2000, 5, 15),
			Dependents = new List<Dependent>
				{
					new()
					{
						Id = 1,
						FirstName = "Spouse",
						LastName = "Morant",
						Relationship = Relationship.Spouse,
						DateOfBirth = new DateTime(2000, 3, 3)
					},
					new()
					{
						Id = 2,
						FirstName = "Child1",
						LastName = "Morant",
						Relationship = Relationship.Child,
						DateOfBirth = new DateTime(2020, 6, 23)
					},
					new()
					{
						Id = 3,
						FirstName = "Child2",
						LastName = "Morant",
						Relationship = Relationship.Child,
						DateOfBirth = new DateTime(2021, 5, 18)
					}
				}
		};

		_dbContext.Employees.Add(employee);
		_dbContext.SaveChanges();

		var expectedResponse = new GetBenefitCostsDto
		{
			EmployeeId = 1,
			Paycheck = expectedPaycheck
		};

		// Act
		var response = await HttpClient.GetAsync("/api/v1/benefitcosts/1");

		// Assert
		await response.ShouldReturn(HttpStatusCode.OK, expectedResponse);
	}

	[Fact]
	public async Task WhenAskedForANonexistentBenefitCosts_ShouldReturn404()
	{
		var response = await HttpClient.GetAsync($"/api/v1/benefitcosts/{int.MinValue}");
		await response.ShouldReturn(HttpStatusCode.NotFound);
	}
}

