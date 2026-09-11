namespace Shared.Files.Contracts.Request
{
    public sealed record PresignedUrlRequest(string BucketName, string FileName);
}