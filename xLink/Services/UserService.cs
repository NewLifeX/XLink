namespace xLink.Services
{
    /// <summary>用户服务</summary>
    public class UserService : LinkService<UserSession>
    {
        #region 读写
        ///// <summary>收到写入请求</summary>
        ///// <param name="id">设备</param>
        ///// <param name="start"></param>
        ///// <param name="data"></param>
        //[Api("Write")]
        //public override DataModel OnWrite(String id, Int32 start, String data)
        //{
        //    var us = Session;

        //    var err = "";
        //    try
        //    {
        //        var dv = Device.FindByName(id);
        //        if (dv == null) throw new ApiException(405, "找不到设备！");

        //        var ss = us.Session.AllSessions.FirstOrDefault(e => e["Current"] is DeviceSession d && d.Name.EqualIgnoreCase(id));
        //        if (ss == null) throw new Exception("设备离线");

        //        var ds = ss["Current"] as DeviceSession;
        //        var rs = ds.Write(id, start, data.ToHex()).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex?.GetTrue()?.Message;
        //        throw;
        //    }
        //    finally
        //    {
        //        us.SaveHistory("Write", err.IsNullOrEmpty(), "({0}, {1}, {2}) {3}".F(id, start, data.ToHex(), err));
        //    }

        //    return base.OnWrite(id, start, data);
        //}

        ///// <summary>收到读取请求</summary>
        ///// <param name="id">设备</param>
        ///// <param name="start"></param>
        ///// <param name="count"></param>
        //[Api("Read")]
        //public override DataModel OnRead(String id, Int32 start, Int32 count)
        //{
        //    var us = Session;

        //    var err = "";
        //    try
        //    {
        //        var ss = us.Session.AllSessions.FirstOrDefault(e => e["Current"] is DeviceSession d && d.Name.EqualIgnoreCase(id));
        //        if (ss is DeviceSession ds) Task.Run(() => ds.Read(id, 0, 64));
        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex?.GetTrue()?.Message;
        //        throw;
        //    }
        //    finally
        //    {
        //        us.SaveHistory("Read", err.IsNullOrEmpty(), "({0}, {1}, {2}) {3}".F(id, start, count, err));
        //    }

        //    return base.OnRead(id, start, count);
        //}
        #endregion
    }
}