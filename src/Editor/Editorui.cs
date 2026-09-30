using ImGuiNET;

public class EditorUI {
    private readonly EmitterConfig _config;
    private static System.Numerics.Vector4 color;
    
    public EditorUI(EmitterConfig config) {
        _config = config;
    }
    
    public void Draw() {
        ImGui.Begin("Emitter Settings");
        
        ImGui.Text("Texture Path");
        ImGui.InputInt("Texture Index", ref _config.TextureID);
        
        ImGui.Text("Emission");
        ImGui.SliderFloat("Rate (particles/sec)", ref _config.EmissionRate, 0f, 200f);
 
        ImGui.Separator();
        ImGui.Text("Direction & Speed");
        // DragFloatRange2 is the ImGui widget purpose-built for a min/max
        // pair like this — it draws ONE slider with two draggable handles,
        // and guarantees Min never exceeds Max by construction. This is
        // exactly the widget raygui doesn't have, and the whole reason we
        // picked ImGui for a config shaped like this one.
        ImGui.DragFloatRange2("Angle (deg)", ref _config.AngleMin, ref _config.AngleMax, 1f, 0f, 360f);
        ImGui.DragFloatRange2("Speed", ref _config.SpeedMin, ref _config.SpeedMax, 1f, 0f, 500f);
 
        ImGui.Separator();
        ImGui.Text("Physics & Lifetime");
        ImGui.SliderFloat("Gravity", ref _config.Gravity, -200f, 200f);
        ImGui.DragFloatRange2("Lifetime (sec)", ref _config.LifetimeMin, ref _config.LifetimeMax, 0.05f, 0.1f, 10f);
        ImGui.DragFloatRange2("Size", ref _config.SizeMin, ref _config.SizeMax, 0.5f, 1f, 50f);
 
        ImGui.Separator();
        ImGui.Text("Color Over Lifetime");
        DrawColorEdit("Start Color", ref _config.StartR, ref _config.StartG, ref _config.StartB, ref _config.StartA);
        DrawColorEdit("End Color", ref _config.EndR, ref _config.EndG, ref _config.EndB, ref _config.EndA);
 
        ImGui.End();
    }
    
    private static void DrawColorEdit(string label, ref int r, ref int g, ref int b, ref int a) {
        color = new System.Numerics.Vector4(r/255f, g/255f, b/255f, a/255f);
        
        if(ImGui.ColorEdit4(label, ref color)) {
            r = (int)(color.X*255);
            g = (int)(color.Y*255);
            b = (int)(color.Z*255);
            a = (int)(color.W*255);
        }
    }
}