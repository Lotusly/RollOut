using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour
{

	public static Plane instance;
	public int Size;
	public Plate[] plates;
	//private Color[] colors;

	//private int[] buffer;

	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
	
	
	void Start ()
	{
		//colors = new Color[7] {Color.blue, Color.cyan, Color.red, Color.magenta, Color.green, Color.yellow,Color.clear};
		plates = GetComponentsInChildren<Plate>();
		//buffer = new int[plates.Length];

		if (Size * Size != plates.Length)
		{
			Debug.LogError("Plate Size Wrong");
		}

		for (int i = 0; i < Size; i++)
		{
			for (int j = 0; j < Size; j++)
			{
				plates[i * Size + j].AssignIndex(i, j);
			}
		}
		Initialize();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Initialize()
	{
		/*for (int i = 0; i < Size; i++)
		{
			for (int j = 0; j < Size; j++)
			{
				buffer[i * Size + j] = Random.Range(0, 6);
			}
		}*/
		
		for (int i = 0; i < Size; i++)
		{
			for (int j = 0; j < Size; j++)
			{
				plates[i * Size + j].AssignColor(Random.Range(0, 6));
			}
		}
	}

	public void GradualInitialize()
	{
		int tmp = (Size + 1) / 2-1;
		Plate [,] matrix=new Plate[tmp,4*(Size-1)];
		for (int i = 0; i < tmp; i++)
		{
			for (int j = 0; j < (i+1)*2; j ++)
			{
				int x = (Size - 1) / 2 - (i + 1);
				int y = (Size - 1) / 2 - (i + 1) + j;
				print("matrix: " + i + " " + j + " plates: " + x + " " + y);

				matrix[i, j] = plates[x * Size + y];
			}
			for (int j = (i + 1); j < (i + 1); j++)
			{
				int x = (Size - 1) / 2 - (i + 1)+j;
				int y = (Size - 1) / 2 + (i + 1);
				print("matrix: " + i + " " + j + " plates: " + x + " " + y);

				matrix[i, j+(i+1)*2] = plates[x * Size + y];
			}
			for (int j = (i + 1); j < (i + 1); j++)
			{
				int x = (Size - 1) / 2 + (i + 1);
				int y = (Size - 1) / 2 + (i + 1) - j;
				print("matrix: " + i + " " + j + " plates: " + x + " " + y);

				matrix[i, j+(i+1)*4] = plates[x * Size + y];
			}
			for (int j = (i + 1); j < (i + 1); j++)
			{
				int x = (Size - 1) / 2 + (i + 1) - j;
				int y = (Size - 1) / 2 - (i + 1);
				print("matrix: " + i + " " + j + " plates: " + x + " " + y);

				matrix[i, j+(i+1)*8] = plates[x * Size + y];
			}
		}

		plates[(Size - 1) / 2 * Size + (Size - 1) / 2].AssignColor(Random.Range(0, 6));
		StartCoroutine(gradualInitialize(matrix,tmp));


	}

	private IEnumerator gradualInitialize(Plate[,] matrix,int size)
	{
		for (int i = 0; i < size; i++)
		{
			yield return new WaitForSecondsRealtime(0.2f);
			for (int j = 0; j < (i + 1) * 8; j++)
			{
				matrix[i, j].AssignColor(Random.Range(0, 6));
			}
		}

	}

	public void PlayerGoLeft(int n)
	{
		/*for (int times = 0; times < n; times++)
		{
			for (int i = 0; i < Size; i++)
			{
				for (int j = Size - 1; j > 0; j--)
				{
					buffer[i * Size + j] = buffer[i * Size + j-1];
				}
				buffer[i * Size] = Random.Range(0, 6);
			}
		}*/
		//flush();
		for (int times = 0; times < n; times++)
		{
			for (int i = 0; i < Size; i++)
			{
				Plate tmp = plates[i*Size+Size-1];
				for (int j =Size-1; j >0; j--)
				{
					plates[i * Size + j] = plates[i* Size + j-1];
				}
				plates[i*Size] = tmp;
				tmp.AssignColor(Random.Range(0,6));
			}
		}
	}

	public void PlayerGoRight(int n)
	{
		/*for (int times = 0; times < n; times++)
		{
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size-1; j++)
				{
					buffer[i * Size + j] = buffer[i * Size + j + 1];
				}
				buffer[i * Size + Size - 1] = Random.Range(0, 6);
			}
			
		}*/
		//flush();
		for (int times = 0; times < n; times++)
		{
			for (int i = 0; i < Size; i++)
			{
				Plate tmp = plates[i*Size];
				for (int j = 0; j <Size-1; j++)
				{
					plates[i * Size + j] = plates[i* Size + j+1];
				}
				plates[i*Size+Size-1] = tmp;
				tmp.AssignColor(Random.Range(0,6));
			}
		}
	}
	
	public void PlayerGoDown(int n)
	{
		//print("down "+n.ToString());
		//Color mode
		/*for (int times = 0; times < n; times++)
		{
			for (int j = 0; j < Size; j++)
			{
				for (int i = Size-1; i >0; i--)
				{
					buffer[i * Size + j] = buffer[(i-1) * Size + j];
				}
				buffer[ j] = Random.Range(0, 6);
			}
			
		}*/
		//flush();
		
		//Transform mode
		
		for (int times = 0; times < n; times++)
		{
			for (int j = 0; j < Size; j++)
			{
				Plate tmp = plates[(Size - 1) * Size + j];
				for (int i = Size - 1; i > 0; i--)
				{
					plates[i * Size + j] = plates[(i - 1) * Size + j];
				}
				plates[j] = tmp;
				tmp.AssignColor(Random.Range(0,6));
			}
		}
	}
	
	public void PlayerGoUp(int n)
	{
		/*for (int times = 0; times < n; times++)
		{
			for (int j = 0; j < Size-1; j++)
			{
				for (int i = 0; i < Size-1; i++)
				{
					buffer[i * Size + j] = buffer[(i+1) * Size + j];
				}
				buffer[(Size-1) * Size + j] = Random.Range(0, 6);
			}
			
		}*/
		//print("up "+n.ToString());
		for (int times = 0; times < n; times++)
		{
			for (int j = 0; j < Size; j++)
			{
				Plate tmp = plates[j];
				for (int i = 0; i <Size-1; i++)
				{
					plates[i * Size + j] = plates[(i + 1) * Size + j];
				}
				plates[(Size-1)*Size+j] = tmp;
				tmp.AssignColor(Random.Range(0,6));
			}
		}
		//flush();
	}

	
	


	public void Flush()
	{
		//Update color mode
		/*for (int i = 0; i < Size; i++)
		{
			for (int j = 0; j < Size; j++)
			{
				plates[i * Size + j].AssignColor(buffer[i * Size + j]);
			}
		}*/
		//Update Transform mode
		int dev = (Size - 1) / 2;
		for (int i = 0; i < Size; i++)
		{
			for (int j = 0; j < Size; j++)
			{
				plates[i*Size+j].transform.position=new Vector3(j-dev,0,i-dev);
			}
		}
	}
	
	public int GetCenter()
	{
		
		return plates[((Size - 1) / 2)*Size + ((Size - 1) / 2)].GetColor();
	}

	public void HitCenter()
	{
		plates[((Size - 1) / 2) * Size + ((Size - 1) / 2)].AssignColor(7);
	}
}
