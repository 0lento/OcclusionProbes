#if UNITY_EDITOR
using UnityEngine;

public partial class OcclusionProbes : MonoBehaviour 
{
	private struct Vector3i
    {
        public int x;
        public int y;
        public int z;

        public Vector3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3i operator +(Vector3i v1, Vector3i v2) 
		{
			return new Vector3i(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}
    }

    static Vector3i[] s_Offsets = new Vector3i[]{new Vector3i( 1, 0, 0),
                                                 new Vector3i(-1, 0, 0),
                                                 new Vector3i( 0, 1, 0),
                                                 new Vector3i( 0,-1, 0),
                                                 new Vector3i( 0, 0, 1),
                                                 new Vector3i( 0, 0,-1)};

	static int IndexAt(Vector3i pos, Vector3i count)
    {
    	return pos.x + pos.y * count.x + pos.z * count.x * count.y;
    }

    static Vector4 Sample(Vector4[] data, Vector3i pos, Vector3i count)
    {
        if (pos.x < 0 || pos.y < 0 || pos.z < 0 || pos.x >= count.x || pos.y >= count.y || pos.z >= count.z)
            return new Vector4(0, 0, 0, 1);
        return data[IndexAt(pos, count)];
    }

    static bool OverwriteInvalidProbe(Vector4[] dataSrc, Vector4[] dataDst, Vector3i pos, Vector3i count, float backfaceTolerance)
    {
        Vector4 center = Sample(dataSrc, pos, count);

        int centerIndex = IndexAt(pos, count);
        dataDst[centerIndex] = center;

        if (center.w <= backfaceTolerance)
            return true;

        int weights = 0;
        Vector4 result = Vector4.zero;

        foreach(Vector3i offset in s_Offsets)
        {
        	Vector3i samplePos = pos + offset;
        	Vector4 sample = Sample(dataSrc, samplePos, count);
	        if (sample.w > backfaceTolerance)
	        	// invalid sample, don't use
	        	continue;
	        
	        result += sample;
	        weights++;
        }

        if (weights > 0)
        {
        	dataDst[centerIndex] = result/weights;
            return true;
        }

        // Haven't managed to overwrite an invalid probe
        return false;
    }

    static void DilateOverInvalidProbes(ref Vector4[] data, Vector3i count, float backfaceTolerance, int dilateIterations)
    {
    	if (dilateIterations == 0)
    		return;

        Vector4[] dataBis = new Vector4[data.Length];

        for (int i = 0; i < dilateIterations; ++i)
        {
            bool invalidProbesRemaining = false;

            for (int z = 0; z < count.z; ++z)
                for (int y = 0; y < count.y; ++y)
                    for (int x = 0; x < count.x; ++x)
                        invalidProbesRemaining |= !OverwriteInvalidProbe(data, dataBis, new Vector3i(x, y, z), count, backfaceTolerance);

            // Swap buffers
            Vector4[] dataTemp = data;
            data = dataBis;
            dataBis = dataTemp;

            if (!invalidProbesRemaining)
                break;
        }

    }

    static void ClampEdgesToWhite(ref Vector4[] data, Vector3i count)
    {
        Vector3i pos = new Vector3i();
        Vector4 white = new Vector4(1, 1, 1, 0);
        int maxz = count.z - 1;
        for(int x = 0; x < count.x; x++)
            for(int y = 0; y < count.y; y++)
            {
                pos.x = x;
                pos.y = y;
                pos.z = 0;
                data[IndexAt(pos, count)] = white;
                pos.z = maxz;
                data[IndexAt(pos, count)] = white;
            }

        int maxx = count.x - 1;
        for(int z = 0; z < count.z; z++)
            for(int y = 0; y < count.y; y++)
            {
                pos.z = z;
                pos.y = y;
                pos.x = 0;
                data[IndexAt(pos, count)] = white;
                pos.x = maxx;
                data[IndexAt(pos, count)] = white;
            }
    }
}
#endif