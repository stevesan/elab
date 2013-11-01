using UnityEngine;
using System.Collections.Generic;
using SteveSharp;

public class MainController : MonoBehaviour
{
    public GUIText outText;

    string state = "";
    float runTime = 0f;

    List<Level> levels = new List<Level>();

    int activeLevelNum = -1;
    Level activeLevelInst;

	// Use this for initialization
	void Start ()
    {
        state = "levelselect";

        HashSet<string> names = new HashSet<string>();

        foreach( Level lev in GetComponentsInChildren<Level>() )
        {
            levels.Add(lev);
            lev.gameObject.SetActive(false);

            if( names.Contains( lev.gameObject.name ) )
            {
                Debug.LogError("Duplicate level name: "+lev.gameObject.name);
            }
            names.Add( lev.gameObject.name );
        }
	}

    string GetBestTimeKey( Level lev )
    {
        return "best-time-"+lev.gameObject.name;
    }

    float GetBestTime( Level lev )
    {
        return PlayerPrefs.GetFloat( GetBestTimeKey(lev), 0f );
    }

    void SetBestTime( Level lev, float t )
    {
        PlayerPrefs.SetFloat( GetBestTimeKey(lev), t );
    }

    void ToPlanMode()
    {
        if( activeLevelInst != null )
            Destroy(activeLevelInst.gameObject);
        activeLevelInst = Utility.MyInstantiate( levels[activeLevelNum], Vector3.zero, transform );
        Time.timeScale = 0f;
        state = "plan";
    }

    void ToLevelSelect()
    {
        if( activeLevelInst != null )
            Destroy(activeLevelInst.gameObject);
        Time.timeScale = 0f;
        state = "levelselect";
    }
	
	// Update is called once per frame
	void Update()
    {
        if( state == "levelselect" )
        {
            outText.text = "Press # to choose level!\n";
            for( int i = 0; i < levels.Count; i++ )
            {
                outText.text += (i+1)+". "+levels[i].gameObject.name
                    +" (best: "+GetBestTime(levels[i]).ToString("0.00")+")"
                    + "\n";
            }

            // 
            for( int i = 0; i < levels.Count; i++ )
            {
                if( Input.GetKeyDown(""+(i+1)) )
                {
                    activeLevelNum = i;
                    ToPlanMode();
                }
            }
        }
        else if( state == "plan" )
        {
            outText.text = "Press SPACE to run!\nR to reset\nB for level select";
            if( Input.GetKeyDown(KeyCode.Space) )
            {
                state = "sim";
                runTime = 0f;
                Time.timeScale = 1f;
            }
            
            if( Input.GetKeyDown("r") )
                ToPlanMode();
            else if( Input.GetKeyDown("b") )
                ToLevelSelect();
        }
        else if( state == "sim" )
        {
            runTime += Time.deltaTime;
            outText.text = "Playing, "+runTime.ToString("0.00")+"s"
                + "\nR to reset";

            if( Input.GetKeyDown("r") )
                ToPlanMode();
            else if( Input.GetKeyDown("b") )
                ToLevelSelect();
        }
        else if( state == "win" )
        {
            if( Input.GetKeyDown("r") )
                ToPlanMode();
            else if( Input.GetKeyDown("b") )
                ToLevelSelect();
        }
	}

    public void OnGoalHit()
    {
        if( state == "sim" )
        {
            state = "win";
            outText.text = "Goal got! Time = "+runTime.ToString("0.00")+" seconds\nPress R to reset, B for level select";

            if( runTime > GetBestTime( levels[activeLevelNum] ) )
            {
                SetBestTime( levels[activeLevelNum], runTime );
                outText.text += "NEW HIGH SCORE!";
            }
        }
    }
}
