using System.Numerics;
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
    
    public static void SetupCatppuccinMochaTheme() {
        ImGuiStylePtr style = ImGui.GetStyle();

        // Catppuccin Mocha Palette
        // --------------------------------------------------------
        Vector4 baseColor = new(0.117f, 0.117f, 0.172f, 1.0f); // #1e1e2e
        Vector4 mantle     = new(0.109f, 0.109f, 0.156f, 1.0f); // #181825
        Vector4 surface0   = new(0.200f, 0.207f, 0.286f, 1.0f); // #313244
        Vector4 surface1   = new(0.247f, 0.254f, 0.337f, 1.0f); // #3f4056
        Vector4 surface2   = new(0.290f, 0.301f, 0.388f, 1.0f); // #4a4d63
        Vector4 overlay0   = new(0.396f, 0.403f, 0.486f, 1.0f); // #65677c
        Vector4 overlay2   = new(0.576f, 0.584f, 0.654f, 1.0f); // #9399b2
        Vector4 text       = new(0.803f, 0.815f, 0.878f, 1.0f); // #cdd6f4
        Vector4 subtext0   = new(0.639f, 0.658f, 0.764f, 1.0f); // #a3a8c3
        Vector4 mauve      = new(0.796f, 0.698f, 0.972f, 1.0f); // #cba6f7
        Vector4 peach      = new(0.980f, 0.709f, 0.572f, 1.0f); // #fab387
        Vector4 yellow     = new(0.980f, 0.913f, 0.596f, 1.0f); // #f9e2af
        Vector4 green      = new(0.650f, 0.890f, 0.631f, 1.0f); // #a6e3a1
        Vector4 teal       = new(0.580f, 0.886f, 0.819f, 1.0f); // #94e2d5
        Vector4 sapphire   = new(0.458f, 0.784f, 0.878f, 1.0f); // #74c7ec
        Vector4 blue       = new(0.533f, 0.698f, 0.976f, 1.0f); // #89b4fa
        Vector4 lavender   = new(0.709f, 0.764f, 0.980f, 1.0f); // #b4befe

        // Main window and backgrounds
        style.Colors[(int)ImGuiCol.WindowBg]              = baseColor;
        style.Colors[(int)ImGuiCol.ChildBg]               = baseColor;
        style.Colors[(int)ImGuiCol.PopupBg]               = surface0;
        style.Colors[(int)ImGuiCol.Border]                = surface1;
        style.Colors[(int)ImGuiCol.BorderShadow]          = new Vector4(0, 0, 0, 0);

        style.Colors[(int)ImGuiCol.FrameBg]               = surface0;
        style.Colors[(int)ImGuiCol.FrameBgHovered]        = surface1;
        style.Colors[(int)ImGuiCol.FrameBgActive]         = surface2;

        style.Colors[(int)ImGuiCol.TitleBg]               = mantle;
        style.Colors[(int)ImGuiCol.TitleBgActive]         = surface0;
        style.Colors[(int)ImGuiCol.TitleBgCollapsed]      = mantle;
        style.Colors[(int)ImGuiCol.MenuBarBg]             = mantle;

        style.Colors[(int)ImGuiCol.ScrollbarBg]           = surface0;
        style.Colors[(int)ImGuiCol.ScrollbarGrab]         = surface2;
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered]  = overlay0;
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive]   = overlay2;

        style.Colors[(int)ImGuiCol.CheckMark]             = green;
        style.Colors[(int)ImGuiCol.SliderGrab]            = sapphire;
        style.Colors[(int)ImGuiCol.SliderGrabActive]      = blue;

        style.Colors[(int)ImGuiCol.Button]                = surface0;
        style.Colors[(int)ImGuiCol.ButtonHovered]         = surface1;
        style.Colors[(int)ImGuiCol.ButtonActive]          = surface2;

        style.Colors[(int)ImGuiCol.Header]                = surface0;
        style.Colors[(int)ImGuiCol.HeaderHovered]         = surface1;
        style.Colors[(int)ImGuiCol.HeaderActive]          = surface2;

        style.Colors[(int)ImGuiCol.Separator]             = surface1;
        style.Colors[(int)ImGuiCol.SeparatorHovered]      = mauve;
        style.Colors[(int)ImGuiCol.SeparatorActive]       = mauve;

        style.Colors[(int)ImGuiCol.ResizeGrip]             = surface2;
        style.Colors[(int)ImGuiCol.ResizeGripHovered]      = mauve;
        style.Colors[(int)ImGuiCol.ResizeGripActive]       = mauve;

        style.Colors[(int)ImGuiCol.Tab]                   = surface0;
        style.Colors[(int)ImGuiCol.TabHovered]            = surface2;
        style.Colors[(int)ImGuiCol.TabSelected]           = surface1;
        style.Colors[(int)ImGuiCol.TabDimmed]             = surface0;
        style.Colors[(int)ImGuiCol.TabDimmedSelected]     = surface1;

        style.Colors[(int)ImGuiCol.DockingPreview]        = sapphire;
        style.Colors[(int)ImGuiCol.DockingEmptyBg]        = baseColor;

        style.Colors[(int)ImGuiCol.PlotLines]              = blue;
        style.Colors[(int)ImGuiCol.PlotLinesHovered]       = peach;
        style.Colors[(int)ImGuiCol.PlotHistogram]          = teal;
        style.Colors[(int)ImGuiCol.PlotHistogramHovered]   = green;

        style.Colors[(int)ImGuiCol.TableHeaderBg]          = surface0;
        style.Colors[(int)ImGuiCol.TableBorderStrong]     = surface1;
        style.Colors[(int)ImGuiCol.TableBorderLight]      = surface0;
        style.Colors[(int)ImGuiCol.TableRowBg]             = new Vector4(0, 0, 0, 0);
        style.Colors[(int)ImGuiCol.TableRowBgAlt]          = new Vector4(1, 1, 1, 0.06f);

        style.Colors[(int)ImGuiCol.TextSelectedBg]         = surface2;
        style.Colors[(int)ImGuiCol.DragDropTarget]         = yellow;
        style.Colors[(int)ImGuiCol.NavWindowingHighlight]  = lavender;

        style.Colors[(int)ImGuiCol.NavWindowingHighlight] =
            new Vector4(1.0f, 1.0f, 1.0f, 0.7f);

        style.Colors[(int)ImGuiCol.NavWindowingDimBg] =
            new Vector4(0.8f, 0.8f, 0.8f, 0.2f);

        style.Colors[(int)ImGuiCol.ModalWindowDimBg] =
            new Vector4(0, 0, 0, 0.35f);

        style.Colors[(int)ImGuiCol.Text]         = text;
        style.Colors[(int)ImGuiCol.TextDisabled] = subtext0;

        // Rounded corners
        style.WindowRounding    = 6.0f;
        style.ChildRounding     = 6.0f;
        style.FrameRounding     = 4.0f;
        style.PopupRounding     = 4.0f;
        style.ScrollbarRounding = 9.0f;
        style.GrabRounding      = 4.0f;
        style.TabRounding       = 4.0f;

        // Padding and spacing
        style.WindowPadding    = new Vector2(8.0f, 8.0f);
        style.FramePadding     = new Vector2(5.0f, 3.0f);
        style.ItemSpacing      = new Vector2(8.0f, 4.0f);
        style.ItemInnerSpacing = new Vector2(4.0f, 4.0f);

        style.IndentSpacing = 21.0f;
        style.ScrollbarSize = 14.0f;
        style.GrabMinSize   = 10.0f;

        // Borders
        style.WindowBorderSize = 1.0f;
        style.ChildBorderSize  = 1.0f;
        style.PopupBorderSize  = 1.0f;
        style.FrameBorderSize  = 0.0f;
        style.TabBorderSize    = 0.0f;
    }
}