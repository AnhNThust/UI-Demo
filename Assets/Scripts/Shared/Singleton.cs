using UnityEngine;

namespace Assets.Scripts.Shared
{
	public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		static T instance;

		public static T Instance
		{
			get
			{
				if (null == instance)
				{
					instance = FindAnyObjectByType(typeof(T)) as T;
					if (null == instance)
					{
						GameObject obj = new GameObject(typeof(T).Name);
						instance = obj.AddComponent<T>();
					}
				}

				return instance;
			}
		}
	}

	public abstract class Singleton<T> where T : class, new()
	{
		static T instance;

		public static T Instance 
		{
			get
			{
				instance ??= new T();

				return instance;
			}
		}
	}
}
