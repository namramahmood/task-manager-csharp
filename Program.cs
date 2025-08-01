using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization; // This is for handling date formats like dd/MM/yyyy

// This class stores info about a single task
public class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public string Priority { get; set; }
}

// This class handles all tasks and their actions
public class TaskManager
{
    public List<Task> Tasks { get; set; } = new List<Task>();

    public void AddTask(Task task) => Tasks.Add(task);

    public void ListTasks()
    {
        if (Tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        for (int i = 0; i < Tasks.Count; i++)
        {
            var task = Tasks[i];
            string status = task.IsCompleted ? "Completed" : "Pending";
            Console.WriteLine($"{i + 1}. {task.Title} - {status} - Due: {task.DueDate:dd/MM/yyyy} - Priority: {task.Priority}");
        }
    }

    public void ListCompletedTasks()
    {
        var completed = Tasks.FindAll(t => t.IsCompleted);
        if (completed.Count == 0)
        {
            Console.WriteLine("No completed tasks.");
            return;
        }

        for (int i = 0; i < completed.Count; i++)
        {
            var task = completed[i];
            Console.WriteLine($"{i + 1}. {task.Title} - Completed - Due: {task.DueDate:dd/MM/yyyy} - Priority: {task.Priority}");
        }
    }

    public void ListPendingTasks()
    {
        var pending = Tasks.FindAll(t => !t.IsCompleted);
        if (pending.Count == 0)
        {
            Console.WriteLine("No pending tasks.");
            return;
        }

        for (int i = 0; i < pending.Count; i++)
        {
            var task = pending[i];
            Console.WriteLine($"{i + 1}. {task.Title} - Pending - Due: {task.DueDate:dd/MM/yyyy} - Priority: {task.Priority}");
        }
    }

    public void MarkComplete(int index)
    {
        if (index >= 0 && index < Tasks.Count)
        {
            Tasks[index].IsCompleted = true;
            Console.WriteLine($"Task '{Tasks[index].Title}' marked as complete.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    public void EditTask(int index)
    {
        if (index < 0 || index >= Tasks.Count)
        {
            Console.WriteLine("Invalid task number.");
            return;
        }

        var task = Tasks[index];
        Console.WriteLine("Leave blank if you want to keep the current value.");

        Console.Write($"Current title: {task.Title} | New title: ");
        string title = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(title)) task.Title = title;

        Console.Write($"Current description: {task.Description} | New description: ");
        string description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description)) task.Description = description;

        // Show date and ask for new input in dd/MM/yyyy format
        Console.Write($"Current due date: {task.DueDate:dd/MM/yyyy} | New due date (dd/MM/yyyy): ");
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input) &&
            DateTime.TryParseExact(input, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime newDate))
        {
            task.DueDate = newDate;
        }
        else if (!string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid date format. Keeping old date.");
        }

        Console.Write($"Current priority: {task.Priority} | New priority (High/Medium/Low): ");
        string priority = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priority)) task.Priority = priority;

        Console.WriteLine("Task updated!");
    }

    public void SaveTasks(string fileName)
    {
        try
        {
            string json = JsonSerializer.Serialize(Tasks);
            File.WriteAllText(fileName, json);
            Console.WriteLine("Tasks saved.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving tasks: {e.Message}");
        }
    }

    public void LoadTasks(string fileName)
    {
        try
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("No save file found.");
                return;
            }

            string json = File.ReadAllText(fileName);
            Tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            Console.WriteLine("Tasks loaded.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading tasks: {e.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        TaskManager taskManager = new TaskManager();
        string fileName = "taskmanager.json";
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("1 - Add task");
            Console.WriteLine("2 - List all tasks");
            Console.WriteLine("3 - List completed tasks");
            Console.WriteLine("4 - List pending tasks");
            Console.WriteLine("5 - Mark task complete");
            Console.WriteLine("6 - Edit a task");
            Console.WriteLine("7 - Save tasks");
            Console.WriteLine("8 - Load tasks");
            Console.WriteLine("9 - Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Title: ");
                    string title = Console.ReadLine();

                    Console.Write("Description: ");
                    string description = Console.ReadLine();

                    Console.Write("Due Date (dd/MM/yyyy): ");
                    DateTime dueDate;
                    while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out dueDate))
                        Console.Write("Invalid date. Please enter due date (dd/MM/yyyy): ");

                    Console.Write("Priority (High/Medium/Low): ");
                    string priority = Console.ReadLine();

                    taskManager.AddTask(new Task
                    {
                        Title = title,
                        Description = description,
                        DueDate = dueDate,
                        Priority = priority,
                        IsCompleted = false
                    });
                    Console.WriteLine("Task added!");
                    break;

                case "2":
                    taskManager.ListTasks();
                    break;

                case "3":
                    taskManager.ListCompletedTasks();
                    break;

                case "4":
                    taskManager.ListPendingTasks();
                    break;

                case "5":
                    Console.Write("Enter task number to mark complete: ");
                    if (int.TryParse(Console.ReadLine(), out int completeIndex))
                        taskManager.MarkComplete(completeIndex - 1);
                    else
                        Console.WriteLine("Please enter a valid number.");
                    break;

                case "6":
                    Console.Write("Enter task number to edit: ");
                    if (int.TryParse(Console.ReadLine(), out int editIndex))
                        taskManager.EditTask(editIndex - 1);
                    else
                        Console.WriteLine("Please enter a valid number.");
                    break;

                case "7":
                    taskManager.SaveTasks(fileName);
                    break;

                case "8":
                    taskManager.LoadTasks(fileName);
                    break;

                case "9":
                    running = false;
                    Console.WriteLine("Thank you for using Task Manager. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
