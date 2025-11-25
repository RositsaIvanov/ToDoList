# Web Application User Guide

## Overview
The TodoList Web Application is a modern, responsive Razor Pages client that provides an intuitive interface for managing tasks with real-time progress tracking. It communicates with the TodoList Web API for all data operations.

## Prerequisites
- .NET 6.0 or higher
- TodoList Web API running on `https://localhost:7126`
- Modern web browser (Chrome, Firefox, Edge, Safari)

---

## Getting Started

### Starting the Application

**Step 1:** Ensure the Web API is running
```bash
# In one terminal
cd ToDoList.WebApi
dotnet run
```

**Step 2:** Start the Web Client
```bash
# In another terminal
cd ToDoList.Client
dotnet run
```

**Step 3:** Open your browser
```
https://localhost:7266
```

---

## User Interface Overview

The application features a **two-column layout**:

```
┌─────────────────────────────────────────────────────────┐
│                      ToDoList                           │
├──────────────────────┬──────────────────────────────────┤
│                      │                                  │
│      TASKS           │      CREATE TASK                 │
│   (Left Panel)       │     (Right Panel)                │
│                      │                                  │
│  • Task List         │  • Task Form                     │
│  • Progress Bars     │  • Progress Registration         │
│  • Action Buttons    │                                  │
│                      │                                  │
└──────────────────────┴──────────────────────────────────┘
```

---

## Features & Operations

### 📋 Viewing Tasks (Left Panel)

The left panel displays all your tasks with:

**Task Card Elements:**
- **ID and Title** - Unique identifier and task name
- **Description** - Detailed task information
- **Category Badge** - Visual category indicator
- **Completion Status** - Green badge (True) or Yellow badge (False)
- **Progress Bar** - Visual representation of overall progress
- **Progress History** - Timestamped log of all progress entries
- **Action Buttons** - Update and Delete buttons

**Example Task Display:**
```
┌────────────────────────────────────────────────┐
│ 1) Implement authentication - Add JWT tokens  │
│ (Work) Completed: False                        │
│                                                │
│ ████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  │
│ Overall Progress: 35%                          │
│                                                │
│ 11/25/2025 10:00:00 AM                        │
│ |OOOOOOOOOOOOO                                |│
│                                                │
│ 11/25/2025 2:30:00 PM                         │
│ |OOOOOOOOOOOOOOOOO                            |│
│                                                │
│ [Update] [Delete]                              │
└────────────────────────────────────────────────┘
```

**Progress Visualization:**
- **Blue progress bar** - Task in progress (0-99%)
- **Green progress bar** - Task completed (100%)
- **ASCII bars** - Historical progress snapshots (50 characters wide)

---

### ✨ Creating a Task (Right Panel - Top Section)

**Steps:**

1. **Enter Title** (Required)
   - Type the task name in the "Titl" field
   - Example: `Implement user authentication`

2. **Enter Description** (Optional)
   - Provide detailed information in the "Descripcion" textarea
   - Example: `Add JWT-based authentication with refresh tokens`

3. **Select Category** (Required)
   - Choose from the dropdown menu
   - Available categories are loaded automatically from the API

4. **Click "Save"**
   - Task is created with auto-generated ID
   - Success message appears
   - Task list refreshes automatically
   - Form resets for next entry

5. **Click "Cancel"** (Optional)
   - Clears the form without saving

**Form Validation:**
- Title is required - you'll see an alert if empty
- Category is pre-selected with first available option
- Description can be left empty

**Example Workflow:**
```
Title: Complete project documentation
Description: Write user guides for console app, API, and web client
Category: Work
[Save] → Success! Task created with ID 5
```

---

### 📊 Registering Progress (Right Panel - Bottom Section)

Track incremental progress on any task.

**Steps:**

1. **Enter Task ID**
   - Type the ID of the task (visible in left panel)
   - Example: `5`

