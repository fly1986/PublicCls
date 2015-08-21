using CacheCls;
using System;
using System.Configuration;

namespace CommonCls
{
	/// <summary>
	/// web.config 操作类 获取自定义或者数据库连接 字符串
	/// </summary>
	public sealed class ConfigHelper
	{
		/// <summary>
		/// 得到AppSettings中的配置字符串信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetConfigString(string key)
		{

            string CacheKey =string.Format(CacheKeys.APPSETTINGS_VALUE,key);
            object objModel = CacheUtil.Get<object>(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key]; 
                    if (objModel != null)
                    {
                        CacheUtil.Set<object>(CacheKey, objModel, CacheTime.Long);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objModel.ToString();
		}

		/// <summary>
		/// 得到AppSettings中的配置Bool信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetConfigBool(string key)
		{
			bool result = false;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = bool.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}
			return result;
		}
		/// <summary>
		/// 得到AppSettings中的配置Decimal信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static decimal GetConfigDecimal(string key)
		{
			decimal result = 0;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = decimal.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}
		/// <summary>
		/// 得到AppSettings中的配置int信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static int GetConfigInt(string key)
		{
			int result = 0;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = int.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}
	}

    #region 自定义 Config读取设置类
    public class MyCustomSection : ConfigurationSection    // 所有配置节点都要选择这个基类
    {
        private static readonly ConfigurationProperty s_property
            = new ConfigurationProperty(string.Empty, typeof(MyKeyValueCollection), null,
                                            ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public MyKeyValueCollection KeyValues
        {
            get
            {
                return (MyKeyValueCollection)base[s_property];
            }
        }
    }

    //public class MyCustomSection : ConfigurationSection    // 所有配置节点都要选择这个基类
    //{
    //    [ConfigurationProperty("type", IsRequired = true)]
    //    public string Type
    //    {
    //        get { return (string)this["type"]; }
    //    }
    //}


    [ConfigurationCollection(typeof(MyKeyValueSetting))]
    public class MyKeyValueCollection : ConfigurationElementCollection        // 自定义一个集合
    {
        // 基本上，所有的方法都只要简单地调用基类的实现就可以了。

        public MyKeyValueCollection()
            : base(StringComparer.OrdinalIgnoreCase)    // 忽略大小写
        {
        }

        // 其实关键就是这个索引器。但它也是调用基类的实现，只是做下类型转就行了。
        new public MyKeyValueSetting this[string name]
        {
            get
            {
                return (MyKeyValueSetting)base.BaseGet(name);
            }
        }

        // 下面二个方法中抽象类中必须要实现的。
        protected override ConfigurationElement CreateNewElement()
        {
            return new MyKeyValueSetting();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MyKeyValueSetting)element).Key;
        }

        // 说明：如果不需要在代码中修改集合，可以不实现Add, Clear, Remove
        public void Add(MyKeyValueSetting setting)
        {
            this.BaseAdd(setting);
        }
        public void Clear()
        {
            base.BaseClear();
        }
        public void Remove(string name)
        {
            base.BaseRemove(name);
        }
    }

    public class MyKeyValueSetting : ConfigurationElement    // 集合中的每个元素
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return this["key"].ToString(); }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return this["value"].ToString(); }
            set { this["value"] = value; }
        }
    }
#endregion
}
