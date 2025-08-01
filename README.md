# Task Manager App

A simple console app to help you keep track of tasks. You can add, edit, list, and mark tasks as done. It saves your tasks in a JSON file so you don’t lose them after closing the app.

## Features
- Add tasks with title, description, due date, and priority (High/Medium/Low)
- Edit tasks
- List all, completed, or pending tasks
- Mark tasks as complete
- Save and load tasks from a JSON file

## Setup
1. Install the .NET SDK if you don’t have it yet.
2. Clone this repo and open the folder.
3. Run `dotnet build` to build the project.
4. Run `dotnet run` to start the app.
5. Use the menu to manage your tasks. Tasks save to `taskmanager.json`.

## Notes
- I built this following what I learned at Le Wagon bootcamp.
- The app uses a simple JSON file for saving — no database needed.
- Make sure to enter dates like `dd/MM/yyyy`.
- Editing tasks is done through option 6 in the menu.
