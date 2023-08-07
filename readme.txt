Questions about requirements:
1) an employee may only have 1 spouse or domestic partner (not both) AND an employee may have an unlimited number of children
	- this should be restricted when adding new dependent to an employee. But POST endpoint is not in the requirements, there's only GET...
2) email and PDF requirements are different... I will implement requirements from the email
3) I guess paycheck can not be < 0 ... It's not in the requirements though, not sure how this works or if this is even possible



My questions or thoughts
1) in controller classes: inject service classes or implement repository and inject repo classes? I saw both approaches, not sure about consequences.
	solution: Im gonna inject services.
2) return paycheck as part of employee in GetEmployeeDto, or create another endpoint? 
	solution: new endpoint just for the paychecks by employee ID
3) convert to DTO in service or in controller? because now I just copy-pasted calculated property "Age" both in model and dto -> wrong, code duplicated
4) Integration tests - should it use real DB? I would say rather yes, but opinions differ on this topic.
	I'm gonna follow: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
	solution: I used in-memory sqlite db provider.
	PROBLEM: DB seeding in CustomDbContext.cs needs to be commented out, otherwise some tests fail.
	PROBLEM: integration tests need to be run by parts, i.e. BenefitCostsIntegrationTests only, then EmployeeIntegrationTests only and then DependentIntegrationTests only,
		otherwise some tests fail. 
	REASON FOR THAT: Program.cs is being run even when tests are run, so real DB is created anyway. That's not correct, we want to create just in memory sqlite DB.
		On top of that, I'd need to clean DB on each test run.
5) I seed some data in db context. I wanted to omit seeding when I am running tests, but this is out of scope of this exercise I guess. 
	solution: it's commented out now so there's no seed. To get some data when running the app, it's need to be seeded
6) even though Employee is nullable in Dependent, I can not add dependent to Db without employee being set, otherwise I get sqlite constraint error...
	probably change to: public int? EmployeeId { get; set; } in Dependent model could help, but I'm not sure if there can be standalone dependents or not. I would say not.
7) BenefitCostsService class, GetMonthlyPaycheck method. Parameter should be employee id, not whole dto.
	But dont want to spent more time.

