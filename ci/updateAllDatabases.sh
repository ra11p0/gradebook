#!/bin/bash
cd ../backend/src/;

#Update itentity
cd Gradebook.Foundation.Identity;
dotnet ef database update;
cd ..;

#Update foundation
cd Gradebook.Foundation.Database;
dotnet ef database update;
cd ..;

#Update permissions
cd Gradebook.Permissions.Database;
dotnet ef database update;
cd ..;

#Update settings
cd Gradebook.Settings.Database;
dotnet ef database update;
cd ..;

#End
echo 'All databases updated.';
