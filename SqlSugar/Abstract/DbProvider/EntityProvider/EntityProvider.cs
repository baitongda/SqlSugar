﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar
{
    public class EntityProvider
    {
        public SqlSugarClient Context { get; set; }
        public EntityInfo GetEntityInfo<T>()where T:class,new()
        {
            string cacheKey = "GetEntityInfo";
            return CacheFactory.Func<EntityInfo>(cacheKey,
            (cm, key) =>
            {
                return cm[cacheKey];

            }, (cm, key) =>
            {
                EntityInfo result = new EntityInfo();
                var type = typeof(T);
                result.Type = type;
                result.Type.GetProperties();
                result.Name =result.Type.Name;
                result.Columns = new List<EntityColumnInfo>();
                foreach (var item in result.Type.GetProperties())
                {
                    EntityColumnInfo columns = new EntityColumnInfo();
                    columns.ColumnName = item.Name;
                    columns.PropertyInfo = item;
                }
                return result;
            });
        }
    }
}
