using Raylib_cs;

public class OffscreenRenderer : IDisposable {
    private RenderTexture2D _target;
    public int Width {get;}
    public int Height {get;}
    
    public OffscreenRenderer(int width, int height) {
        Width = width;
        Height = height;
        _target = Raylib.LoadRenderTexture(width, height);
    }
    
    public Image CaptureFrame(Action drawCall) {
        Raylib.BeginTextureMode(_target);
        Raylib.ClearBackground(Color.Blank);
        drawCall();
        Raylib.EndTextureMode();
        
        Image image = Raylib.LoadImageFromTexture(_target.Texture);
        Raylib.ImageFlipVertical(ref image);
        
        return image;
    }
    
    public void Dispose() {
        Raylib.UnloadRenderTexture(_target);
    }
}