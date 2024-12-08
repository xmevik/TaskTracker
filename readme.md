# ▎Task Tracker CLI Application

Solution for the [task-tracker](https://roadmap.sh/projects/task-tracker) challenge from [roadmap.sh](https://roadmap.sh/).

## ▎Overview

Task Tracker is a command line interface (CLI) application designed to help you track and manage your tasks efficiently. This project allows users to add, update, delete, and manage tasks, providing a simple yet effective way to stay organized. The application stores tasks in a JSON file, making it easy to persist data across sessions.

## ▎Features

• Add, Update, and Delete Tasks: Manage your tasks effortlessly.

• Task Status Management: Mark tasks as in progress or done.

• Task Listing: View all tasks or filter them by status (done, not done, in progress).

## ▎Requirements

The application must meet the following requirements:

• Run from the command line and accept user actions and inputs as arguments.

• Store tasks in a JSON file located in the current directory.

• Create the JSON file if it does not already exist.

• Utilize the native file system capabilities of C# to interact with the JSON file.

• Handle errors and edge cases gracefully without relying on external libraries or frameworks.

## ▎Commands

Here are the available commands and their usage:

### ▎Adding a New Task

```bash
task-cli add "Buy groceries"

Output: Task added successfully (ID: 1)
```

### ▎Updating and Deleting Tasks

```bash
task-cli update 1 "Buy groceries and cook dinner"
task-cli delete 1
```

### ▎Marking a Task as In Progress or Done

```bash
task-cli mark inProgress 1
task-cli mark done 1
```

### ▎Listing All Tasks

```bash
task-cli list
```

### ▎Listing Tasks by Status

```bash
task-cli list done
task-cli list todo
task-cli list inProgress
```

## ▎Testing

The project includes a comprehensive suite of unit tests to ensure the reliability and correctness of the application. The tests cover various functionalities, including:

• Adding tasks

• Updating tasks

• Deleting tasks

• Changing task statuses

• Listing tasks based on their statuses

To run the tests, use the following command:

```bash
dotnet test
```

## ▎Getting Started

To get started with the Task Tracker CLI application:

1.Clone the repository

```bash
git clone https://github.com/yourusername/task-tracker.git
cd task-tracker
```

2.Build the application

```bash
dotnet build
```

3.Run the application

```bash
dotnet run -- [command] [arguments]
```

Replace [command] and [arguments] with the desired command and its corresponding arguments.

## ▎Contributing

Contributions are welcome! If you have suggestions for improvements or new features, please open an issue or submit a pull request.

## ▎License

This project is licensed under the MIT License - see the LICENSE file for details.
