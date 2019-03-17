using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace BLL.UserInfoManger.Car
{
    public static class CarManager
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public static void Delete_CarInfo(int id)
        {
            try
            {
                TB_carID tB_carID = db.TB_carID.Find(id);
                FileUpload.FileExists(tB_carID.车牌照片前);
                FileUpload.FileExists(tB_carID.车牌照片后);
                if(!File.Exists(tB_carID.车牌照片前) && !File.Exists(tB_carID.车牌照片后))
                {
                    db.TB_carID.Remove(tB_carID);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Create_CarInfo(int id, string CarID, string F_photo, string B_photo)
        {
            try
            {
                if(File.Exists(F_photo) && File.Exists(B_photo))
                {
                    TB_carID tB_Car = new TB_carID
                    {
                        车牌号 = CarID,
                        用户编号 = id,
                        状态编号 = 4,
                        车牌照片前 = F_photo,
                        车牌照片后 = B_photo
                    };
                    db.TB_carID.Add(tB_Car);
                    db.SaveChanges();

                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static void Edit_CarInfo(TB_carID msg, string path, string id)
        {
            try
            {
                TB_carID tB_carID = db.TB_carID.Find(msg.id);

                msg.车牌照片前 = FileUpload.SaveReadyInfo(msg.车牌照片前, path, id);
                if((File.Exists(msg.车牌照片前)) && (msg.车牌照片前 != null))
                {
                    if(!(msg.车牌照片前.Equals(tB_carID.车牌照片前)) && File.Exists(tB_carID.车牌照片前))
                        FileUpload.FileExists(tB_carID.车牌照片前);

                    tB_carID.车牌照片前 = msg.车牌照片前;
                    if(!((msg.车牌号 + "(前)").Equals(Path.GetFileNameWithoutExtension(tB_carID.车牌照片前))))
                        tB_carID.车牌照片前 = FileUpload.Change_photoName(tB_carID.车牌照片前, msg.车牌号 + "(前)");
                }


                msg.车牌照片后 = FileUpload.SaveReadyInfo(msg.车牌照片后, path, id);
                if((File.Exists(msg.车牌照片后)) && (msg.车牌照片后 != null))
                {
                    if(!(msg.车牌照片后.Equals(tB_carID.车牌照片后)) && File.Exists(tB_carID.车牌照片后))
                        FileUpload.FileExists(tB_carID.车牌照片后);


                    tB_carID.车牌照片后 = msg.车牌照片后;

                }


                if(!((msg.车牌号 + "(前)").Equals(Path.GetFileNameWithoutExtension(tB_carID.车牌照片前))))
                    tB_carID.车牌照片前 = FileUpload.Change_photoName(tB_carID.车牌照片前, msg.车牌号 + "(前)");

                if(!((msg.车牌号 + "(后)").Equals(Path.GetFileNameWithoutExtension(tB_carID.车牌照片后))))
                    tB_carID.车牌照片后 = FileUpload.Change_photoName(tB_carID.车牌照片后, msg.车牌号 + "(后)");

                tB_carID.状态编号 = 4;
                tB_carID.车牌号 = msg.车牌号;
                db.Entry(tB_carID).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}