using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

/// <summary>
/// A Base Class for the Window that supports OpenGL
/// </summary>
public class Window
{
    // Windowing Components
    private GameWindow m_window;
    private GameWindowSettings m_gameWindowSettings;
    private NativeWindowSettings m_nativeWindowSettings;

    private IWindowActivity m_windowActivity;

    // 
    private int m_width = 800;
    private int m_height = 600;
    private const int m_depthBits = 24;

    private bool m_vsync = false;
    private bool m_fullscreen = false;

    /// <summary>
    /// A Base Class for the Window that supports OpenGL
    /// </summary>
    /// <param name="width">: The Width of the Window</param>
    /// <param name="height">: The Height of the Window</param>
    /// <param name="title">: The Title of the Window (OPTIONAL)</param>
    /// <param name="vsync">: VSync? (OPTIONAL)</param>
    /// <param name="fullscreen">: Fullscreen (OPTIONAL)</param>
    public Window(int width, int height, string title = "Default Title", bool vsync = false, bool fullscreen = false)
    {
        m_width = width;
        m_height = height;
        m_vsync = vsync;
        m_fullscreen = fullscreen;

        // Get Primary Monitor Information
        MonitorInfo info = Monitors.GetPrimaryMonitor();

        // Default Window Settings
        m_nativeWindowSettings = new NativeWindowSettings();
        m_nativeWindowSettings.API = ContextAPI.OpenGL;
        m_nativeWindowSettings.APIVersion = new Version(3, 3);
        m_nativeWindowSettings.Profile = ContextProfile.Core;
        m_nativeWindowSettings.DepthBits = m_depthBits;
        m_nativeWindowSettings.Flags = ContextFlags.Default;
        m_nativeWindowSettings.SrgbCapable = false;
        m_nativeWindowSettings.StartVisible = false;
        // Set Window Stats the same as the primary monitor ones
        m_nativeWindowSettings.RedBits = info.CurrentVideoMode.RedBits;
        m_nativeWindowSettings.GreenBits = info.CurrentVideoMode.GreenBits;
        m_nativeWindowSettings.BlueBits = info.CurrentVideoMode.BlueBits;

        // Window Title and Sizing
        m_nativeWindowSettings.Title = title;
        m_nativeWindowSettings.MinimumClientSize = new Vector2i(640, 480);
        m_nativeWindowSettings.ClientSize = new Vector2i(m_width, m_height);

        // GL Rendering Window Settings
        m_gameWindowSettings = new GameWindowSettings();
        m_gameWindowSettings.UpdateFrequency = info.CurrentVideoMode.RefreshRate;
        m_gameWindowSettings.Win32SuspendTimerOnDrag = true;

        // Window Creation
        m_window = new GameWindow(m_gameWindowSettings, m_nativeWindowSettings);
        m_window.VSync = vsync ? VSyncMode.Off : VSyncMode.On;
    }

    /// <summary>
    /// Starts the Window.
    /// </summary>
    public void Run()
    {
        m_window.Load += OnLoad;
        m_window.Resize += OnResize;
        m_window.UpdateFrame += OnUpdate;
        m_window.RenderFrame += OnRender;
        m_window.Unload += OnUnload;

        m_window.Run();
    }

    private void OnLoad()
    {
        if (m_windowActivity != null) m_windowActivity.ActivityLoad();
        else Utils.Log("No Window Activity has been defined.\n\n", ConsoleColor.Red);

        m_window.IsVisible = true;
        m_window.CenterWindow();
        Utils.Log("Window has been loaded.");
        Utils.Log($"Window Information:\nWidth: {m_width}\nHeight: {m_height}\nVSync: {m_vsync}\n\nOpenGL Information: \nVendor: {GL.GetString(StringName.Vendor)}\nOpenGL Version: {GL.GetString(StringName.Version)}\nGLSL Version: {GL.GetString(StringName.ShadingLanguageVersion)}", ConsoleColor.Yellow);
    }

    private void OnUnload()
    {
        if (m_windowActivity != null) m_windowActivity.ActivityUnload();

        Console.WriteLine("Window has been closed.");
    }

    private void OnUpdate(FrameEventArgs args)
    {
        if (m_windowActivity != null) m_windowActivity.ActivityUpdate(args.Time);
        
        if (Keyboard.IsKeyPressed(Keys.Escape)) Close();
    }

    private void OnRender(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.ClearColor(0.15f, 0.15f, 0.15f, 1.0f);

        if (m_windowActivity != null) m_windowActivity.ActivityRender(args.Time);

        m_window.SwapBuffers();
    }

    private void OnResize(ResizeEventArgs args)
    {
        m_width = args.Width;
        m_height = args.Height;
        GL.Viewport(0, 0, m_width, m_height);
    }

    // Public Methods
    
    /// <summary>
    /// Closes the Window on call.
    /// </summary>
    public void Close()
    {
        m_window.Close();
    }

    /// <summary>
    /// Sets a Title for the Window.
    /// </summary>
    /// <param name="newTitle">: Title for the Window.</param>
    public void SetTitle(string newTitle)
    {
        m_window.Title = newTitle;
    }

    /// <summary>
    /// Sets a Size for the Window.
    /// </summary>
    /// <param name="width">: Width of the Window.</param>
    /// <param name="height">: Height of the Window.</param>
    public void SetSize(int width, int height)
    {
        m_window.ClientSize = new Vector2i(width, height);
    }

    /// <summary>
    /// Sets your Windowing Activity. It's basically a "Game" logic for OpenTK to Comprehend.
    /// </summary>
    /// <param name="activity"></param>
    public void SetWindowActivity(IWindowActivity activity)
    {
        m_windowActivity = activity;
    }

    // Getters

    /// <summary>
    /// Gets the aspect ratio.
    /// </summary>
    public float AspectRatio
    {
        get
        {
            return (float)m_width / (float)m_height;
        }
    }

    /// <summary>
    /// Checks whether the window is running.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            return m_window.Exists;
        }
    }

    /// <summary>
    /// Gets the Keyboard of the window.
    /// </summary>
    public KeyboardState Keyboard
    {
        get
        {
            return m_window.KeyboardState;
        }
    }
}
