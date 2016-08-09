using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for User
/// </summary>
public class UserAccess
{
    const string _profileFile = "MyProfile.ini";
    
    private string _user;
    private UserProfile _profile;

    public string Hello;

    public UserAccess(string UserId)
	{
        _user = UserId;
	}

    /// <summary>
    /// Add current download to user profile to view it when user logs in next
    /// </summary>
    /// <param name="MainframePath"></param>
    /// <param name="LocalPath"></param>
    public void AddDownload(DownloadItem item)
    {
        _readProfile();

        _profile.AddDownloadInformation(item);

        _save();
    }

    public List<DownloadItem> GetDownloadInformation()
    {
        _readProfile();
        return _profile.DownloadInformation;
    }

    private void _readProfile()
    {
        if (_profile != null) return;

        string fileName = Configuration.ConcatPath(Configuration.DirectoryUserProfile(_user), _profileFile);
        _profile = UserProfile.LoadProfileFromFile(fileName);

        if (_profile == null)
            _profile = new UserProfile(_user);
    }

    private void _save()
    {
        string fileName = Configuration.ConcatPath(Configuration.DirectoryUserProfile(_user), _profileFile);
        UserProfile.SaveProfileToFile(_profile, fileName);
    }

    /// <summary>
    /// Add current download information to user profile
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="MainframePath"></param>
    /// <param name="LocalPath"></param>
    static public void AddDownload(string UserId, DownloadItem item)
    {
        if (UserId.Length == 0)
            UserId = "Global";

        UserAccess user = new UserAccess(UserId);
        
        user.AddDownload(item);
        user = null;
    }

    static public List<DownloadItem> GetMyList(string UserId)
    {

        if (UserId.Length == 0)
            UserId = "Global";

        UserAccess user = new UserAccess(UserId);
        return user.GetDownloadInformation();
    }

    
}