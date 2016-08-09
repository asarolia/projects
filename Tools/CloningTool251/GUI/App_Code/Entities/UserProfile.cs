using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// Summary description for UserProfile
/// </summary>
public class UserProfile
{
    public string UserId;
    public List<DownloadItem> DownloadInformation;
	public UserProfile(string User)
	{
        UserId = User;
        DownloadInformation = new List<DownloadItem>();
	}

    /// <summary>
    /// Add item to Download Information dictionary
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Path"></param>
    public void AddDownloadInformation(DownloadItem item)
    {
        DownloadInformation.Add(item);
    }

    /// <summary>
    /// Save profile object to JSON file
    /// </summary>
    /// <param name="Profile"></param>
    /// <param name="FileName"></param>
    static public void SaveProfileToFile(UserProfile Profile, string FileName)
    {
        using (StreamWriter sw = new StreamWriter(FileName))
        using (JsonWriter jw = new JsonTextWriter(sw))
        {
            jw.Formatting = Formatting.Indented;

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jw, Profile);
        }
    }

    /// <summary>
    /// Loads profile from JSON file
    /// </summary>
    /// <param name="FileName"></param>
    /// <returns></returns>
    public static UserProfile LoadProfileFromFile(string FileName)
    {
        UserProfile ret;

        if (!File.Exists(FileName))
            return null;
        try
        {
            using (StreamReader reader = new StreamReader(FileName))
            using (JsonReader jreader = new JsonTextReader(reader))
            {
                JsonSerializer serializer = new JsonSerializer();
                ret = serializer.Deserialize<UserProfile>(jreader);
            }
        }
        catch
        {
            return null;
        }

        return ret;
    }
}