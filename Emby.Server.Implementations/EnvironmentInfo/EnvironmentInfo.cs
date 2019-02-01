using System;
using System.Runtime.InteropServices;
using MediaBrowser.Model.System;
using OperatingSystem = MediaBrowser.Model.System.OperatingSystem;

namespace Emby.Server.Implementations.EnvironmentInfo
{
    public class EnvironmentInfo : IEnvironmentInfo
    {
        private readonly Lazy<OperatingSystem> _lazyOperatingSystem;

        public EnvironmentInfo()
        {
            _lazyOperatingSystem = new Lazy<OperatingSystem>(GetOperatingSystem);
        }

        public OperatingSystem OperatingSystem => _lazyOperatingSystem.Value;

        public string OperatingSystemName
        {
            get
            {
                switch (OperatingSystem)
                {
                    case OperatingSystem.Android: return "Android";
                    case OperatingSystem.BSD: return "BSD";
                    case OperatingSystem.Linux: return "Linux";
                    case OperatingSystem.OSX: return "macOS";
                    case OperatingSystem.Windows: return "Windows";
                    default: throw new Exception($"Unknown OS {OperatingSystem}");
                }
            }
        }

        public string OperatingSystemVersion => Environment.OSVersion.Version.ToString() + " " + Environment.OSVersion.ServicePack.ToString();

        public Architecture SystemArchitecture => RuntimeInformation.OSArchitecture;

        private static OperatingSystem GetOperatingSystem()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                    return OperatingSystem.OSX;
                case PlatformID.Win32NT:
                    return OperatingSystem.Windows;
                default:
                {
                    string osDescription = RuntimeInformation.OSDescription;
                    if (osDescription.ToLower().Contains("linux"))
                    {
                        return OperatingSystem.Linux;
                    }
                    if (osDescription.ToLower().Contains("darwin"))
                    {
                        return OperatingSystem.OSX;
                    }
                    if (osDescription.ToLower().Contains("bsd"))
                    {
                        return OperatingSystem.BSD;
                    }
                    throw new Exception($"Can't resolve OS with description: '{osDescription}'");
                }
            }
        }
    }
}
