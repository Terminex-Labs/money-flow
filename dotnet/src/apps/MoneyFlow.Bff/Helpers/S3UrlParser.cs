namespace MoneyFlow.Bff.Helpers
{
    public static class S3UrlParser
    {
        public static (string? BuckerName, string? FileName) Parse(string dbUrl)
        {
            if (string.IsNullOrWhiteSpace(dbUrl))
                return (null, null);

            var file = dbUrl.Split(":");

            var bucketName = file[0];
            var fileName = file[1];

            return (bucketName, fileName);
        }
    }
}