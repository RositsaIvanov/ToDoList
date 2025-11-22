// site.js - Razor Pages front-end client for ToDoList API
// Handles dynamic CRUD operations and progress registration without page reloads

var apiBase = "https://localhost:7126/api/ToDoList";

// -------------------------------------------------
// Helper: Perform API requests
// -------------------------------------------------
async function apiRequest(url, method = "GET", body = null) {
    const options = { method, headers: { "Content-Type": "application/json" } };
    if (body) options.body = JSON.stringify(body);

    const response = await fetch(url, options);
    if (!response.ok) throw new Error(await response.text());
    // If the API returns 204 (No Content), return null. Otherwise, parse JSON.
    return response.status !== 204 ? response.json().catch(() => null) : null;
}

// -------------------------------------------------
// Helper: Build HTML display for progressions
// -------------------------------------------------
function buildProgressDisplay(progressions) {
    if (!progressions || progressions.length === 0) {
        return '<div class="progress-info">No progress registered.</div>';
    }

    // The final accumulated percent is the overall progress
    // Ensure access safety in case progressions array is empty
    const finalProgress = progressions[progressions.length - 1]?.percent || 0;

    // Overall Bootstrap-style progress bar
    const overallBar = `
        <div class="progress mb-2" style="height: 10px;">
            <div class="progress-bar" role="progressbar" 
                 style="width: ${finalProgress}%; background-color: ${finalProgress === 100 ? '#28a745' : '#007bff'};" 
                 aria-valuenow="${finalProgress}" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
        <div class="mb-2 text-muted small">Overall Progress: ${finalProgress}%</div>
    `;

    // Detailed log of each progression step
    const detailLog = progressions.map(p => {
        // Format the date as required by the example (e.g., 3/18/2025 12:00:00 AM)
        const dateString = new Date(p.date).toLocaleString('en-US', {
            month: 'numeric', day: 'numeric', year: 'numeric',
            hour: 'numeric', minute: '2-digit', hour12: true
        });

        const bar = buildAsciiBar(p.accumulatedPercent);

        return `<div class="progress-step small">
                    ${dateString}
                    <pre class="m-0">${bar}</pre>
                </div>`;
    }).join('');

    return overallBar + detailLog;
}

// Helper to create the basic ASCII/textual progress bar (using '0' character)
function buildAsciiBar(percent) {
    const totalChars = 50;
    const completedChars = Math.round((percent / 100) * totalChars);
    const incompleteChars = totalChars - completedChars;

    const completedPart = '0'.repeat(completedChars);
    const incompletePart = ' '.repeat(incompleteChars);

    return `|${completedPart}${incompletePart}|`;
}

// -------------------------------------------------
// Progress Form Handler
// -------------------------------------------------

/**
 * Handles the submission of the progress registration form.
 * @param {Event} event 
 */
async function handleProgressSubmit(event) {
    event.preventDefault();

    const id = document.getElementById('progId').value;
    const dateInput = document.getElementById('progDate').value;
    const percentInput = document.getElementById('progPercent').value;

    const percent = parseFloat(percentInput);

    if (!id || !dateInput || isNaN(percent) || percent <= 0 || percent > 100) {
        alert("Please ensure Task ID, Date, and a valid Percentage (1-100) are entered.");
        return;
    }

    const progressData =  {
        date: new Date(`${dateInput}:00`).toISOString(),
        percentage: percent
    };

    try {
        await apiRequest(`${apiBase}/${id}/progression`, "POST", progressData);
        alert(`Progress of ${percent}% registered successfully for Task ID ${id}!`);

        document.getElementById('progId').value = '';
        document.getElementById('progDate').value = '';
        document.getElementById('progPercent').value = '';

        loadItems();
    } catch (error) {
        console.error("Error registering progress:", error);
        alert(`Error registering progress: ${error.message}`);
    }
}


// -------------------------------------------------
// Load all ToDo items on page load and setup listeners
// -------------------------------------------------
document.addEventListener("DOMContentLoaded", () => {
    const listContainer = document.getElementById("todoList");
    if (listContainer) {
        loadItems();
    }

    const todoForm = document.getElementById("todoForm");
    if (todoForm) {
        todoForm.addEventListener("submit", handleFormSubmit);
    }

    // Listener for the static progress form submission
    const progressForm = document.getElementById("progressForm");
    if (progressForm) {
        progressForm.addEventListener("submit", handleProgressSubmit);
    }
});

