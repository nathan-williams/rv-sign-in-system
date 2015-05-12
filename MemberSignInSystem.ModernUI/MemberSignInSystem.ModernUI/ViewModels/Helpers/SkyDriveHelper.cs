using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using HgCo.WindowsLive.SkyDrive;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using System.IO;

namespace MemberSignInSystem.ModernUI.ViewModels.Helpers
{
    static class SkyDriveHelper
    {
        private static SkyDriveWebClient skyDrive = CreateClient("rvstc_signin@outlook.com", "GoDolphins!!!");
        private static WebFolderInfo documentsWebFolder = skyDrive.ListRootWebFolders()[0];

        public static void InitializeSkyDriveApplicationSettings()
        {
            Application.Current.Resources["SkyDriveServiceClient"] = skyDrive;
            Application.Current.Resources["DocumentsWebFolderInfo"] = documentsWebFolder;
        }
        private static SkyDriveWebClient CreateClient(String username, String password)
        {
            SkyDriveWebClient client = new SkyDriveWebClient();
            client.LogOn(username, password);
            return client;
        }
        public static WebFileInfo GetWebFileInfo(SkyDriveWebClient skyDrive, WebFolderInfo parentWebFolder, String targetName)
        {
            WebFileInfo target = null;
            foreach (WebFileInfo item in skyDrive.ListSubWebFolderFiles(parentWebFolder))
            {
                if (item.Name == targetName)
                {
                    target = item;
                }
            }
            //if (target != null) target.Path = target.Path.Replace("/.", "/");
            return target;
        }
        public static WebFolderInfo GetWebFolderInfo(SkyDriveWebClient skyDrive, WebFolderInfo parentWebFolder, String targetName)
        {
            WebFolderInfo target = null;
            foreach (WebFolderInfo item in skyDrive.ListSubWebFolders(parentWebFolder))
            {
                if (item.Name == targetName)
                {
                    target = item;
                }
            }
            return target;
        }

        private static Boolean downloading = false;

        public static void Sync()
        {
            if (downloading)
                return;

            MainWindow mw = Application.Current.MainWindow as MainWindow;

            mw.IsProgressRingVisible = true;

            // Work around to make download occur on a different thread. Prevents UI update blocking.
            System.Timers.Timer t = new System.Timers.Timer(1) { AutoReset = false };
            t.Elapsed += async delegate
            {
                downloading = true;
                DbHelper.closeConnection(); // make sure temporary db file isn't open (.laccdb)

                WebFileInfo dbWebFileInfo = GetWebFileInfo(skyDrive, documentsWebFolder, "membership.mdb");
                String directoryUri = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Membership Records\\");
                if (!Directory.Exists(directoryUri)) Directory.CreateDirectory(directoryUri);
                
                DirectoryInfo poolMembershipDirectoryInfo = new DirectoryInfo(directoryUri);
                DownloadFile(dbWebFileInfo, poolMembershipDirectoryInfo);
                //DownloadDirectory(documentsWebFolder, poolMembershipDirectoryInfo);

                WebFileInfo announcementsWebFileInfo = GetWebFileInfo(skyDrive, documentsWebFolder, "Announcements.txt");
                DownloadFile(announcementsWebFileInfo, poolMembershipDirectoryInfo);
                MemberSignInSystem.ModernUI.Content.MarqueeText.LoadAnnouncements();

                WebFolderInfo memberPicturesWebFolderInfo = GetWebFolderInfo(skyDrive, documentsWebFolder, "Member Pictures");
                if (!Directory.Exists(directoryUri + "Member Pictures\\")) Directory.CreateDirectory(directoryUri + "Member Pictures\\");
                DirectoryInfo memberPicturesFolderInfo = new DirectoryInfo(directoryUri + "Member Pictures\\");
                DownloadDirectory(memberPicturesWebFolderInfo, memberPicturesFolderInfo);

                await mw.Dispatcher.BeginInvoke(new Action(() => mw.IsProgressRingVisible = false));
                t.Dispose();

                downloading = false;
            };
            t.Start();
        }

