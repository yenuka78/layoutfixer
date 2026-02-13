using Layoutfixer.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Layoutfixer.Services;

public class SettingsStore
{
    private readonly string _path;

    public SettingsStore()
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Layoutfixer");
        Directory.CreateDirectory(dir);
        _path = Path.Combine(dir, "settings.json");
    }

    public Settings Load()
    {
        if (!File.Exists(_path)) return new Settings();
        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
    }

    public void Save(Settings settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, json);
    }
}