2. **Select Date and Time**
   - Use the datetime picker
   - Format: `MM/DD/YYYY HH:MM AM/PM`
   - Example: `11/25/2025 2:30 PM`

3. **Enter Percentage**
   - Type a number between 1-100
   - This percentage **adds** to existing progress
   - Example: `25`

4. **Click "Save"**
   - Progress is registered
   - Success message shows: `Progress of 25% registered successfully for Task ID 5!`
   - Task list refreshes to show updated progress
   - Form clears automatically

5. **Click "Cancel"** (Optional)
   - Clears the form without saving

**Important: Cumulative Progress**
```
Initial: 0%
Register 25% → Total: 25%
Register 30% → Total: 55%
Register 45% → Total: 100% (Task marked as Completed!)
```

**Form Validation:**
- All fields are required
- Percentage must be 1-100
- Invalid inputs trigger an alert

---

### ✏️ Updating a Task

Modify the description of an existing task.

**Steps:**

1. **Click "Update" button** on any task card
2. **Enter new description** in the prompt dialog
3. **Click "OK"** to save or "Cancel" to abort
4. Success message appears
5. Task list refreshes with updated description

**Example:**
```
Current: "Add JWT tokens"
Update to: "Add OAuth2 authentication with Google provider"
```

**Restrictions:**
- ⚠️ **Cannot update tasks with >50% progress**
- Update button is **disabled** (grayed out) for tasks >50%
- Error message: `Cannot update an item with more than 50% progress`

---

### 🗑️ Deleting a Task

Remove a task from your list.

**Steps:**

1. **Click "Delete" button** on any task card
2. **Confirm deletion** in the confirmation dialog
3. Success message appears
4. Task is removed from the list

**Restrictions:**
- ⚠️ **Cannot delete tasks with >50% progress**
- Delete button is **disabled** (grayed out) for tasks >50%
- Error message: `Cannot delete an item with more than 50% progress`

**Safety Features:**
- Confirmation dialog prevents accidental deletion
- Deletion is permanent and cannot be undone

---

## Visual Indicators

### Status Badges

| Badge | Color | Meaning |
|-------|-------|---------|
| **Completed: True** | Green | Task is 100% complete |
| **Completed: False** | Yellow | Task is in progress |
| **Category Badge** | Gray | Task category label |

### Button States

| Button | Color | State | Meaning |
|--------|-------|-------|---------|
| Update | Yellow | Enabled | Can modify description |
| Update | Gray | Disabled | Progress >50%, locked |
| Delete | Red | Enabled | Can remove task |
| Delete | Gray | Disabled | Progress >50%, locked |
| Save | Blue | Always Enabled | Submit form |
| Cancel | Gray | Always Enabled | Clear form |

### Progress Bars

**Overall Progress Bar:**
- **Blue** (0-99%): Task in progress
- **Green** (100%): Task completed
- **Height**: 10px
- **Width**: Proportional to percentage

**ASCII Progress Bars:**
- **Character**: `0` for completed portion
- **Width**: 50 characters total
- **Format**: `|OOOOOO                    |`

---

## Real-Time Features

### Auto-Refresh
The task list automatically refreshes after:
- ✅ Creating a new task
- ✅ Updating a task description
- ✅ Deleting a task
- ✅ Registering progress

### Dynamic Updates
- Progress bars update instantly
- Completion status changes when reaching 100%
- Button states update based on progress percentage
- No page reload required

---

## Error Handling

### Common Errors & Solutions

**"Please ensure Task ID, Date, and a valid Percentage (1-100) are entered"**
- **Cause**: Missing or invalid progress form fields
- **Solution**: Fill all fields with valid data

**"Title is required"**
- **Cause**: Attempting to create task without title
- **Solution**: Enter a title before clicking Save

**"Cannot update an item with more than 50% progress"**
- **Cause**: Trying to update a task >50% complete
- **Solution**: Complete the task or create a new one

**"Cannot delete an item with more than 50% progress"**
- **Cause**: Trying to delete a task >50% complete
- **Solution**: Wait until task is complete or contact admin

