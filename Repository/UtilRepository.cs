using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// UtilRepository : 유틸 리포지토리
    /// </summary>
    public static  class UtilRepository
    {
        /// <summary>
        /// ConverToEntityList : 데이터 테이블 을 특정 타입의 List 로 변환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="dt">데이터 테이블</param>
        /// <returns></returns>
        public static List<T> ConverToEntityList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        /// <summary>
        /// GetItem : 데이터 로우 를 특정 타입으로 변환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="dr">데이터 로우</param>
        /// <returns></returns>
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        /// <summary>
        /// ConvertToEntity : 데이터 로우 를 특정 타입의 Entity 로 변환
        /// </summary>
        /// <typeparam name="T">변환할 타입</typeparam>
        /// <param name="this">데이터 로우</param>
        /// <returns></returns>
        public static T ConvertToEntity<T>(this DataRow @this) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var entity = new T();

            foreach (PropertyInfo property in properties)
            {
                if (@this.Table.Columns.Contains(property.Name))
                {
                    Type valueType = property.PropertyType;
                    property.SetValue(entity, @this[property.Name], null);
                }
            }

            foreach (FieldInfo field in fields)
            {
                if (@this.Table.Columns.Contains(field.Name))
                {
                    Type valueType = field.FieldType;
                    field.SetValue(entity, @this[field.Name]);
                }
            }

            return entity;
        }
    }
}
