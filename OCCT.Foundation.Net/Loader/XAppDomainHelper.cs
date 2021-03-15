using System;
using System.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using XModel.Interface;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using XModel.Elements;

namespace OCCT.Foundation.Net.Loader
{
    #if NET48
    /// <summary>
    /// 
    /// </summary>
    public class XAppDomainHelper
    {
        /// <summary>
        /// 构建插件
        /// </summary>
        /// <param name="plugin">插件反射基本信息</param>
        /// <param name="DesignPlugin">插件结果</param>
        /// <returns>应用程序域</returns>
        public static AppDomain DesignPluginInitialize(XPluginAssembly plugin, ref XDesignPlugin DesignPlugin)
        {
            string PluginName = plugin.PluginName;
            //// Construct and initialize settings for a second AppDomain.
            //AppDomainSetup ads = new AppDomainSetup {
            //    ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
            //    DisallowBindingRedirects = false,
            //    DisallowCodeDownload = true,
            //    ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            //};
            // Create the second AppDomain.
            string friendlyName = $"{PluginName}Domain";
            AppDomain appDomain = AppDomain.CreateDomain(friendlyName);//, adevidence, setup, perSet, null);
            XDesignPlugin XPlugin = (XDesignPlugin)appDomain.CreateInstanceAndUnwrap(plugin.AssemblyName, plugin.AssemblyFullName);
            DesignPlugin = XPlugin.LoadAssembly(plugin.PluginPath);
            if (LoadedAssemblys == null)
                LoadedAssemblys = new Dictionary<string, AppDomain>();
            LoadedAssemblys.Add(friendlyName, appDomain);
            return appDomain;
        }
        /// <summary>
        /// 卸载插件模块
        /// </summary>
        /// <param name="iDesignPlugin">插件类</param>
        /// <returns>是否加载完成</returns>
        public static bool UnLoadPlugin(XDesignPlugin DesignPlugin)
        {
            if (LoadedAssemblys.ContainsKey(DesignPlugin.PluginId)) {
                AppDomain.Unload(LoadedAssemblys[DesignPlugin.PluginId]);
                LoadedAssemblys.Remove(DesignPlugin.PluginId);
            }
            return true;
        }

        #region 字段属性
        private static Dictionary<string, AppDomain> LoadedAssemblys;
        #endregion
    }
    #endif
}
