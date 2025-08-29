
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

// Ultra-simple JSON parser for basic level loading
public static class JsonLite
{
    public static Dictionary<string, object> Parse(string json)
    {
        if (string.IsNullOrEmpty(json)) return new Dictionary<string, object>();
        
        json = json.Trim();
        if (json.StartsWith("{") && json.EndsWith("}"))
        {
            return ParseObject(json.Substring(1, json.Length - 2));
        }
        return new Dictionary<string, object>();
    }

    private static Dictionary<string, object> ParseObject(string content)
    {
        var result = new Dictionary<string, object>();
        var pairs = SplitTopLevel(content, ',');
        
        foreach (var pair in pairs)
        {
            var colonIndex = pair.IndexOf(':');
            if (colonIndex == -1) continue;
            
            var key = pair.Substring(0, colonIndex).Trim().Trim('"');
            var value = pair.Substring(colonIndex + 1).Trim();
            
            result[key] = ParseValue(value);
        }
        
        return result;
    }

    private static object ParseValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return null;
        
        if (value == "null") return null;
        if (value == "true") return true;
        if (value == "false") return false;
        
        if (value.StartsWith("\"") && value.EndsWith("\""))
        {
            return value.Substring(1, value.Length - 2);
        }
        
        if (value.StartsWith("[") && value.EndsWith("]"))
        {
            return ParseArray(value.Substring(1, value.Length - 2));
        }
        
        if (value.StartsWith("{") && value.EndsWith("}"))
        {
            return ParseObject(value.Substring(1, value.Length - 2));
        }
        
        if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double numValue))
        {
            return numValue;
        }
        
        return value;
    }

    private static List<object> ParseArray(string content)
    {
        var result = new List<object>();
        if (string.IsNullOrEmpty(content)) return result;
        
        var items = SplitTopLevel(content, ',');
        foreach (var item in items)
        {
            result.Add(ParseValue(item.Trim()));
        }
        
        return result;
    }

    private static List<string> SplitTopLevel(string content, char separator)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        int depth = 0;
        bool inString = false;
        bool escape = false;
        
        for (int i = 0; i < content.Length; i++)
        {
            char c = content[i];
            
            if (escape)
            {
                current.Append(c);
                escape = false;
                continue;
            }
            
            if (c == '\\' && inString)
            {
                escape = true;
                current.Append(c);
                continue;
            }
            
            if (c == '"')
            {
                inString = !inString;
                current.Append(c);
                continue;
            }
            
            if (!inString)
            {
                if (c == '{' || c == '[') depth++;
                if (c == '}' || c == ']') depth--;
                
                if (c == separator && depth == 0)
                {
                    result.Add(current.ToString());
                    current.Clear();
                    continue;
                }
            }
            
            current.Append(c);
        }
        
        if (current.Length > 0)
        {
            result.Add(current.ToString());
        }
        
        return result;
    }
}
