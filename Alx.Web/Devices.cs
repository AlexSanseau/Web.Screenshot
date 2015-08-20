
namespace Alx.Web
{
    public enum Devices
    {
        [Size(375, 667)]    // Screen size for iPhone 6
        PhonePortrait,

        [Size(667, 375)]    // Screen size for iPhone 6
        PhoneLandscape,
        
        [Size(600, 960)]   // Screen size for Google Nexus 7
        TabletPortrait,

        [Size(960, 600)]   // Screen size for Google Nexus 7
        TabletLandscape,
        
        [Size(1920, 1080)]  // Screen size for Dell XPS12
        Desktop
    }
}
