using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WS.PluginCore.Plugin
{
    /// <summary>
    /// 插件加载器
    /// </summary>
    public class DirectoryLoader
    {
        //protected ILogger Logger = LoggerManager.GetLogger("DirectoryLoader");

        /// <summary>
        /// 加载文件夹
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public virtual List<Assembly> LoadFromDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return new List<Assembly>();
            }

            List<Assembly> assemblyList = new List<Assembly>();
            string[] dllFiles = Directory.GetFiles(dir, "*.dll");
            foreach (string file in dllFiles)
            {
                if (!file.EndsWith(".dll"))
                {
                    continue;
                }
                //Logger.Trace("loading assembly: {0}", file);

                try
                {
//#if netcore
 //                   Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
//#else
                    Assembly assembly = Assembly.LoadFrom(file);
//#endif
                    assemblyList.Add(assembly);
                }
                catch (System.Exception e)
                {
                    //Logger.Error("can not load service:{0}\r\n{1}", file, e.ToString());
                    Console.WriteLine("can not load service:{0}\r\n{1}", file, e.ToString());
                }
            }

            return assemblyList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="baseType"></param>
        /// <param name="checker"></param>
        /// <returns></returns>
        public virtual List<Type> GetTypes(Assembly assembly, Type baseType, Func<Type, bool> checker)
        {
            Func<Type, bool> actualCheck = (ti) =>
            {
                bool isOk = true;
                if (baseType != null)
                {
                    if (baseType.GetTypeInfo().IsAssignableFrom(ti))
                    {
                        isOk = true;
                    }
                    else
                    {
                        isOk = false;
                    }
                }

                if (isOk)
                {
                    if (checker != null)
                    {
                        isOk = checker(ti);
                    }
                }

                return isOk;
            };

            return assembly.GetTypes().Where(actualCheck).ToList();
        }
    }
}
