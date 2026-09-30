using Raylib_cs;

public static class SpritesheetExporter {
    public static void Save(Image[] frames, int columns, string path) {
        if(frames.Length == 0) {
            throw new ArgumentException("No frames to export - did the capture loop run?");
        }
        
        int frameWidth = frames[0].Width;
        int frameHeight = frames[0].Height;
        
        int rows = (frames.Length + columns - 1)/columns;
        
        int sheetWidth = frameWidth * columns;
        int sheetHeight = frameHeight * rows;
        
        Image sheet = Raylib.GenImageColor(sheetWidth, sheetHeight, Color.Blank);
        
        for(int i = 0; i<frames.Length; i++) {
            int column = i%columns;
            int row = i/columns;
            
            var destPosition = new System.Numerics.Vector2(column*frameWidth, row*frameHeight);
            
            var sourceRect = new Rectangle(0, 0, frameWidth, frameHeight);
            var destRect = new Rectangle(destPosition.X, destPosition.Y, frameWidth, frameHeight);
            Raylib.ImageDraw(ref sheet, frames[i], sourceRect, destRect, Color.White);
        }
        
        Raylib.ExportImage(sheet, path);
        
        Raylib.UnloadImage(sheet);
        foreach(Image frame in frames) {
            Raylib.UnloadImage(frame);
        }
    }
}