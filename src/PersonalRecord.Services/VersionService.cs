namespace PersonalRecord.Services
{
    using PersonalRecord.Infrastructure.Attributes;
    using PersonalRecord.Infrastructure.Constants;
    using PersonalRecord.Services.Interfaces;
    using System.Reflection;

    public class VersionService : IVersionService
    {
        private const int START_INDEX = 0;
        private const int OFFSET = 1;

        public string GetAppVersion()
        {
            var assemblyVersion = GetType().Assembly.GetName().Version!;
            var version = $"V{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";
            return version;
        }

        public string GetCommitHash()
        {
            var informationalVersion = GetInformationalVersion();
            var index = informationalVersion.IndexOf('+');
            var toDeleteCount = index + OFFSET;
            var commitHash = informationalVersion.Remove(START_INDEX, toDeleteCount);
            return commitHash;
        }

        public string GetInformationalVersion()
        {
            var assembly = GetType().Assembly;
            var informationalVersionAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var informationalVersion = informationalVersionAttribute!.InformationalVersion;
            return informationalVersion;
        }

        public DateTime GetBuildDate()
        {
            var assembly = GetType().Assembly;
            var attribute = assembly.GetCustomAttribute<BuildDateAttribute>();
            var buildDate = attribute?.DateTime ?? default;
            return buildDate;
        }

        public string GetRepositoryUrl()
        {
            var metadataAttributes = GetMetadataAttributes();
            var attribute = metadataAttributes?.First(c => c.Key.Equals(EnvironmentConstants.REPOSITORY_URL_ATTRIBUTE));
            var url = attribute?.Value ?? string.Empty;
            return url;
        }

        public string GetCopyright()
        {
            var assembly = GetType().Assembly;
            var attribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            var copyright = attribute?.Copyright ?? string.Empty;
            return copyright;
        }

        private IEnumerable<AssemblyMetadataAttribute> GetMetadataAttributes()
        {
            var assembly = GetType().Assembly;
            var metadataAttributes = assembly.GetCustomAttributes<AssemblyMetadataAttribute>();
            return metadataAttributes;
        }
    }
}