async function loadItems() {
    const table = document.getElementById("todoList");

    if (!table) {
        console.error("Element with ID 'todoList' not found.");
        return;
    }

    table.innerHTML = "<li>Loading...</li>";

    try {
        const items = await apiRequest(apiBase);

        if (!items || items.length === 0) {
            table.innerHTML = "<li>No tasks found.</li>";
            return;
        }

        // Items must be ordered by Id
        items.sort((a, b) => a.id - b.id);

        table.innerHTML = items.map(item => {
            // Determine total progress to enforce update/delete rules
            const totalProgress = item.progressions && item.progressions.length > 0
                ? item.progressions[item.progressions.length - 1].accumulatedPercent
                : 0;
            const disableButtons = totalProgress > 50 ? 'disabled' : '';

            return `
                <li class="p-3 mb-3 border rounded ${item.isCompleted ? 'border-success' : 'border-secondary'}">
                    
                    <div class="task-header mb-2">
                        <strong>${item.id}) ${item.title}</strong> - ${item.description} 
                        (<span class="badge bg-secondary">${item.category}</span>) 
                        <span class="badge ${item.isCompleted ? 'bg-success' : 'bg-warning text-dark'}">
                            Completed: ${item.isCompleted ? 'True' : 'False'}
                        </span>
                    </div>

                    <div class="task-progress ps-3">
                        ${buildProgressDisplay(item.progressions)} 
                    </div>

                    <div class="task-actions mt-2">
                        <button onclick="updateItem(${item.id})" class="btn btn-warning btn-sm me-2" ${disableButtons}>
                            Update
                        </button>
                        <button onclick="deleteItem(${item.id})" class="btn btn-danger btn-sm" ${disableButtons}>
                            Delete
                        </button>
                    </div>
                </li>
            `;
        }).join("");
    } catch (error) {
        console.error("Error loading items:", error);
        table.innerHTML = "<li>Error loading tasks.</li>";
    }
}


// -------------------------------------------------
// Create item
// -------------------------------------------------
// -------------------------------------------------
// Create item
// -------------------------------------------------
async function handleFormSubmit(event) {
    event.preventDefault();

    const title = document.getElementById("title").value.trim();
    const description = document.getElementById("description").value.trim();
    const category = document.getElementById("category").value;

    if (!title) {
        alert("Title is required.");
        return;
    }

    try {
        const newId = await apiRequest(`${apiBase}/nextId`, "GET");

        const task = {
            id: newId,
            title,
            description,
            category
        };

        await apiRequest(apiBase, "POST", task);

        alert("Task saved successfully!");
        loadItems();
        event.target.reset();
    } catch (error) {
        console.error("Error saving task:", error);
        alert(`An error occurred while saving the task: ${error.message}`);
    }
}


// -------------------------------------------------
// Delete item
// -------------------------------------------------
async function deleteItem(id) {
    if (!confirm("Are you sure you want to delete this item?")) return;

    try {
        await apiRequest(`${apiBase}/${id}`, "DELETE");
        alert("Task deleted successfully!");
        loadItems();
    } catch (error) {
        console.error("Error deleting task:", error);
        if (error.message && error.message.includes("50%")) {
            alert(`Error deleting task: Cannot delete an item with more than 50% progress.`);
        } else {
            alert(`An error occurred while deleting the task: ${error.message}`);
        }
    }
}


// -------------------------------------------------
// Update item
// -------------------------------------------------
async function updateItem(id) {
    const newDescription = prompt("Enter new description for the task:");

    if (newDescription === null || newDescription.trim() === "") return;

    const updateData = { description: newDescription.trim() };

    try {
        await apiRequest(`${apiBase}/${id}`, "PUT", updateData);
        alert("Task updated successfully!");
        loadItems();
    } catch (error) {
        console.error("Error updating task:", error);

        if (error.message && error.message.includes("50% progress")) {
            alert(`Error updating task: Cannot update an item with more than 50% progress.`);
        } else {
            alert(`An error occurred while updating the task: ${error.message}`);
        }
    }
}