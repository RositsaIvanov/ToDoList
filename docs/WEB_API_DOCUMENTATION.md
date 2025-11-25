# TodoList Web API Documentation

## Overview
RESTful API for managing todo items with progress tracking. Built with ASP.NET Core, featuring Swagger/OpenAPI documentation and CORS support.

## Base URL
```
https://localhost:7126/api/ToDoList
```

## Authentication
Currently, the API does not require authentication (development mode).

---

## Endpoints

### 📋 Get All Items
Retrieves all todo items with their progress history.

**Endpoint:** `GET /api/ToDoList`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "title": "Implement authentication",
    "description": "Add JWT-based authentication",
    "category": "Work",
    "isCompleted": false,
    "progressions": [
      {
        "date": "2025-11-25T14:00:00Z",
        "percent": 25,
        "accumulatedPercent": 25
      },
      {
        "date": "2025-11-26T10:00:00Z",
        "percent": 30,
        "accumulatedPercent": 55
      }
    ]
  }
]
```

**cURL Example:**
```bash
curl -X GET "https://localhost:7126/api/ToDoList" -H "accept: application/json"
```

---

### 🔍 Get Item by ID
Retrieves a specific todo item.

**Endpoint:** `GET /api/ToDoList/{id}`

**Parameters:**
- `id` (path, required) - Integer ID of the todo item

**Response:** `200 OK`
```json
{
  "id": 1,
  "title": "Implement authentication",
  "description": "Add JWT-based authentication",
  "category": "Work",
  "isCompleted": false,
  "progressions": [...]
}
```

**Response:** `404 Not Found`
```json
"Item not found."
```

**cURL Example:**
```bash
curl -X GET "https://localhost:7126/api/ToDoList/1" -H "accept: application/json"
```

---

### 🆔 Get Next Available ID
Returns the next available ID for creating a new item.

**Endpoint:** `GET /api/ToDoList/nextId`

**Response:** `200 OK`
```json
5
```

**Usage:** Call this before creating a new item to get the appropriate ID.

**cURL Example:**
```bash
curl -X GET "https://localhost:7126/api/ToDoList/nextId" -H "accept: application/json"
```

---

### 🏷️ Get Categories
Retrieves all available task categories.

**Endpoint:** `GET /api/ToDoList/categories`

**Response:** `200 OK`
```json
[
  "Work",
  "Personal",
  "Shopping",
  "Health"
]
```

**cURL Example:**
```bash
curl -X GET "https://localhost:7126/api/ToDoList/categories" -H "accept: application/json"
```

---

### ✨ Create Item
Creates a new todo item.

**Endpoint:** `POST /api/ToDoList`

**Request Body:**
```json
{
  "id": 5,
  "title": "Buy groceries",
  "description": "Weekly shopping at the supermarket",
  "category": "Personal"
}
```

**Field Validation:**
- `id` (required) - Must be unique, use `/nextId` to get valid ID
- `title` (required) - Non-empty string
- `description` (optional) - String
- `category` (required) - Must match an existing category

**Response:** `201 Created`
```json
{
  "id": 5,
  "title": "Buy groceries",
  "description": "Weekly shopping at the supermarket",
  "category": "Personal"
}
```

**Response:** `400 Bad Request`
```json
"Validation error message"
```

**cURL Example:**
```bash
curl -X POST "https://localhost:7126/api/ToDoList" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 5,
    "title": "Buy groceries",
    "description": "Weekly shopping",
    "category": "Personal"
  }'
```

---

### ✏️ Update Item
Updates the description of an existing item.

**Endpoint:** `PUT /api/ToDoList/{id}`

**Parameters:**
- `id` (path, required) - ID of the item to update

**Request Body:**
```json
{
  "description": "Updated description text"
}
```

**Business Rules:**
- ⚠️ **Cannot update items with >50% progress**
- Only description can be updated (title and category are immutable)

**Response:** `200 OK`

**Response:** `400 Bad Request`
```json
"Cannot update an item with more than 50% progress"
```

**cURL Example:**
```bash
curl -X PUT "https://localhost:7126/api/ToDoList/1" \
  -H "Content-Type: application/json" \
  -d '{
    "description": "Add OAuth2 authentication with Google"
  }'
```

---

### 🗑️ Delete Item
Deletes a todo item.

**Endpoint:** `DELETE /api/ToDoList/{id}`

**Parameters:**
- `id` (path, required) - ID of the item to delete

**Business Rules:**
- ⚠️ **Cannot delete items with >50% progress**

**Response:** `200 OK`

**Response:** `400 Bad Request`
```json
"Cannot delete an item with more than 50% progress"
```

**cURL Example:**
```bash
curl -X DELETE "https://localhost:7126/api/ToDoList/1" \
  -H "accept: application/json"
```

---

### 📊 Register Progression
Adds a progress entry to a todo item.

**Endpoint:** `POST /api/ToDoList/{id}/progression`

**Parameters:**
- `id` (path, required) - ID of the item

**Request Body:**
```json
{
  "date": "2025-11-25T14:30:00Z",
  "percentage": 25
}
```

**Field Validation:**
- `date` (required) - ISO 8601 datetime format
- `percentage` (required) - Decimal between 1-100

**Progress Behavior:**
- Progress is **cumulative** (adds to previous total)
- When total reaches 100%, item is marked as `isCompleted: true`
- Each entry is stored with accumulated percentage

**Response:** `200 OK`

**Response:** `400 Bad Request`
```json
"Validation error or business rule violation"
```

**cURL Example:**
```bash
curl -X POST "https://localhost:7126/api/ToDoList/1/progression" \
  -H "Content-Type: application/json" \
  -d '{
    "date": "2025-11-25T14:30:00Z",
    "percentage": 25
  }'
