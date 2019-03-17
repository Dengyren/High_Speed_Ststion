using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace BLL.UserInfoManger
{

    public static class FileUpload
    {


        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        #region 判断文件夹是否存在
        public static string CreatePath(string path, string Userid)
        {
            try
            {
                string targtpath = Path.Combine(path, "UserCardInfo", Userid);
                if(!Directory.Exists(targtpath))//判断是否存在
                {
                    Directory.CreateDirectory(targtpath);//创建新路径
                }
                return targtpath;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 上传图片到服务器
        public static string SaveFileInUserInfo(HttpPostedFileBase file, string path, string username, string Userid)
        {
            try
            {
                if(file != null)
                {
                    string finalpath = "";
                    string newpath = CreatePath(path, Userid);
                    string fileName = file.FileName.ToString();
                    newpath = Path.Combine(newpath, fileName);
                    file.SaveAs(newpath);
                    if(File.Exists(newpath))
                    {
                        finalpath = Change_photoName(newpath, username);
                    }
                    return finalpath;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion

        #region 判断文件是否存在，若存在删除原文件
        public static void FileExists(string newpath)
        {
            try
            {
                if(File.Exists(newpath))
                    File.Delete(newpath);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 修改上传文件的名称为用户姓名
        public static string Change_photoName(string path, string UserName)
        {
            string oldpath = Path.GetDirectoryName(path);
            string newpath = Path.Combine(oldpath, UserName) + Path.GetExtension(path);
            FileExists(newpath);
            System.IO.File.Move(path, newpath);
            return newpath;
        }
        #endregion

        #region 用于编辑时修改图片名称为最新用户姓名
        public static string Change_edit_photoName(string path, string UserName)
        {
            string oldpath = Path.GetDirectoryName(path);
            string newpath = Path.Combine(oldpath, UserName) + Path.GetExtension(path);
            System.IO.File.Move(path, newpath);
            return newpath;
        }
        #endregion



        #region 判断临时文件夹是否存在
        public static string CreatePath(string path)
        {
            try
            {
                string targtpath = Path.Combine(path, "Ready_Upload");
                if(!Directory.Exists(targtpath))//判断是否存在
                {
                    Directory.CreateDirectory(targtpath);//创建新路径
                }
                return targtpath;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


        #region 用于上传照片信息

        #region 上传图片到服务器临时文件夹中
        public static string SaveInfo(HttpPostedFileBase file, string path, string name)
        {
            try
            {
                if(file != null)
                {
                    string newpath = CreatePath(path);
                    string fileName = file.FileName.ToString();
                    newpath = Path.Combine(newpath, fileName);
                    file.SaveAs(newpath);
                    if(File.Exists(newpath))
                    {
                        string readypath = Path.GetDirectoryName(newpath);
                        name += Path.GetExtension(newpath);
                        readypath = Path.Combine(readypath, name);
                        FileExists(readypath);
                        System.IO.File.Move(newpath, readypath);
                        readypath = "/Ready_Upload/" + Path.GetFileName(readypath);
                        return readypath;
                    }
                    return null;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion

        #region 将临时图片存进用户文件夹
        public static string SaveReadyInfo(string ready_path, string path, string id)
        {
            try
            {
                string newpath = CreatePath(path, id);
                string photo_name = Path.GetFileName(ready_path);
                newpath = Path.Combine(newpath, photo_name);
                string TargetPath = Path.Combine(path, "Ready_Upload", photo_name);
                //复制原路径的文件到新的路径去，并且对文件进行重命名，然后删除原文件
                //如果在同一磁盘，即copy+detele
                if(File.Exists(TargetPath))//判断是否存在
                {
                    FileExists(newpath);
                    System.IO.File.Move(TargetPath, newpath);
                    return newpath;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #endregion
    }
}