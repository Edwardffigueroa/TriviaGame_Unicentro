using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logic : MonoBehaviour {

	//Lista de preguntas
	public Question [] preguntas;
	private static List<Question> preguntasSinResponder;

	private Question currentQuestion;

	//timers
	private float timeBetweenQuestion= 5f;

	
	private float timerForAnswer = 15f;

	public Text timer; 

	private bool startTimer = false;

	public Slider timerFeed;

	private bool readyAnswer;

	// --------UI -----------



	[SerializeField]
	private Text preguntaUI;

	[SerializeField]
	private Text OpcionA;

	[SerializeField]
	private Text OpcionB;

	[SerializeField]
	private Text OpcionC;

	[SerializeField]
	private Text OpcionD;


	public GameObject panelCorrecta;
	public GameObject panelIncorrecta;
	public GameObject panelTiempoFuera;

//	public GameObject panelCronometro;

//buttons reference
	public Button optionAbtn;
	public Button optionBbtn;
	public Button optionCbtn;
	public Button optionDbtn;

	public AudioClip win;
	public AudioClip wrong;

	public AudioClip button;

	public AudioClip temporizadorSound;
	public AudioSource sourceAudio;

	public AudioSource backgroundAudio;


	void Start () {

		optionAbtn.interactable=false;
		optionBbtn.interactable=false;
		optionCbtn.interactable=false;
		optionDbtn.interactable=false;

		sourceAudio.PlayOneShot(button);
		//sourceAudio.clip = temporizadorSound;
		


		panelCorrecta.gameObject.SetActive(false);
		panelIncorrecta.gameObject.SetActive(false);
		panelTiempoFuera.gameObject.SetActive(false);
		//panelCronometro.gameObject.SetActive(true);

	
        
		timerFeed.value = timerForAnswer;
		
		readyAnswer =false;
		

		if(preguntasSinResponder ==null || preguntasSinResponder.Count==0){

			preguntasSinResponder= preguntas.ToList<Question>();

		}

		GetCurrentQuestion();
		startTimer =false;
	}

	public void GetCurrentQuestion(){

		int ramdomIndex= Random.Range(0,preguntasSinResponder.Count);

		currentQuestion = preguntasSinResponder[ramdomIndex];

		//---- ui-----

		preguntaUI.text = currentQuestion.pregunta;
		OpcionA.text = currentQuestion.respuestaA;
		OpcionB.text = currentQuestion.respuestaB;
		OpcionC.text = currentQuestion.respuestaC;
		OpcionD.text = currentQuestion.respuestaD;

		// ------------

		

	}

	public void boton (string res){
	//	Debug.Log("se presionó: "+res);

		readyAnswer = true;
		sourceAudio.PlayOneShot(button);
		if(res == currentQuestion.correcta){

			Debug.Log("ACERTASTE!!");

			//StartCoroutine(NextQuestion("correcta"));
			panelCorrecta.gameObject.SetActive(true);
			sourceAudio.PlayOneShot(win);
			
		}else{

			Debug.Log("NEL!!");

			sourceAudio.PlayOneShot(wrong);

			panelIncorrecta.gameObject.SetActive(true);

			//StartCoroutine(NextQuestion("incorrecta"));

		}
	}

	public void reiniciar(){
sourceAudio.PlayOneShot(button);
		preguntasSinResponder.Remove(currentQuestion);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		

	}

	public void feedCorrecta(string tipoFeed){

		

		if(tipoFeed=="correcta"){

			panelCorrecta.gameObject.SetActive(true);
			
		
		}else{
			panelIncorrecta.gameObject.SetActive(true);
			
			
		}

	}

	IEnumerator NextQuestion(string tipoFeed){

		//preguntasSinResponder.Remove(currentQuestion);
		feedCorrecta(tipoFeed);
		yield return new WaitForSeconds(timeBetweenQuestion);
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

	
	
	// Update is called once per frame
	void Update () {


		if(startTimer){

		timerForAnswer -= Time.deltaTime;
		//timerFeed.gameObject.SetActive(true);

		}else{
		//	timerFeed.gameObject.SetActive(false);
		}

		if(readyAnswer){
		 startTimer =false;
		 backgroundAudio.Stop();
		
		}
		
		if(timerForAnswer < 1){
			startTimer =false;
			backgroundAudio.Stop();
	
			panelTiempoFuera.gameObject.SetActive(true);
		
		}

		timer.text= timerForAnswer.ToString("f0");
		
		timerFeed.value = timerForAnswer;
		
		
	}

	public void home(){

		sourceAudio.PlayOneShot(button);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1); 

	}

	public void empezarCronometro(){

			startTimer =true;
		optionAbtn.interactable=true;
		optionBbtn.interactable=true;
		optionCbtn.interactable=true;
		optionDbtn.interactable=true;
		
		//	panelCronometro.gameObject.SetActive(false);
			backgroundAudio.PlayOneShot(temporizadorSound);

	}
}