```

---

## Data Models

### ToDoItem
```typescript
{
  id: number;              // Unique identifier
  title: string;           // Task title
  description: string;     // Detailed description
  category: string;        // Category name
  isCompleted: boolean;    // Auto-set when progress reaches 100%
  progressions: Progression[];  // Array of progress entries
}
```

### Progression
```typescript
{
  date: string;            // ISO 8601 datetime
  percent: number;         // Percentage added in this entry (1-100)
  accumulatedPercent: number;  // Total cumulative percentage
}
```

### CreateRequest
```typescript
{
  id: number;              // Get from /nextId endpoint
  title: string;           // Required
  description: string;     // Optional
  category: string;        // Must match existing category
}
```

### UpdateRequest
```typescript
{
  description: string;     // New description text
}
```

### RegisterProgressionRequest
```typescript
{
  date: string;            // ISO 8601 format
  percentage: number;      // 1-100
}
```

---

## HTTP Status Codes

| Code | Meaning | When Used |
|------|---------|-----------|
| 200 OK | Success | GET, PUT, DELETE successful |
| 201 Created | Resource created | POST successful |
| 400 Bad Request | Invalid input | Validation errors, business rule violations |
| 404 Not Found | Resource not found | GET by ID with invalid ID |

---

## Business Rules Summary

### 50% Progress Rule
Items with >50% accumulated progress cannot be:
- ❌ Updated (description changes blocked)
- ❌ Deleted (removal blocked)

**Rationale:** Prevents modification of substantially complete tasks

### Completion Rule
- When accumulated progress reaches 100%, `isCompleted` is automatically set to `true`
- Progress entries continue to be tracked even after completion

### Category Validation
- Categories must exist in the system
- Use `/categories` endpoint to get valid options
- Categories are case-sensitive

---

## CORS Configuration

The API is configured to accept requests from:
```
https://localhost:7266
```

**Headers Allowed:**
- All headers (`AllowAnyHeader`)

**Methods Allowed:**
- All HTTP methods (`AllowAnyMethod`)

---

## Swagger/OpenAPI

### Accessing Swagger UI
When running in development mode:
```
https://localhost:7126/swagger
```

### Features
- Interactive API testing
- Request/response examples
- Schema definitions
- Try-it-out functionality

### Using Swagger UI
1. Navigate to the Swagger URL
2. Expand any endpoint
3. Click "Try it out"
4. Fill in parameters/request body
5. Click "Execute"
6. View response

---

## Example Workflows

### Creating a Complete Task

**Step 1:** Get next available ID
```bash
GET /api/ToDoList/nextId
# Response: 10
```

**Step 2:** Get available categories
```bash
GET /api/ToDoList/categories
# Response: ["Work", "Personal", "Shopping", "Health"]
```

**Step 3:** Create the item
```bash
POST /api/ToDoList
{
  "id": 10,
  "title": "Complete project documentation",
  "description": "Write user guides and API docs",
  "category": "Work"
}
```

**Step 4:** Register progress (25%)
```bash
POST /api/ToDoList/10/progression
{
  "date": "2025-11-25T10:00:00Z",
  "percentage": 25
}
```

**Step 5:** Register more progress (50%)
```bash
POST /api/ToDoList/10/progression
{
  "date": "2025-11-25T15:00:00Z",
  "percentage": 50
}
# Total progress now: 75%
```

**Step 6:** Complete the task (25%)
```bash
POST /api/ToDoList/10/progression
{
  "date": "2025-11-25T18:00:00Z",
  "percentage": 25
}
# Total progress: 100%, isCompleted: true
```

---

## Error Handling

### Common Errors

**Invalid Category:**
```json
// Request
POST /api/ToDoList
{
  "id": 1,
  "title": "Test",
  "category": "InvalidCategory"
}

// Response: 400 Bad Request
"Category 'InvalidCategory' does not exist"
```

**Duplicate ID:**
```json
// Response: 400 Bad Request
"Item with ID 1 already exists"
```

**Update >50% Progress:**
```json
// Response: 400 Bad Request
"Cannot update an item with more than 50% progress"
```

**Invalid Percentage:**
```json
// Response: 400 Bad Request
"Percentage must be between 1 and 100"
```

---

## Testing with Postman

### Setup
1. Import the Swagger JSON from `/swagger/v1/swagger.json`
2. Set base URL: `https://localhost:7126/api/ToDoList`
3. Disable SSL verification (development only)

### Collection Structure
```
TodoList API
├── Get All Items
├── Get Item by ID
├── Get Next ID
├── Get Categories
├── Create Item
├── Update Item
├── Delete Item
└── Register Progression
```

---

## Performance Considerations

- **In-Memory Storage**: Data is not persisted between application restarts
- **No Pagination**: All items returned in single request
- **Synchronous Operations**: All endpoints execute synchronously

---

## Security Notes

> [!WARNING]
> This API is configured for development use only

**Current Limitations:**
- No authentication/authorization
- CORS allows specific origin only
- HTTPS required
- No rate limiting

**Production Recommendations:**
- Implement JWT authentication
- Add API key validation
- Configure production CORS policy
- Add rate limiting middleware
- Enable request logging
- Implement data persistence

---

## Support & Resources

- **Swagger UI**: `https://localhost:7126/swagger`
- **OpenAPI Spec**: `https://localhost:7126/swagger/v1/swagger.json`
- **Source Code**: Check the `ToDoList.WebApi` project
