using System.Text.Json;

public static class JsonExporter {
    private static readonly JsonSerializerOptions Options = new() {
        WriteIndented = true,
        IncludeFields = true
    };
    
    public static void Save(EmitterConfig config, string path) {
        string json = JsonSerializer.Serialize(config, Options);
        File.WriteAllText(path, json);
    }
    
    public static EmitterConfig Load(string path) {
        if(!File.Exists(path))
            throw new FileNotFoundException($"No config file found at: {path}");
        
        string json = File.ReadAllText(path);
        EmitterConfig? config = JsonSerializer.Deserialize<EmitterConfig>(json, Options);
        
        return config ?? throw new InvalidDataException(
            $"Config file at {path} did not contain a valid EmitterConfig"
        );
    }
    
    public static void LoadInto(EmitterConfig targetConfig, string path) {
        EmitterConfig loaded = Load(path);
        CopyFields(loaded, targetConfig);
    }
    
    /// <summary>
    /// Copies every public field from source into target using reflection
    /// (GetFields()/GetValue()/SetValue()) instead of listing all ~20
    /// EmitterConfig fields by hand.
    /// </summary>
    public static void CopyFields(EmitterConfig source, EmitterConfig target) {
        foreach(var field in typeof(EmitterConfig).GetFields()) {
            field.SetValue(target, field.GetValue(source));
        }
    }
}