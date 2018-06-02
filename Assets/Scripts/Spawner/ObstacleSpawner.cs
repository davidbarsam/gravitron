using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	public static ObstacleSpawner SharedInstance;

[HeaderAttribute("Spawners")]
	public List<Transform> leftSpawners;
	public List<Transform> rightSpawners;
[HeaderAttribute("Indicators")]
	public List<GameObject> leftIndicators;
	public List<GameObject> rightIndicators;

[HeaderAttribute("Variables")]
	public float timeToWait = 5f;

	public GameObject parentContainer;

	float timer;

	private void Awake()
	{
		SharedInstance = this;
	}

	void Start ()
	{
		timer = 0;
		ObjectPooler.SharedInstance.GetPooledObject();
	}
	
	void Update ()
	{

	}

	private void FixedUpdate()
	{
		if(!FindObjectOfType<Player>().ReturnGameStatus())
		{
			if(leftSpawners != null && rightSpawners != null)
			{
				if(Time.timeScale != 0)
				{
					timer += Time.deltaTime;
					if(Mathf.Round((timer / timeToWait)) == 1)
					{
						SpawnPattern();
						
						timer = 0;
					}
				}
			}
		}
	}

	/// <summary>
	/// This function runs every few seconds, and spawns a new wave of obstacles.
	/// The var "num" is a random number of 0 or 1, where the left spawners are 0
	/// and the right spawners are 1. A switch case checks the num and calls
	/// ReturnPattern(), returning a List of numbers corresponding to spots
	/// in the spawner lists
	/// </summary>
	/// <returns></returns>
	void SpawnPattern()
	{
		float side = Random.Range(0, 2); // Left = 0, Right = 1
		List<int> lanesToSpawn = ReturnPattern();
		if(side == 0)	// Left side
		{
			for(int i = 0; i < lanesToSpawn.Count; i++)
			{
				leftIndicators[lanesToSpawn[i]].SetActive(true);
				GameObject obj = ObjectPooler.SharedInstance.GetPooledObject();
				obj.transform.SetParent(parentContainer.transform);
				obj.transform.position = leftSpawners[lanesToSpawn[i]].transform.position;
				obj.SetActive(true);
				obj.GetComponent<Obstacle>().movingRight = true;
			}
		}
		if(side == 1)	// Right side
		{
			for (int i = 0; i < lanesToSpawn.Count; i++)
			{
				rightIndicators[lanesToSpawn[i]].SetActive(true);
				GameObject obj = ObjectPooler.SharedInstance.GetPooledObject();
				obj.transform.SetParent(parentContainer.transform);
				obj.transform.position = rightSpawners[lanesToSpawn[i]].transform.position;
				obj.SetActive(true);
				obj.GetComponent<Obstacle>().movingLeft = true;
			}
		}
	}

	/// <summary>
	/// This function randomly generates a list of positions
	/// for the obstacles to spawn in.
	/// </summary>
	/// <returns>List of positions</returns>
	public List<int> ReturnPattern()
	{
		List<int> positions = new List<int>();

		for(int i = 0; i < (Random.Range(1,3) + 1); i++)
		{
			int p = Random.Range(0, leftSpawners.Count);
			if(!positions.Contains(p))
			{
				positions.Add(p);
			}
		}

		return positions;
	}

}
