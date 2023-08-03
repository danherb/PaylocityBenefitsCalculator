using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services.BenefitCosts;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.UnitTests
{
    public class BenefitCostsServiceTests
    {
		// Each child, spouse or domestict partner costs 600$, so there
		// is no reason to create a test for each group. Although, in a real world,
		// children could cost e.g. 500$, spouse 400$ etc, so I keep tests for it

        [Fact]
        public void GetMonthlyPaycheck_OneSpouse_ReturnCorrectResult()
        {
			// Arrange
			const decimal expected = 400;
            var service = new BenefitCostsService();
			var employee = new GetEmployeeDto
			{
				Id = 1,
				FirstName = "Adam",
				LastName = "Morant",
				Salary = 52000,
				DateOfBirth = new DateTime(2000, 5, 15),
				Dependents = new List<GetDependentDto>
				{
					new()
					{
						Id = 1,
						FirstName = "Spouse",
						LastName = "Morant",
						Relationship = Relationship.Spouse,
						DateOfBirth = new DateTime(2000, 3, 3)
					}
				}
			};

			// Act
			var result = service.GetMonthlyPaycheck(employee);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void GetMonthlyPaycheck_OneChild_ReturnCorrectResult()
		{
			// Arrange
			const decimal expected = 400;
			var service = new BenefitCostsService();
			var employee = new GetEmployeeDto
			{
				Id = 1,
				FirstName = "Adam",
				LastName = "Morant",
				Salary = 52000,
				DateOfBirth = new DateTime(2000, 5, 15),
				Dependents = new List<GetDependentDto>
				{
					new()
					{
						Id = 2,
						FirstName = "Child1",
						LastName = "Morant",
						Relationship = Relationship.Child,
						DateOfBirth = new DateTime(2020, 6, 23)
					}
				}
			};

			// Act
			var result = service.GetMonthlyPaycheck(employee);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void GetMonthlyPaycheck_OneDomesticPartner_ReturnCorrectResult()
		{
			// Arrange
			const decimal expected = 400;
			var service = new BenefitCostsService();
			var employee = new GetEmployeeDto
			{
				Id = 1,
				FirstName = "Adam",
				LastName = "Morant",
				Salary = 52000,
				DateOfBirth = new DateTime(2000, 5, 15),
				Dependents = new List<GetDependentDto>
				{
					new()
					{
						Id = 1,
						FirstName = "Spouse",
						LastName = "Morant",
						Relationship = Relationship.DomesticPartner,
						DateOfBirth = new DateTime(2000, 3, 3)
					}
				}
			};

			// Act
			var result = service.GetMonthlyPaycheck(employee);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void GetMonthlyPaycheck_HighSalary_ReturnCorrectResult()
		{
			// Arrange
			const decimal expected = 2920;
			var service = new BenefitCostsService();
			var employee = new GetEmployeeDto
			{
				Id = 1,
				FirstName = "Adam",
				LastName = "Morant",
				Salary = 104000,
				DateOfBirth = new DateTime(2000, 5, 15),
				Dependents = new List<GetDependentDto>()
			};

			// Act
			var result = service.GetMonthlyPaycheck(employee);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void GetMonthlyPaycheck_HighAge_ReturnCorrectResult()
		{
			// Arrange
			const decimal expected = 800;
			var service = new BenefitCostsService();
			var employee = new GetEmployeeDto
			{
				Id = 1,
				FirstName = "Adam",
				LastName = "Morant",
				Salary = 52000,
				DateOfBirth = new DateTime(1930, 5, 15),
				Dependents = new List<GetDependentDto>()
			};

			// Act
			var result = service.GetMonthlyPaycheck(employee);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
