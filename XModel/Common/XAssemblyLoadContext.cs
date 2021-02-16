using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using XModel.Elements;
using XModel.Interface;

namespace XModel.Common
{
    public class XAssemblyLoadContext : AssemblyLoadContext
    {
        public XAssemblyLoadContext() { }
        /// <summary>
        /// 指定位置的插件库集合
        /// </summary>
        AssemblyDependencyResolver resolver { get; set; }

        public AssemblyLoadContext AssemblyLoad(XPluginAssembly plugin, ref XDesignPlugin DesignPlugin) {
            try {
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
                    DesignPlugin = (XDesignPlugin)Activator.CreateInstance(Plugins.First());
                    DesignPlugin.PluginAssembly = _Assembly;
                    //DesignPlugin = DesignPlugin.LoadAssembly(plugin.PluginPath);
                    if (LoadedAssemblys == null)
                        LoadedAssemblys = new Dictionary<string, AssemblyLoadContext>();
                    LoadedAssemblys.Add(friendlyName, _AssemblyLoadContext);
                    return _AssemblyLoadContext;
                }
            } catch (Exception ex) { Console.WriteLine(ex.Message); };
            return null;
        }

        private Assembly _AssemblyLoadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
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
        private static Dictionary<string, AssemblyLoadContext> LoadedAssemblys { get; set; }
        #endregion
    }
}
