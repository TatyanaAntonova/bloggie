# Bloggie

To build a migration:

% dotnet ef migrations add "Initial Migration"
% dotnet ef database update
% dotnet ef database remove

Retrieve the local secrets that were set from the command line, using:
% dotnet user-secrets init
% dotnet user-secrets set OpenAIKey <your-openai-key>