# DataPatrol
## Technical Assignment
### Task1
#### Part1: Asp.net core Restful API
Create an API with one endpoint (Method: GET), this API will return a json payload as follows:
```json {
“Data”: {
“Number”: 4
}
}
```

The API will generate a random number between 1 ~ 10, and send it back as a response.

--- 

#### Part2: Windows Form
Create a Windows Form application (.net framework v4.7.2) with one Form:
1. Two buttons (Start, Stop) which do the following:
    a. Start: will create a APIClient object with empty listener object list in a thread to call that
API every 10 seconds
    b. Stop: will stop and kill that thread
2. Two buttons (Register, Unregister):
    a. Register: will create a new listener and add it into the listener object list; this listener will
handle the payload from API, and check if the number equals the Target of the listener, if
so, the internal counter of that listener will be incremented.
    b. Unregister: will remove the selected listener from list
3. ListView: which will show the listener list on it.

### IMPORTANT: Use System.Text.Json library
### IMPORTANT: Use Design Patterns, OOP, Threads/Tasks; write advanced code