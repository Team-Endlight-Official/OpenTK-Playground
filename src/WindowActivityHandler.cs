
/// <summary>
/// Window activity manager that can load, unload and load new Activities. Convenient as a "Scene changer".
/// </summary>
public class WindowActivityHandler
{
    private readonly Stack<WindowActivity> activities;

    /// <summary>
    /// Window activity manager that can load, unload and load new Activities. Convenient as a "Scene changer".
    /// </summary>
    public WindowActivityHandler()
    {
        activities = new Stack<WindowActivity>();
    }

    /// <summary>
    /// Adds and Loads a new Window Activity.
    /// </summary>
    /// <param name="activity">: Desired activity to load</param>
    public void LoadActivity(WindowActivity activity)
    {
        activity.Load();
        activities.Push(activity);
    }

    /// <summary>
    /// Removes the current Window Activity.
    /// </summary>
    public void RemoveActivity()
    {
        activities.Pop();
    }

    /// <summary>
    /// Gets the current Window Activity.
    /// </summary>
    public WindowActivity GetActivity
    {
        get
        {
            return activities.Peek();
        }
    }
}
