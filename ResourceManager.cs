using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using L = LogManager;
public class ResourceManager : Singleton<ResourceManager>
{
	public override void Init()
	{
		base.Init();
	}
	/// <summary>
	/// 리소스 로드
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="path"></param>
	/// <returns></returns>
	public T Load<T>(string path) where T : Object
	{
		T loaded = Resources.Load<T>(path);
		if (loaded == null)
		{
			L.Print($"{this.name} : Load failed asset '{path}'.", logType: L.LogType.Error, saveOption: L.SaveOption.Save);
			return null;
		}
		return loaded;
	}
	/// <summary>
	/// 어드레서블 리소스 로드
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="path"></param>
	/// <returns></returns>
	public async UniTask<T> LoadAddressable<T>(string path, bool isPrefab = true) where T : Object
	{
		string fileName;
		if (isPrefab == true)
		{
			fileName = $"{path}.prefab";
		}
		else
		{
			fileName = path;
		}
		T loaded = await Addressables.LoadAssetAsync<T>(fileName);
		if (loaded == null)
		{
			L.Print($"{this.name} : Load failed addressable asset '{path}.prefab'.", logType: L.LogType.Error, saveOption: L.SaveOption.Save);
			return null;
		}
		return loaded;
	}
	/// <summary>
	/// 메모리 로드 없이 생성
	/// </summary>
	public async UniTask InstantiateImmediately(string path, Transform argTransform, bool argWorldPos = false, bool tracking = true)
	{
		try
		{
			await Addressables.InstantiateAsync($"{path}.prefab", argTransform, argWorldPos, tracking);
		}
		catch(System.Exception ex)
		{
			L.Print($"{this.name} : Instantiate failed addressable asset '{path}.prefab'. {ex}", logType: L.LogType.Error, saveOption: L.SaveOption.Save);
		}
	}
    /// <summary>
    /// 메모리 로드 없이 생성
    /// </summary>
    public async UniTask InstantiateImmediately(string path, Vector3 argPosition, Quaternion argRotation, Transform parent = null, bool tracking = true)
	{
		try
		{
			await Addressables.InstantiateAsync($"{path}.prefab", argPosition, argRotation, parent, tracking);
		}
		catch(System.Exception ex)
		{
			L.Print($"{this.name} : Instantiate failed addressable asset '{path}.prefab'. {ex}", logType: L.LogType.Error, saveOption: L.SaveOption.Save);
		}
	}
}