**"Error loading tasks"**
- **Cause**: Web API is not running or unreachable
- **Solution**: Ensure API is running on `https://localhost:7126`

**"No tasks found"**
- **Cause**: No tasks exist in the system
- **Solution**: Create your first task using the form

---

## Keyboard Shortcuts & Accessibility

### Form Navigation
- **Tab**: Move between form fields
- **Shift+Tab**: Move backwards
- **Enter**: Submit form (when in input field)
- **Esc**: Close prompt dialogs

### Browser Features
- **Ctrl+R / F5**: Refresh page (reloads all tasks)
- **Ctrl+Plus/Minus**: Zoom in/out
- **Ctrl+0**: Reset zoom

---

## Best Practices

### 📝 Task Creation
- Use **clear, descriptive titles** (e.g., "Implement user authentication" not "Auth")
- Add **detailed descriptions** to track requirements and context
- Choose **appropriate categories** for better organization
- Create tasks **before** starting work to track from 0%

### 📊 Progress Tracking
- Register progress **regularly** (daily or at milestones)
- Use **realistic percentages** based on actual completion
- Remember progress is **cumulative** - plan increments carefully
- Example: Break 100% into logical chunks (25%, 25%, 25%, 25%)

### 🔒 50% Rule Awareness
- **Plan ahead**: Once >50%, you cannot modify or delete
- **Review carefully** before registering progress >50%
- **Use descriptions wisely** - make them complete before 50%

### 🎯 Workflow Optimization
- Keep the **API running** while using the web app
- Use **multiple browser tabs** if managing many tasks
- **Refresh the page** if data seems stale
- Check **browser console** (F12) for detailed error messages

---

## Troubleshooting

### Tasks Not Loading

**Symptoms:** "Loading..." message persists or "Error loading tasks" appears

**Solutions:**
1. Verify Web API is running:
   ```bash
   # Check if API is accessible
   curl https://localhost:7126/api/ToDoList
   ```
2. Check browser console (F12) for errors
3. Verify CORS configuration in API
4. Ensure correct API URL in `site.js` (line 4)

---

### Progress Not Updating

**Symptoms:** Progress bars don't reflect recent changes

**Solutions:**
1. **Refresh the page** (Ctrl+R or F5)
2. Check if progress was successfully registered (look for success message)
3. Verify the task ID is correct
4. Check browser console for API errors

---

### Buttons Disabled Unexpectedly

**Symptoms:** Update/Delete buttons are grayed out

**Explanation:** This is **intentional** - tasks with >50% progress are protected

**Solutions:**
- **For Updates**: Complete the task or create a new one
- **For Deletions**: Wait until task is fully complete
- **Check progress**: Look at the overall progress percentage

---

### Form Not Submitting

**Symptoms:** Clicking Save does nothing or shows validation error

**Solutions:**
1. **Check required fields**: Title must be filled
2. **Check percentage range**: Must be 1-100
3. **Check date format**: Use the datetime picker
4. **Look for alerts**: Browser will show validation messages

---

### Categories Not Showing

**Symptoms:** Category dropdown is empty

**Solutions:**
1. Ensure Web API is running
2. Check that API has categories configured
3. Verify API endpoint: `GET /api/ToDoList/categories`
4. Refresh the page

---

## Technical Details

### API Integration
- **Base URL**: `https://localhost:7126/api/ToDoList`
- **Communication**: AJAX via Fetch API
- **Data Format**: JSON
- **CORS**: Configured for `https://localhost:7266`

### Browser Compatibility
- ✅ Chrome 90+
- ✅ Firefox 88+
- ✅ Edge 90+
- ✅ Safari 14+

### Performance
- **Auto-refresh**: After each operation
- **No pagination**: All tasks loaded at once
- **Client-side rendering**: Dynamic HTML generation
- **Responsive**: Adapts to screen size

---

## Example Workflows

### Complete Task Lifecycle

