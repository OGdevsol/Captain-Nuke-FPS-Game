using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
[Serializable]
public class AudioPool
{
	// Token: 0x06000952 RID: 2386 RVA: 0x00032704 File Offset: 0x00030904
	public void play()
	{
		if (Time.time - lastPlay > minInterval)
		{
			lastPlay = Time.time;
			sources[index].Play();
			index = (index + 1) % length;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0003275C File Offset: 0x0003095C
	public void setVolume(float newVolume)
	{
		for (int i = 0; i < length; i++)
		{
			sources[i].volume = newVolume;
		}
	}

	// Token: 0x040007B9 RID: 1977
	public string name;

	// Token: 0x040007BA RID: 1978
	public AudioType type;

	// Token: 0x040007BB RID: 1979
	[NonSerialized]
	public AudioSource[] sources;

	// Token: 0x040007BC RID: 1980
	public AudioClip[] clips;

	// Token: 0x040007BD RID: 1981
	[NonSerialized]
	public int index;

	// Token: 0x040007BE RID: 1982
	[NonSerialized]
	public int length;

	// Token: 0x040007BF RID: 1983
	public float minInterval;

	// Token: 0x040007C0 RID: 1984
	private float lastPlay;
}
