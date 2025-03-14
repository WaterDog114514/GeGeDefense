using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace WDFramework
{
    /// <summary>
    /// 资源加载的核心类，封装了各个子模块的加载
    /// </summary>
    public class ResLoader : Singleton<ResLoader>
    {
        //所有子模块
        private main_ResoucesLoader resourcesLoader;
        private main_ABLoader abLoader;
        private main_Preloader preloader;
        //核心设置配置文件
        private FrameworkSettingData settingData;
        //编辑器加载，只有在未发布下有用
#if UNITY_EDITOR
        private main_EditorABLoader editorLoader;
#endif
        /// <summary>
        /// 所有的异步加载任务，包括已经执行和未执行的
        /// </summary>
       public Dictionary<string, AsyncLoadTask> dic_LoadedTask = new Dictionary<string, AsyncLoadTask>();
        /// <summary>
        /// 已经加载过的资源
        /// </summary>
        public Dictionary<string, Res> dic_LoadedRes = new Dictionary<string, Res>();
        public ResLoader()
        {
            settingData = SettingDataLoader.Instance.LoadData<FrameworkSettingData>();
            //初始化所有必备加载子模块
            resourcesLoader = new main_ResoucesLoader();
            abLoader = new main_ABLoader();
            preloader = new main_Preloader();
#if UNITY_EDITOR
            editorLoader = new main_EditorABLoader();
#endif
        }

        #region AB包加载相关
        //下面两者创建任务然后直接加载
        // 通过类型加载AB包
        public void LoadAB_Async<T>(E_ABPackName ABPackName, string resName, UnityAction<T> callback) where T : UnityEngine.Object
        {
            AsyncLoadTask task = CreateAB_Async<T>(ABPackName, resName, callback);
            if (task != null)
                task.StartAsyncLoad();
        }
        // 通过名字异步加载AB包资源
        public void LoadAB_Async(E_ABPackName ABPackName, string resName, UnityAction<UnityEngine.Object> callback)
        {
            AsyncLoadTask task = CreateAB_Async<UnityEngine.Object>(ABPackName, resName, callback);
            if (task != null)
                task.StartAsyncLoad();
        }
      

        //创建AB包任务，不会立即加载，需要手动加载才行
        public AsyncLoadTask CreateAB_Async<T>(E_ABPackName ABPackName, string resName, UnityAction<T> callback) where T : UnityEngine.Object
        {

            string key = $"AB_{ABPackName}_{resName}";
            //先任务是否第一次加载完成了吗
            if (!IsFirstAsyncLoad(key, callback))
                return null;

#if UNITY_EDITOR
            if (settingData.abLoadSetting.IsDebugABLoad)
            {
                EditorLoadABRes(ABPackName, resName, callback);
                return null;
            }
#endif

            //第一次就新建任务
            AsyncLoadTask task = new AsyncLoadTask();
            task.IntiLoadOperation(abLoader.ReallyLoadAsync<T>(ABPackName.ToString(), resName, task));
            task.AddCallbackCommand(new LoadResAsyncCommand<T>(callback, task));
            //加载资源占位
            dic_LoadedRes.Add(key, null);
            //记录到字典中
            dic_LoadedTask.Add(key, task);
            //异步加载完成记录
            task.AddCallbackCommand(() =>
            {
                dic_LoadedRes[key] = task.ResInfo;
                dic_LoadedTask.Remove(key);
            });

            return task;
        }

        /// <summary>
        /// (开始任务后立即加载)通过类型进行同步加载AB包
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public T LoadAB_Sync<T>(E_ABPackName  ABPackName, string resName) where T : UnityEngine.Object
        {
            string key = $"AB_{ABPackName}_{resName}";
            if (dic_LoadedRes.ContainsKey(key))
            {
                if (dic_LoadedRes[key] != null)
                    return dic_LoadedRes[key].GetAsset<T>();
                else Debug.LogError("获取资源失败，可能是正在进行异步加载中！，请通过异步获取");
            }
#if UNITY_EDITOR
            //编辑器调试加载
            if (settingData.abLoadSetting.IsDebugABLoad)
            {
                return EditorLoadABRes<T>(ABPackName, resName);
            }
#endif
            //加载操作
            T res = abLoader.ReallyLoadSync<T>(ABPackName.ToString(), resName);
            Res ResInfo = new Res(typeof(T));
            ResInfo.Asset = res;
            dic_LoadedRes.Add(key, ResInfo);
            return res;

        }
        /// <summary>
        /// 通过名字进行同步加载AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAB_Sync(E_ABPackName ABPackName, string resName)
        {
            return LoadAB_Sync<UnityEngine.Object>(ABPackName, resName);
        }
        #endregion

        #region Resources加载模块
        public void LoadRes_Async<T>(string path, UnityAction<T> callback) where T : UnityEngine.Object
        {
            AsyncLoadTask asyncLoadTask = CreateRes_Async<T>(path, callback);
            asyncLoadTask?.StartAsyncLoad();
        }
        //创建Res异步任务
        public AsyncLoadTask CreateRes_Async<T>(string path, UnityAction<T> callback) where T : UnityEngine.Object
        {
            //先任务是否第一次加载完成了吗
            if (!IsFirstAsyncLoad(path, callback))
            {
                return null;
            }
            //第一次就新建任务
            AsyncLoadTask task = new AsyncLoadTask();
            task.IntiLoadOperation(resourcesLoader.reallyLoadAsync<T>(path, task));
            task.AddCallbackCommand(new LoadResAsyncCommand<T>(callback, task));
            //加载资源占位
            dic_LoadedRes.Add(path, null);
            //记录到字典中
            dic_LoadedTask.Add(path, task);
            //异步加载完成记录
            task.AddCallbackCommand(() =>
            {
                dic_LoadedRes[path] = task.ResInfo;
                dic_LoadedTask.Remove(path);
            });
            return task;
        }
        /// <summary>
        /// 同步快速加载配置文件等，需要非常迅速，所以不用Task了，直接完成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadRes_Sync<T>(string path) where T : UnityEngine.Object
        {
            if (dic_LoadedRes.ContainsKey(path))
            {
                if (dic_LoadedRes[path] != null)
                    return dic_LoadedRes[path].GetAsset<T>();
                else Debug.LogError($"获取资源{path}失败，可能是正在进行异步加载中！请通过异步获取");
            }
            return resourcesLoader.LoadSync<T>(path);
        }
        #endregion

        #region 预加载模块相关
        /// <summary>
        /// 开始进行预加载  (需要先创建预加载任务！！)
        /// </summary>
        public void StartPreload()
        {
            preloader.StartLoad();
        }

        //预加载任务皆为 异步加载任务，可以把普通加载任务添加到预加载之中，到时候启动预加载就会一起加载了
        /// <summary>
        /// 创建预加载任务
        /// </summary>
        public void CreatePreloadTask(params AsyncLoadTask[] tasks)
        {

            PreLoadTask preLoadTask = new PreLoadTask();
            preLoadTask.TaskList.AddRange(tasks);
            preloader.CreatePreLoadTask(preLoadTask);
        }
        //[Obsolete("需要修改！！需要大改！！")]
        //public void CreatePreloadTaskFromExcel<T>(string ResPathName = "ResPath", UnityAction<Res[]> callback = null) where T : ExcelConfigurationContainer
        //{
        //    //预加载总任务
        //    //string[] paths = GameExcelDataLoader.Instance.GetDataPropertyInfo<T>(ResPathName);
        //    //CreatePreloadTaskFromPaths(paths);
        //}
        ////每个Excel表可以创建一个预加载总任务
        public void CreatePreloadTaskFromPaths((E_ABPackName, string)[] paths, UnityAction<Res[]> callback = null)
        {
            preloader.CreatePreloadTaskFromPaths(paths, callback);
        }
        #endregion
        /// <summary>
        /// 判断是不是第一次执行异步加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private bool IsFirstAsyncLoad<T>(string key, UnityAction<T> callback = null) where T : UnityEngine.Object
        {
            //先判断是否是第一次加载某任务
            if (!dic_LoadedRes.ContainsKey(key)) return true;
            //已经完事，直接用
            if (!dic_LoadedTask.ContainsKey(key) && dic_LoadedRes[key] != null)
            {
                callback?.Invoke(dic_LoadedRes[key].Asset as T);
                return false;
            }
            //没有完事，还在加载中情况，需要让它加载完后使用
            else
            {
                //创建任务用于判断
                AsyncLoadTask task = dic_LoadedTask[key];
                task.AddCallbackCommand(new LoadResAsyncCommand<T>(callback, task));
                return false;
            }
        }
        //使用编辑器加载时候记录资源
#if UNITY_EDITOR
        private T EditorLoadABRes<T>(E_ABPackName ABPackName, string resName, UnityAction<T> callback = null) where T : UnityEngine.Object
        {
            AsyncLoadTask task = new AsyncLoadTask();
            T res = editorLoader.LoadEditorRes<T>(ABPackName + "/" + resName);

            //直接秒速完成任务
            Res resInfo = new Res(typeof(T));
            resInfo.Asset = res;
            callback?.Invoke(res);
            task.FinishTask(resInfo);
            dic_LoadedRes.Add($"AB_{ABPackName}_{resName}", resInfo);
            dic_LoadedTask.Add($"AB_{ABPackName}_{resName}", task);
            return res;
        }
#endif
    }


}

