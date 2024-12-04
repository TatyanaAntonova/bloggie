# Bloggie

To build a migration:
% dotnet ef migrations add "Initial Migration"
% dotnet ef database update
% dotnet ef database remove

If you have more than one DbContext, you need to specify it each time:
% dotnet ef migrations add "Adding Identity" --context "AuthDbContext"
% dotnet ef database update --context "AuthDbContext"
% dotnet ef database remove --context "AuthDbContext"

Note: pay attention to EntityFramework imported packages versions in NuGet. Different versions mau cause problems with
migrations.

Retrieve the local secrets that were set from the command line, using:
% dotnet user-secrets init
% dotnet user-secrets set OpenAIKey <your-openai-key>