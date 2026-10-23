using Raylib_cs;

public class TextureLibrary : IDisposable {
    private Dictionary<int, Texture2D> _textures = new();
    private Dictionary<int, string> _texturePaths = new();
    
    public void Load(int id, string path) {
        Texture2D texture = Raylib.LoadTexture(path);
        _texturePaths.Add(id, path);
        _textures[id] = texture;
    }
    
    public Texture2D Get(int id) {
        if(_textures.TryGetValue(id, out Texture2D texture)) {
            return texture;
        }
        
        throw new KeyNotFoundException(
            $"No texture registered for id {id}. Did you forget to call Load()?"
        );
    }
    
    public string GetTexturePath(int id) {
        if(_texturePaths.TryGetValue(id, out var value)) {
            return value;
        }
        
        return "";
    }
    
    public int GetNumTextures() {
        return _textures.Count;
    }
    
    public void Dispose() {
        foreach(Texture2D texture in _textures.Values) {
            Raylib.UnloadTexture(texture);
        }
        
        _textures.Clear();
    }
}