        public static void DownloadDirectory(WebFolderInfo src, DirectoryInfo dst)
        {
            if ((dst == null) || (src != null && dst != null))// && dst.LastWriteTimeUtc < src.DateModified))
            {
                WebFileInfo[] webFiles = skyDrive.ListSubWebFolderFiles(src);
                String[] webFileNames = GetWebFileNames(webFiles);
                String[] localFiles = Directory.GetFiles(dst.FullName);
                List<String> downloaded = new List<String>();
                foreach (WebFileInfo item in webFiles)
                {
                    downloaded.Add(item.Name);
                    DownloadFile(item, dst);
                }
                foreach (String item in localFiles)
                {
                    if (!downloaded.Contains(Path.GetFileName(item)))
                    {
                        FileInfo f = new FileInfo(item);
                        if (!downloaded.Contains(f.Name))
                            UploadFile(f, src);
                    }
                }
            }
        }
        private static String[] GetWebFileNames(WebFileInfo[] webFiles)
        {
            List<String> ret = new List<String>();
            foreach (WebFileInfo item in webFiles)
                ret.Add(item.Name);
            return ret.ToArray();
        }
        public static void DownloadFile(WebFileInfo src, DirectoryInfo dst)
        {
            if (src != null)
            {
                String dstFolderPath = dst.FullName;
                String dstFilePath = Path.Combine(dstFolderPath, src.Name);
                if (!Directory.Exists(dstFolderPath)) Directory.CreateDirectory(dstFolderPath);
                FileInfo dstFileInfo = null;
                if (File.Exists(dstFilePath)) dstFileInfo = new FileInfo(dstFilePath);
                if (dstFileInfo == null || (dstFileInfo != null && dstFileInfo.LastWriteTimeUtc < src.DateModified))
                {
                    //src.Path = src.Path.Replace("/.", "/");
                    Stream fs = null;
                    try { fs = File.Open(dstFilePath, FileMode.OpenOrCreate); }
                    catch (Exception e)
                    {
                        DbHelper.closeConnection();
                        try { fs = File.Open(dstFilePath, FileMode.OpenOrCreate); }
                        catch (Exception e2) { }
                    }
                    Stream ds = skyDrive.DownloadWebFile(src);
                    if (fs != null)
                    {
                        FileStorageHelper.CopyStream(ds, fs);
                    }
                }
                else if (dstFileInfo != null && dstFileInfo.LastWriteTimeUtc > src.DateModified)
                {
                    String webPath = src.PathUrl.Substring(0, src.PathUrl.LastIndexOf("/") + 1);//.Replace("/.", "/");
                    WebFolderInfo wfi = null;
                    if (webPath == "/Documents/")
                    {
                        wfi = documentsWebFolder;
                    }
                    else if (webPath == "/Documents/Member Pictures/")
                    {
                        wfi = GetWebFolderInfo(skyDrive, documentsWebFolder, "Member Pictures");
                    }
                    UploadFile(dstFileInfo, wfi);
                }
            }
        }
        public static void UploadFile(FileInfo src, WebFolderInfo dst)
        {
            if (src != null && dst != null)
            {
                WebFileInfo dstWebFileInfo = GetWebFileInfo(skyDrive, dst, src.Name);
                if (dstWebFileInfo == null || (dstWebFileInfo != null && dstWebFileInfo.DateModified < src.LastWriteTimeUtc))
                {
                    if (dstWebFileInfo != null)
                    {
                        skyDrive.DeleteWebFile(dstWebFileInfo);
                    }
                    if (src.Extension != ".laccdb")
                    {
                        //dst.Path.Replace("/.", "/");
                        try
                        {
                            skyDrive.UploadWebFile(src.FullName, dst);
                        }
                        catch (Exception e) // File probably open in a different program
                        {
                            DbHelper.closeConnection();
                            try { skyDrive.UploadWebFile(src.FullName, dst); }
                            catch (Exception e2) { }
                        }
                    }
                }
            }
        }
    }
}
