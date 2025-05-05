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
    private GameWindow m_Window;
    private GameWindowSettings m_GameWindowSettings;
    private NativeWindowSettings m_NativeWindowSettings;

    // 
    private int m_Width = 800;
    private int m_Height = 600;
    private const int m_Depth = 24;

    private bool m_VSync = false;
    private bool m_Fullscreen = false;

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
        m_Width = width;
        m_Height = height;
        m_VSync = vsync;
        m_Fullscreen = fullscreen;

        // Get Primary Monitor Information
        MonitorInfo info = Monitors.GetPrimaryMonitor();

        // Default Window Settings
        m_NativeWindowSettings = new NativeWindowSettings();
        m_NativeWindowSettings.API = ContextAPI.OpenGL;
        m_NativeWindowSettings.APIVersion = new Version(3, 3);
        m_NativeWindowSettings.Profile = ContextProfile.Core;
        m_NativeWindowSettings.DepthBits = m_Depth;
        m_NativeWindowSettings.Flags = ContextFlags.Default;
        m_NativeWindowSettings.SrgbCapable = false;
        m_NativeWindowSettings.StartVisible = false;
        // Set Window Stats the same as the primary monitor ones
        m_NativeWindowSettings.RedBits = info.CurrentVideoMode.RedBits;
        m_NativeWindowSettings.GreenBits = info.CurrentVideoMode.GreenBits;
        m_NativeWindowSettings.BlueBits = info.CurrentVideoMode.BlueBits;

        // Window Title and Sizing
        m_NativeWindowSettings.Title = title;
        m_NativeWindowSettings.MinimumClientSize = new Vector2i(640, 480);
        m_NativeWindowSettings.ClientSize = new Vector2i(m_Width, m_Height);

        // GL Rendering Window Settings
        m_GameWindowSettings = new GameWindowSettings();
        m_GameWindowSettings.UpdateFrequency = info.CurrentVideoMode.RefreshRate;
        m_GameWindowSettings.Win32SuspendTimerOnDrag = true;

        // Window Creation
        m_Window = new GameWindow(m_GameWindowSettings, m_NativeWindowSettings);
        m_Window.VSync = vsync ? VSyncMode.Off : VSyncMode.On;
    }

    /// <summary>
    /// Starts the Window.
    /// </summary>
    public void Run()
    {
        m_Window.Load += OnLoad;
        m_Window.FramebufferResize += OnFramebufferResize;
        m_Window.Resize += OnResize;
        m_Window.UpdateFrame += OnUpdate;
        m_Window.RenderFrame += OnRender;
        m_Window.Unload += OnUnload;

        m_Window.Run();
    }

    private void OnLoad()
    {
        m_Window.IsVisible = true;
        Console.WriteLine("Window has been loaded.");
        Console.WriteLine($"Window Information:\nWidth: {m_Width}\nHeight: {m_Height}\nVSync: {m_VSync}\n\nOpenGL Information: \nVendor: {GL.GetString(StringName.Vendor)}\nOpenGL Version: {GL.GetString(StringName.Version)}\nGLSL Version: {GL.GetString(StringName.ShadingLanguageVersion)}");
    }

    private void OnUnload()
    {
        Console.WriteLine("Window has been closed.");
    }

    private void OnUpdate(FrameEventArgs args)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape)) Close();
    }

    private void OnRender(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(0.15f, 0.15f, 0.15f, 1.0f);

        m_Window.SwapBuffers();
    }

    private void OnFramebufferResize(FramebufferResizeEventArgs args)
    {
        m_Width = args.Width;
        m_Height = args.Height;
        GL.Viewport(0, 0, m_Width, m_Height);
    }

    private void OnResize(ResizeEventArgs args)
    {
        m_Width = args.Width;
        m_Height = args.Height;
        GL.Viewport(0, 0, m_Width, m_Height);
    }

    // Public Methods
    
    /// <summary>
    /// Closes the Window on call.
    /// </summary>
    public void Close()
    {
        m_Window.Close();
    }

    /// <summary>
    /// Sets a Title for the Window.
    /// </summary>
    /// <param name="newTitle">: Title for the Window.</param>
    public void SetTitle(string newTitle)
    {
        m_Window.Title = newTitle;
    }

    /// <summary>
    /// Sets a Size for the Window.
    /// </summary>
    /// <param name="width">: Width of the Window.</param>
    /// <param name="height">: Height of the Window.</param>
    public void SetSize(int width, int height)
    {
        m_Window.ClientSize = new Vector2i(width, height);
    }

    // Getters

    /// <summary>
    /// Gets the aspect ratio.
    /// </summary>
    public float AspectRatio
    {
        get
        {
            return (float)m_Width / (float)m_Height;
        }
    }

    /// <summary>
    /// Checks whether the window is running.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            return m_Window.Exists;
        }
    }

    /// <summary>
    /// Gets the Keyboard of the window.
    /// </summary>
    public KeyboardState Keyboard
    {
        get
        {
            return m_Window.KeyboardState;
        }
    }
}
