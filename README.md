C# Learnings
**Key Points:**

Task firstTask:
Created using the constructor new Task().
Tasks created this way require explicit starting with Start() to begin execution. Until you call firstTask.Start(), it remains in the Created state and does not run.

Task secondTask and Task thirdTask:
Created by directly calling the asynchronous method ConsoleAfterDelayAsync.
These tasks are already started when the method ConsoleAfterDelayAsync is invoked because asynchronous methods return hot tasks (tasks that begin running immediately).
Attempting to call Start() on a hot task throws an exception (InvalidOperationException) because the task is already running or completed.

**Why firstTask.Start() Works:**
When you call firstTask.Start(), you manually move it from the Created state to the Running state. The task runs as expected, and once completed, it transitions to the RanToCompletion state.

**Why secondTask.Start() Fails:**
secondTask is created by calling the asynchronous method ConsoleAfterDelayAsync. This method starts executing immediately and returns a Task representing its execution. Since the task is already started (hot), calling Start() again is invalid and causes the exception.

**What Happens When firstTask.Start() Is Not Called:**
If you do not call firstTask.Start(), it remains in the Created state and will never execute. The program continues executing secondTask and thirdTask because these are already hot tasks and are either running or awaiting completion.

**Execution Order:**
Tasks in your code execute asynchronously. The order of execution depends on when they are started and how long they take to complete. For example:
Console.WriteLine("After the Task was created...") runs before any task completes because it is synchronous and does not depend on any task.
The await statements ensure that subsequent code waits for the respective task to finish before continuing, preserving logical order.

**Key Takeaways:**
Cold vs. Hot Tasks:
Cold tasks (created with new Task()) require explicit Start() to begin execution.
Hot tasks (from methods like Task.Run or async methods returning Task) start immediately and do not require (or allow) Start().
Exception on Start():
Calling Start() on an already-started task (hot task) throws InvalidOperationException.
Mixing Task Styles:
Be cautious when mixing cold and hot tasks in the same program. They behave differently and can lead to confusion or errors like the one you encountered.

**Suggested Fix:**
To simplify your code and avoid such issues, use consistent task creation patterns. For example, replace new Task() with Task.Run:
csharp
CopyEdit
Task firstTask = Task.Run(() =>
{
    Thread.Sleep(1000);
    Console.WriteLine("Task 1");
});

This ensures all tasks are hot and do not require Start(). Your program will then execute as expected without the risk of mismanaging task states.