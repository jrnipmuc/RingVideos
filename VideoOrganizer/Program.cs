using System.Text.RegularExpressions;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var folderPath = args.Length > 0 ? args[0] : string.Empty;

if (string.IsNullOrWhiteSpace(folderPath))
{
    Console.WriteLine("Usage: VideoOrganizer <folder-path>");
    Console.WriteLine("Example: VideoOrganizer C:\\Videos");
    return 1;
}

if (!Directory.Exists(folderPath))
{
    Console.WriteLine($"Error: Directory '{folderPath}' does not exist.");
    return 1;
}

try
{
    var mp4Files = Directory.GetFiles(folderPath, "*.mp4", SearchOption.TopDirectoryOnly);
    
    if (mp4Files.Length == 0)
    {
        Console.WriteLine($"No MP4 files found in '{folderPath}'.");
        return 0;
    }

    Console.WriteLine($"Found {mp4Files.Length} MP4 file(s) to organize.");
    
    // Pattern: <yyyy-MM-dd-THH_mm_ss>--<camera name>.mp4
    var pattern = @"^(\d{4}-\d{2}-\d{2})-T\d{2}_\d{2}_\d{2}--(.+)\.mp4$";
    var regex = new Regex(pattern, RegexOptions.IgnoreCase);
    
    var movedCount = 0;
    var skippedCount = 0;
    
    foreach (var filePath in mp4Files)
    {
        var fileName = Path.GetFileName(filePath);
        var match = regex.Match(fileName);
        
        if (!match.Success)
        {
            Console.WriteLine($"Skipping '{fileName}' - does not match expected format.");
            skippedCount++;
            continue;
        }
        
        var dateString = match.Groups[1].Value; // yyyy-MM-dd
        var cameraName = match.Groups[2].Value;
        
        // Create destination path: <camera name>/<yyyy-MM-dd>
        var cameraFolder = Path.Combine(folderPath, cameraName);
        var dateFolder = Path.Combine(cameraFolder, dateString);
        
        // Create directories if they don't exist
        Directory.CreateDirectory(dateFolder);
        
        var destinationPath = Path.Combine(dateFolder, fileName);
        
        // Move the file
        if (File.Exists(destinationPath))
        {
            Console.WriteLine($"Warning: '{fileName}' already exists at destination. Skipping.");
            skippedCount++;
            continue;
        }
        
        File.Move(filePath, destinationPath);
        Console.WriteLine($"Moved: '{fileName}' -> '{cameraName}/{dateString}'");
        movedCount++;
    }
    
    Console.WriteLine($"\nOrganization complete: {movedCount} moved, {skippedCount} skipped.");
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    return 1;
}
