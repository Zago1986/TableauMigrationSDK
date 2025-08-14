using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

public static class MigrationListLoader
{
    public static List<MigrationListEntry> Load(string csvPath)
    {
        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return new List<MigrationListEntry>(csv.GetRecords<MigrationListEntry>());
    }
}
