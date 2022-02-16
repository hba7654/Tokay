using UnityEngine;
using System.Collections;

public class TokayController : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;
	private TokayState state;
	private AudioSource sound;
	private float speed;

	public AudioClip[] tokaySounds;
	public float walkSpeed = 6.0f;
	public float runSpeed = 3.0f;
	public float turnSpeed = 60.0f;
	private Vector3 moveDirection = Vector3.zero;

	private enum TokayState
    {
		Patrolling,
		Alerted,
		Hunting,
		Enraged,
		Dead
    };

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		sound = GetComponent<AudioSource>();

		state = TokayState.Patrolling;

	}

	// Update is called once per frame
	void Update()
	{ 
		switch(state)
        {
			case TokayState.Patrolling:
				{
					anim.SetInteger("battle", 0);
					speed = walkSpeed;

					sound.clip = tokaySounds[0];
					sound.loop = true;

					break;
				}
			case TokayState.Alerted:
				{
					anim.SetInteger("battle", 1);
					anim.SetInteger("moving", 0);
					speed = 0;

					sound.clip = tokaySounds[2];
					sound.loop = false;

					StartCoroutine(Wait(3f));

					state = TokayState.Hunting;

					break;
				}
			case TokayState.Hunting:
				{
					anim.SetInteger("battle", 1);
					anim.SetInteger("moving", 1);
					speed = runSpeed;

					sound.clip = tokaySounds[1];
					sound.loop = true;

					break;
				}
			case TokayState.Enraged:
				{
					anim.SetInteger("battle", 1);
					speed = runSpeed;

					sound.clip = tokaySounds[3];
					sound.loop = false;

					break;
				}
			case TokayState.Dead:
				{
					anim.SetInteger("moving", 13);
					speed = 0;

					sound.clip = tokaySounds[3];
					sound.loop = false;

					break;
				}
		}
	}

	IEnumerator Wait(float sec)
    {
		yield return new WaitForSeconds(sec);
    }
}



