# Task Manager App

This is a simple C# console app to help you keep track of your daily tasks. You can add new tasks, mark them as done, edit them, and list completed or pending tasks. It saves your tasks in a JSON file so they’re still there when you come back.

## Features
- Add tasks with title, description, due date, and priority (High/Medium/Low)
- Edit existing tasks (update title, description, date, or priority)
- List all tasks, or filter by completed/pending
- Mark tasks as complete
- Save and load tasks from a JSON file

## Setup
1. Install the .NET SDK if you don’t have it yet.
2. Clone this repo and open the folder.
3. Run `dotnet build` to build the project.
4. Run `dotnet run` to start the app.
5. Use the menu to manage your tasks. Tasks save to `taskmanager.json`.

## Notes
- The app doesn’t use a database — just a JSON file for simplicity.
- Make sure to enter dates using `dd/MM/yyyy` format.
- You can edit a task by choosing option 6 from the menu.
