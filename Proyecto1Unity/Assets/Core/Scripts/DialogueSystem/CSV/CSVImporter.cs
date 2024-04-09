using System;
using UnityEngine;

public static class CSVImporter
{
    public static string ImportCSV(string path)
    {
        return Resources.Load<TextAsset>(path).ToString();
    }
}
