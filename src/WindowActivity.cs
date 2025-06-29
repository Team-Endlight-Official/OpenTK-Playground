using OpenTK.Windowing.Common;

/// <summary>
/// A abstract class for the Window Activity that handles everything from the Window class!
/// </summary>
public abstract class WindowActivity
{
    /// <summary>
    /// The Window context defined in the base constructor.
    /// </summary>
    protected readonly Window Context;

    /// <summary>
    /// The Window Activity Handler of the defined Context.
    /// </summary>
    protected readonly WindowActivityHandler WindowActivityHandler;

    /// <summary>
    /// The Aspect ratio of the Context.
    /// </summary>
    public float AspectRatio
    {
        get
        {
            if (Context != null) return Context.AspectRatio;
            return 0f;
        }
    }

    public Window Window
    {
        get
        {
            return Context;
        }
    }

    /// <summary>
    /// A abstract class for the Window Activity that handles everything from the Window class!
    /// </summary>
    /// <param name="context">: The window context.</param>
    public WindowActivity(Window context)
    {
        Context = context;
        this.WindowActivityHandler = context.WindowActivityHandler;
    }

    /// <summary>
    /// The load method, is a must for loading resources.
    /// </summary>
    public abstract void Load();
    /// <summary>
    /// The update method, managing updated calculations. NOT FOR DRAWING.
    /// </summary>
    /// <param name="args">: The Frame event arguments</param>
    public abstract void Update(FrameEventArgs args);
    /// <summary>
    /// The render method, managing rendering operation. NOT FOR UPDATING.
    /// </summary>
    /// <param name="args">: The Frame event arguments</param>
    public abstract void Render(FrameEventArgs args);
    /// <summary>
    /// The unload method, is a must have for freeing resources.
    /// </summary>
    public abstract void Unload();
}
