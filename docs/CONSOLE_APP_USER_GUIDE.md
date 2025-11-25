# Console Application User Guide

## Overview
The TodoList Console Application provides a command-line interface for managing tasks with full CRUD operations, progress tracking, and structured logging.

## Prerequisites
- .NET 6.0 or higher
- Windows/Linux/macOS terminal

## Getting Started

### Running the Application
1. Navigate to the TodoList project directory
2. Run the application:
   ```bash
   dotnet run --project TodoList
   ```

### Main Menu
Upon launch, you'll see the following menu:

```
Todo List Application
1. Add Item
2. Update Item
3. Remove Item
4. Register Progression
5. Print Items
6. Exit
Select an option:
```

## Features & Operations

### 1. Add Item
Creates a new task in your todo list.

**Steps:**
1. Select option `1`
2. Enter the task **title** (required)
3. Enter the task **description** (optional but recommended)
4. View available categories (displayed automatically)
5. Enter the **category** from the available options

**Example:**
```
Select an option: 1
Enter title: Implement user authentication
Enter description: Add JWT-based authentication to the API
Available categories: Work, Personal, Shopping, Health
Enter category: Work
```

**Result:** Task is created with a unique ID and logged to `logs/todolist.log`

---

### 2. Update Item
Modifies the description of an existing task.

**Steps:**
1. Select option `2`
2. Enter the **item ID** to update
3. Enter the **new description**

**Example:**
```
Select an option: 2
Enter item ID to update: 3
Enter new description: Add OAuth2 authentication with Google provider
```

**Important Rules:**
- ⚠️ **Cannot update items with >50% progress** - This prevents modification of nearly-complete tasks
- Only the description can be updated (title and category are immutable)

---

### 3. Remove Item
Deletes a task from your todo list.

**Steps:**
1. Select option `3`
2. Enter the **item ID** to remove

**Example:**
```
Select an option: 3
Enter item ID to remove: 5
```

**Important Rules:**
- ⚠️ **Cannot delete items with >50% progress** - Protects tasks that are substantially complete
- Deletion is permanent and cannot be undone

---

### 4. Register Progression
Track incremental progress on a task.

**Steps:**
1. Select option `4`
2. Enter the **item ID**
3. Enter the **date** in format `yyyy-MM-dd` (e.g., `2025-11-25`)
4. Enter the **percentage** (1-100)

**Example:**
```
Select an option: 4
Enter item ID: 3
Enter date (yyyy-MM-dd): 2025-11-25
Enter percent: 25
```

**How Progress Works:**
- Progress is **cumulative** - each entry adds to the total
- Example: If you register 25%, then 30%, the total progress is 55%
- When total reaches 100%, the task is marked as **Completed**
- Progress entries are timestamped and logged

---

### 5. Print Items
Displays all tasks with detailed progress visualization.

**Steps:**
1. Select option `5`

**Output Format:**
```
1) Implement authentication - Add JWT tokens (Work) Completed:False
11/25/2025 12:00:00 PM - 25% |OOOOOOOOOOOO                                      |
11/26/2025 2:30:00 PM - 55% |OOOOOOOOOOOOOOOOOOOOOOOOOOO                       |

2) Buy groceries - Weekly shopping (Personal) Completed:True
11/24/2025 9:00:00 AM - 100% |OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO|
```

**Display Elements:**
- **ID)** Task identifier
- **Title** - **Description** (Category)
- **Completed status** (True/False)
- **Progress bars** showing cumulative progress over time (50 characters wide)

---

### 6. Exit
Safely closes the application.

**Steps:**
1. Select option `6`
2. Application logs shutdown and terminates

---

## Error Handling

### Input Validation
The application validates all inputs:
- **Numbers**: Prompts repeatedly until valid integer/decimal is entered
- **Dates**: Must be in `yyyy-MM-dd` format
- **Percentages**: Must be between 1-100

### Business Rule Violations
Common errors and their meanings:

| Error Message | Cause | Solution |
|--------------|-------|----------|
| "Cannot update an item with more than 50% progress" | Attempting to modify a task >50% complete | Complete or delete the task first |
| "Cannot delete an item with more than 50% progress" | Attempting to remove a task >50% complete | Wait until task is complete or contact admin |
| "Item not found" | Invalid item ID | Use option 5 to view valid IDs |
| "Invalid category" | Category doesn't exist | Check available categories when adding items |

---

## Logging

### Log Files
All operations are logged to:
- **Location**: `logs/todolist.log`
- **Rotation**: Daily (new file each day)
- **Format**: Structured logging with timestamps

### Log Levels
- **Information**: Successful operations (add, update, delete, progress)
- **Error**: Failed operations with exception details
- **Fatal**: Application crashes

### Example Log Entries
```
2025-11-25 14:23:15 [INF] Application Starting
2025-11-25 14:23:45 [INF] Item added: "Implement authentication"
2025-11-25 14:24:12 [INF] Progression registered: 3 25%
2025-11-25 14:25:03 [ERR] Operation Failed (Option 3): Cannot delete item with >50% progress
2025-11-25 14:30:00 [INF] Application Exiting
```

---

## Tips & Best Practices

### 📋 Task Management
- Use **descriptive titles** for easy identification
- Add **detailed descriptions** to track requirements
- Organize tasks with **categories** for better filtering

### 📊 Progress Tracking
- Register progress **regularly** to maintain accurate status
- Use **realistic percentages** based on actual completion
- Remember progress is **cumulative** - plan increments accordingly

### 🔒 Protection Rules
- The 50% rule prevents accidental modification/deletion of advanced tasks
- Plan your task breakdown to avoid needing changes after 50% completion

### 🗂️ Categories
- Use consistent category names (case-sensitive)
- Common categories: `Work`, `Personal`, `Shopping`, `Health`, `Learning`
- Categories help organize and filter tasks mentally

---

## Troubleshooting

### Application Won't Start
```bash
# Ensure you're in the correct directory
cd TodoList

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run again
dotnet run
```

### "Invalid option" Error
- Ensure you're entering only the **number** (1-6)
- Press Enter after typing the number

### Progress Bar Not Showing
- Ensure you've registered at least one progression entry
- Use option 5 to view items after registering progress

### Date Format Errors
- Use format: `yyyy-MM-dd` (e.g., `2025-11-25`)
- Use 4-digit year, 2-digit month, 2-digit day
- Separate with hyphens, not slashes

---

## Quick Reference

| Operation | Option | Required Fields | Restrictions |
|-----------|--------|----------------|--------------|
| Add Item | 1 | Title, Description, Category | None |
| Update Item | 2 | ID, New Description | Progress ≤50% |
| Remove Item | 3 | ID | Progress ≤50% |
| Register Progress | 4 | ID, Date, Percentage | 1-100% |
| Print Items | 5 | None | None |
| Exit | 6 | None | None |

---

## Support
For issues or questions:
- Check log files in `logs/todolist.log`
- Review error messages for specific guidance
- Ensure all dependencies are installed with `dotnet restore`
