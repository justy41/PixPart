using Raylib_cs;

public class ParticleRenderer {
    private TextureLibrary _texture;
    
    public ParticleRenderer(TextureLibrary textures) {
        _texture = textures;
    }
    
    public void Draw(ReadOnlySpan<Particle> particles) {
        foreach(Particle p in particles) {
            DrawOne(p);
        }
    }
    
    public void DrawOne(Particle p) {
        Texture2D texture = _texture.Get(Math.Clamp(p.TextureID, 0, _texture.GetNumTextures()-1));
        
        var source = new Rectangle(0, 0, texture.Width, texture.Height);
        var dest = new Rectangle(p.X, p.Y, p.Size, p.Size);
        var origin = new System.Numerics.Vector2(p.Size/2, p.Size/2);
        Color tint = LerpColor(p);
        
        Raylib.DrawTexturePro(texture, source, dest, origin, p.Rotation, tint);
    }
    
    private static Color LerpColor(Particle p) {
        float t = p.AgeFraction();
        
        int r = LerpInt(p.R0, p.R1, t);
        int g = LerpInt(p.G0, p.G1, t);
        int b = LerpInt(p.B0, p.B1, t);
        int a = LerpInt(p.A0, p.A1, t);
        
        return new Color(r, g, b, a);
    }
    
    private static int LerpInt(int a, int b, float t) {
        float result = a+(b-a)*t;
        return (int)Math.Clamp(result, 0, 255);
    }
}