**1. Create Task**
```
Title: Build user dashboard
Description: Create responsive dashboard with charts
Category: Work
[Save] → Task ID 8 created
```

**2. Start Work - Register 25% Progress**
```
ID: 8
Date: 11/25/2025 9:00 AM
Percent: 25
[Save] → Progress registered! Total: 25%
```

**3. Continue Work - Register 30% Progress**
```
ID: 8
Date: 11/25/2025 2:00 PM
Percent: 30
[Save] → Progress registered! Total: 55%
```

**4. Update Description (Still possible at 55%)**
```
❌ Update button is disabled (>50% progress)
Cannot modify description anymore
```

**5. Complete Task - Register 45% Progress**
```
ID: 8
Date: 11/25/2025 5:00 PM
Percent: 45
[Save] → Progress registered! Total: 100%
Task marked as Completed: True ✅
```

---

### Managing Multiple Tasks

**Scenario:** Track multiple projects simultaneously

**Step 1:** Create tasks for each project
```
Task 1: Frontend development (Work)
Task 2: Backend API (Work)
Task 3: Database design (Work)
Task 4: Buy groceries (Personal)
```

**Step 2:** Register progress on active tasks
```
Task 1: 25% (morning work)
Task 2: 15% (afternoon work)
```

**Step 3:** Update descriptions as needed
```
Task 4: Update to "Buy groceries + pharmacy items"
```

**Step 4:** Complete and delete finished tasks
```
Task 4: Register 100% → Completed
Task 4: Delete (if no longer needed)
```

---

## Tips for Power Users

### 🚀 Efficiency Tips
- **Keep API running** in background terminal
- **Use browser bookmarks** for quick access
- **Monitor browser console** (F12) for real-time feedback
- **Plan progress increments** before registering

### 🎨 Visual Organization
- Use **categories** to color-code mentally
- Watch **progress bars** for quick status overview
- Look for **green badges** to identify completed tasks
- **Disabled buttons** indicate protected tasks

### 🔍 Debugging
- **F12**: Open browser developer tools
- **Network tab**: Monitor API requests/responses
- **Console tab**: View JavaScript errors
- **Application tab**: Check for CORS issues

---

## Support & Resources

### Quick Links
- **Web App**: `https://localhost:7266`
- **API**: `https://localhost:7126/api/ToDoList`
- **Swagger**: `https://localhost:7126/swagger`

### Getting Help
1. Check browser console for errors (F12)
2. Verify API is running and accessible
3. Review API documentation for endpoint details
4. Check CORS configuration if requests fail

### Related Documentation
- [Console App User Guide](CONSOLE_APP_USER_GUIDE.md)
- [Web API Documentation](WEB_API_DOCUMENTATION.md)

---

## Frequently Asked Questions

**Q: Can I use the web app without the API?**  
A: No, the web app requires the API to be running for all operations.

**Q: Why can't I update my task?**  
A: Tasks with >50% progress are locked to prevent modification of nearly-complete work.

**Q: Is my data saved permanently?**  
A: Currently, data is stored in-memory and will be lost when the API restarts.

**Q: Can I access this from my phone?**  
A: Yes, the interface is responsive, but you'll need the API running on an accessible server.

**Q: What happens if I register >100% total progress?**  
A: The API should validate this, but plan your increments to total exactly 100%.

**Q: Can I change the category of a task?**  
A: No, categories are immutable after creation. Create a new task if needed.

**Q: How do I know which tasks are completed?**  
A: Look for green "Completed: True" badges and 100% progress bars.

---

## Conclusion

The TodoList Web Application provides an intuitive, modern interface for task management with powerful progress tracking capabilities. The 50% protection rule ensures data integrity, while real-time updates keep you informed of all changes.

For optimal experience:
- ✅ Keep the API running
- ✅ Plan your tasks and progress increments
- ✅ Use descriptive titles and descriptions
- ✅ Monitor progress bars for quick status checks

Happy task managing! 🎯
