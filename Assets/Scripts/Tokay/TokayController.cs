using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class TokayController : MonoBehaviour
{
	private Animator anim;
	private TokayState state;
	private AudioSource sound;
	private Transform[] patrolPoints;
	private NavMeshAgent nav;
	private GameManager gameManager;

	public AudioClip[] tokaySounds;
	public float walkSpeed = 3.0f;
	public float runSpeed = 6.0f;
	public float turnSpeed = 60.0f;
	private Vector3 moveDirection = Vector3.zero;
    private bool haveNext;

    private enum TokayState
    {
		Start,
		Patrolling,
		Paused,
		Alerted,
		Hunting,
		Enraged,
		Dead
    };

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
		nav = GetComponent<NavMeshAgent>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		patrolPoints = gameManager.patrolPoints;

		state = TokayState.Start;
		Debug.Log(nav.stoppingDistance);

	}

	// Update is called once per frame
	void Update()
	{
		StartCoroutine(TokayControl());
	}

	private IEnumerator TokayControl()
	{
		switch (state)
		{
			case TokayState.Start:
				{
					if (anim.GetInteger("moving") != 15)
					{
						anim.SetInteger("moving", 13);
					}

					yield return new WaitForSeconds(5f);
					break;
				}
			case TokayState.Patrolling:
				{
					anim.SetInteger("battle", 0);
					anim.SetInteger("moving", 1);

					sound.clip = tokaySounds[0];
					sound.loop = true;

					Patrol();

					yield return null;
					break;
				}
			case TokayState.Paused:
				{
					anim.SetInteger("battle", 0);
					anim.SetInteger("moving", 0);

					sound.clip = tokaySounds[0];
					sound.loop = true;
					sound.Play();

					Patrol();

					yield return new WaitForSeconds(3f);
					state = TokayState.Patrolling;

					break;
				}
			case TokayState.Alerted:
				{
					anim.SetInteger("battle", 1);
					anim.SetInteger("moving", 0);

					sound.clip = tokaySounds[2];
					sound.loop = false;
					sound.Play();

					yield return new WaitForSeconds(3f);

					state = TokayState.Hunting;

					break;
				}
			case TokayState.Hunting:
				{
					anim.SetInteger("battle", 1);
					anim.SetInteger("moving", 1);

					sound.clip = tokaySounds[1];
					sound.loop = true;
					sound.Play();

					yield return null;
					break;
				}
			case TokayState.Enraged:
				{
					anim.SetInteger("battle", 1);

					sound.clip = tokaySounds[3];
					sound.loop = false;
					sound.Play();

					yield return null;
					break;
				}
			case TokayState.Dead:
				{
					anim.SetInteger("moving", 13);

					sound.clip = tokaySounds[3];
					sound.loop = false;
					sound.Play();

					yield return new WaitForSeconds(5f);

					break;
				}
		}
	}

    private void OnMouseOver()
    {
        if(state == TokayState.Start)
        {
			if(gameManager.objCounter == 4 || gameManager.objCounter == 6)
			{
				gameManager.objCounter++;

			}
        }
    }

	public IEnumerator Rise()
	{
		Debug.Log("RISE");
		anim.SetInteger("battle", 0);
		anim.SetInteger("moving", 15);

		yield return new WaitForSeconds(5f);


		state = TokayState.Paused;
	}

	private void Patrol()
    {
		if (state == TokayState.Patrolling)
		{
			if(Vector3.Distance(transform.position, nav.destination) <= 1f)
            {
				Debug.Log("ARRIVED WOOO.");
				state = TokayState.Paused;

				haveNext = false;
            }
			nav.speed = walkSpeed;
			return; 
		}

		if (!haveNext && state == TokayState.Paused)
		{
			haveNext = true;
			int newPointIndex = (int)Mathf.Floor(Random.Range(0, patrolPoints.Length));
			Debug.Log(newPointIndex);
			Transform newPoint = patrolPoints[newPointIndex];

			nav.SetDestination(newPoint.position);
			nav.speed = 0;
		}
    }
}



