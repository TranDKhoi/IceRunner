using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    //game play
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    //congfigurable fields
    [SerializeField] private int firstChunkSpawnPosition = 5;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float despawnDistance = 5.0f;

    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        //check if chunkPrefab list is empty

        if (chunkPrefab.Count == 0)
        {
            return;
        }

        //try to assign the cameraTransform
        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        ScanPosition();
    }

    private void ScanPosition()
    {
        float cameraZ = cameraTransform.position.z;
        var lastChunk = activeChunks.Peek();

        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }

    }

    private void SpawnNewChunk()
    {
        //get random index for prefab to spawn
        int randomIndex = Random.Range(0, chunkPrefab.Count);

        //does it already exist in our pool
        Chunk chunk = chunkPool.Find(x => !x.gameObject.activeSelf && x.name == (chunkPrefab[randomIndex].name + "(Clone)"));

        //if not, create a chunk
        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefab[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        //place object and show it
        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.chunkLength;

        //put it in pool to reuse
        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        //reset the chunkSpawnZ
        chunkSpawnZ = firstChunkSpawnPosition;

        for (int i = activeChunks.Count; i != 0; i--)
            DeleteLastChunk();

        for (int i = 0; i < chunkOnScreen; i++)
            SpawnNewChunk();

    }

    private void Awake()
    {
        ResetWorld();
    }
}
