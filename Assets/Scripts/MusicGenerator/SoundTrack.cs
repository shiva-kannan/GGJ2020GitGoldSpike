﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{
    public GameObject grid;
    public GameObject player;

    public AudioClip baseTrackClip;
    public AudioSource baseTrack;
    bool baseTrackOn=false;
    bool trackTriggered = false;

    int lastFrameSongInBeats;
    //beats per minute of a song
    public float bpm;
    //the current position of the song (in seconds)
    float songPosition;

    //the current position of the song (in beats)
    public int songPositionInBeats;

    //the duration of a beat
    float secPerBeat;

    //how much time (in seconds) has passed since the song started
    float dspSongTime;

    private TileMap tileMapObject;
    private Vector2 currentGridPosition;

    // Start is called before the first frame update
    void Start()
    {
        tileMapObject = grid.GetComponent<TileMap>();

        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = 60f / bpm;
    
        //record the time when the song starts
        dspSongTime = (float) AudioSettings.dspTime;

        //start the song
        //GetComponent<AudioSource>().Play();
        beginSoundTrack();

    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = (int)(songPosition / secPerBeat);

        //Start playing the base track when the game starts
        if (baseTrackOn){
            if(songPositionInBeats % 4 == 0 && songPositionInBeats !=lastFrameSongInBeats){
                Debug.Log("Play");
                //baseTrack.PlayOneShot(baseTrackClip, 0.4f);
                StartCoroutine(playOneShot(baseTrackClip, baseTrack));
                
            }
        }

        lastFrameSongInBeats = songPositionInBeats;

        triggerPositionBasedMusic();
    }

    void beginSoundTrack(){ 
        baseTrackOn = true;
    }

    IEnumerator playOneShot(AudioClip a, AudioSource s){
        
        //if (!trackTriggered){
        s.PlayOneShot(a, 0.4f);
        //trackTriggered = true;
        //}
        yield return new WaitForSeconds(2f);
        // trackTriggered = false;
    }

    void triggerPositionBasedMusic(){
        Debug.Log(tileMapObject.GetTileUnderPoint(player.transform.position));
        if (tileMapObject.GetTileUnderPoint(player.transform.position) != null){
            currentGridPosition = tileMapObject.GetTileUnderPoint(player.transform.position)._gridPos;
            Debug.Log(currentGridPosition);
        }        
    }
}