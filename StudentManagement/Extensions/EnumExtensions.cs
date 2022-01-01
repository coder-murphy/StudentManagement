using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudentManagement.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举名
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDisplayName(this System.Enum en)
        {
            Type t = en.GetType();
            MemberInfo[] memberInfos = t.GetMember(en.ToString());
            if(memberInfos != null && memberInfos.Length > 0)
            {
                object[] atts = memberInfos[0].GetCustomAttributes(typeof(DisplayAttribute), true);
                if(atts != null && atts.Length > 0)
                {
                    return ((DisplayAttribute)atts[0]).Name;
                }
            }
            return en.ToString();
        }
    }
}
