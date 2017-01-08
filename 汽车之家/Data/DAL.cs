using Dos.ORM;
using System;
using System.Collections.Generic;
using Model;

public class DAL
{
    #region Area
    public List<Area> GetProvince()
    {
        return DB.Context.From<Area>()
            .Select(Area._.ProId, Area._.Pro)
            .Distinct()
            .ToList();
    }

    public List<Area> GetCity(string proId)
    {
        var where = new Where<Area>();
        where.And(w => w.ProId == proId);

        return DB.Context.From<Area>()
            .Where(where)
            .ToList();
    }
    #endregion

}