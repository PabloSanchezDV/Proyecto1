using System.Collections;
using System.Collections.Generic;

public static class CSVParser
{
    public static List<string[]> ParseCSV(string csv)
    {
        List<string[]> parsedElements = new List<string[]>();

        string[] rows = SplitStringByRows(csv);
        foreach (string row in rows)
        {
            parsedElements.Add(SplitRowsByElements(row));
        }

        return parsedElements;
    }

    private static string[] SplitStringByRows(string csvString)
    {
        string[] rows = csvString.Split('\n');
        return rows;
    }

    private static string[] SplitRowsByElements(string csvRow)
    {
        string[] row = csvRow.Split(';');
        return row;
    }
}
