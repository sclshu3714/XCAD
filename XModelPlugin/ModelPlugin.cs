﻿using System;
using System.Linq;
using System.Reflection;
using XModel.Interface;

namespace XModelPlugin
{
    public class ModelPlugin : XDesignPlugin
    {
        public ModelPlugin()
        {
            this.ProjectId = Guid.NewGuid().ToString();
            this.PluginName = Guid.NewGuid().ToString("N");
            this.PluginCaption = Guid.NewGuid().ToString("N");
            this.PluginTag = Guid.NewGuid().ToString();
            //this.PluginIcon = Guid.NewGuid().ToString();
            this.PluginCategory = Guid.NewGuid().ToString("N");
            //this.LogonUser = Guid.NewGuid().ToString();
            this.ProjectId = Guid.NewGuid().ToString();
            this.ProjectName = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>

        public override object Invoke(string fullClassName, string methodName, params Object[] args)
        {
            if (PluginAssembly == null)
                return false;
            Type tp = PluginAssembly.GetType(fullClassName);
            if (tp == null)
                return false;
            MethodInfo method = tp.GetMethod(methodName);
            if (method == null || method.GetParameters().Count() != args.Length)
                return false;
            Object obj = Activator.CreateInstance(tp);
            return method.Invoke(obj, args);
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>

        public override object Invoke(string methodName, params Object[] args)
        {
            switch (methodName) {
                case "Add":
                    return Add((int)args[0], (int)args[1]);
                default:
                    return null;
            }
        }
        public int Add(int a, int b) {
            return a + b;
        }
    }
}
