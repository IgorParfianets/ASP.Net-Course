﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Affin
{
    public static Dictionary<string, long?> FromJson(string json) => 
        JsonConvert.DeserializeObject<Dictionary<string, long?>>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this Dictionary<string, long?> self) 
        => JsonConvert.SerializeObject(self, Converter.Settings);

}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}