using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject menuPanel;
	public GameObject helpPanel;
	public GameObject gamePanel;
	public GameObject gameOverPanel;
    public GameObject creditsPanel;
	public Slider slider;
	public Text scoreValue;
	// public Text levelValue;
	public Text bestScoreValue;
	public Text ResultScoreValue;
    public GameObject newBestScore;
	
	public Text questionsText;
	public List<Text> answers;
	
	// private int currentLevel;
	private int score;
	
	private List<float> tempAnswers = new List<float>();
	private int trueAnswer;
	private int bestScore;
	private bool isPlaying = false;
	private float timer = 1;

	
	void Start(){
		helpPanel.SetActive(false);										
		gamePanel.SetActive(false);					
		gameOverPanel.SetActive(false);
		menuPanel.SetActive(true);
		creditsPanel.SetActive(false);
		bestScore = PlayerPrefs.GetInt("bestScore", 0); 
        bestScoreValue.text = bestScore.ToString();
		
	}
	
	void Update(){										
		if(isPlaying) {
			slider.value -= Time.deltaTime/30;			
			if(slider.value <= 0) {						
				GameOver();
			}
			timer += Time.deltaTime;					
		}

	}
	
	public void PlayButton(){	
		helpPanel.SetActive(true);						
		gamePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		menuPanel.SetActive(false);
		creditsPanel.SetActive(false);

		SoundManager.sound.playBgmGame();
	}

	public void OkButton(){	
		helpPanel.SetActive(false);						
		gamePanel.SetActive(true);
		gameOverPanel.SetActive(false);
		menuPanel.SetActive(false);
		creditsPanel.SetActive(false);

		// SoundManager.sound.playBgmGame();

		isPlaying = true;
		slider.value = 1;
		score = 0;
		scoreValue.text = score.ToString();
		// currentLevel = 1;
		// levelValue.text = currentLevel.ToString();
		Generate();
	}

	public void HelpButton(){	
		helpPanel.SetActive(true);						
		gamePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		menuPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}

	public void CreditsButton() {
		creditsPanel.SetActive(true);
		gamePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		menuPanel.SetActive(false);
	}

	public void ExitButton() {
		Application.Quit();
	}
	
	public void CheckAnswer(int asnwer){				
		if(asnwer == trueAnswer) {
			gotRight();
		} else {
			gotWrong();
		}
	}
	
	public void gotRight(){								
		slider.value += 0.3f;
		score += 10;
		scoreValue.text = score.ToString();
														
		// if(score >= 100 * currentLevel){
		// 	levelUp();
		// }
		
		SoundManager.sound.playCorrect();	
		Generate();										
	}
	
	// public void levelUp(){								
	// 	// currentLevel++;	
	// 	// levelValue.text = currentLevel.ToString();
	// 	SoundManager.sound.playlevelUp();
	// }
	
	public void gotWrong(){								
		slider.value -= 0.07f;                        
        SoundManager.sound.playWrong();				
	}
	
	public void GameOver(){								
		gamePanel.SetActive(false);					
		newBestScore.SetActive(false);
		gameOverPanel.SetActive(true);
		isPlaying = false;                              
        ResultScoreValue.text = score.ToString();

        if (score >= bestScore){							
			bestScore = score;
			PlayerPrefs.SetInt("bestScore", bestScore);
            SoundManager.sound.playbestScore();
			newBestScore.SetActive(true);
            bestScoreValue.text = bestScore.ToString();	
		}
        SoundManager.sound.playGameOver();	
		SoundManager.sound.playBgmDefault();
	}

	public void gotoMenu(){								
		isPlaying = false;								
		gamePanel.SetActive(false);					
		gameOverPanel.SetActive(false);
		creditsPanel.SetActive(false);
		menuPanel.SetActive(true);
		SoundManager.sound.playBgmDefault();
	}

	public void Generate() {								
		tempAnswers.Clear();							
		int operation;

		// if(currentLevel == 1) {						
		// 	operation = Random.Range(1,2);
		// } else if(currentLevel <= 5) {										
		// 	operation = Random.Range(1,3);			
		// } else {
		// 	operation = Random.Range(1,5);
		// }

		operation = Random.Range(1,5);

		// int currentNumberLimit = 10 + (currentLevel - 1) * 5;		
		// float left = Random.Range((currentLevel - 1) * 5, currentNumberLimit);

		int currentNumberLimit = 10;		
		float left = Random.Range(5, currentNumberLimit);

		float right = 0;								
		string operationTxt = "";						
		float result = 0;								
		
		switch (operation){	
			//Operation +						
			case 1:										
				right = Random.Range(1,currentNumberLimit);	
				operationTxt = "+";						
				result = left + right;					

				tempAnswers.Add(result);				
				
				if((left-right) >= 0) {					
					tempAnswers.Add(left - right);
				} else {
					if(right-left != result) {
						tempAnswers.Add(right - left);
					} else {
						tempAnswers.Add(result * 2);
					}
				}
				tempAnswers.Add(result + 1);
				tempAnswers.Add(result - 1);
			break;
			//Operation -
			case 2:										
				right = Random.Range(1,(int)Mathf.Round (left + 1));
				operationTxt = "-";						
				result = left - right;						
				
				tempAnswers.Add(result);				
				tempAnswers.Add(result + 1);
				tempAnswers.Add(result - 1);
				tempAnswers.Add(left + right);
			break;
			//Operation x
			case 3:										
				// right = Random.Range(1, 3 + (currentLevel - 1) * 2);
				right = Random.Range(1, 3 + 2);
				operationTxt = "×";						
				result = left * right;					
				
				tempAnswers.Add(result);				
				tempAnswers.Add(result + right);
				tempAnswers.Add(result - 1);
				tempAnswers.Add(result + 1);
			break;
			//OPeration ÷
			case 4:										
				// left = Random.Range(1, 3 + (currentLevel - 1) * 2);
				// right = Random.Range(1, 3 + (currentLevel - 1) * 2);
				left = Random.Range(1, 3 + 2);
				right = Random.Range(1, 3 + 2);
				float tempSum = left * right;
				left = tempSum;
				operationTxt = "÷";
				result = left / right;					
				
				tempAnswers.Add(result);

				if((left - right) != result) {
					tempAnswers.Add(left - right);
				} else {
					tempAnswers.Add(left + right);
				}

				tempAnswers.Add(result + 1);

				if(left * right != result) {
					tempAnswers.Add(left * right);
				} else {
					tempAnswers.Add(result * 2);
				}
			break;
		}
		
		questionsText.text = left.ToString() + " " + operationTxt + " " + right.ToString() + " = ?";
		
		trueAnswer = Random.Range(0, 4);						
		answers[trueAnswer].text = result.ToString();		
		
		tempAnswers.RemoveAt(0);						
		int i = 0;										
		foreach(Text answer in answers){				
			if(i != trueAnswer){							
				int rand = Random.Range(0, tempAnswers.Count);
				answer.text = tempAnswers[rand].ToString();
				tempAnswers.RemoveAt(rand);
			}
			i++;
		}

	}
	
}
