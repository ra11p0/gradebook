using Gradebook.Permissions.Database;

await new PermissionsDatabaseContext().Migrate();
