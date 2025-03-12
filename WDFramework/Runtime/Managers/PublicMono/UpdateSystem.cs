using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 公共mono管理器,直属生命周期管理器管辖
/// 开启关闭协程直接UpdateSystem.Instance.Start/Stop Coroutine
/// </summary>
public class UpdateSystem : MonoSingleton<UpdateSystem>, IKernelSystem
{
    private Dictionary<E_UpdateLayer, UpdateLayer> updateLayers;
    public void InitializedKernelSystem()
    {
        updateLayers = new Dictionary<E_UpdateLayer, UpdateLayer>();
        //添加四大更新
        updateLayers[E_UpdateLayer.GameSystem] = new UpdateLayer();
        updateLayers[E_UpdateLayer.FrameworkSystem] = new UpdateLayer();
        updateLayers[E_UpdateLayer.UI] = new UpdateLayer();
        updateLayers[E_UpdateLayer.Voice] = new UpdateLayer();
    }
    public UpdateSystem()
    {
        //注册当阶段变更时候，自动启动新阶段的所有update
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, E_PhaseState>(E_FrameworkEvent.ChangePhase, OnPhaseChangeStartUpdate, 5);
    }

    /// <summary>
    /// 添加Update帧更新监听函数
    /// </summary>
    /// <param name="layer">更新层</param>
    /// <param name="updateFun">更新函数</param>
    public void AddUpdateListener(E_UpdateLayer layer, UnityAction updateFun)
    {
        if (updateLayers.ContainsKey(layer))
        {
            updateLayers[layer].UpdateEvent += updateFun;
        }
    }

    /// <summary>
    /// 移除Update帧更新监听函数
    /// </summary>
    /// <param name="layer">更新层</param>
    /// <param name="updateFun">更新函数</param>
    public void RemoveUpdateListener(E_UpdateLayer layer, UnityAction updateFun)
    {
        if (updateLayers.ContainsKey(layer))
        {
            updateLayers[layer].UpdateEvent -= updateFun;
        }
    }

    /// <summary>
    /// 添加FixedUpdate帧更新监听函数
    /// </summary>
    /// <param name="layer">更新层</param>
    /// <param name="updateFun">更新函数</param>
    public void AddFixedUpdateListener(E_UpdateLayer layer, UnityAction updateFun)
    {
        if (updateLayers.ContainsKey(layer))
        {
            updateLayers[layer].FixedUpdateEvent += updateFun;
        }
    }

    /// <summary>
    /// 移除FixedUpdate帧更新监听函数
    /// </summary>
    /// <param name="layer">更新层</param>
    /// <param name="updateFun">更新函数</param>
    public void RemoveFixedUpdateListener(E_UpdateLayer layer, UnityAction updateFun)
    {
        if (updateLayers.ContainsKey(layer))
        {
            updateLayers[layer].FixedUpdateEvent -= updateFun;
        }
    }

    /// <summary>
    /// 添加LateUpdate帧更新监听函数
    /// </summary>
    /// <param name="layer">更新层</param>
    /// <param name="updateFun">更新函数</param>
    public void AddLateUpdateListener(E_UpdateLayer layer, UnityAction updateFun)
    {
        if (updateLayers.ContainsKey(layer))
        {
            updateLayers[layer].LateUpdateEvent += updateFun;
        }
    }

    /// <summary>
    /// 移除LateUpdate帧更新监听函数
    /// </summary>
    /// <param name="layer">更新层</param>
    /// <param name="updateFun">更新函数</param>
    public void RemoveLateUpdateListener(E_UpdateLayer layer, UnityAction updateFun)
    {
        if (updateLayers.ContainsKey(layer))
        {
            updateLayers[layer].LateUpdateEvent -= updateFun;
        }
    }

    /// <summary>
    /// 注册当阶段变更时候，自动启动新阶段的所有update
    /// </summary>
    /// <param name="phase"></param>
    private void OnPhaseChangeStartUpdate(E_PhaseState phase)
    {
        // Implement logic to start updates for the new phase
    }

    /// <summary>
    /// 启用所有更新
    /// </summary>
    public void StartAllUpdate()
    {
        foreach (var layer in updateLayers.Values)
        {
            layer.isFreezed = false;
        }
    }
    /// <summary>
    /// 冻结所有更新
    /// </summary>
    public void FreezeAllUpdate()
    {
        foreach (var layer in updateLayers.Values)
        {
            layer.isFreezed = true;
        }
    }
    private void Update()
    {
        foreach (var layer in updateLayers.Values)
        {
            layer.InvokeUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (var layer in updateLayers.Values)
        {
            layer.InvokeFixedUpdate();
        }
    }

    private void LateUpdate()
    {
        foreach (var layer in updateLayers.Values)
        {
            layer.InvokeLateUpdate();
        }
    }

}

