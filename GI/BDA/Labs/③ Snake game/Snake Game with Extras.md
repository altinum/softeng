> [!NOTE]
> These tasks are just extras for individual practice; they are not mandatory to complete. 

## Poisons and Apples - Individual Tasks

If you place food and poisons in the form’s controls list, the approach of using `Controls.RemoveAt(0);` to remove the oldest element will become ineffective. To solve this issue, store `SnakeSegment` objects representing the snake’s body in a separate list. Create a list of `SnakeSegment` objects at the class level in `Form1`:

```c#
public partial class Form1 : Form
{
    int directonX = 1;
    int directonY = 0;
    int headX = 100;
    int headY = 100;
    int stepCounter = 0;
    int snakeLength = 5;

    List<SnakeSegment> snake1 = new List<SnakeSegment>();
    ...
```

When a new head is added to the snake, it should be added both to the `snake` list and the form’s control list:

``` csharp
// Grow the head
head_x += direction_x * SnakeSegment.Size;
head_y += direction_y * SnakeSegment.Size;   
SnakeSegment se = new SnakeSegment();
snake.Add(se); // Add the new head to the `snake` list...
Controls.Add(se); // ...and to the form's controls list
```

During tail trimming, determine which `SnakeSegment` to remove based on the oldest element in the `snake` list. Collision detection is also handled differently:

```c#
private void timer1_Tick(object sender, EventArgs e)
{
    headX += directonX * SnakeSegment.Size;
    headY += directonY * SnakeSegment.Size;

    foreach (SnakeSegment item in snake1)
    {
        if (item.Top == headY && item.Left == headX)
        {
            timer1.Enabled = false;
        }
    }

    SnakeSegment ss = new SnakeSegment(stepCounter);

    ss.Top = headY;
    ss.Left = headX;

    Controls.Add(ss);
    snake1.Add(ss);

    stepCounter++;

    if (snake1.Count > snakeLength)
    {
        SnakeSegment segmentToBeCut = snake1[0];
        snake1.RemoveAt(0);
        Controls.Remove(segmentToBeCut);
    }
}
```

Now you can create new classes for food and poison, which can be placed randomly on the form at intervals.

