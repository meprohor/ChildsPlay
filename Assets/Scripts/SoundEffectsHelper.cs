using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {
	public AudioClip jumpingSound;
	public AudioClip fallFromJumpSound;
	public AudioClip movingSound;

	private AudioSource source;
	private AudioSource source2;
	private AudioClip currentSound;
	private GameObject player;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		source2 = gameObject.AddComponent<AudioSource>();
		source2.clip = movingSound;
		source2.loop = true;
		source2.playOnAwake = true;
		source2.volume = 1;
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<Rigidbody2D>().velocity.x > 0 || player.GetComponent<Rigidbody2D>().velocity.x < 0  &&   player.GetComponent<PlayerScript>().isGrounded){
			if(!source2.isPlaying){
				source2.Play();
			}
		}else{
			source2.Pause();
		}
	}

	void JumpingSound(){
		if(!source.isPlaying || currentSound != jumpingSound){
			currentSound = jumpingSound;
			source.PlayOneShot(currentSound, 0.2f);
		}
	}

	void FallFromJumpSound(){
		if(!source.isPlaying || currentSound != fallFromJumpSound){
			currentSound = fallFromJumpSound;
			source.PlayOneShot(currentSound, 0.2f);
		}
	}
}
