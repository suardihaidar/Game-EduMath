using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public AudioClip wrong;
	public AudioClip correct;
	public AudioClip levelUp;
	public AudioClip bestScore;
	public AudioClip gameover;
	public AudioClip buttons;

	public GameObject soundButtonMute;
	public GameObject soundButton;

	public static SoundManager sound;				
	
	public AudioSource audioSource_Default;
	public AudioSource audioSource_Game;

	void Start(){
		sound = this;									
	}

	public void playBgmDefault() {

		audioSource_Default.GetComponent<AudioSource> ();
		audioSource_Default.UnPause();
		audioSource_Game.Stop();

	}

	public void playBgmGame() {

		audioSource_Game.GetComponent<AudioSource> ();
		audioSource_Game.Play();
		audioSource_Default.Pause();

	}

	public void mute() {
		soundButton.SetActive(false);
		soundButtonMute.SetActive(true);
		AudioListener.volume = 0f;
	}

	public void soundOn() {
		soundButtonMute.SetActive(false);
		soundButton.SetActive(true);
		AudioListener.volume = 1f;
	}
		
	public void playWrong(){
		GetComponent<AudioSource>().PlayOneShot(wrong);
	}

	public void playCorrect(){
		GetComponent<AudioSource>().PlayOneShot(correct);
	}
	
	public void playlevelUp(){
		GetComponent<AudioSource>().PlayOneShot(levelUp);
	}
	
	public void playbestScore(){
		GetComponent<AudioSource>().PlayOneShot(bestScore);
	}
	
	public void playGameOver(){
		GetComponent<AudioSource>().PlayOneShot(gameover);
	}

	public void playButtons(){
		GetComponent<AudioSource>().PlayOneShot(buttons);
	}

}
