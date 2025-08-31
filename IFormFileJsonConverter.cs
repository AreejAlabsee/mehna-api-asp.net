public class IFormFileJsonConverter : JsonConverter<IFormFile>
{
    public override IFormFile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var base64 = reader.GetString();
        if (string.IsNullOrEmpty(base64))
            return null;

        var bytes = Convert.FromBase64String(base64);
        var stream = new MemoryStream(bytes);

        // You may want to set the file name and content type as needed
        return new FormFile(stream, 0, bytes.Length, "profileImage", "profileImage.jpg");
    }

    public override void Write(Utf8JsonWriter writer, IFormFile value, JsonSerializerOptions options)
    {
        using var ms = new MemoryStream();
        value.CopyTo(ms);
        var base64 = Convert.ToBase64String(ms.ToArray());
        writer.WriteStringValue(base64);
    }
}
