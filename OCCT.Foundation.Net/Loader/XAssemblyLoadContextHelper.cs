using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using XModel.Elements;
using XModel.Interface;

namespace OCCT.Foundation.Net.Loader
{
    public class XAssemblyLoadContextHelper
    {
        /// <summary>
        /// 指定位置的插件库集合
        /// </summary>
        private static AssemblyDependencyResolver resolver { get; set; }
        /// <summary>
        /// 构建插件
        /// </summary>
        /// <param name="plugin">插件反射基本信息</param>
        /// <param name="DesignPlugin">插件结果</param>
        /// <returns>应用程序域</returns>
        public static AssemblyLoadContext DesignPluginInitialize(XPluginAssembly plugin, ref XDesignPlugin DesignPlugin)
        {
            string PluginName = plugin.PluginName;
            string friendlyName = $"{PluginName}Domain";
            resolver = new AssemblyDependencyResolver(plugin.PluginPath);
            AssemblyLoadContext _AssemblyLoadContext = new AssemblyLoadContext(friendlyName, true);
            using (var fs = new FileStream(plugin.PluginPath, FileMode.Open, FileAccess.Read)) {
                var _Assembly = _AssemblyLoadContext.LoadFromStream(fs);
                var Modules = _Assembly.Modules;
                IEnumerable<Type> Plugins = _Assembly.GetTypes().Where(P => P.GetInterface("IPlugin") != null);
                if (Plugins == null || Plugins.Count() == 0)
                    return null;
                _AssemblyLoadContext.Resolving += _AssemblyLoadContext_Resolving;
                XDesignPlugin XPlugin = (XDesignPlugin)Activator.CreateInstance(Plugins.First());
                DesignPlugin = XPlugin.LoadAssembly(plugin.PluginPath);
                if (LoadedAssemblys == null)
                    LoadedAssemblys = new Dictionary<string, AssemblyLoadContext>();
                LoadedAssemblys.Add(friendlyName, _AssemblyLoadContext);
                return _AssemblyLoadContext;
            }
            
        }
        private static Assembly _AssemblyLoadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            var path = resolver.ResolveAssemblyToPath(arg2);
            if (!string.IsNullOrEmpty(path)) {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    return arg1.LoadFromStream(fs);
                }
            }
            return null;

        }

        /// <summary>
        /// 卸载插件模块
        /// </summary>
        /// <param name="iDesignPlugin">插件类</param>
        /// <returns>是否加载完成</returns>
        public static bool UnLoadPlugin(XDesignPlugin DesignPlugin)
        {
            if (LoadedAssemblys.ContainsKey(DesignPlugin.PluginId)) {
                LoadedAssemblys[DesignPlugin.PluginId].Unload();
                LoadedAssemblys.Remove(DesignPlugin.PluginId);
            }
            return true;
        }

        #region 字段属性
        private static Dictionary<string, AssemblyLoadContext> LoadedAssemblys;
        #endregion
    }
}
