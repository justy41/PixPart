using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using NativeFileDialogSharp;

namespace HelloWorld;

// TODO: Fix Spritesheet export to actually export something, not just a blank png file

internal static class Program {
    // STAThread is required if you deploy using NativeAOT on Windows
    // See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main() {
        Raylib.InitWindow(Utils.WindowWidth, Utils.WindowHeight, "Pixel Art Particle Generator");
        Raylib.SetTargetFPS(60);
        rlImGui.Setup(enableDocking:true);
        
        var config = new EmitterConfig();
        var liveSimulation = new Simulation(config, maxParticles: 3000);
        
        using var textures = new TextureLibrary();
        textures.Load(0, "src/Textures/particle.png");
        textures.Load(1, "src/Textures/leaf.png");
        var particleRenderer = new ParticleRenderer(textures);
        
        var editorUI = new EditorUI(config);
                
        while (!Raylib.WindowShouldClose()) {
            float deltaTime = Raylib.GetFrameTime();
            liveSimulation.Update(deltaTime);
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            particleRenderer.Draw(liveSimulation.Pool.Active);
            
            rlImGui.Begin();
            editorUI.Draw();
            DrawExportPanel(config, particleRenderer);
            DrawDiagnosticsPanel(liveSimulation);
            rlImGui.End();

            Raylib.EndDrawing();
        }

        rlImGui.Shutdown();
        Raylib.CloseWindow();
    }
    
    static void DrawExportPanel(EmitterConfig config, ParticleRenderer renderer) {
        ImGui.Begin("Export");
        
        if(ImGui.Button("Save Config as JSON")) {
            DialogResult result = Dialog.FileSave("json");
            
            if(result.IsOk) {
                JsonExporter.Save(config, result.Path+".json");
            }
        }
        
        if(ImGui.Button("Export Spritesheet (60 frames)")) {
            ExportSpritesheet(config, renderer, frameCount: 60, columns: 8);
        }
        
        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();
        
        if(ImGui.Button("Load Config as JSON")) {
            DialogResult result = Dialog.FileOpen("json");
            
            if(result.IsOk) {
                JsonExporter.LoadInto(config, result.Path);
            }
            else if(result.IsError) {
                Console.WriteLine($"File dialog error: {result.ErrorMessage}");
            }
        }
        
        ImGui.End();
    }
    
    static void ExportSpritesheet(EmitterConfig config, ParticleRenderer renderer, int frameCount, int columns) {
        var exportSimulation = new Simulation(config, maxParticles: 2000, seed: 12345);
    
        using var offscreen = new OffscreenRenderer(128, 128);
        var frames = new Image[frameCount];
        
        const float fixedTimestep = 1f / 30f;
    
        for (int i = 0; i < frameCount; i++)
        {
            exportSimulation.Update(fixedTimestep);
            frames[i] = offscreen.CaptureFrame(() => renderer.Draw(exportSimulation.Pool.Active));
        }
    
        SpritesheetExporter.Save(frames, columns, "output_spritesheet.png");
    }
    
    static void DrawDiagnosticsPanel(Simulation simulation) {
        ImGui.Begin("Diagnostics");
        ImGui.Text($"Active particles: {simulation.Pool.ActiveCount} / {simulation.Pool.Capacity}");
    
        if (simulation.Pool.ActiveCount >= simulation.Pool.Capacity)
            ImGui.TextColored(new System.Numerics.Vector4(1f, 0.3f, 0.3f, 1f), "POOL FULL — spawning is stalling!");
    
        ImGui.End();
    }
}