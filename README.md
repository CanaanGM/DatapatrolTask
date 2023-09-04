# Datapatrol task
> a winform app with a simple api.



## API
- used minimal controllers to keep it minimal . . . 
- Result and Data classes are in the same file as Program
    - adding 2 more files for 3 lines of code doesn't make sense here
### Endpoints
- GET generateRandomNumber
    - return a random number from 1 - 10

---

## Front End
> reading the code will make much more sense than me explaing here
but i'll highlight some of my desicions

 > unlike in the API, in here i've made different files for the small classes so they don't pollute the Listener class. 

> instead of custom error handling i'm just logging to the console

 
### Buttons
> all 4 buttons utilize threads in a safe manner ; no thread messes with what the other is creating/managing.

- StartButton
    - **Enables** the other buttons and **disables** itself and the text input.
    - initializes an instance of ***Listener*** and make it start listening in a thread.
    - adds the new thread to ***activeList*** thread list, which **keeps track** of all active threads.


- RegisterButton
    -  initializes an instance of ***Listener*** and make it start listening in a thread.
    - adds the new thread to ***activeList*** thread list, which **keeps track** of all active threads.

- UnregisterButton
    - gets the selected Listener's *name* from the Listeners list view.
    - tries to get it from a **listenerListViewMap** a `dict<string, Listener>`.
    - stops it from listening , removes it from the listview, the lookup dict and the activeListeners list.

- StopButton
    > should reset the state of the app to the initial state
    
    - stops all active listeners.
    - clears both lists and the lookup dict.
    - resets the button's initial state.


### ListView

- onload the threads populate it with their data
- updates are done by calling it's update method from CounterChanged event invoked from the Listenr class.
 

### TextInput

- gets locked when the app starts
- intial text is the address of the api (time saver)

 ---

 ### Classes

 ### Listener

 > i thought of splitting it to different classes but decided not to, as the way it is now makes it a "complete" class ; 
 all functionality -aside from retries- strongly relate to it.


 #### properities / fields

 - Name `string` :  a random name generated from monster names from [Monster Hunter](https://en.wikipedia.org/wiki/Monster_Hunter)
 - Target `int` : a random number **from 1 - 10**, for matching with what the API returns
 - Counter `int`: how many times the **returned** number from the API *matched* **Target**
 - CounterChanged `event` : get's invoked when the counter increases

 - cancellationTokenSource `CancellationTokenSource`
 - threadStoppedEvent `ManualResetEvent` : to gracefully kill the thread 

 - MaxRetries `int` : how many times to retry contact with the API if a problem occurs
 - MinDelaySeconds `int` 
 - MaxDelaySeconds `int`

 --- 
 #### Methods

 - **StartMonitoringAsync**
    - every 10 secs , it calls **MakeApiCallWithRetryAsync**

- **MakeApiCallWithRetryAsync**
    - Contact the API 
    - Parses the data
    - checks for a match an on it invokes `CounterChanged` with the new counter
    - retries on failure for up to **MaxRetries**

- **StopMonitoring**
    - stops the running thread with a grace period of 10 secs

- **Dispose**
    - Same with the deconstructor safly stops the thread "diposes" of the evidence
    - the perfect crime !

--- 

### DataObject

> holds the payload from the API

- Number `int`

---

### ApiResponce

> the reponse object from the Api

- Data `DataObject` 



--- 

