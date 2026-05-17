---
uid: networking-file-uploads
title: File uploads
---

# File uploads

The `MultipartFormDataPostNetworkRequest` handles file uploads using multipart/form-data encoding. It provides a fluent API for building the request content, supporting multiple files and form fields in a single request.

## Uploading a file

```csharp
var request = requestFactory.PostMultipart("https://api.example.com/upload")
    .AddStreamContent("file", fileStream, "photo.png", "image/png")
    .AddStringContent("description", "Profile photo");

var result = await request.ExecuteAsync<UploadResult>();
```

## Content types

You can add three types of content to a multipart request:

### AddStreamContent

Adds a file from a `Stream` with a file name and content type:

```csharp
request.AddStreamContent("file", fileStream, "document.pdf", "application/pdf");
```

### AddByteArrayContent

Adds a file from a byte array:

```csharp
byte[] imageBytes = await File.ReadAllBytesAsync("photo.jpg");
request.AddByteArrayContent("file", imageBytes, "photo.jpg", "image/jpeg");
```

### AddStringContent

Adds a text form field:

```csharp
request.AddStringContent("title", "My Document");
request.AddStringContent("category", "reports");
```

## Uploading multiple files

Chain multiple `AddStreamContent` or `AddByteArrayContent` calls:

```csharp
var request = requestFactory.PostMultipart("https://api.example.com/upload")
    .AddStreamContent("files", stream1, "photo1.png", "image/png")
    .AddStreamContent("files", stream2, "photo2.png", "image/png")
    .AddStringContent("album", "Vacation");

var result = await request.ExecuteAsync<UploadResult>();
```

## Best practices

- **Dispose streams after the request completes.** The request reads the stream during execution but doesn't take ownership of it.
- **Set the correct content type** for each file. Some APIs validate the content type and will reject uploads with incorrect MIME types.
- **Consider file size limits.** Large file uploads may need increased timeout settings on your `HttpClient` configuration.
