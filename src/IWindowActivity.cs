/// <summary>
/// A "Logic" for your OpenTK window. it's a small codebase where you can implement it into any logic you want/need.
/// </summary>
public interface IWindowActivity
{
    /// <summary>
    /// A Call for Load for your OpenTK Window.
    /// </summary>
    void ActivityLoad();

    /// <summary>
    /// A Call for Update for your OpenTK Window.
    /// </summary>
    /// <param name="time">: The FrameEventArgs Time</param>
    void ActivityUpdate(double time);

    /// <summary>
    /// A Call for Rendeer for your OpenTK Window.
    /// </summary>
    /// <param name="time">: The FrameEventArgs Time</param>
    void ActivityRender(double time);

    /// <summary>
    /// A Call for Unload for your OpenTK Window.
    /// </summary>
    void ActivityUnload();
}
