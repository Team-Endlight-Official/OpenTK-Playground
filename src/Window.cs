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
    private GameWindow _window;
    private GameWindowSettings _gameWindowSettings;
    private NativeWindowSettings _nativeWindowSettings;

    private WindowActivityHandler _windowActivityHandler;

    // 
    private int _width = 800;
    private int _height = 600;
    private const int _depthBits = 24;

    private bool _vsync = false;
    private bool _fullscreen = false;

    /// <summary>
    /// A Base Class for the Window that supports OpenGL
    /// </summary>
    /// <param name="width">The Width of the Window</param>
    /// <param name="height">The Height of the Window</param>
    /// <param name="title">The Title of the Window (OPTIONAL)</param>
    /// <param name="vsync">VSync? (OPTIONAL)</param>
    /// <param name="fullscreen">Fullscreen (OPTIONAL)</param>
    public Window(int width, int height, string title = "Default Title", bool vsync = false, bool fullscreen = false)
    {
        _width = width;
        _height = height;
        _vsync = vsync;
        _fullscreen = fullscreen;

        // Get Primary Monitor Information
        MonitorInfo info = Monitors.GetPrimaryMonitor();

        // Default Window Settings
        _nativeWindowSettings = new NativeWindowSettings();
        _nativeWindowSettings.API = ContextAPI.OpenGL;
        _nativeWindowSettings.APIVersion = new Version(3, 3);
        _nativeWindowSettings.Profile = ContextProfile.Core;
        _nativeWindowSettings.DepthBits = _depthBits;
        _nativeWindowSettings.Flags = ContextFlags.Default;
        _nativeWindowSettings.SrgbCapable = false;
        _nativeWindowSettings.StartVisible = false;
        // Set Window Stats the same as the primary monitor ones
        _nativeWindowSettings.RedBits = info.CurrentVideoMode.RedBits;
        _nativeWindowSettings.GreenBits = info.CurrentVideoMode.GreenBits;
        _nativeWindowSettings.BlueBits = info.CurrentVideoMode.BlueBits;

        // Window Title and Sizing
        _nativeWindowSettings.Title = title;
        _nativeWindowSettings.MinimumClientSize = new Vector2i(640, 480);
        _nativeWindowSettings.ClientSize = new Vector2i(_width, _height);

        // GL Rendering Window Settings
        _gameWindowSettings = new GameWindowSettings();
        _gameWindowSettings.UpdateFrequency = info.CurrentVideoMode.RefreshRate;
        _gameWindowSettings.Win32SuspendTimerOnDrag = true;

        // Window Creation
        _window = new GameWindow(_gameWindowSettings, _nativeWindowSettings);
        _window.VSync = vsync ? VSyncMode.Off : VSyncMode.On;

        _windowActivityHandler = new WindowActivityHandler();
    }

    /// <summary>
    /// Starts the Window.
    /// </summary>
    public void Run()
    {
        _window.Load += OnLoad;
        _window.Resize += OnResize;
        _window.UpdateFrame += OnUpdate;
        _window.RenderFrame += OnRender;
        _window.Unload += OnUnload;

        _window.Run();
    }

    private void OnLoad()
    {
        _windowActivityHandler.LoadActivity(new TestActivity(this));

        _window.IsVisible = true;
        _window.CenterWindow();
        Utils.Log("Window has been loaded.");
        Utils.Log($"Window Information:\nWidth: {_width}\nHeight: {_height}\nVSync: {_vsync}\n\nOpenGL Information: \nVendor: {GL.GetString(StringName.Vendor)}\nOpenGL Version: {GL.GetString(StringName.Version)}\nGLSL Version: {GL.GetString(StringName.ShadingLanguageVersion)}", ConsoleColor.Yellow);
    }

    private void OnUnload()
    {
        _windowActivityHandler.GetActivity.Unload();

        Console.WriteLine("Window has been closed.");
    }

    private void OnUpdate(FrameEventArgs args)
    {
        _windowActivityHandler.GetActivity.Update(args);

        if (Keyboard.IsKeyPressed(Keys.Escape)) Close();
    }

    private void OnRender(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.ClearColor(0.15f, 0.15f, 0.15f, 1.0f);

        _windowActivityHandler.GetActivity.Render(args);

        _window.SwapBuffers();
    }

    private void OnResize(ResizeEventArgs args)
    {
        _width = args.Width;
        _height = args.Height;
        GL.Viewport(0, 0, _width, _height);
    }

    // Public Methods
    
    /// <summary>
    /// Closes the Window on call.
    /// </summary>
    public void Close()
    {
        _window.Close();
    }

    /// <summary>
    /// Sets a Title for the Window.
    /// </summary>
    /// <param name="newTitle">Title for the Window.</param>
    public void SetTitle(string newTitle)
    {
        _window.Title = newTitle;
    }

    /// <summary>
    /// Sets a Size for the Window.
    /// </summary>
    /// <param name="width">Width of the Window.</param>
    /// <param name="height">Height of the Window.</param>
    public void SetSize(int width, int height)
    {
        _window.ClientSize = new Vector2i(width, height);
    }

    /// <summary>
    /// Gets the window activity handler.
    /// </summary>
    public WindowActivityHandler WindowActivityHandler
    {
        get
        {
            return _windowActivityHandler;
        }
    }

    // Getters

    /// <summary>
    /// Gets the aspect ratio.
    /// </summary>
    public float AspectRatio
    {
        get
        {
            return (float)_width / (float)_height;
        }
    }

    /// <summary>
    /// Checks whether the window is running.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            return _window.Exists;
        }
    }

    /// <summary>
    /// Gets the Keyboard of the window.
    /// </summary>
    public KeyboardState Keyboard
    {
        get
        {
            return _window.KeyboardState;
        }
    }

    /// <summary>
    /// Gets the Mouse of the window.
    /// </summary>
    public MouseState Mouse
    {
        get
        {
            return _window.MouseState;
        }
    }
}
